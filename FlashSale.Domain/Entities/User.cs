
using FlashSale.Domain.Enums;

namespace FlashSale.Domain.Entities
{
	public class User
	{
		public Guid Id { get; private set; }
		public string UserEmail{  get; private set; }=string.Empty;
		public string Password { get; private set; }= string.Empty;
		public UserRole Role{ get; private set; }
		public ICollection<Reservation> Reservations{  get; private set; } = new List<Reservation>();
		public ICollection<Order> Orders{ get; private set; }=new List<Order>();

		private User()
		{
			
		}

		public User(string useremail,string password)
		{
			if (string.IsNullOrWhiteSpace(useremail))
			{
				throw new ArgumentException("Email must not be null/empty/whitespace.");
			}

			if (string.IsNullOrWhiteSpace(password))
			{
				throw new ArgumentException("Password hash is required.");
			}

			Id = Guid.NewGuid();
			UserEmail= useremail;
			Password= password;
			Role= UserRole.Customer;
		}
	}
}
