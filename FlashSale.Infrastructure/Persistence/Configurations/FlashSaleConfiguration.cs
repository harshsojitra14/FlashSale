using FlashSale.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlashSale.Infrastructure.Persistence.Configurations
{
	public class FlashSaleConfiguration : IEntityTypeConfiguration<FlashSaleEntity>
	{
		public void Configure(EntityTypeBuilder<FlashSaleEntity> builder)
		{
			builder.HasKey(f => f.Id);

			builder.Property(f => f.SalePrice)
				.HasPrecision(18, 2)
				.IsRequired();

			builder.Property(f => f.StartTime)
				.IsRequired();

			builder.Property(f => f.EndTime)
				.IsRequired();

			builder.Property(f => f.CreatedAt)
				.IsRequired();

			builder.Property(f => f.UpdatedAt)
				.IsRequired();

			builder.Property(f => f.Status)
				.IsRequired();

			builder.HasOne(f => f.Product)
				.WithMany(p => p.FlashSales)
				.HasForeignKey(f => f.ProductId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasIndex(f => f.ProductId);
		}
	}
}
