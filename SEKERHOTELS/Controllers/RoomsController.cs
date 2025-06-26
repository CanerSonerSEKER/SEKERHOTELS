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
    public class RoomsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var indexNames = DP.Listeleme<dynamic>("GetListNameRooms");
            return View(indexNames);
        }


        public IActionResult CreateAndUpdate(int id = 0)
        {

            var hotelName = DP.Listeleme<HotelsModel>("GetAllHotels");
            ViewBag.HotelsName = new SelectList(hotelName, "Id", "Name");


            if (id == 0 )
            {
                RoomsModel createPage = new RoomsModel();
                return View(createPage);
            }
            else
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", id);
                return View(DP.Listeleme<RoomsModel>("GetRoomsId", param).FirstOrDefault());
            }
        }

        [HttpPost]
        public IActionResult CreateAndUpdate(RoomsModel rooms)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@Id", rooms.Id);
            param.Add("@HotelId", rooms.HotelId);
            param.Add("@RoomNumber", rooms.RoomNumber);
            param.Add("@Capacity", rooms.Capacity);
            param.Add("@PricePerNight", rooms.PricePerNight);

            DP.ExecuteReturn("RoomsAddUpdate", param);
            return RedirectToAction("Index");
        }


    }
}

