using Dapper;
using Microsoft.AspNetCore.Mvc;
using SEKERHOTELS.Models;

namespace SEKERHOTELS.Controllers
{
    public class ReservationsReportController : Controller
    {
        public IActionResult Index(
            string CustomerName, 
            string HotelName, 
            string RoomNumber, 
            DateTime CheckInDate,
            DateTime CheckOutDate,
            int TotalNights,
            decimal PricePerNight,
            decimal TotalPrice)
        {

            DynamicParameters param = new DynamicParameters();

            param.Add("CustomerName", CustomerName);
            param.Add("HotelName", HotelName);
            param.Add("RoomNumber", RoomNumber);
            param.Add("CheckInDate", CheckInDate == default(DateTime) ? null : CheckInDate);
            param.Add("CheckOutDate", CheckOutDate == default(DateTime) ? null : CheckOutDate);
            param.Add("TotalNights", (CheckInDate - CheckOutDate).Days);
            param.Add("PricePerNights", PricePerNight);
            param.Add("TotalPrice", (TotalNights * PricePerNight));

            var listReservationsReport = DP.Listeleme<ReservationsReportViewModel>("ReservationsReport", param);
            return View(listReservationsReport);
        }
    }
}
