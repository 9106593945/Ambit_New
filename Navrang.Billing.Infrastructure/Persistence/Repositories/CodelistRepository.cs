using Dapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Navrang.Billing.AppCore.Common;
using Navrang.Billing.AppCore.EntityModels;
using Navrang.Billing.AppCore.Repositories;
using Navrang.Billing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Navrang.Billing.Infrastructure.Persistence.Repositories
{
    public class CodelistRepository : BaseRepository, ICodelistRepository
    {
        private readonly IDapper _dapper;
        public CodelistRepository(AppDbContext dbContext, IDapper dapper) : base(dbContext)
        {
            _dapper = dapper;
        }
        public AppDbContext AppDbContext
        {
            get { return _dbContext as AppDbContext; }
        }

    }
}
