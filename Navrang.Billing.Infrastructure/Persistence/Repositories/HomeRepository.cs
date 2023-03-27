﻿using Navrang.Billing.AppCore.Repositories;
using System.Linq;

namespace Navrang.Billing.Infrastructure.Persistence.Repositories
{
	public class HomeRepository : BaseRepository, IHomeRepository
	{
		public HomeRepository(AppDbContext dbContext) : base(dbContext)
		{
		}
		public AppDbContext AppDbContext
		{
			get { return _dbContext as AppDbContext; }
		}


		public long GetCustomerCount()
		{
			var customerCount = _dbContext.Customer.Where(a => a.isDeleted == false).Count();

			return customerCount;
		}

		public long GetItemCount()
		{
			var itemCount = _dbContext.Items.Where(a => a.isDeleted == false).Count();

			return itemCount;
		}

		
	}
}