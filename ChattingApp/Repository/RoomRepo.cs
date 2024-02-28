using ChattingApp.Contracts;
using ChattingApp.Models;
using ChattingApp.Persistence;

namespace ChattingApp.Repository
{
    public sealed class RoomRepo : RepoBase<Room>, IRoomRepo
    {
        private readonly AppDbContext _context;

        public RoomRepo(AppDbContext context) : base(context)
        {
            _context=context;
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

        public async Task<Room> GetRoomByName(string name)
        {
            var room = await GetRoomByName(name);
            return room;
        }

        public async Task<IEnumerable<Room>> GetRoomByUserAsync(User user)
        {
            return _context.Rooms.Where(r => r.users.Contains(user)).ToList() ;
        }
    }
}
