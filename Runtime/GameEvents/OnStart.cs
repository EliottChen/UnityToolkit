using UnityEngine;
using UnityEngine.Events;

namespace EliottChen.GameEvents
{
    /// <summary>
    /// component to callback editor code when MonoBehaviour's start method is called.
    /// Its main purpose is to be used in editor to avoid creating new component just to call
    /// public void methods.
    /// </summary>
    public class OnStart : MonoBehaviour
    {
        public UnityEvent ExecuteOnStart;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            ExecuteOnStart?.Invoke();
        }
    }
}
