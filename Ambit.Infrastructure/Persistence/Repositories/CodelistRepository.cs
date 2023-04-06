using Dapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Ambit.AppCore.Common;
using Ambit.AppCore.EntityModels;
using Ambit.AppCore.Repositories;
using Ambit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Ambit.Infrastructure.Persistence.Repositories
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
