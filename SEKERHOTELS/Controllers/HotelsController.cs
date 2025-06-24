using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEKERHOTELS.Models;
using Dapper;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEKERHOTELS.Controllers
{
    public class HotelsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(DP.Listeleme<HotelsModel>("GetAllHotels"));
        }


        public IActionResult CreateAndUpdate(int id = 0)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", id);
                return View(DP.Listeleme<HotelsModel>("GetHotelsId", param).FirstOrDefault());

            }

        }

        [HttpPost]
        public IActionResult CreateAndUpdate(HotelsModel hotels)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@Id", hotels.Id);
            param.Add("@Name", hotels.Name);
            param.Add("@StarCount", hotels.StarCount);
            param.Add("@City", hotels.City);

            DP.ExecuteReturn("HotelsAddUpdate", param);
            return RedirectToAction("Index");

        }


        public IActionResult Delete(int id = 0)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@Id", id);
            DP.ExecuteReturn("HotelsDelete", param);

            return RedirectToAction("Index");
        }
    }

    
}


