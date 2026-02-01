using UnityEngine;

namespace Gameplay
{
    public class MaskState : MonoBehaviour
    {
        public static MaskState Instance { get; private set; }
        
        public MaskType Current { get; private set; } = MaskType.Glasses;
        
        public delegate void MaskChanged(MaskType maskType);
        public event MaskChanged OnMaskChanged;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject); 
                return;
            }
            
            Instance = this;
        }

        public void SetMask(MaskType mask, bool shouldForce = false)
        {
            if (Current == mask)
            {
                if (shouldForce)
                {
                    OnMaskChanged?.Invoke(Current);
                }
                return;
            }
            Current = mask;
            OnMaskChanged?.Invoke(Current);
        }
    }
}