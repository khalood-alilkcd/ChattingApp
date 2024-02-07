using ChattingApp.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ChattingApp.Extensions
{
    public static class DataBaseExt
    {
        public static void ConfigureAppDbContext(this IServiceCollection services, IConfiguration configuration) {
            var connectionString = configuration.GetValue<string>("DefaultConnection");
            services.AddDbContext<AppDbContext>(
                options => {
                    options.UseSqlServer(connectionString);  
                });
                
        }


        
    }
}
