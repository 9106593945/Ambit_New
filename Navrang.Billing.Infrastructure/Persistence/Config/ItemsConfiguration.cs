using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Navrang.Billing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Navrang.Billing.Infrastructure.Persistence.Config
{
	public class ItemsConfiguration : IEntityTypeConfiguration<Items>
     {
          public void Configure(EntityTypeBuilder<Items> builder)
          {
               builder.ToView("items");
          }
     }
}
