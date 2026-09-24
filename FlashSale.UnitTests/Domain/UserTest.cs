using FlashSale.Domain.Entities;
using FlashSale.Domain.Enums;

namespace FlashSale.UnitTests.Domain;

public class UserTests
{
	[Fact]
	public void CreateUser_WithValidData_ShouldSetRoleToCustomer()
	{
		// Arrange
		var email = "test@example.com";
		var passwordHash = "hashed-password";

		// Act
		var user = new User(email, passwordHash);

		// Assert
		Assert.Equal(UserRole.Customer, user.Role);
		Assert.Equal(email, user.UserEmail);
		Assert.Equal(passwordHash, user.Password);
	}
	[Fact]
	public void CreateUser_WhenEmailIsEmpty_ShouldThrowException()
	{
		Assert.Throws<ArgumentException>(() =>
		{
			var user = new User("", "hashed-password");
		});
	}

	[Fact]
	public void CreateUser_WhenEmailIsWhitespace_ShouldThrowException()
	{
		Assert.Throws<ArgumentException>(() =>
		{
			var user = new User("   ", "hashed-password");
		});
	}

	[Fact]
	public void CreateUser_WhenPasswordHashIsEmpty_ShouldThrowException()
	{
		Assert.Throws<ArgumentException>(() =>
		{
			var user = new User("test@example.com", "");
		});
	}

	[Fact]
	public void CreateUser_WhenPasswordHashIsWhitespace_ShouldThrowException()
	{
		Assert.Throws<ArgumentException>(() =>
		{
			var user = new User("test@example.com", "   ");
		});
	}
}
