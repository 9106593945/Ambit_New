using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ambit.Domain.Entities;

namespace Ambit.Infrastructure.Persistence.Config
{
	public class ViewAllCustomerLoginConfiguration : IEntityTypeConfiguration<ViewAllCustomerLogin>
     {
          public void Configure(EntityTypeBuilder<ViewAllCustomerLogin> builder)
          {
               builder.HasNoKey();
               builder.ToView("ViewAllCustomerLogin");
          }
     }
}
