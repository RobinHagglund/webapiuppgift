using Microsoft.EntityFrameworkCore;
using webapiuppgift.Models.Order;
using webapiuppgift.Models.Product;
using webapiuppgift.Models.User;

namespace webapiuppgift.Data
{
    public class DatebaseContext : DbContext
    {
        public DatebaseContext()
        {
        }

        public DatebaseContext(DbContextOptions<DatebaseContext> options) : base(options)
        {
        }
        public virtual DbSet<ProductCategoryEntity> ProductCategories { get; set; } = null!;
        public virtual DbSet<ProductEntity> Products { get; set; } = null!;
        public virtual DbSet<UserEntity> Users { get; set; } = null!;
        public virtual DbSet<OrderEntity> Orders { get; set; } = null!;
        public virtual DbSet<OrderRowEntity> OrderRows { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderRowEntity>()
                .HasKey(c => new { c.OrderId, c.ArticleNumber });
        }
    }
}
