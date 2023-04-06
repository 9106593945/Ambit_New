using System;

namespace Ambit.Domain.Entities
{
	public class ViewAllCustomerLogin : BaseEntity
	{
          public Int64 customerloginid { get; set; }
          public Int64 customerid { get; set; }
          public string name { get; set; }
          public string username { get; set; }
          public string deviceid { get; set; }    
          public string customer_number{ get; set; }
          public string customername { get; set; }
          public string password { get; set; }
          public bool isapproved { get; set; }
     }
}
