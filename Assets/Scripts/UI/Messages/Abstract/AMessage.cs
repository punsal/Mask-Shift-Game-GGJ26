using UI.Messages.Interface;

namespace UI.Messages.Abstract
{
    public abstract class AMessage : IMessage
    {
        public string Content { get; }

        protected AMessage(string content)
        {
            Content = content;
        }
    }
}