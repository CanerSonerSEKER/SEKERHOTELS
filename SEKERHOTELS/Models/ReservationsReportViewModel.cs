namespace SEKERHOTELS.Models
{
    public class ReservationsReportViewModel
    {

        public string CustomerName { get; set; }

        public string HotelName { get; set; }

        public string RoomNumber { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public int TotalNights
        {
            get
            {
                return (CheckOutDate - CheckInDate).Days;
            }
        }

        public decimal PricePerNight{ get; set; }

        public decimal TotalPrice { get => PricePerNight * TotalNights; }
    }
}
