
#if LEGACY_TMP || UNITY6_TMP

namespace EliottChen
{
    using UnityEngine;
    using TMPro;


    public class ShowUnityVersion : MonoBehaviour
    {
        [SerializeField] TMP_Text textComponent;

        private void Start()
        {
            textComponent.text = $"version: {Application.version}";
        }

    }

}

#endif
