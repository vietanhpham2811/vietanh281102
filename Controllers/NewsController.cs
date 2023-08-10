using Microsoft.AspNetCore.Mvc;

namespace QlBanOpDaDienThoai.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
