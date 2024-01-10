using JavaFlorist.Models.Domain;
using JavaFlorist.Models.DTO;

namespace JavaFlorist.Repositories.Abstract
{
	public interface IOrderService
	{
		bool Add(Order model);
        IEnumerable<Order> GetAllOrders();

        DashboardModel GetLatestOrder();

        IEnumerable<Order> GetAllOrdersByCustomerId(int customerId);

        IEnumerable<decimal> GetAllTotalByReceivedStatus();
        Order GetOrderById(int orderId);
        bool Delete(int id);
        public bool UpdateStatus(int orderId, string status);

       

    }
}
