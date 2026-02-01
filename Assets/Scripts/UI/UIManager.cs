using System.Collections;
using UI.Events;
using UI.Views.Abstract;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("Views")]
        [SerializeField] private AView viewMenu;
        [SerializeField] private AView viewTutorialInfo;
        [SerializeField] private AView viewGameplay;
        

        private void Awake()
        {
            if (viewMenu == null)
            {
                Debug.LogError("UIManager: viewMenu is null");
            }
            
            if (viewTutorialInfo == null)
            {
                Debug.LogError("UIManager: viewTutorialInfo is null");
            }
            
            if (viewGameplay == null)
            {
                Debug.LogError("UIManager: viewGameplay is null");
            }
        }

        private void Start()
        {
            viewMenu.Show();
            viewTutorialInfo.Hide();
            viewGameplay.Hide();
        }

        private void OnEnable()
        {
            UIEvents.OnStart += OnStartHandler;
            UIEvents.OnTutorialInfoEnd += OnTutorialInfoEndHandler;
        }

        private void OnDisable()
        {
            UIEvents.OnStart -= OnStartHandler;
            UIEvents.OnTutorialInfoEnd -= OnTutorialInfoEndHandler;
        }

        private void OnDestroy()
        {
            UIEvents.Clear();
        }
        
        private void OnStartHandler()
        {
            viewMenu.Hide();
            viewTutorialInfo.Show();
            UIEvents.RaiseTutorialInfoStart();
            StartCoroutine(ShowTutorialInfo());
        }
        
        private IEnumerator ShowTutorialInfo()
        {
            yield return new WaitForSeconds(2);
            viewTutorialInfo.Hide();
            UIEvents.RaiseTutorialInfoEnd();
        }
        
        private void OnTutorialInfoEndHandler()
        {
            viewGameplay.Show();
        }
    }
}
