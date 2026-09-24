using FlashSale.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlashSale.Infrastructure.Persistence.Configurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.HasKey(o => o.Id);

			builder.Property(o => o.Quantity)
				.IsRequired();

			builder.Property(o => o.UnitPrice)
				.HasPrecision(18, 2)
				.IsRequired();

			builder.Property(o => o.TotalAmount)
				.HasPrecision(18, 2)
				.IsRequired();

			builder.Property(o => o.Status)
				.IsRequired();

			builder.Property(o => o.CreatedAt)
				.IsRequired();

			builder.Property(o => o.UpdatedAt)
				.IsRequired();

			builder.HasOne(o=>o.User)
					  .WithMany(u=>u.Orders)
					  .HasForeignKey(o=>o.UserId)
					  .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(o => o.Reservation)
					  .WithOne(r => r.Order)
					  .HasForeignKey<Order>(o => o.ReservationId)
					  .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(o => o.Product)
					  .WithMany(p => p.Orders)
					  .HasForeignKey(o => o.ProductId)
					  .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(o => o.FlashSaleEntity)
					  .WithMany(f => f.Orders)
					  .HasForeignKey(o => o.FlashSaleId)
					  .OnDelete(DeleteBehavior.Restrict);

			builder.HasIndex(o => o.UserId);
		}
	}
}
