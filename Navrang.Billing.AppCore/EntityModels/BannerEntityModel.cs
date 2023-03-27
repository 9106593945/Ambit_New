using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Navrang.Billing.AppCore.EntityModels
{
    public class BannerEntityModel
    {
        public long? BannerId { internal get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int? SortOrder { internal get; set; }

    }
}
