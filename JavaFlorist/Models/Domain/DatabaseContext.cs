
using JavaFlorist.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace JavaFlorist.Models.Domain
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customer { get; set; }
        //public DbSet<Admin>? Admin  { get; set; }
        public DbSet<Blog> Blog { get; set; }
        //public DbSet<Cart>? Cart  { get; set; }
        public DbSet<Discount> Discount { get; set; }
        public DbSet<Order>  Order { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }
        public DbSet<Occasion> Occassion { get; set; }
        public DbSet<Contact> Contact { get; set; }
        //public DbSet<Delivery_Info>? Delivery_Info { get; set; }
        public DbSet<Bouquet_Info> Bouquet_Info { get; set; }
		//public DbSet<Cart_Bouquet>? Cart_Bouquet { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<CartItem> CartItems { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(u => u.UserId);
            modelBuilder.Entity<Wishlist>()
                .HasOne(w => w.Bouquet_Info) // Định nghĩa mối quan hệ giữa Wishlist và Bouquet_Info
                .WithMany() // Tùy thuộc vào thiết lập quan hệ ở mô hình dữ liệu của bạn
                .HasForeignKey(w => w.bouquet_id); // Định nghĩa khóa ngoại
            modelBuilder.Entity<Bouquet_Info>()
                .Property(b => b.price)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Customer)
                .WithOne()
                .HasForeignKey<ApplicationUser>(u => u.cust_id);
			modelBuilder.Entity<Order>()
			   .HasOne(o => o.Customer)
			   .WithMany()
			   .HasForeignKey(o => o.cust_id);

			modelBuilder.Entity<Order>()
				.HasOne(o => o.Discount)
				.WithMany()
				.HasForeignKey(o => o.discount_id);

			modelBuilder.Entity<Order>()
				.HasOne(o => o.Delivery_Info)
				.WithMany()
				.HasForeignKey(o => o.delivery_id);

			modelBuilder.Entity<Order>()
				.HasOne(o => o.Cart)
				.WithMany()
				.HasForeignKey(o => o.CartId);

			modelBuilder.Entity<Order>()
				.HasOne(o => o.Occasion)
				.WithMany()
				.HasForeignKey(o => o.Occasion_id);
			modelBuilder.Entity<CartItem>()
				.HasOne(c => c.Bouquet)
				.WithMany() 
				.HasForeignKey(c => c.bouquet_id);
            modelBuilder.Entity<CartItem>()
                .Property(c => c.SubTotal)
                .HasColumnType("decimal(18,2)");


            base.OnModelCreating(modelBuilder);
        }

    }
}
