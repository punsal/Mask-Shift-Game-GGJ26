using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MaskVisibility : MonoBehaviour
    {
        [Tooltip("Object is visible only in these masks. Leave empty to always show.")]
        public MaskType[] visibleIn;
        
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            if (MaskState.Instance != null)
            {
                MaskState.Instance.OnMaskChanged += OnMaskChanged;
            }
            
            OnMaskChanged(MaskState.Instance != null 
                ? MaskState.Instance.Current 
                : MaskType.Glasses);
        }

        private void OnDisable()
        {
            if (MaskState.Instance != null)
            {
                MaskState.Instance.OnMaskChanged -= OnMaskChanged;
            }
        }


        private void OnMaskChanged(MaskType maskType)
        {
            if (visibleIn == null || visibleIn.Length == 0)
            {
                _spriteRenderer.enabled = false;
                return;
            }

            var show = false;
            // ReSharper disable once ForCanBeConvertedToForeach
            // ReSharper disable once LoopCanBeConvertedToQuery
            for (var i = 0; i < visibleIn.Length; i++)
            {
                if (visibleIn[i] != maskType)
                {
                    continue;
                }
                show = true;
                break;
            }
            
            _spriteRenderer.enabled = show;
        }
    }
}