using ChattingApp.Models;

namespace ChattingApp.Contracts
{
    public interface IRoomRepo
    {
        Task<IReadOnlyList<Room>> GetAllRoomAsync();
        Task<Room> GetRoomAsync(int roomId);
        void CreateRoom(Room room);
        void DeleteRoom(int roomId);
    }
}
