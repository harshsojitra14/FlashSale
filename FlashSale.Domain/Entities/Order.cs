using FlashSale.Domain.Enums;

namespace FlashSale.Domain.Entities
{
	public class Order
	{
		public Guid Id{  get; private set; }
		public Guid UserId { get; private set; }
		public Guid ReservationId { get; private set; }
		public Guid ProductId { get; private set; }
		public Guid FlashSaleId { get; private set; }
		public int Quantity{  get; private set; }
		public decimal UnitPrice{  get; private set; }
		public decimal TotalAmount{  get; private set; }
		public OrderStatus Status{ get; private set; }
		public DateTimeOffset CreatedAt{  get; private set; }
		public DateTimeOffset UpdatedAt{ get; private set; }
		public Reservation Reservation { get; private set; } = null!;
		public User User { get; private set; } = null!;
		public Product Product { get; private set; } = null!;
		public FlashSaleEntity FlashSaleEntity { get; private set; } = null!;

		public Order(Guid userId,Guid reservationId,Guid productId,Guid flashSaleId,int quantity,decimal unitPrice)
		{
			if (productId == Guid.Empty || flashSaleId == Guid.Empty || userId == Guid.Empty || reservationId == Guid.Empty)
			{
				throw new ArgumentException("Every Id Is Required.");
			}

			if (quantity <= 0)
				throw new ArgumentException("Quantity must be greater than 0.");

			if (unitPrice <= 0)
				throw new ArgumentException("UnitPrice must be greater than 0");

			Id = Guid.NewGuid();
			UserId= userId;
			ReservationId = reservationId;
			ProductId= productId;
			FlashSaleId= flashSaleId;
			Quantity= quantity;
			UnitPrice= unitPrice;
			TotalAmount = unitPrice * quantity;
			Status = OrderStatus.Pending;
			CreatedAt = DateTimeOffset.UtcNow;
			UpdatedAt = CreatedAt;
		}

		public void Confirm()
		{
			if (Status != OrderStatus.Pending)
				throw new InvalidOperationException("Only a pending order can be confirmed");

			Status = OrderStatus.Confirmed;
			UpdatedAt= DateTimeOffset.UtcNow;
		}

		public void Cancel()
		{
			if (Status != OrderStatus.Pending)
				throw new InvalidOperationException("Only a pending order can be cancelled");

			Status = OrderStatus.Cancelled;
			UpdatedAt = DateTimeOffset.UtcNow;
		}
	}
}
