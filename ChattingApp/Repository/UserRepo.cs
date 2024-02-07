using System.Linq.Expressions;
using ChattingApp.Contracts;
using ChattingApp.Models;
using ChattingApp.Persistence;

namespace ChattingApp.Repository
{
    public sealed class UserRepo : RepoBase<User>, IUserRepo
    {


        public UserRepo(AppDbContext context) : base(context)
        {

        }

        public async Task<IReadOnlyList<User>> GetAllUserAsync()
        {
            var users = await GetAll();
            return users;
        }

        public async Task<IReadOnlyList<User>> GetAllUserByRoomId(int roomId)
        {
            var filter = new List<Expression<Func<User, bool>>>
            {
                u => u.Room.Id.Equals(roomId)
            };
            var users = await FindAllWithExpression(filter);
            return users;
        }

        public async Task<User> GetUserAsync(int userId)
        {
            var user = await GetById(userId);
            return user;
        }

        public void CreateUserAsync(User user)
        {
            Create(user);

        }

        public void DeleteUser(int userId)
        {
            Delete(userId);

        }


    }
}
