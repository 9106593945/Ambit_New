using System.ComponentModel.DataAnnotations;

namespace Ambit.Domain.Entities
{
	public class Order : BaseEntity
	{
		[Key]
		public long orderid { get; set; }
        public string ordernumber { get; set; }
        public string description { get; set; }
        public string customername { get; set; }
        public int CustomerId { get; set; }
        public int status { get; set; }
        public decimal subtotal { get; set; }
        public decimal tax { get; set; }
        public DateTime orderdate { get; set; }
        public decimal discount { get; set; }
        public decimal shipingcharge { get; set; }
        public decimal grandtotal { get; set; }
        public int shippingaddressid { get; set; }
        public int billingaddressid { get; set; }
        public bool Active { get; set; }
    }
}
