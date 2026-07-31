#if UNITY_UIGUI_PRESENT
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace EliottChen.SceneTransitions
{
    /// <summary>
    /// Persistent full-screen fader used to hide level/scene transitions behind a black overlay.
    /// Auto-instantiates itself before the first scene loads, so it is available everywhere
    /// without needing to be dropped in a scene manually.
    /// </summary>
    public class ScreenFader : MonoBehaviour
    {
        public static ScreenFader Instance { get; private set; }

        [Header("Overlay setup")]
        [SerializeField] private int sortingOrder = 9999;
        [SerializeField] private Color fadeColor = Color.black;

        [Header("Default fade speed (alpha units per second)")]
        [SerializeField] private float defaultSpeed = 1f;

        private CanvasGroup canvasGroup;
        private Coroutine activeFadeRoutine;

        /// <summary>
        /// Called to create a new <see cref="ScreenFader"/> component and adds it automatically
        /// to the <see cref="UnityEngine.Object.DontDestroyOnLoad(UnityEngine.Object)"/>'s scene pools.
        /// Calling <see cref="Bootstrap"/> is safe (will be skipped if the instance already exist)
        /// </summary>
        public static void Bootstrap()
        {
            if (Instance != null)
                return;

            var go = new GameObject(nameof(ScreenFader));
            Instance = go.AddComponent<ScreenFader>();
            DontDestroyOnLoad(go);
            Instance.BuildOverlay();
        }

        private void BuildOverlay()
        {
            var canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = sortingOrder;

            gameObject.AddComponent<CanvasScaler>();

            var imageGO = new GameObject("FadeImage");
            imageGO.transform.SetParent(transform, false);

            var image = imageGO.AddComponent<Image>();
            image.color = fadeColor;

            var rect = image.rectTransform;
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            canvasGroup = gameObject.AddComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }

        /// <summary>
        /// Jump the alpha value of the canvas to the input parameters.
        /// Calling this will cut all Coroutines that <see cref="FadeOut(float, Action)"/> or <see cref=" FadeToBlack(float, Action)"/>
        /// may have started.
        /// </summary>
        /// <param name="alpha"> alpha value of the black screen, 0: transparent, 1: full opaque</param>
        public static void SetAlpha(float alpha)
        {
            if (Instance == null)
                Bootstrap();
            Instance.SetAlphaAndCutCoroutine(alpha);
        }

        /// <summary>
        /// Fades the screen to black (alpha 0 -> 1).
        /// </summary>
        /// <param name="inTime"> Time required for the screen going completely transparent, set to 0 For instant fading.</param>
        /// <param name="onComplete">Optional callback fired once fully black.</param>
        static public Coroutine FadeOut(float inTime = 0f, Action onComplete = null)
        {
            if (Instance == null)
                Bootstrap();
            return Instance.StartFade(0f, inTime, onComplete);
        }

        /// <summary>
        /// Fades the screen back from black (alpha 1 -> 0).
        /// </summary>
        /// <param name="inTime"> Time required for the screen going completely black, set to 0 For instant fading.</param>
        /// <param name="onComplete">Optional callback fired once fully transparent.</param>
        static public Coroutine FadeToBlack(float inTime, Action onComplete = null)
        {
            if (Instance == null)
                Bootstrap();
            return Instance.StartFade(1f, inTime, onComplete);
        }

        private void SetAlphaAndCutCoroutine(float alpha)
        {
            if (activeFadeRoutine != null)
                StopCoroutine(activeFadeRoutine);

            canvasGroup.alpha = alpha;
        }

        private Coroutine StartFade(float targetAlpha, float inTime, Action onComplete)
        {
            if (activeFadeRoutine != null)
                StopCoroutine(activeFadeRoutine);

            float appliedSpeed = inTime;
            activeFadeRoutine = StartCoroutine(FadeRoutine(targetAlpha, appliedSpeed, onComplete));

            return activeFadeRoutine;
        }

        private IEnumerator FadeRoutine(float targetAlpha, float inTime, Action onComplete)
        {
            // Blocks input while the overlay is at all visible, so nothing behind it is clickable mid-transition.
            canvasGroup.blocksRaycasts = true;

            // Set the opacity depending on speed
            if (inTime == 0f)
            {
                canvasGroup.alpha = targetAlpha;
            }
            else
            {
                float speed = 1f / inTime; // V = D/T

                while (!UnityEngine.Mathf.Approximately(canvasGroup.alpha, targetAlpha))
                {
                    canvasGroup.alpha = UnityEngine.Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, speed * Time.unscaledDeltaTime);
                    yield return null;
                }
            }

            canvasGroup.alpha = targetAlpha;
            canvasGroup.blocksRaycasts = targetAlpha > 0f;
            activeFadeRoutine = null;

            onComplete?.Invoke();

            Debug.Log("Coroutine ended", this);
            yield break;
        }

        private void OnDestroy()
        {
            if (Instance != null && Instance == this)
            {
                Instance = null;
            }
        }
    }
}
#endif