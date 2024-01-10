using JavaFlorist.Infrastructure;
using JavaFlorist.Models.Domain;
using JavaFlorist.Models.DTO;
using JavaFlorist.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace JavaFlorist.Repositories.Implementation
{
	public class OrderService : IOrderService
	{
		private readonly DatabaseContext ctx;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private ISession _session => _httpContextAccessor.HttpContext.Session;

		public OrderService(DatabaseContext ctx , IHttpContextAccessor httpContextAccessor)
		{
			this.ctx = ctx;
			_httpContextAccessor = httpContextAccessor;
		}
		public bool Add(Order model)
		{
			try
			{

				string cartKey = $"Cart_{model.cust_id}";
				var cartSesionItem = _session.GetJson<Cart>(cartKey) ?? new Cart();
				 var cartItems = cartSesionItem.Items;
				var updatedCartItems = cartItems.Select(item =>
				{
					// Tạo một bản sao của mục hiện tại trong danh sách
					var newItem = new CartItem
					{
						// Sao chép các trường từ mục hiện tại
						SubTotal = item.Quantity * item.Bouquet.price,
						cust_id = item.cust_id,
						Quantity = item.Quantity,
						bouquet_id = item.Bouquet.bouquet_id,

					
					};

					return newItem; // Trả về mục đã chỉnh sửa
				}).ToList();
				var Order = new Order
				{
					order_date = DateTime.Now,
					cust_id = model.cust_id,
					Total = model.Total,
					Delivery_Info = new Delivery_Info
					{
						Name = model.Delivery_Info.Name,
						Address = model.Delivery_Info.Address,
						Date = model.Delivery_Info.Date,
						Phone = model.Delivery_Info.Phone,
						Delivery_status = ""
					},
					Occasion = new Occasion
					{
						Occasion_name = model.Occasion.Occasion_name,
						message = model.Occasion.message,

					},
					

					Cart = new Cart
					{
						Items = updatedCartItems
					}

				};
				if(model.discount_id  != 0)
				{
					Order.discount_id = model.discount_id;
				}

                foreach (var item in updatedCartItems)
                {
                    var bouquet = ctx.Bouquet_Info.Find(item.bouquet_id);
                    if (bouquet != null)
                    {
                        // Update the sold count based on the quantity in the order
                        bouquet.sold += item.Quantity;
                        ctx.Bouquet_Info.Update(bouquet);
                        ctx.SaveChanges();
                    }
                   
                }

                ctx.Order.Add(Order);
				ctx.SaveChanges();

              
                
				return true;
			}catch (Exception ex)
			{
				return false;
			}
		}

        public IEnumerable<Order> GetAllOrders()
        {
            var data = ctx.Order
            .Include(o => o.Customer)
            .Include(o => o.Discount)
            .Include(o => o.Delivery_Info)
            .Include(o => o.Cart)
            .Include(o => o.Occasion)
            .ToList();
			return data;
        }

        public IEnumerable<decimal> GetAllTotalByReceivedStatus()
        {
            DateTime today = DateTime.Today;
            DateTime startOfMonth = new DateTime(today.Year, today.Month, 1);
            DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            var receivedOrdersTotal = ctx.Order
                .Where(o => o.Delivery_Info.Delivery_status == "Received" &&
                            o.order_date >= startOfMonth &&
                            o.order_date <= endOfMonth)
                .Select(o => (decimal)o.Total)
                .ToList();

            return receivedOrdersTotal;
        }


        public Order GetOrderById(int orderId)
        {

            var orderWithDetails = ctx.Order
				.Include(o => o.Customer)
				.Include(o => o.Discount)
				.Include(o => o.Delivery_Info)
				.Include(o => o.Occasion)
				.Include(o => o.Cart)
					.ThenInclude(cart => cart.Items)
					.ThenInclude(item => item.Bouquet)
				.FirstOrDefault(o => o.order_id == orderId);

            return orderWithDetails;
        }


        public IEnumerable<Order> GetAllOrdersByCustomerId(int customerId)
        {
            var data = ctx.Order
           .Include(o => o.Customer)
           .Include(o => o.Discount)
           .Include(o => o.Delivery_Info)
           .Include(o => o.Cart)
               .ThenInclude(cart => cart.Items) // Include CartItems within Cart
                   .ThenInclude(item => item.Bouquet) // Include Bouquet within CartItem
           .Include(o => o.Occasion)
           .Where(o => o.cust_id == customerId) // Filter by cust_id
           .ToList();

            return data;
        }

        public bool UpdateStatus(int orderId, string status)
        {
            try
            {
                var order = ctx.Order
                 .Include(o => o.Customer)
                 .Include(o => o.Discount)
                 .Include(o => o.Delivery_Info)
                 .Include(o => o.Occasion)
                 .Include(o => o.Cart)
                     .ThenInclude(cart => cart.Items)
                     .ThenInclude(item => item.Bouquet)
                 .FirstOrDefault(o => o.order_id == orderId);


                if (order != null)
                {
                  
                    order.Delivery_Info.Delivery_status = status;

                    ctx.Order.Update(order);
                    ctx.SaveChanges();
                    return true;
                }

                return false; 
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var orderToDelete = ctx.Order.FirstOrDefault(o => o.order_id == id);

                if (orderToDelete != null)
                {
                    ctx.Order.Remove(orderToDelete);
                    ctx.SaveChanges();
                    return true;
                }
                else
                {
                    return false; // Không tìm thấy đơn hàng cần xóa
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DashboardModel GetLatestOrder()
        {
            var latestOrders = ctx.Order
           .Include(o => o.Customer)
           .Include(o => o.Delivery_Info)
         .Where(o => o.Delivery_Info.Delivery_status == "")
         .OrderByDescending(o => o.order_date)
         .Take(6)
         .ToList();

          var topSoldBouquets = ctx.Bouquet_Info
        .OrderByDescending(b => b.sold)
        .Take(4)
        .ToList();


            var data = new DashboardModel
            {
                order = latestOrders,
                bouquets = topSoldBouquets
            };

            return data;
        }
    }
}
