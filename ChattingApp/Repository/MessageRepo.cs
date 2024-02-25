using ChattingApp.Contracts;
using ChattingApp.Models;
using ChattingApp.Persistence;
using System.Linq;
using System.Linq.Expressions;
namespace ChattingApp.Repository
{
    public sealed class MessageRepo : RepoBase<Message>, IMessageRepo
    {
        public MessageRepo(AppDbContext context) : base(context)
        {

        }


        public async Task<IReadOnlyList<Message>> GetAllMessageAsync(int convId)
        { 
            var messageList = await FindAllWithExpression(c => 
                    c.Conversation.Id.Equals(convId));
            return messageList;
        }

        public async Task<Message> GetMessageAsync(int messageId)
        {
            var filter = new List<Expression<Func<Message, bool>>>
            {
                u => u.Id.Equals(messageId)
            };
            var msg = await FindByIdWithExpressions(filter);
            return msg;
        }

        public void CreateMessage(Message msg)
        {
            Create(msg);
        }

        public void DeleteMessage(int msgId)
        {
            Delete(msgId);
        }

        public async Task<Message> GetMessagedDbById(int Message)
        {
            var message = await GetById(Message);
            return message;
        }
    }
}
