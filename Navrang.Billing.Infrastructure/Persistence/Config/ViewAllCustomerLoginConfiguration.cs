using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Navrang.Billing.Domain.Entities;

namespace Navrang.Billing.Infrastructure.Persistence.Config
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
