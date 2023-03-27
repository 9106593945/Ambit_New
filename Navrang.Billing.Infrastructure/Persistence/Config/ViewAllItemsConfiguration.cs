using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Navrang.Billing.Domain.Entities;

namespace Navrang.Billing.Infrastructure.Persistence.Config
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
