using FlashSale.Domain.Entities;
using FlashSale.Domain.Enums;

namespace FlashSale.UnitTests.Domain
{
	public class ReservationTest
	{
		[Fact]
		public void CreateReservation_WithValidData_ShouldSetStatusToReserved()
		{
			// Arrange
			var userId = Guid.NewGuid();
			var productId = Guid.NewGuid();
			var flashSaleId = Guid.NewGuid();
			var expiresAt = DateTimeOffset.UtcNow.AddMinutes(10);

			// Act
			var reservation = new Reservation(
				userId,
				productId,
				flashSaleId,
				2,
				expiresAt);

			// Assert
			Assert.Equal(ReservationStatus.Reserved, reservation.Status);
		}
		[Fact]
		public void CreateReservation_WhenUserIdIsEmpty_ShouldThrowException()
		{
			Assert.Throws<ArgumentException>(() =>
			{
				var reservation = new Reservation(
					Guid.Empty,
					Guid.NewGuid(),
					Guid.NewGuid(),
					2,
					DateTimeOffset.UtcNow.AddMinutes(10));
			});
		}

		[Fact]
		public void CreateReservation_WhenProductIdIsEmpty_ShouldThrowException()
		{
			Assert.Throws<ArgumentException>(() =>
			{
				var reservation = new Reservation(
					Guid.NewGuid(),
					Guid.Empty,
					Guid.NewGuid(),
					2,
					DateTimeOffset.UtcNow.AddMinutes(10));
			});
		}

		[Fact]
		public void CreateReservation_WhenFlashSaleIdIsEmpty_ShouldThrowException()
		{
			Assert.Throws<ArgumentException>(() =>
			{
				var reservation = new Reservation(
					Guid.NewGuid(),
					Guid.NewGuid(),
					Guid.Empty,
					2,
					DateTimeOffset.UtcNow.AddMinutes(10));
			});
		}

		[Fact]
		public void CreateReservation_WhenQuantityIsZero_ShouldThrowException()
		{
			Assert.Throws<ArgumentException>(() =>
			{
				var reservation = new Reservation(
					Guid.NewGuid(),
					Guid.NewGuid(),
					Guid.NewGuid(),
					0,
					DateTimeOffset.UtcNow.AddMinutes(10));
			});
		}

		[Fact]
		public void CreateReservation_WhenQuantityIsNegative_ShouldThrowException()
		{
			Assert.Throws<ArgumentException>(() =>
			{
				var reservation = new Reservation(
					Guid.NewGuid(),
					Guid.NewGuid(),
					Guid.NewGuid(),
					-1,
					DateTimeOffset.UtcNow.AddMinutes(10));
			});
		}

		[Fact]
		public void CreateReservation_WhenExpirationIsInPast_ShouldThrowException()
		{
			Assert.Throws<ArgumentException>(() =>
			{
				var reservation = new Reservation(
					Guid.NewGuid(),
					Guid.NewGuid(),
					Guid.NewGuid(),
					2,
					DateTimeOffset.UtcNow.AddMinutes(-10));
			});
		}

		[Fact]
		public void Confirm_WhenReservationIsReserved_ShouldSetStatusToConfirmed()
		{
			// Arrange
			var reservation = CreateReservation();

			// Act
			reservation.Confirm();

			// Assert
			Assert.Equal(ReservationStatus.Confirmed, reservation.Status);
		}

		[Fact]
		public void Cancel_WhenReservationIsReserved_ShouldSetStatusToCancelled()
		{
			// Arrange
			var reservation = CreateReservation();

			// Act
			reservation.Cancel();

			// Assert
			Assert.Equal(ReservationStatus.Cancelled, reservation.Status);
		}

		[Fact]
		public void Expire_WhenReservationIsReserved_ShouldSetStatusToExpired()
		{
			// Arrange
			var reservation = CreateReservation();

			// Act
			reservation.Expire();

			// Assert
			Assert.Equal(ReservationStatus.Expired, reservation.Status);
		}

		[Fact]
		public void Confirm_WhenReservationIsNotReserved_ShouldThrowException()
		{
			// Arrange
			var reservation = CreateReservation();
			reservation.Cancel();

			// Act & Assert
			Assert.Throws<InvalidOperationException>(() =>
			{
				reservation.Confirm();
			});
		}

		[Fact]
		public void Cancel_WhenReservationIsNotReserved_ShouldThrowException()
		{
			// Arrange
			var reservation = CreateReservation();
			reservation.Confirm();

			// Act & Assert
			Assert.Throws<InvalidOperationException>(() =>
			{
				reservation.Cancel();
			});
		}

		[Fact]
		public void Expire_WhenReservationIsNotReserved_ShouldThrowException()
		{
			// Arrange
			var reservation = CreateReservation();
			reservation.Confirm();

			// Act & Assert
			Assert.Throws<InvalidOperationException>(() =>
			{
				reservation.Expire();
			});
		}

		private static Reservation CreateReservation()
		{
			return new Reservation(
				Guid.NewGuid(),
				Guid.NewGuid(),
				Guid.NewGuid(),
				2,
				DateTimeOffset.UtcNow.AddMinutes(10));
		}
	}
}
