using Ambit.AppCore.Common;
using Ambit.AppCore.Repositories;

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
