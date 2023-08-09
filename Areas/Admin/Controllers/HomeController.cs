using Microsoft.AspNetCore.Mvc;
using QlBanOpDaDienThoai.Areas.Admin.Attributes;

namespace QlBanOpDaDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    [CheckLogin]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
