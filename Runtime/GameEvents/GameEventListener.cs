using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EliottChen.GameEvents
{
    public class GameEventListener : MonoBehaviour
    {
        [Tooltip("Event source to listen, will callback the OnEventReceived UnityEvent when fired")]
        [SerializeField] GameEvent eventToListen;
        [Tooltip("Responses to invoke when events are raised")]
        public UnityEvent OnEventReceived;

        private void OnEventRaised()
        {
            OnEventReceived?.Invoke();
        }

        private void OnEnable()
        {
            eventToListen.AddListener(OnEventRaised);
        }

        private void OnDisable()
        {
            eventToListen.RemoveListener(OnEventRaised);
        }


    }
}