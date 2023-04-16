using Ambit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
