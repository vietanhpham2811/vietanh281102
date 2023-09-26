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

        public readonly Net20ProjectContext db;

        public UsersController()
        {
            db = new Net20ProjectContext();
        }
        //public bool CheckPermissions(int idPermission)
        //{
        //    int idUserSession = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
        //    var count = db.Permissions.Count(item => item.IdUser == idUserSession && item.IdFuntion == idPermission);
        //    if (count == 0)
        //    {
        //        //báo không có quyền
        //        return false;


        //    }
        //    else
        //    {
        //        return true;
        //    }

        //}
        [AdminAuthorize(idFunction = 1)]

        public IActionResult Index(int? p)

        {
            // if(CheckPermissions(1)==false) {
            //    return Redirect("/admin/error/khongcoquyen");
            //}

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
        [AdminAuthorize(idFunction = 3)]

        public IActionResult UpdatePost(int id, IFormCollection fc)
        {
            try
            {


                User record = db.Users.Where(item => item.Id == id).FirstOrDefault();
                string email = fc["email"].ToString().Trim();
                string[] permission = fc["Permission"];
                string name = fc["name"].ToString().Trim();
                string password = fc["password"].ToString().Trim();
                record.Name = name;
                record.Email = email;

                var lstPer = db.Permissions.Where(item=>item.IdUser==id).ToList();
                // Lấy danh sách quyền hiện có nếu có
                //List<Permissions> existingPermissions = db.Permissions.Where(item => item.IdUser == id).ToList();

                //if (existingPermissions != null && existingPermissions.Count > 0)
                //{
                //    // Xóa tất cả các quyền hiện có của người dùng
                //    db.Permissions.RemoveRange(existingPermissions);
                //}

                foreach (var item in lstPer)
                {
                    db.Permissions.Remove(item);
                    db.SaveChanges();
                }
                //List<Permissions> existingPermissions = db.Permissions.Where(item => item.IdUser == id).ToList();
                //db.Permissions.RemoveRange(existingPermissions);
                foreach (string item in permission)
                {

                    Permissions newrecordper = new Permissions();
                    newrecordper.IdUser = record.Id;
                    newrecordper.IdFunction = Convert.ToInt32(item);
                    db.Permissions.Add(newrecordper);

                    db.SaveChanges();

                }

                if (!String.IsNullOrEmpty(password)) { record.Password = password; }


                db.Users.Update(record);
                db.SaveChanges();
                return RedirectToAction("index", "users");
            }
            catch (Exception)
            {
                throw;
            }
        }
        [AdminAuthorize(idFunction = 4)]

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
        [AdminAuthorize(idFunction = 2)]

        public IActionResult CreatePost(IFormCollection fc)
        {

            User record = new User();
            string name = fc["name"].ToString().Trim();
            string email = fc["email"].ToString().Trim();
            string password = fc["password"].ToString().Trim();
            string[] permission = fc["Permission"];
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
