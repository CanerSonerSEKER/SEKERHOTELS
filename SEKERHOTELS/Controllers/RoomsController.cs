using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using SEKERHOTELS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Metadata;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEKERHOTELS.Controllers
{
    public class RoomsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index(string name, int roomNum, int capacity, int price)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@Name", string.IsNullOrWhiteSpace(name) ? null : name);
            param.Add("@RoomNumber", string.IsNullOrWhiteSpace(roomNum.ToString()) ? null : roomNum);
            param.Add("@Capacity", capacity == 0 ? null : capacity);
            param.Add("@PricePerNight", price == 0 ? null : price);

            var listWithSearch = DP.Listeleme<dynamic>("SearchingRooms", param);


            //var indexNames = DP.Listeleme<dynamic>("GetListNameRooms");
            return View(listWithSearch);
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




/* -- Salih prosedür kısmı arama çubuğu 
ALTER PROCEDURE [dbo].[PersonelAdIdArama]
@AdSoyad NVARCHAR(200) = NULL,
@DepartmanID INT = NULL
AS
BEGIN
SELECT p.PersonelID,
p.AdSoyad,
d.DepartmanAd,
z.PozisyonAd
FROM Personeller p
LEFT JOIN Departmanlar d ON d.DepartmanID = p.DepartmanID
LEFT JOIN Pozisyonlar z ON z.PozisyonID = p.PozisyonID
WHERE (@AdSoyad IS NULL OR p.AdSoyad LIKE '%' + @AdSoyad + '%')
AND (@DepartmanID IS NULL OR p.DepartmanID = @DepartmanID)
END
GO

 -- Kod Kısmı

public IActionResult Index(string adSoyad, int? departmanId)
{
ViewBag.Departmanlar = DP.Listeleme<Departmanlar>("DepartmanViewAll").ToList();

var param = new DynamicParameters();
param.Add("@AdSoyad", string.IsNullOrWhiteSpace(adSoyad) ? null : adSoyad);
param.Add("@DepartmanID", departmanId);

var personelListe = DP.Listeleme<dynamic>("PersonelAdIdDüzenleme", param);
return View(personelListe);
}*/

