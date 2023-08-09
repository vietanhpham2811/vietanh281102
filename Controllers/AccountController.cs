using Microsoft.AspNetCore.Mvc;
//su dung doi tuong thao tac IFormCollection
using Microsoft.AspNetCore.Http;
//nhin thay cac file .cs ben trong folder Models
using QlBanOpDaDienThoai.Models;
//su dung entity framework
using Microsoft.EntityFrameworkCore;
//phan trang
using X.PagedList;
//nhin thay file CheckLogin.cs tron thu muc Attributes
using QlBanOpDaDienThoai.Areas.Admin.Attributes;
//doi tuong thao tac file
using System.IO;
//su dung kieu List
using System.Collections.Generic;
//doi tuong ma hoa password
using BC = BCrypt.Net.BCrypt;

namespace QlBanOpDaDienThoai.Controllers
{


    public class AccountController : Controller
    {
        Net20ProjectContext db = new Net20ProjectContext();
        public IActionResult login()
        {

            return View();
        }
        [HttpPost]
        public IActionResult loginpost(IFormCollection fc)
        {
            string email = fc["email"].ToString().Trim();
            string password = fc["password"].ToString().Trim();
            Customer record = db.Customers.Where(item => item.Email == email).FirstOrDefault();
            if (record != null && BC.Verify(password, record.Password) == true)
            {

                HttpContext.Session.SetString("customer_name", record.Name.ToString());

                HttpContext.Session.SetString("customer_email", record.Email.ToString());
                HttpContext.Session.SetString("customer_id", record.Id.ToString());


            }
            else
            {
                return Redirect("/Account/Login?notify=login-invalid");

            }
            ViewBag.name = record.Name;


            return Redirect("/home/index");

        }
        public IActionResult logout()
        {
            HttpContext.Session.Remove("customer_name");

            HttpContext.Session.Remove("customer_email");
            HttpContext.Session.Remove("customer_id");
            return Redirect("/Account/Login");  
        }
    }
}
