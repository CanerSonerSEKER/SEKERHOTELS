using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using SEKERHOTELS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEKERHOTELS.Controllers
{
    public class ReservationsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var indexList = DP.Listeleme<dynamic>("GetListWithName");
            return View(indexList);
            //return View(DP.Listeleme<ReservationsModel>("GetAllReservations"));
        }


        public IActionResult CreateAndUpdate(int id = 0)
        {
            var customerName = DP.Listeleme<CustomersModel>("GetAllCustomers");
            ViewBag.CustomerName = new SelectList(customerName, "Id", "FullName");

            var roomName = DP.Listeleme<RoomsModel>("GetAllRooms");
            ViewBag.RoomName = new SelectList(roomName, "Id", "RoomNumber");

            if (id == 0 )
            {
                ReservationsModel reservation = new ReservationsModel();
                return View(reservation);
            }
            else
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", id);
                return View(DP.Listeleme<ReservationsModel>("GetReservationsId", param).FirstOrDefault());
            }
        }

        [HttpPost]
        public IActionResult CreateAndUpdate(ReservationsModel reservations)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@Id", reservations.Id);
            param.Add("@CustomerId", reservations.CustomerId);
            param.Add("@RoomId", reservations.RoomId);
            param.Add("@CheckInDate", reservations.CheckInDate);
            param.Add("@CheckOutDate", reservations.CheckOutDate);

            DP.ExecuteReturn("ReservationsAddUpdate", param);
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id = 0)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@Id", id);

            DP.ExecuteReturn("ReservationsDelete", param);
            return RedirectToAction("Index");
        }



    }
}

