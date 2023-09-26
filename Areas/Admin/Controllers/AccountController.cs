using Microsoft.AspNetCore.Mvc;
using QlBanOpDaDienThoai.Models;

namespace QlBanOpDaDienThoai.Areas.Admin.Controllers
{
    [Area("admin")]
    
    public class AccountController : Controller
    {
        public Net20ProjectContext db= new Net20ProjectContext();
       
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoginPost(IFormCollection fc)
        {
            string email = fc["email"].ToString().Trim();
            string password = fc["password"].ToString().Trim();
            User record = db.Users.Where(item => item.Email == email).FirstOrDefault();
            int idRecord = record.Id;
            if (record != null) {
                if (record.Password==password)
                {
                    //Tạo session email
                    HttpContext.Session.SetString("email", email);
                    HttpContext.Session.SetInt32("id",idRecord);
                

                    //đăng nhập thành công
                    return Redirect("/Admin/Home/index");
                }
                

            } return Redirect("/admin/account/login?notify=login-fail");


           
        }
        public IActionResult logout()
        {
            HttpContext.Session.Remove("email");    
            

            return Redirect("/admin/account/login");
        }
    }
}
