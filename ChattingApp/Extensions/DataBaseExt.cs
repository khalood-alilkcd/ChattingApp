using ChattingApp.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

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

        public static void ConfigureCorsPolicy(this IServiceCollection services)=> 
            services.AddCors(options => 
                    options.AddPolicy("CorsPolicy", builder => 
                        builder.WithOrigins("http://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials()
                    )
        );
        
    }
}
