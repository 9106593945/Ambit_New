using Ambit.AppCore.Common;
using Ambit.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ambit.Infrastructure
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
