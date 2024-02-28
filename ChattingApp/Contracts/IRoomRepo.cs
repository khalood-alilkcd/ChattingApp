using ChattingApp.Models;

namespace ChattingApp.Contracts
{
    public interface IRoomRepo
    {
        Task<IReadOnlyList<Room>> GetAllRoomAsync();
        Task<Room> GetRoomAsync(int roomId);
        Task<Room> GetRoomByName(string name);
        Task<IEnumerable<Room>> GetRoomByUserAsync(User user);
        void CreateRoom(Room room);
        void DeleteRoom(int roomId);
        
    }
}
