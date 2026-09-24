using FlashSale.Domain.Enums;
using FlashSaleEntity = FlashSale.Domain.Entities.FlashSaleEntity;

namespace FlashSale.UnitTests.Domain
{
	public class FlashSaleTest
	{
		[Fact]
		public void CreateFlashSale_WithValidData_ShouldSetStatusToScheduled()
		{

			// Arrange
			var productId = Guid.NewGuid();
			var startTime = DateTimeOffset.UtcNow.AddHours(1);
			var endTime = DateTimeOffset.UtcNow.AddHours(2);

			// Act
			var flashSale = new FlashSaleEntity(
				productId,
				40000m,
				startTime,
				endTime);

			// Assert
			Assert.Equal(FlashSaleStatus.Scheduled, flashSale.Status);
		}
		[Fact]
		public void CreateFlashSale_WhenProductIdIsEmpty_ShouldThrowException()
		{
			Assert.Throws<ArgumentException>(() =>
			{
				var flashSale = new FlashSaleEntity(
					Guid.Empty,
					40000m,
					DateTimeOffset.UtcNow.AddHours(1),
					DateTimeOffset.UtcNow.AddHours(2));
			});
		}

		[Fact]
		public void CreateFlashSale_WhenSalePriceIsZero_ShouldThrowException()
		{
			Assert.Throws<ArgumentException>(() =>
			{
				var flashSale = new FlashSaleEntity(
					Guid.NewGuid(),
					0m,
					DateTimeOffset.UtcNow.AddHours(1),
					DateTimeOffset.UtcNow.AddHours(2));
			});
		}

		[Fact]
		public void CreateFlashSale_WhenSalePriceIsNegative_ShouldThrowException()
		{
			Assert.Throws<ArgumentException>(() =>
			{
				var flashSale = new FlashSaleEntity(
					Guid.NewGuid(),
					-100m,
					DateTimeOffset.UtcNow.AddHours(1),
					DateTimeOffset.UtcNow.AddHours(2));
			});
		}

		[Fact]
		public void CreateFlashSale_WhenStartTimeIsAfterEndTime_ShouldThrowException()
		{
			Assert.Throws<ArgumentException>(() =>
			{
				var flashSale = new FlashSaleEntity(
					Guid.NewGuid(),
					40000m,
					DateTimeOffset.UtcNow.AddHours(2),
					DateTimeOffset.UtcNow.AddHours(1));
			});
		}

		[Fact]
		public void CreateFlashSale_WhenStartTimeEqualsEndTime_ShouldThrowException()
		{
			var time = DateTimeOffset.UtcNow.AddHours(1);

			Assert.Throws<ArgumentException>(() =>
			{
				var flashSale = new FlashSaleEntity(
					Guid.NewGuid(),
					40000m,
					time,
					time);
			});
		}

		[Fact]
		public void Activate_WhenSaleIsScheduled_ShouldSetStatusToActive()
		{
			// Arrange
			var flashSale = CreateFlashSale();

			// Act
			flashSale.Activate();

			// Assert
			Assert.Equal(FlashSaleStatus.Active, flashSale.Status);
		}

		[Fact]
		public void End_WhenSaleIsActive_ShouldSetStatusToEnded()
		{
			// Arrange
			var flashSale = CreateFlashSale();

			flashSale.Activate();

			// Act
			flashSale.End();

			// Assert
			Assert.Equal(FlashSaleStatus.Ended, flashSale.Status);
		}

		[Fact]
		public void Cancel_WhenSaleIsScheduled_ShouldSetStatusToCancelled()
		{
			// Arrange
			var flashSale = CreateFlashSale();

			// Act
			flashSale.Cancel();

			// Assert
			Assert.Equal(FlashSaleStatus.Cancelled, flashSale.Status);
		}

		[Fact]
		public void Activate_WhenSaleIsNotScheduled_ShouldThrowException()
		{
			// Arrange
			var flashSale = CreateFlashSale();
			flashSale.Activate();
			flashSale.End();

			// Act & Assert
			Assert.Throws<InvalidOperationException>(() =>
			{
				flashSale.Activate();
			});
		}

		[Fact]
		public void End_WhenSaleIsNotActive_ShouldThrowException()
		{
			// Arrange
			var flashSale = CreateFlashSale();

			// Act & Assert
			Assert.Throws<InvalidOperationException>(() =>
			{
				flashSale.End();
			});
		}

		[Fact]
		public void Cancel_WhenSaleIsNotScheduled_ShouldThrowException()
		{
			// Arrange
			var flashSale = CreateFlashSale();
			flashSale.Activate();

			// Act & Assert
			Assert.Throws<InvalidOperationException>(() =>
			{
				flashSale.Cancel();
			});
		}

		private static FlashSaleEntity CreateFlashSale()
		{
			return new FlashSaleEntity(
				Guid.NewGuid(),
				40000m,
				DateTimeOffset.UtcNow.AddHours(1),
				DateTimeOffset.UtcNow.AddHours(2));
		}
	}
}
