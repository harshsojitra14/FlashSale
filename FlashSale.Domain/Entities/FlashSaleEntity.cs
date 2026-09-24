using FlashSale.Domain.Enums;

namespace FlashSale.Domain.Entities
{
	public class FlashSaleEntity
	{
		public Guid Id{ get; private set; }
		public Guid ProductId{  get; private set; }
		public Product Product { get; private set; } = null!;
		public decimal SalePrice{  get; private set; }
		public DateTimeOffset StartTime { get; private set; }
		public DateTimeOffset EndTime { get; private set; }
		public DateTimeOffset CreatedAt { get; private set; }
		public DateTimeOffset UpdatedAt { get; private set; }
		public FlashSaleStatus Status{ get; private set; }
		public ICollection<Reservation> Reservations { get; private set; }= new List<Reservation>();
		public ICollection<Order> Orders { get; private set; }=new List<Order>();
		public FlashSaleEntity(Guid productId, decimal salePrice, DateTimeOffset startTime, DateTimeOffset endTime)
		{
			if (productId == Guid.Empty)
			{
				throw new ArgumentException("ProductId Is Required.");
			}

			if (salePrice <= 0)
			{
				throw new ArgumentException("Sale price must be greater than zero.");
			}

			if (startTime >= endTime)
			{
				throw new ArgumentException("Start time must be before end time.");
			}

			Id = Guid.NewGuid();
			ProductId = productId;
			SalePrice = salePrice;
			Status= FlashSaleStatus.Scheduled;
			StartTime = startTime;
			EndTime = endTime;
			CreatedAt = DateTimeOffset.UtcNow;
			UpdatedAt = CreatedAt;
		}

		public void Activate()
		{
			if (Status != FlashSaleStatus.Scheduled)
				throw new InvalidOperationException("Sale Is Must Be Scheduled Before Activate the Sale");

			Status= FlashSaleStatus.Active;
			UpdatedAt = DateTimeOffset.UtcNow;
		}

		public void End()
		{
			if (Status != FlashSaleStatus.Active)
				throw new InvalidOperationException("Sale Is Must Be Activate Before Ending the Sale");

			Status = FlashSaleStatus.Ended;
			UpdatedAt = DateTimeOffset.UtcNow;
		}

		public void Cancel()
		{
			if (Status != FlashSaleStatus.Scheduled)
				throw new InvalidOperationException("Sale Is Must Be Scheduled Before Cancelled the Sale");

			Status = FlashSaleStatus.Cancelled;
			UpdatedAt = DateTimeOffset.UtcNow;
		}
	}
}
