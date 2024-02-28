using ChattingApp.Models;

namespace ChattingApp.Contracts
{
    public interface IConversationRepo
    {
        Task<Conversation> GetConvAsync(int userId, int roomId);
        Task<Conversation> GetConvByIdDbAsync(int conv);
        void CreateConvAsync(Conversation conv);
        void DeleteConv(int convId);
        
    }
}
