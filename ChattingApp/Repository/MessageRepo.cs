using ChattingApp.Contracts;
using ChattingApp.Models;
using ChattingApp.Persistence;
using System.Linq;
namespace ChattingApp.Repository
{
    public sealed class MessageRepo : RepoBase<Message>, IMessageRepo
    {
        public MessageRepo(AppDbContext context) : base(context)
        {

        }
        

        public async Task<IReadOnlyList<Message>> GetAllMessageAsync(int convId, bool trackChanges)
        {
            var messageList = await FindAllWithExpression(c => c.Conversation.Id.Equals(convId), trackChanges);
            return messageList;
        }
       
        public async Task<Message> GetMessageAsync(int convId , int msgId, bool trackChanges)
        {
            var msg = await FindByIdWithExpressions(u => u.Conversation.Id.Equals(convId) && u.Id.Equals(msgId), trackChanges);
            return msg;
        }

        public void CreateMessageAsync(Message msg)
        {
            Create(msg);
        }

        public void DeleteMessage(int msgId)
        {
            Delete(msgId);
        }

    }
}
