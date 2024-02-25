using ChattingApp.Contracts;
using ChattingApp.Repository;

namespace ChattingApp.Extensions
{
    public static class ClassesRelationAddExt
    {
        public static void ConfigureRepoBase(this IServiceCollection services) 
            => services.AddScoped(typeof(IRepoBase<>), typeof(RepoBase<>));

        public static void ConfigureUserRepo(this IServiceCollection services)
            => services.AddScoped<IUserRepo, UserRepo>();

        public static void ConfigureRoomRepo(this IServiceCollection services)
            => services.AddScoped<IRoomRepo, RoomRepo>();

        public static void ConfigureMessageRepo(this IServiceCollection services)
            => services.AddScoped<IMessageRepo, MessageRepo>();

        public static void ConfigureConversationRepo(this IServiceCollection services)
            => services.AddScoped<IConversationRepo, ConversationRepo>();
    }
}
