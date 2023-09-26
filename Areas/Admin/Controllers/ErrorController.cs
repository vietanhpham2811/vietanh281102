using Microsoft.AspNetCore.Mvc;
using QlBanOpDaDienThoai.Areas.Admin.Attributes;

namespace QlBanOpDaDienThoai.Areas.Admin.Controllers
{
    [Area("admin")]
    [CheckLogin]
    public class ErrorController : Controller
    {
        public IActionResult errorPermission()
        {
            return View();
        }
    }
}
