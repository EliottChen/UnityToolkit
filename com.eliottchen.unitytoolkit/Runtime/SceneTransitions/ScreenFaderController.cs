using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EliottChen.SceneTransitions
{
    public class ScreenFaderController : MonoBehaviour
    {
        [SerializeField] float fadeSpeed = 1.0f;
        [SerializeField] ActionType aaction = ActionType.FadeToTransparent;


        enum ActionType
        {
            FadeToBlack,
            FadeToTransparent
        }
        // Start is called before the first frame update
        void Start()
        {
            switch (aaction)
            {
                case ActionType.FadeToBlack:
                    ScreenFader.FadeToBlack(fadeSpeed); break;
                case ActionType.FadeToTransparent:
                    ScreenFader.FadeOut(fadeSpeed); break;
                default:
                    break;
            }
        }
    }
}