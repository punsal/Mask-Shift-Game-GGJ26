using UI.Views.Extensions;
using UI.Views.Interface;
using UnityEngine;

namespace UI.Views.Abstract
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class AView : MonoBehaviour, IView
    {
        private CanvasGroup _canvasGroup;

        protected virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
    
        public void Show()
        {
            _canvasGroup.SafeSetActive(true);
        }
    
        public void Hide()
        {
            _canvasGroup.SafeSetActive(false);
        }
    }
}