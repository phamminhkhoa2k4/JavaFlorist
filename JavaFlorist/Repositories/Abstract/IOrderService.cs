using JavaFlorist.Models.Domain;

namespace JavaFlorist.Repositories.Abstract
{
	public interface IOrderService
	{
		bool Add(Order model);
        IEnumerable<Order> GetAllOrders();

        IEnumerable<Order> GetAllOrdersByCustomerId(int customerId);

        Order GetOrderById(int orderId);
        bool Delete(int id);
        public bool UpdateStatus(int orderId, string status);

       

    }
}
