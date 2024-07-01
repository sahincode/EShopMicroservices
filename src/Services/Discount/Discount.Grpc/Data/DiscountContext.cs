using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext :DbContext
    {
        public DbSet<Coupon> Coupons { get; set; }
        public DiscountContext(DbContextOptions options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon { Id = 1, ProductName = "Iphone X", Description = "It is so good product", Amount = 1200 },
                new Coupon { Id = 2, ProductName = "Iphone XS", Description = "It is better   than X", Amount = 1500 }
                );
        }
    }
}
