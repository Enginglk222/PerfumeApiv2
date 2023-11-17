using FinekraApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinekraApi.Core
{
    public class PerfumeDbContext : DbContext
    {
        public PerfumeDbContext(DbContextOptions<PerfumeDbContext> options) : base(options)
        {
        }

        public DbSet<Brands> Brands { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Perfumes> Perfumes { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Brands varlık tipine birincil anahtar ekleyin
            modelBuilder.Entity<Brands>().HasKey(b => b.BrandId);

            // OrderDetails varlık tipine birincil anahtar ekleyin
            modelBuilder.Entity<OrderDetails>().HasKey(od => od.OrderDetailId);

            // Orders varlık tipine birincil anahtar ekleyin
            modelBuilder.Entity<Orders>().HasKey(o => o.OrderId);

            // Perfumes varlık tipine birincil anahtar ekleyin
            modelBuilder.Entity<Perfumes>().HasKey(p => p.PerfumeId);

            // UserDetails varlık tipine birincil anahtar ekleyin
            modelBuilder.Entity<UserDetails>().HasKey(ud => ud.UserDetailId);

            // Diğer varlık türlerinin konfigürasyonları burada yapılabilir

            base.OnModelCreating(modelBuilder);
        }
    }
}
