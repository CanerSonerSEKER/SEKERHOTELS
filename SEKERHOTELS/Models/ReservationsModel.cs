	using System;


	namespace SEKERHOTELS.Models
	{
		public class ReservationsModel
		{
			public int Id { get; set; }
			public int CustomerId { get; set; }
			public int RoomId { get; set; }
			public DateTime CheckInDate { get; set; }
			public DateTime CheckOutDate { get; set; }
		}
	}

