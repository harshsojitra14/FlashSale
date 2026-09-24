
using FlashSale.Domain.Enums;

namespace FlashSale.Domain.Entities
{
	public class Reservation
	{
		public Guid Id{  get; private set; }
		public Guid UserId{ get; private set; }
		public User User { get; private set; } = null!;
		public Product Product { get; private set; } = null!;
		public FlashSaleEntity FlashSaleEntity { get; private set; }=null!;
		public Order? Order { get; private set; }
		public Guid ProductId { get; private set; }
		public Guid FlashSaleId { get; private set; }
		public int Quantity{  get; private set; }
		public ReservationStatus Status{ get; private set; }
		public DateTimeOffset CreatedAt{  get; private set; }
		public DateTimeOffset UpdatedAt { get; private set; }
		public DateTimeOffset ExpiresAt { get; private set; }

		public Reservation(Guid userId,Guid productId,Guid flashSaleId,int quantity,DateTimeOffset expiresAt)
		{
			if (productId == Guid.Empty || flashSaleId == Guid.Empty || userId == Guid.Empty)
			{
				throw new ArgumentException("Every Id Is Required.");
			}
			if (quantity <= 0)
				throw new ArgumentException("Quantity must be greater than 0.");

			if (expiresAt <= DateTimeOffset.UtcNow)
				throw new ArgumentException("A new reservation must have an expiration time in the future.");


			Id =Guid.NewGuid();
			UserId = userId;
			ProductId = productId;
			FlashSaleId = flashSaleId;
			Quantity = quantity;
			Status = ReservationStatus.Reserved;
			CreatedAt = DateTimeOffset.UtcNow;
			UpdatedAt = CreatedAt;
			ExpiresAt = expiresAt;
		}

		public void Confirm()
		{
			if (Status != ReservationStatus.Reserved)
				throw new InvalidOperationException("Only a reserved Product can be Confirmed.");

			Status = ReservationStatus.Confirmed;
			UpdatedAt = DateTimeOffset.UtcNow;
		}

		public void Cancel()
		{
			if (Status != ReservationStatus.Reserved)
				throw new InvalidOperationException("Only a reserved Product can be Cancel.");

			Status = ReservationStatus.Cancelled;
			UpdatedAt = DateTimeOffset.UtcNow;
		}

		public void Expire()
		{
			if (Status != ReservationStatus.Reserved)
				throw new InvalidOperationException("Only a reserved Product can be Expired.");
			
			Status = ReservationStatus.Expired;
			UpdatedAt = DateTimeOffset.UtcNow;
		}
	}
}
