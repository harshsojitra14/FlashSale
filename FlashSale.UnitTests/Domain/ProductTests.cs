using FlashSale.Domain.Entities;

namespace FlashSale.UnitTests.Domain
{
	public class ProductTests
	{
		[Fact]
		public void CreateProduct_WithValidData_ShouldSetAvailableStockToTotalStock()
		{
			var product = new Product("S25Ultra", "Best Product", 50000m, 100, null);

			var availablestock = product.AvailableStock;

			Assert.Equal(100, availablestock);
		}

		[Fact]
		public void ReserveStock_WithAvailableQuantity_ShouldReduceAvailableStock()
		{
			var product = new Product("S25Ultra", "Best Product", 50000m, 100, null);

			product.ReserveStock(20);

			var availablestock = product.AvailableStock;

			Assert.Equal(80, availablestock);

		}

		[Fact]
		public void ReserveStock_WhenQuantityExceedsAvailableStock_ShouldThrowException()
		{
			var product = new Product("S25Ultra", "Best Product", 50000m, 100, null);


			Assert.Throws<InvalidOperationException>(() =>
			{
				product.ReserveStock(120);
			});
		}

		[Fact]
		public void ReleaseStock_ShouldIncreaseAvailableStock()
		{
			var product = new Product("S25Ultra", "Best Product", 50000m, 100, null);

			product.ReserveStock(20);
			product.ReleaseStock(10);

			Assert.Equal(90, product.AvailableStock);
		}

		[Fact]
		public void ReleseStock_WhenQuantityExceedsAvailableStock_ShouldThrowException()
		{
			var product = new Product("S25Ultra", "Best Product", 50000m, 100, null);

			Assert.Throws<InvalidOperationException>(() =>
			{
				product.ReleaseStock(120);
			});
		}

		[Fact]
		public void CreateProduct_WhenProductNameIsEmpty_ShouldThrowException()
		{
			Assert.Throws<ArgumentException>(()=>
			{
				var product = new Product("", "Best Product", 50000m, 100, null);
			});
		}

		[Fact]
		public void CreateProduct_WhenPriceIsLessThanZero_ShouldThrowException()
		{
			Assert.Throws<ArgumentException>(() =>
			{
				var product = new Product("S25Ultra", "Best Product", -10, 100, null);
			});
		}

		[Fact]
		public void CreateProduct_WhenQuantityIsLessThanZero_ShouldThrowException()
		{
			Assert.Throws<ArgumentException>(() =>
			{
				var product = new Product("S25Ultra", "Best Product", 50000m,0, null);
			});
		}
	}
}
