using ChattingApp.Models;

namespace ChattingApp.Contracts
{
    public interface IMessageRepo
    {
        Task<IReadOnlyList<Message>> GetAllMessageAsync(int convId);
        Task<Message> GetMessageAsync(int convId, int msgId);
        void CreateMessage(Message msg);
        void DeleteMessage(int msgId);
    }
}
