using ChattingApp.Contracts;
using ChattingApp.Models;
using ChattingApp.Persistence;

namespace ChattingApp.Repository
{
    public sealed class ConversationRepo : RepoBase<Conversation>, IConversationRepo
    {
        public ConversationRepo(AppDbContext context) : base(context)
        {
           
        }

        public Task<Conversation> GetConvAsync(int userId, int roomId)
        {
            var conv = FindByIdWithExpressions(c => c.Id.Equals(roomId), c => c.User.Id.Equals(userId));
            return conv;
        }

        public void CreateConvAsync(Conversation conv)
        {
            Create(conv);
        }

        public void DeleteConv(int convId)
        {
            Delete(convId);
        }
    }
}
