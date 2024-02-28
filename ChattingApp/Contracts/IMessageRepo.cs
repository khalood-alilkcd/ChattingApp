using ChattingApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Reflection;

namespace ChattingApp.Contracts
{
    public interface IMessageRepo
    {
        Task<IReadOnlyList<Message>> GetAllMessageAsync(int convId);
        Task<Message> GetMessagedDbById(int messageId);
        void CreateMessage(Message msg);
        void DeleteMessage(int msgId);
        
    }
}
