using JavaFlorist.Models.Domain;

namespace JavaFlorist.Repositories.Abstract
{
	public interface IOrderService
	{
		bool Add(Order model);
        IEnumerable<Order> GetAllOrders();
    }
}
