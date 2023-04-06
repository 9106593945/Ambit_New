namespace Ambit.AppCore.Models
{
	public interface IHomeService
	{
		long GetInvoiceCounts();
		long GetCustomerCount();
		long GetItemCount();
		long GetSupplierCount();
		decimal GetTotalReceivedPayment();
		decimal GetTotalSales();
		decimal GetTotalRecievedAmount();
		decimal GetTotalPayableAmount();
		decimal GetTotalStock();
	}
}
