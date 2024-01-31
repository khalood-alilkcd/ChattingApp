namespace ChattingApp.Models
{
    public class Conversation : IBaseEntity<int>
    {
        public int Id { get; set; }

        public string? from { get; set; }

        public string? to { get; set; }

        public Room? Room { get; set; }
        public IList<Message> Messages { get; set; } = new List<Message>();

    }
}
