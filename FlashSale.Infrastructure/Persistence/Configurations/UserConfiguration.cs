using FlashSale.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace FlashSale.Infrastructure.Persistence.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(u => u.Id);

			builder.Property(u => u.UserEmail)
			   .IsRequired()
			   .HasMaxLength(320);

			builder.HasIndex(u => u.UserEmail)
				.IsUnique();

			builder.Property(u => u.Password)
				.IsRequired()
				.HasMaxLength(500);

			builder.Property(u => u.Role)
				.IsRequired();

			
		}
	}
}
