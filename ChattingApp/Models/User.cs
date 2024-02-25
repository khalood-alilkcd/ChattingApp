using System.ComponentModel.DataAnnotations.Schema;

namespace ChattingApp.Models
{
    public class User : IBaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        
        public Room? Room { get; set; }
        [NotMapped]
        public IList<Conversation> Conversations { get; set; } = new List<Conversation>();
    }
}
