using UI.Messages.Abstract;

namespace UI.Messages
{
    public class GameInfoMessage : AMessage
    {
        public GameInfoMessage() : base("masks change what you can see and what you can do") { }
    }
}