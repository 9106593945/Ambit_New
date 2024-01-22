using Ambit.AppCore.Common;
using Ambit.AppCore.Repositories;
using Ambit.Domain.Common;
using Ambit.Domain.Entities;
using Ambit.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ambit.Infrastructure.Persistence
{
	public class RepoSupervisor : IRepoSupervisor
	{
		private readonly AppDbContext _dbContext;
		private readonly IDapper _dapper;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public ILoginRepository Logins { get; private set; }
		public IItemRepository Items { get; private set; }
		public ICompanyRepository Company { get; private set; }
		public ICategoryRepository Category { get; private set; }
		public ICustomerRepository Customer { get; private set; }
		public ICartRepository Cart { get; private set; }
		public IOrderRepository Order { get; private set; }
		public IHomeRepository Home { get; private set; }
		public IBannerRepository Banner { get; private set; }
		public ICodelistRepository Codelist { get; private set; }

		public RepoSupervisor(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor, IDapper dapper)
		{
			_dbContext = dbContext;
			_httpContextAccessor = httpContextAccessor;
			_dapper = dapper;
			//Actions = new ActionRepository(_dbContext);
			Logins = new LoginRepository(_dbContext);
			Items = new ItemRepository(_dbContext, _dapper);
			Company = new ComapnyRepository(_dbContext);
			Customer = new CustomerRepository(_dbContext, _dapper);
			Cart = new CartRepository(_dbContext, _dapper);
			Order = new OrderRepository(_dbContext, _dapper);
			Home = new HomeRepository(_dbContext);
			Category = new CategoryRepository(_dbContext);
			Banner = new BannerRepository(_dbContext, _dapper);
			Codelist = new CodelistRepository(_dbContext, _dapper);
		}

		public int Complete(string username)
		{
			AddTimestamps(username);
			return _dbContext.SaveChanges();
		}

		private void AddTimestamps(string username)
		{
			var entities = _dbContext.ChangeTracker
						.Entries()
						.Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

				username = username ?? "System";

			var currentUserId = Convert.ToInt32(_httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(s=>s.Type == ClaimTypes.Sid).Value);

			if (false && currentUserId > 0)
			{
				var name = _dbContext.Users.Find(currentUserId)?.Name;
				username = name ?? username;
			}

			username = username.TrimToMaxLength(40);
			foreach (var entity in entities)
			{
				var currentDateTime = DateTime.Now;
				if (entity.State == EntityState.Added)
				{
					((BaseEntity)entity.Entity).Created_On = currentDateTime;
					((BaseEntity)entity.Entity).Created_By = currentUserId;
				}

				if (((BaseEntity)entity.Entity).isDeleted == null)
				{
					((BaseEntity)entity.Entity).isDeleted = false;
				}
				if (((BaseEntity)entity.Entity).Active == null)
				{
					((BaseEntity)entity.Entity).Active = true;
				}

				((BaseEntity)entity.Entity).Updated_On = currentDateTime;
				((BaseEntity)entity.Entity).Updated_By = currentUserId;
			}
		}

		public void Dispose()
		{
			_dbContext.Dispose();
		}
	}
}
