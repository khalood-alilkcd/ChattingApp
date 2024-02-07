using System.ComponentModel.DataAnnotations.Schema;

namespace ChattingApp.Models
{
    public class Conversation : IBaseEntity
    {
        public string? FromUser { get; set; }

        public string? ToUser { get; set; }
        public User User { get; set; }
        public int RoomId { get; set; }
        [NotMapped]
        public Room Room { get; set; }
        [NotMapped]
        public IList<Message> Messages { get; set; } = new List<Message>();

    }
}
