using FlashSale.Domain.Entities;
using FlashSale.Domain.Enums;

namespace FlashSale.UnitTests.Domain;

public class OrderTests
{
	[Fact]
	public void CreateOrder_WithValidData_ShouldSetStatusToPending()
	{
		var order = CreateOrder();

		Assert.Equal(OrderStatus.Pending, order.Status);
	}

	[Fact]
	public void CreateOrder_WhenUserIdIsEmpty_ShouldThrowException()
	{
		Assert.Throws<ArgumentException>(() =>
		{
			var order = new Order(
				Guid.Empty,
				Guid.NewGuid(),
				Guid.NewGuid(),
				Guid.NewGuid(),
				2,
				40000m);
		});
	}

	[Fact]
	public void CreateOrder_WhenReservationIdIsEmpty_ShouldThrowException()
	{
		Assert.Throws<ArgumentException>(() =>
		{
			var order = new Order(
				Guid.NewGuid(),
				Guid.Empty,
				Guid.NewGuid(),
				Guid.NewGuid(),
				2,
				40000m);
		});
	}

	[Fact]
	public void CreateOrder_WhenProductIdIsEmpty_ShouldThrowException()
	{
		Assert.Throws<ArgumentException>(() =>
		{
			var order = new Order(
				Guid.NewGuid(),
				Guid.NewGuid(),
				Guid.Empty,
				Guid.NewGuid(),
				2,
				40000m);
		});
	}

	[Fact]
	public void CreateOrder_WhenFlashSaleIdIsEmpty_ShouldThrowException()
	{
		Assert.Throws<ArgumentException>(() =>
		{
			var order = new Order(
				Guid.NewGuid(),
				Guid.NewGuid(),
				Guid.NewGuid(),
				Guid.Empty,
				2,
				40000m);
		});
	}

	[Fact]
	public void CreateOrder_WhenQuantityIsZero_ShouldThrowException()
	{
		Assert.Throws<ArgumentException>(() =>
		{
			var order = new Order(
				Guid.NewGuid(),
				Guid.NewGuid(),
				Guid.NewGuid(),
				Guid.NewGuid(),
				0,
				40000m);
		});
	}

	[Fact]
	public void CreateOrder_WhenQuantityIsNegative_ShouldThrowException()
	{
		Assert.Throws<ArgumentException>(() =>
		{
			var order = new Order(
				Guid.NewGuid(),
				Guid.NewGuid(),
				Guid.NewGuid(),
				Guid.NewGuid(),
				-1,
				40000m);
		});
	}

	[Fact]
	public void CreateOrder_WhenUnitPriceIsZero_ShouldThrowException()
	{
		Assert.Throws<ArgumentException>(() =>
		{
			var order = new Order(
				Guid.NewGuid(),
				Guid.NewGuid(),
				Guid.NewGuid(),
				Guid.NewGuid(),
				2,
				0m);
		});
	}

	[Fact]
	public void CreateOrder_WhenUnitPriceIsNegative_ShouldThrowException()
	{
		Assert.Throws<ArgumentException>(() =>
		{
			var order = new Order(
				Guid.NewGuid(),
				Guid.NewGuid(),
				Guid.NewGuid(),
				Guid.NewGuid(),
				2,
				-100m);
		});
	}

	[Fact]
	public void CreateOrder_ShouldCalculateTotalAmountCorrectly()
	{
		var order = new Order(
			Guid.NewGuid(),
			Guid.NewGuid(),
			Guid.NewGuid(),
			Guid.NewGuid(),
			3,
			40000m);

		Assert.Equal(120000m, order.TotalAmount);
	}

	[Fact]
	public void Confirm_WhenOrderIsPending_ShouldSetStatusToConfirmed()
	{
		var order = CreateOrder();

		order.Confirm();

		Assert.Equal(OrderStatus.Confirmed, order.Status);
	}

	[Fact]
	public void Cancel_WhenOrderIsPending_ShouldSetStatusToCancelled()
	{
		var order = CreateOrder();

		order.Cancel();

		Assert.Equal(OrderStatus.Cancelled, order.Status);
	}

	[Fact]
	public void Confirm_WhenOrderIsNotPending_ShouldThrowException()
	{
		var order = CreateOrder();

		order.Cancel();

		Assert.Throws<InvalidOperationException>(() =>
		{
			order.Confirm();
		});
	}

	[Fact]
	public void Cancel_WhenOrderIsNotPending_ShouldThrowException()
	{
		var order = CreateOrder();

		order.Confirm();

		Assert.Throws<InvalidOperationException>(() =>
		{
			order.Cancel();
		});
	}

	private static Order CreateOrder()
	{
		return new Order(
			Guid.NewGuid(),
			Guid.NewGuid(),
			Guid.NewGuid(),
			Guid.NewGuid(),
			2,
			40000m);
	}
}