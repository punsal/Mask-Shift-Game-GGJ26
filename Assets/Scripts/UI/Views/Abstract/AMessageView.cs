using TMPro;
using UI.Messages;
using UI.Messages.Interface;
using UI.Views.Extensions;
using UnityEngine;

namespace UI.Views.Abstract
{
    public abstract class AMessageView<TMessage> : AView where TMessage : IMessage, new()
    {
        [Header("References")]
        [SerializeField] private TextMeshProUGUI contentText;
        
        private TMessage _message;

        protected override void Awake()
        {
            base.Awake();
            
            _message = new TMessage();
            contentText.SafeSetText(_message.Content);
        }
    }
}