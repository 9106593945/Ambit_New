using Microsoft.Extensions.Options;
using Ambit.AppCore.Common;
using Ambit.AppCore.EntityModels;
using Ambit.AppCore.Models;
using Ambit.API.Helpers;
using System.Collections.Generic;

namespace Navrang.Services
{
    public class BannerService : IBannerService
    {
		private readonly AppSettings _appSettings;
		private readonly IRepoSupervisor _repoSupervisor;
		public BannerService(IOptions<AppSettings> appSettings, IRepoSupervisor repoSupervisor)
		{
			_appSettings = appSettings.Value;
			_repoSupervisor = repoSupervisor;
		}

		public IEnumerable<BannerEntityModel> GetAllBanners()
		{
			return _repoSupervisor.Banner.GetAllBanners();
		}
    }
}
