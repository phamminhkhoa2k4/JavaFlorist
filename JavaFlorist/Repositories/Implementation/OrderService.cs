﻿using JavaFlorist.Infrastructure;
using JavaFlorist.Models.Domain;
using JavaFlorist.Models.DTO;
using JavaFlorist.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

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
					},
					Occasion = new Occasion
					{
						Occasion_name = model.Occasion.Occasion_name,
						message = model.Occasion.message,

					},
					discount_id = model.discount_id,

					Cart = new Cart
					{
						Items = updatedCartItems
					}

				};
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
    }
}
