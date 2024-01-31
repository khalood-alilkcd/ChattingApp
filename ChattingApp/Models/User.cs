namespace ChattingApp.Models
{
    public class User : IBaseEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Room Room { get; set; }
        public IList<Conversation> Conversations { get; set; } = new List<Conversation>();

    }
}
