using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Ambit.AppCore.EntityModels;
using Ambit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Ambit.Infrastructure.Persistence
{
	public class AppDbContext : DbContext
     {
          public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
          {
          }

          private string ExecuteDbFunctionReturnString(string functionName, params object[] sqlParameters)
          {
               var parameters = new List<SqlParameter>();
               if (sqlParameters != null && sqlParameters.Length > 0)
               {
                    for (int i = 0; i < sqlParameters.Length; i++)
                    {
                         parameters.Add(new SqlParameter("@p" + i, sqlParameters[i] ?? (object)DBNull.Value));
                    }
               }

               var outputParameter = new SqlParameter("@result", SqlDbType.NVarChar);
               // Size -1 treats as NVarChar(max)
               outputParameter.Size = -1;
               outputParameter.Direction = ParameterDirection.Output;

               var parameterIndexes = string.Join(",", parameters.Select(x => x.ParameterName));
               parameters.Add(outputParameter);

               this.Database.ExecuteSqlRaw("set @result = " + functionName + "(" + parameterIndexes + ")", parameters);
               return outputParameter.Value?.ToString();
          }

          [DbFunction]
          public string GetEmailsForBuyerRenter(string buyerRenterId)
          {
               return ExecuteDbFunctionReturnString("dbo.get_email_koper_huurder", buyerRenterId);
          }

          [DbFunction]
          public string GetSalutationForEmail(string organisationId, string personId, string relationId, string buyerRenterId, bool? formal)
          {
               return ExecuteDbFunctionReturnString("dbo.get_briefaanhef", organisationId, personId, relationId, buyerRenterId, formal);
          }

          [DbFunction]
          public string GetDomainTranslation(string domainId, string dbValue)
          {
               return ExecuteDbFunctionReturnString("dbo.vertaal_domein", domainId, dbValue);
          }


          public DbSet<Users> Users { get; set; }
          public DbSet<Items> Items { get; set; }
          public DbSet<favoriteitem> favoriteItem { get; set; }
          public DbSet<Company> Company { get; set; }
          public DbSet<Customer> Customer { get; set; }
          public DbSet<CustomerLogin> CustomerLogin { get; set; }
          public DbSet<cart> Cart { get; set; }
          public DbSet<category> Category{ get; set; }
          public DbSet<categoryitemlink>  Categoryitemlink{ get; set; }
          public DbSet<cartitems> CartItems { get; set; }
          public DbSet<ViewAllItems> ViewAllItems { get; set; }
          public DbSet<ViewAllCustomerLogin> ViewAllCustomerLogin { get; set; }

          protected override void OnModelCreating(ModelBuilder builder)
          {
               //builder.Entity<Items>().HasKey(o => o.itemid);
               builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
               base.OnModelCreating(builder);
          }
     }
}
