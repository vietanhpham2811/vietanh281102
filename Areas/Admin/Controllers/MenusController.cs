using Microsoft.AspNetCore.Mvc;
using System.IO; //thao tac voi file, thu muc
using Newtonsoft.Json;//thao tac voi file json
using System.Data;//su dung DataTalbe, DataRow
using System.Data.SqlClient;//su dung SqlConnection, DataAdapter...
using X.PagedList;//su dung cac ham phan trang
using BC = BCrypt.Net;//doi tuong ma hoa csdl, gan doi tuong nay thanh BC
using QlBanOpDaDienThoai.Models;//nhan dien cac file trong thu muc Models
using System.Security.Cryptography;

namespace QlBanOpDaDienThoai.Areas.Admin.Controllers
{
    [Area("admin")]

    public class MenusController : Controller
    {
        Net20ProjectContext db =new Net20ProjectContext();
        public IActionResult Index(int?p)
        {
            int pageSize = 10;
            int pageNumber = p ?? 1;
            List<Menu> list = db.Menus.OrderByDescending(item => item.Arrange).ToList();
            return View(list.ToPagedList(pageNumber,pageSize));
        }
        public IActionResult Create()
        {
            ViewBag.action = "/admin/Menus/CreatePost";
            return View("FormCreateUpdate");
        }
        public IActionResult CreatePost(IFormCollection fc)
        {
            Menu record = new Menu();
            string name = fc["name"].ToString().Trim();   
            string controller_name = fc["controller_name"].ToString().Trim();   
            string link = fc["link"].ToString().Trim();
            record.Name = name;
            record.ControllerName = controller_name;
            record.Link= link;  
            db.Menus.Add(record);   
            db.SaveChanges();
            int idInsert = record.Id;
            Menu recordInsert=db.Menus.Where(item=>item.Id==idInsert).FirstOrDefault();
            recordInsert.Arrange = idInsert;
            db.SaveChanges();
            return RedirectToAction("Index","Menus");

        }
    }
}
