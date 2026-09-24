using FlashSale.Domain.Entities;
using FlashSale.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace FlashSale.IntegrationTests
{
	public class RelationshipDatabaseTests
	{
		private DbContextOptions<AppDbContext> CreateOptions()
		{
			var connectionString =
				Environment.GetEnvironmentVariable("FLASHSale_TEST_CONNECTION");

			return new DbContextOptionsBuilder<AppDbContext>()
				.UseNpgsql(connectionString)
				.Options;
		}

		[Fact]
		public async Task Should_Save_And_Read_Related_Entities()
		{
			// Arrange

			var user = new User(
				"integration@test.com",
				"hashed-password");

			var product = new Product(
				"Integration Product",
				"Product for relationship test",
				500m,
				100,
				null);

			var flashSale = new FlashSaleEntity(
				product.Id,
				400m,
				DateTimeOffset.UtcNow.AddMinutes(-10),
				DateTimeOffset.UtcNow.AddHours(1));

			var reservation = new Reservation(
				user.Id,
				product.Id,
				flashSale.Id,
				2,
				DateTimeOffset.UtcNow.AddMinutes(10));

			var order = new Order(
				user.Id,
				reservation.Id,
				product.Id,
				flashSale.Id,
				2,
				400m);


			// Act

			await using (var context = new AppDbContext(CreateOptions()))
			{
				context.Users.Add(user);
				context.Products.Add(product);
				context.FlashSales.Add(flashSale);
				context.Reservations.Add(reservation);
				context.Orders.Add(order);

				await context.SaveChangesAsync();
			}


			// Assert

			await using (var context = new AppDbContext(CreateOptions()))
			{
				var savedOrder = await context.Orders
					.Include(o => o.User)
					.Include(o => o.Product)
					.Include(o => o.FlashSaleEntity)
					.Include(o => o.Reservation)
					.FirstOrDefaultAsync(o => o.Id == order.Id);

				Assert.NotNull(savedOrder);

				Assert.Equal(user.Id, savedOrder.UserId);
				Assert.Equal(product.Id, savedOrder.ProductId);
				Assert.Equal(flashSale.Id, savedOrder.FlashSaleId);
				Assert.Equal(reservation.Id, savedOrder.ReservationId);

				Assert.Equal("integration@test.com",
					savedOrder.User.UserEmail);

				Assert.Equal("Integration Product",
					savedOrder.Product.ProductName);

				Assert.Equal(400m,
					savedOrder.FlashSaleEntity.SalePrice);

				Assert.Equal(2,
					savedOrder.Reservation.Quantity);
			}


			// Cleanup

			await using (var context = new AppDbContext(CreateOptions()))
			{
				var savedOrder = await context.Orders
					.FirstOrDefaultAsync(o => o.Id == order.Id);

				var savedReservation = await context.Reservations
					.FirstOrDefaultAsync(r => r.Id == reservation.Id);

				var savedFlashSale = await context.FlashSales
					.FirstOrDefaultAsync(f => f.Id == flashSale.Id);

				var savedProduct = await context.Products
					.FirstOrDefaultAsync(p => p.Id == product.Id);

				var savedUser = await context.Users
					.FirstOrDefaultAsync(u => u.Id == user.Id);

				if (savedOrder != null)
					context.Orders.Remove(savedOrder);

				if (savedReservation != null)
					context.Reservations.Remove(savedReservation);

				if (savedFlashSale != null)
					context.FlashSales.Remove(savedFlashSale);

				if (savedProduct != null)
					context.Products.Remove(savedProduct);

				if (savedUser != null)
					context.Users.Remove(savedUser);

				await context.SaveChangesAsync();
			}
		}
	}
}


