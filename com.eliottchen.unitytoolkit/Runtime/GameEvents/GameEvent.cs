using System;
using System.Collections.Generic;
using UnityEngine;

// -----------------------------------------------------------------
// Author : Eliott TAN
// Date : 15/07/2026
//
// Strongly inspired by GDC Talk : Unite 2017 - Game Architecture with Scriptable Objects
//
// -----------------------------------------------------------------

namespace EliottChen.GameEvents
{
    /// <summary>
    /// Game Event Class represent an event that is stored inside a unity scriptable object
    /// </summary>
    [CreateAssetMenu(menuName = "Utility/Game Event", fileName = "New Game Event")]
    public class GameEvent : ScriptableObject
    {
        private readonly List<Action> subscribers = new List<Action>();

        public void Invoke()
        {
            int lCount = subscribers.Count;
            for (int i = lCount - 1; i >= 0; i--)
            {
                subscribers[i]?.Invoke();
            }
        }

        public void AddListener(Action pListener)
        {
            if (!subscribers.Contains(pListener))
                subscribers.Add(pListener);
        }

        public void RemoveListener(Action pListener)
        {
            if (subscribers.Contains(pListener))
                subscribers.Remove(pListener);
        }

        public void RemoveAllListeners()
        {
            subscribers.Clear();
        }

    }
}