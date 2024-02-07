namespace ChattingApp.Models
{
    public enum MessageType { TEXT, AUDIO,VIDEO, IMAGE, DOCUMENT }

    public enum Direction { INBOUND,OUTBOUND }

    public class Message : IBaseEntity
    {
        public MessageType Type { get; set; }

        public Direction Direction { get; set; }    

        public string? Content { get; set; }

        public Conversation? Conversation { get; set; }
    }
}
