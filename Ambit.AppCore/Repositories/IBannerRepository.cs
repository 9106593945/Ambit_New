using Ambit.AppCore.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ambit.AppCore.Repositories
{
    public interface IBannerRepository
    {
        IEnumerable<BannerEntityModel> GetAllBanners();
    }
}
