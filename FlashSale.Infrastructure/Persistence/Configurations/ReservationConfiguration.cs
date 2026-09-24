using FlashSale.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace FlashSale.Infrastructure.Persistence.Configurations
{
	public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
	{
		public void Configure(EntityTypeBuilder<Reservation> builder)
		{
			builder.HasKey(r => r.Id);

			builder.Property(r => r.Quantity)
				.IsRequired();

			builder.Property(r => r.Status)
				.IsRequired();

			builder.Property(r => r.CreatedAt)
				.IsRequired();

			builder.Property(r => r.UpdatedAt)
				.IsRequired();

			builder.Property(r => r.ExpiresAt)
				.IsRequired();

			builder.HasOne(r=>r.User)
					  .WithMany(u=>u.Reservations)
					  .HasForeignKey(r=> r.UserId)
					  .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(r => r.Product)
					  .WithMany(p => p.Reservations)
					  .HasForeignKey(r => r.ProductId)
					  .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(r => r.FlashSaleEntity)
					  .WithMany(f => f.Reservations)
					  .HasForeignKey(r => r.FlashSaleId)
					  .OnDelete(DeleteBehavior.Restrict);

			builder.HasIndex(r => r.UserId);
			builder.HasIndex(r => r.FlashSaleId);
			builder.HasIndex(r => r.ExpiresAt);
		}
	}
}
