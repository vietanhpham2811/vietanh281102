using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;//doc thong tin trong file appsetting.json
using QlBanOpDaDienThoai.Models; //nhin thay cac file .cs trong folder Models
using System.Data;//su dung DataTable
using System.Data.SqlClient; //su dung cho doi tuong SqlConnection,SqlDataAdapter, SqlCommand
using X.PagedList; //phan trang
using Microsoft.AspNetCore.Http;//su dung IFormCollection
using QlBanOpDaDienThoai.Areas.Admin.Attributes; //nhin thay cac file .cs trong folder Attributes
using System.IO;//doi tuong thao tac voi file, folder
//doi tuong ma hoa password
using BC = BCrypt.Net.BCrypt;
using Microsoft.Extensions.Logging.Abstractions;

namespace QlBanOpDaDienThoai.Areas.Admin.Controllers
{[Area("admin")]
        [CheckLogin]
    public class OrdersController : Controller
    {
        public Net20ProjectContext db =new Net20ProjectContext();   

        public IActionResult Index(int? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 10;
            List<Order> list_record = db.Orders.OrderByDescending(item=>item.Id).ToList();  

            return View(list_record.ToPagedList(pageNumber,pageSize));
        }
        public IActionResult Detail(int? id)
        {
            ViewBag.OrderId = id;
            List<OrdersDetail> detail = db.OrdersDetails.Where(item =>item.OrderId == id).ToList();   
            return View("Detail",detail);  
        }
        public IActionResult Delivery(int? id) {

            Order record=db.Orders.Where(item=>item.Id==id).FirstOrDefault();
            if (record != null)
            {
                record.Status = 1;
                db.SaveChanges();

            }
            return Redirect("/Admin/Orders");

        }

    }
}
