using UI.Events;
using UI.Views.Abstract;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class ViewMenu : AView
    {
        [SerializeField] private Button startButton;

        protected override void Awake()
        {
            base.Awake();
            if (startButton == null)
            {
                Debug.LogError("ViewMenu: startButton is null");
            }
        }

        private void OnEnable()
        {
            if (startButton) startButton.onClick.AddListener(OnStartButtonClicked);
        }

        private void OnDisable()
        {
            if (startButton) startButton.onClick.RemoveListener(OnStartButtonClicked);
        }

        private static void OnStartButtonClicked()
        {
            UIEvents.RaiseStart();
        }
    }
}