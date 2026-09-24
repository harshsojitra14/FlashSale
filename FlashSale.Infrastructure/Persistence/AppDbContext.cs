using FlashSale.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlashSale.Infrastructure.Persistence
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

		public DbSet<Product> Products { get; set; }
		public DbSet<FlashSaleEntity> FlashSales { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Reservation> Reservations { get; set; }
		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

			base.OnModelCreating(modelBuilder);
		}
	}
	
}
