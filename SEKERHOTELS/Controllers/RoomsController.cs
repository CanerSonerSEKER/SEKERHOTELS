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


//// @section Scripts {
//< script >$(document).ready(function() {
//// Modal içindeki arama butonu
//$('#searchButton').click(function() {
//        performSearch();
//    });

//// Enter tuşu ile arama
//$('#searchQuery').keypress(function(e) {
//        if (e.which === 13)
//        {
//            performSearch();
//        }
//    });

//    function performSearch()
//    {
//        var query = $('#searchQuery').val().toLowerCase().trim();
//        var $rows = $('#hotelsTableBody tr[data-hotel-name]');
//        var foundCount = 0;

//        if (query.length === 0)
//        {
//// Eğer arama kutusu boşsa tüm otelleri göster
//$rows.show();
//$('#searchResults').html('<div class="alert alert-info">Tüm oteller listeleniyor</div>');
//            return;
//        }

//$rows.each(function() {
//            var hotelName = $(this).data('hotel-name');
//            if (hotelName.includes(query))
//            {
//$(this).show();
//                foundCount++;
//            }
//            else
//            {
//$(this).hide();
//            }
//        });

//    // Arama sonuçlarını göster
//    if (foundCount > 0)
//    {
//$('#searchResults').html(`< div class= "alert alert-success" >
//< i class= "fas fa-check-circle me-2" ></ i >
//"${query}" için ${foundCount} sonuç bulundu.
//</ div >`);
//} else
//{
//$('#searchResults').html(`< div class= "alert alert-danger" >
//< i class= "fas fa-exclamation-circle me-2" ></ i >
//"${query}" ile eşleşen otel bulunamadı.
//</div>`);
//}
//}

//// Modal açıldığında arama kutusuna odaklan
//$('#searchModal').on('shown.bs.modal', function() {
//$('#searchQuery').focus();
//});
//});</ script >
//}
//< button type = "button" class= "btn btn-primary btn-lg me-2" data - bs - toggle = "modal" data - bs - target = "#searchModal" >
//< i class= "fas fa-search me-2" ></ i > Otel Ara
//</ button >
//LAYOUTA EKLENECEK

//<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
//<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
//<script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.3.0/js/bootstrap.bundle.min.js"></script>



