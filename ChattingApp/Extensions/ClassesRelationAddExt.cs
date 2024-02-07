using ChattingApp.Contracts;
using ChattingApp.Repository;

namespace ChattingApp.Extensions
{
    public static class ClassesRelationAddExt
    {
        public static void ConfigureRepoBase(this IServiceCollection services) => services.AddScoped(typeof(IRepoBase<>), typeof(RepoBase<>));
    }
}
