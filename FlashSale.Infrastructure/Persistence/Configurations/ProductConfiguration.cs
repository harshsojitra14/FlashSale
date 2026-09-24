using FlashSale.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FlashSale.Infrastructure.Persistence.Configurations
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasKey(p=>p.Id);

			builder.Property(p => p.ProductName)
					  .IsRequired()
					  .HasMaxLength(200);
		
			builder.Property(p=>p.Description)
					  .IsRequired()
					  .HasMaxLength (2000);

			builder.Property(p => p.RegularPrice)
					  .HasPrecision(18, 2);

			builder.Property(p => p.TotalStock)
					  .IsRequired();

			builder.Property(p => p.AvailableStock)
					  .IsRequired();

			builder.Property(p => p.IsActive)
					  .IsRequired();

			builder.Property(p => p.CreatedAt)
					  .IsRequired();

			builder.Property(p => p.UpdatedAt)
					  .IsRequired();

			
		}
	}
}
