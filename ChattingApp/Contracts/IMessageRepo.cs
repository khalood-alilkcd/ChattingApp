using ChattingApp.Models;

namespace ChattingApp.Contracts
{
    public interface IMessageRepo
    {
        Task<IReadOnlyList<Message>> GetAllMessageAsync(int convId, bool trackChanges); 
        Task<Message> GetMessageAsync(int convId, int msgId, bool trackChanges);
        void CreateMessageAsync(Message msg);
        void DeleteMessage(int msgId);
    }
}
