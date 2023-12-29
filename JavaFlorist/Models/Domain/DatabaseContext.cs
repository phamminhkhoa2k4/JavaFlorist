
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
        //public DbSet<Order>? Order  { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }
        //public DbSet<Occasion>? Occassion  { get; set; }
        public DbSet<Contact> Contact { get; set; }
        //public DbSet<Delivery_Info>? Delivery_Info { get; set; }
        public DbSet<Bouquet_Info> Bouquet_Info { get; set; }
        //public DbSet<Cart_Bouquet>? Cart_Bouquet { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(u => u.UserId);
            modelBuilder.Entity<Wishlist>()
                .HasOne(w => w.Bouquet_Info) // Định nghĩa mối quan hệ giữa Wishlist và Bouquet_Info
                .WithMany() // Tùy thuộc vào thiết lập quan hệ ở mô hình dữ liệu của bạn
                .HasForeignKey(w => w.bouquet_id); // Định nghĩa khóa ngoại
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Customer)
                .WithOne()
                .HasForeignKey<ApplicationUser>(u => u.cust_id);
        }

    }
}
