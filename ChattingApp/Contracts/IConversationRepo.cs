using ChattingApp.Models;

namespace ChattingApp.Contracts
{
    public interface IConversationRepo
    {
        Task<Conversation> GetConvAsync(int userId, int roomId);
        void CreateConvAsync(Conversation conv);
        void DeleteConv(int convId);
    }
}
