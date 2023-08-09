using Microsoft.AspNetCore.Mvc;
using QlBanOpDaDienThoai.Models;

namespace QlBanOpDaDienThoai.Controllers
{
    public class CartController : Controller
    {
        Net20ProjectContext db =new Net20ProjectContext();
        public IActionResult Index()
        {
            List<Item> cart = Cart.GetCart(HttpContext.Session);
            if (cart != null)
            {
                ViewBag.cart = cart;
            }
            return View();
        }
        public IActionResult Buy(int id)
        {
            Cart.CartAdd(HttpContext.Session, id);
            return RedirectToAction("Index");

        }
        public IActionResult remove(int id)
        {
            Cart.CartRemove(HttpContext.Session, id);
            return RedirectToAction("Index");

        }
        public IActionResult Destroy()
        {
            Cart.CartDestroy(HttpContext.Session);
            return RedirectToAction("Index");

        }
        public IActionResult Update()
        {
            List<Item> cart = Cart.GetCart(HttpContext.Session);
            foreach(var item in cart)
            {
                int quantity = Convert.ToInt32(Request.Form["product_" + item.ProductRecord.Id]);
                Cart.CartUpdate(HttpContext.Session, item.ProductRecord.Id, quantity);

            }
            return RedirectToAction("Index");

        }
        public IActionResult CheckOut()
        {
            //kiểm tra xem người dùng đã đăng nhập chưa
            string customer_id = HttpContext.Session.GetString("customer_id");
            if (!String.IsNullOrEmpty(customer_id))
            {
                Cart.CartCheckOut(HttpContext.Session, Convert.ToInt32(customer_id));
            }
            else
                return Redirect("/Account/Login");
            return Redirect("/Home");
        }
    }
}
