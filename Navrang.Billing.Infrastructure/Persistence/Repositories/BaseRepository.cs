using System;
using System.Collections.Generic;
using System.Text;

namespace Navrang.Billing.Infrastructure.Persistence.Repositories
{
     public abstract class BaseRepository
     {
          protected readonly AppDbContext _dbContext;
          public BaseRepository(AppDbContext dbContext)
          {
               _dbContext = dbContext;
          }
     }
}
