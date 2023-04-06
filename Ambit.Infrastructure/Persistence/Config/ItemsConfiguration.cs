using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ambit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ambit.Infrastructure.Persistence.Config
{
	public class ItemsConfiguration : IEntityTypeConfiguration<Items>
     {
          public void Configure(EntityTypeBuilder<Items> builder)
          {
               builder.ToView("items");
          }
     }
}
