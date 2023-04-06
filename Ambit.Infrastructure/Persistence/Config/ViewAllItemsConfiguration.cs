using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ambit.Domain.Entities;

namespace Ambit.Infrastructure.Persistence.Config
{
	public class ViewAllItemsConfiguration : IEntityTypeConfiguration<ViewAllItems>
     {
          public void Configure(EntityTypeBuilder<ViewAllItems> builder)
          {
               builder.HasNoKey();
               builder.ToView("ViewAllItems");
          }
     }
}
