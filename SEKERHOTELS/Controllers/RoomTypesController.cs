using Microsoft.AspNetCore.Mvc;
using Dapper;
using SEKERHOTELS.Models;

namespace SEKERHOTELS.Controllers
{
    public class RoomTypesController : Controller
    {
        public IActionResult Index()
        {
            return View(DP.Listeleme<RoomTypesModel>("GetAllRoomTypes"));
        }


        public IActionResult CreateAndUpdate(int id = 0)
        {
            if (id == 0)
            {
                var model = new RoomTypesModel();
                return View(model);
            }
            else
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", id);
                return View(DP.Listeleme<RoomTypesModel>("GetRoomTypesId", param).FirstOrDefault());
            }
        }

        [HttpPost]
        public IActionResult CreateAndUpdate(RoomTypesModel roomTypes)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@Id", roomTypes.Id);
            param.Add("@Name", roomTypes.Name);
            param.Add("@Description", roomTypes.Description);

            DP.ExecuteReturn("TypesAddUpdate", param);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id = 0)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id", id);
            DP.ExecuteReturn("RoomTypeDelete", param);
            return RedirectToAction("Index");
        }
    }
}
