using FlashSale.Domain.Entities;
using FlashSale.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace FlashSale.IntegrationTests
{
	public class ProductDatabaseTests
	{
		private DbContextOptions<AppDbContext> CreateOptions()
		{
			var connectionString = Environment.GetEnvironmentVariable("FLASHSale_TEST_CONNECTION");

			return new DbContextOptionsBuilder<AppDbContext>()
				.UseNpgsql(connectionString)
				.Options;
		}

		[Fact]
		public async Task Should_Save_And_Read_Product()
		{
			// Arrange
			var product = new Product(
				"Test Product",
				"Integration test product",
				999.99m,
				100,
				null);

			// Act
			await using (var context = new AppDbContext(CreateOptions()))
			{
				context.Products.Add(product);
				await context.SaveChangesAsync();
			}

			// Assert
			await using (var context = new AppDbContext(CreateOptions()))
			{
				var savedProduct = await context.Products
					.FirstOrDefaultAsync(p => p.Id == product.Id);

				Assert.NotNull(savedProduct);
				Assert.Equal("Test Product", savedProduct.ProductName);
				Assert.Equal(999.99m, savedProduct.RegularPrice);
				Assert.Equal(100, savedProduct.TotalStock);
				Assert.Equal(100, savedProduct.AvailableStock);
			}

			// Cleanup
			await using (var context = new AppDbContext(CreateOptions()))
			{
				var productToDelete = await context.Products
					.FirstOrDefaultAsync(p => p.Id == product.Id);

				if (productToDelete != null)
				{
					context.Products.Remove(productToDelete);
					await context.SaveChangesAsync();
				}
			}
		}
	}
}
