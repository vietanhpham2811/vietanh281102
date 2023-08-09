using Microsoft.AspNetCore.Mvc;
//nhìn thấy các file trong thư mục Models
using QlBanOpDaDienThoai.Models;
//nhìn thấy các file trong thư mục Attributes
//thư viện mã hóa password
using BC = BCrypt.Net.BCrypt;
//thư viện phân trang
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using QlBanOpDaDienThoai.Areas.Admin.Attributes;

namespace QlBanOpDaDienThoai.Areas.Admin.Controllers
{

    [Area("admin")]
    [CheckLogin]
    public class UsersController : Controller
    {
        public Net20ProjectContext db = new Net20ProjectContext();

        public IActionResult Index(int? p)
        {

            int pageNumber = p ?? 1;
            int pageSize = 4;
            List<User> list = db.Users.OrderByDescending(item => item.Id).ToList();
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        public IActionResult Update(int? id)
        {
            User record = db.Users.Where(item => item.Id == id).FirstOrDefault();
            ViewBag.action = "/admin/users/updatepost/" + id;
            return View("FormCreateUpdate", record);

        }
        [HttpPost]
        public IActionResult UpdatePost(int? id, IFormCollection fc)
        {
            User record = db.Users.Where(item => item.Id == id).FirstOrDefault();
            string email = fc["email"].ToString().Trim();

            string name = fc["name"].ToString().Trim();
            string password = fc["password"].ToString().Trim();
            record.Name = name;
            record.Email = email;
            if (!String.IsNullOrEmpty(password)) { record.Password = password; }
            db.Users.Update(record);
            db.SaveChanges();
            return RedirectToAction("index", "users");

        }
        public IActionResult Delete(int? id)
        {
            User record = db.Users.Where(item => item.Id == id).FirstOrDefault();
            db.Users.Remove(record);
            db.SaveChanges();
            return RedirectToAction("index", "users");
        }
        public IActionResult create()
        {
            ViewBag.action = "/admin/users/createpost";
            return View("formcreateupdate");
        }
        [HttpPost]
        public IActionResult CreatePost(IFormCollection fc)
        {
            User record = new User();
            string name = fc["name"].ToString().Trim();
            string email = fc["email"].ToString().Trim();
            string password = fc["password"].ToString().Trim();

            bool emailexits = db.Users.Any(item => item.Email == email);
            if (emailexits)
            {
                ViewBag.errormessage = "Email đã tồn tại vui lòng nhập email khác!";
                ViewBag.Action = "/admin/users/createpost";
                return View("formcreateupdate");
            }
            else { record.Email = email; }
            record.Name = name;
            record.Password = password;
            db.Users.Add(record);
            db.SaveChanges();

            return RedirectToAction("index", "users");

        }
    }
}
