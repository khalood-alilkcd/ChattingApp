using ChattingApp.Contracts;
using ChattingApp.Models;
using ChattingApp.Persistence;

namespace ChattingApp.Repository
{
    public sealed class RoomRepo : RepoBase<Room>, IRoomRepo
    {
        public RoomRepo(AppDbContext context) : base(context)
        {
            
        }
        

        public async Task<IReadOnlyList<Room>> GetAllRoomAsync()
        {
            var rooms = await GetAll();
            return rooms;
        }

        public async Task<Room> GetRoomAsync(int roomId)
        {
            var room = await GetById(roomId);
            return room;
        }

        public void CreateRoom(Room room)
        {
            Create(room);
        }

        public void DeleteRoom(int roomId)
        {
            Delete(roomId);
        }
    }
}
