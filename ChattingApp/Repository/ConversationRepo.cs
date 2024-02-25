using System.Linq.Expressions;
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
            List<Expression<Func<Conversation, bool>>> felters = new List<Expression<Func<Conversation, bool>>>
            {
                c => c.User.Id.Equals(userId),
                c => c.RoomId.Equals(roomId)
            };
            var conv = FindByIdWithExpressions(felters);
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

        public async Task<Conversation> GetConvByIdDbAsync(int conv)
        {
            var conversation = await GetById(conv);
            return conversation;
        }
    }

}
