namespace ChattingApp.Models
{
    public class Room : IBaseEntity<int>
    {
      
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty; 
        public string Title { get; set; } = string.Empty;

        public int ConversationId { get; set; }
        public Conversation? Conversation { get; set; }

        public IList<User> users = new List<User>();

    }
}
