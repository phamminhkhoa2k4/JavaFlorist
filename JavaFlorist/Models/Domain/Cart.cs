﻿using JavaFlorist.Models.DTO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace JavaFlorist.Models.Domain
{
	public class Cart
	{

        [Column(TypeName = "numeric(18, 0)")]
        public int CartId { get; set; }


		public List<CartItem> Items { get; set; } = new List<CartItem>();

		public void AddItem(Bouquet_Info bouquet, int quantity, int cust_id)
		{
			CartItem? item = Items
				.Where(p => p.Bouquet.bouquet_id == bouquet.bouquet_id)
				.FirstOrDefault();
			if (item == null)
			{
				Items.Add(new CartItem
				{

					Bouquet = bouquet,
					Quantity = quantity,
					cust_id = cust_id,

				});
			}
			else
			{
				item.Quantity += quantity;
			}
		}

		public void RemoveItem(Bouquet_Info bouquet) => Items.RemoveAll(i => i.Bouquet.bouquet_id == bouquet.bouquet_id);

		

		public void UpdateQuantity(int bouquetId, int newQuantity)
		{
			CartItem? item = Items.FirstOrDefault(p => p.Bouquet.bouquet_id == bouquetId);
			if (item != null)
			{
				item.Quantity = newQuantity;
			}

		}
		public void Clear() => Items.Clear();
	}
}
