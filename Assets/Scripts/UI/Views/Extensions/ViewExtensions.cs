using TMPro;
using UnityEngine;

namespace UI.Views.Extensions
{
    public static class ViewExtensions
    {
        public static void SafeSetActive(this CanvasGroup canvasGroup, bool active)
        {
            if (!canvasGroup)
            {
                Debug.LogWarning("CanvasGroup is null");
                return;
            }
            
            canvasGroup.alpha = active ? 1 : 0;
            canvasGroup.interactable = active;
            canvasGroup.blocksRaycasts = active;
        }
        
        public static void SafeSetText(this TextMeshProUGUI textMeshProUGUI, string text)
        {
            if (!textMeshProUGUI)
            {
                Debug.LogWarning("TextMeshProUGUI is null");
                return;
            }
            
            textMeshProUGUI.text = text;
        }
    }
}