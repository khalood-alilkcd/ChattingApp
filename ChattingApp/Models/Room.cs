using System.ComponentModel.DataAnnotations.Schema;

namespace ChattingApp.Models
{
    public class Room : IBaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public Conversation? Conversation { get; set; }
        [NotMapped]
        public IList<User> users = new List<User>();
    }
}
