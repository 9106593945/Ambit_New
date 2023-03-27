using Navrang.Billing.AppCore.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Navrang.Billing.AppCore.Repositories
{
    public interface IBannerRepository
    {
        IEnumerable<BannerEntityModel> GetAllBanners();
    }
}
