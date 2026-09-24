using System;

namespace FlashSale.Domain.Entities
{
	public class Product
	{
		public Guid Id { get;private set; }
		public string ProductName { get; private set; }=string.Empty;
		public string Description { get; private set; }= string.Empty;
		public string? ImageUrl{ get; private set; }
		public decimal RegularPrice { get;private set; }
		public int TotalStock { get; private set; }
		public int AvailableStock { get; private set; }
		public bool IsActive { get; private set; }
		public DateTimeOffset CreatedAt { get; private set; }
		public DateTimeOffset UpdatedAt { get; private set; }
		public ICollection<FlashSaleEntity> FlashSales{  get; private set; }=new List<FlashSaleEntity>();
		public ICollection<Reservation> Reservations{ get; private set; } =new List<Reservation>();
		public ICollection<Order> Orders{ get; private set; }= new List<Order>();

		public Product(string productName,string description,decimal regularPrice,int totalStock,string? imageUrl)
		{
			if(string.IsNullOrWhiteSpace(productName))
				throw new ArgumentException("Product name is required.");

			if (regularPrice <= 0)
				throw new ArgumentException("Regular price must be greater than zero.");

			if (totalStock <= 0)
				throw new ArgumentException("Total stock must be greater than zero.");

			Id = Guid.NewGuid();
			ProductName = productName;
			Description = description;
			RegularPrice = regularPrice;
			TotalStock = totalStock;
			AvailableStock = totalStock;
			IsActive = true;
			ImageUrl = imageUrl;
			CreatedAt = DateTimeOffset.UtcNow;
			UpdatedAt = CreatedAt;
		}

		public void ReserveStock(int quantity)
		{
			if (quantity <= 0)
				throw new ArgumentException("Quantity is must be greater than 0");

			if(quantity>AvailableStock)
				throw new InvalidOperationException($"{quantity} Quantity is not available");

			AvailableStock-=quantity;
			UpdatedAt= DateTimeOffset.UtcNow;
		}

		public void ReleaseStock(int quantity)
		{
			if (quantity <= 0)
				throw new ArgumentException("Quantity is must be greater than 0");

			if (AvailableStock +quantity >TotalStock)
				throw new InvalidOperationException("Cannot release more stock than was originally available.");

			AvailableStock += quantity;
			UpdatedAt=DateTimeOffset.UtcNow;
		}
	}
}
