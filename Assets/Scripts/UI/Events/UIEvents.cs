using System;
using UnityEngine;

namespace UI.Events
{
    public static class UIEvents
    {
        private static event Action onStart;
        public static event Action OnStart
        {
            add => onStart += value;
            remove => onStart -= value;
        }
        
        public static void RaiseStart() => onStart?.Invoke();

        private static event Action onTutorialInfoStart;
        public static event Action OnTutorialInfoStart
        {
            add => onTutorialInfoStart += value;
            remove => onTutorialInfoStart -= value;
        }
        
        public static void RaiseTutorialInfoStart() => onTutorialInfoStart?.Invoke();
        
        private static event Action onTutorialInfoEnd;
        public static event Action OnTutorialInfoEnd
        {
            add => onTutorialInfoEnd += value;
            remove => onTutorialInfoEnd -= value;
        }
        
        public static void RaiseTutorialInfoEnd() => onTutorialInfoEnd?.Invoke();
        
        public static void Clear()
        {
            onStart = null;
            onTutorialInfoStart = null;
            onTutorialInfoEnd = null;
            Debug.Log("UIEvents cleared");
        }
    }
}