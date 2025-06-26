using System;
namespace SEKERHOTELS.Models
{
	public class RoomsModel
	{
		public int Id { get; set; }
		public int HotelId { get; set; }
		public string RoomNumber { get; set; }
		public int Capacity { get; set; }
		public decimal PricePerNight { get; set; }

	}
}

