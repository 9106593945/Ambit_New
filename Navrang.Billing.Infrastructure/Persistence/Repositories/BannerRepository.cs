using Dapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Navrang.Billing.AppCore.Common;
using Navrang.Billing.AppCore.EntityModels;
using Navrang.Billing.AppCore.Repositories;
using Navrang.Billing.Domain.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Navrang.Billing.Infrastructure.Persistence.Repositories
{
	public class BannerRepository : BaseRepository, IBannerRepository
	{
		private readonly IDapper _dapper;
		public BannerRepository(AppDbContext dbContext, IDapper dapper) : base(dbContext)
		{
			_dapper = dapper;
		}
		public AppDbContext AppDbContext
		{
			get { return _dbContext as AppDbContext; }
		}

		public IEnumerable<BannerEntityModel> GetAllBanners()
		{
			var Banners = _dapper.GetAll<BannerEntityModel>($"exec [GetAllBanners]", null, commandType: CommandType.Text);

			return Banners;
		}
	}
}
