using IdentityTutorialApp.Repository.Contexts.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace IdentityTutorialApp.Repository.Extensions.Microsoft
{
    public static class DependencyResolvers
    {
        public static void AddDependenciesForRepositoryLayer(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("SqlConnection"), options =>
                {
                    options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name); 
                });
            });
        }
    }
}
