using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Navrang.Billing.AppCore.Common;
using Navrang.Billing.Infrastructure.Persistence;

namespace Navrang.Billing.Infrastructure
{
	public static class RepoDI
     {
          public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
          {
               services.AddDbContext<AppDbContext>(o => o.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Navrang")));

               services.AddScoped<IRepoSupervisor, RepoSupervisor>();

               return services;
          }
     }
}
