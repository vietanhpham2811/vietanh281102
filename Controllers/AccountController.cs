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
        public IActionResult Register()
        {
            return View();
        }
        //khi an nut dang ky
        [HttpPost]
        public IActionResult RegisterPost(IFormCollection fc)
        {
            string _name = fc["name"];
            string _email = fc["email"];
            string _phone = fc["phone"];
            string _address = fc["address"];
            string _password = fc["password"];
            //ma hoa password
            _password = BC.HashPassword(_password);
            //kiem tra xem email da ton tai trong table customers chua, neu chua thi moi cho insert du lieu vao
            int checkMail = db.Customers.Where(item => item.Email == _email).Count();
            if (checkMail == 0)
            {
                Customer record = new Customer();
                record.Name = _name;
                record.Email = _email;
                record.Phone = _phone;
                record.Address = _address;
                record.Password = _password;
                //---
                db.Customers.Add(record);
                db.SaveChanges();
            }
            else
            {
                return Redirect("/Account/Register?notify=exists");
            }
            return Redirect("/Account/Login?notify=register-success");
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
