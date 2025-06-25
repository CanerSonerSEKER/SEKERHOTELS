using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using SEKERHOTELS.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEKERHOTELS.Controllers
{
    public class CustomersController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {

            return View(DP.Listeleme<CustomersModel>("GetAllCustomers"));
        }

        public IActionResult CreateAndUpdate(int id = 0)
        {
            if (id == 0)
            {
                CustomersModel customer = new CustomersModel();
                return View(customer);
            }
            else
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Id", id);
                return View(DP.Listeleme<CustomersModel>("GetCustomersId", param).FirstOrDefault());
            }
        }

        [HttpPost]
        public IActionResult CreateAndUpdate(CustomersModel customer)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@Id", customer.Id);
            param.Add("@FullName", customer.FullName);
            param.Add("@Email", customer.Email);
            param.Add("@Phone", customer.Phone);

            DP.ExecuteReturn("CustomersAddUpdate", param);
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id = 0)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@Id", id);
            DP.ExecuteReturn("CustomersDelete", param);
            return RedirectToAction("Index");
        }
    }
}

