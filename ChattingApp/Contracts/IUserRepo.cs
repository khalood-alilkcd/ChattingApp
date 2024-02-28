using ChattingApp.Models;


namespace ChattingApp.Contracts
{
    public interface IUserRepo
    {
        Task<IReadOnlyList<User>> GetAllUserAsync();
        Task<IReadOnlyList<User>> GetAllUserByRoomId(int roomId);
        Task<User> GetUserAsync(int userId);
        Task<User> GetUserByNameAsync(string name);
        void CreateUserAsync(User user);
        void DeleteUser(int userId);
        
    }
}
