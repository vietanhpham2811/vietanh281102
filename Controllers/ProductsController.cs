using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Templating;
using QlBanOpDaDienThoai.Models;
using X.PagedList;

namespace QlBanOpDaDienThoai.Controllers
{
    public class ProductsController : Controller
    {
        public Net20ProjectContext db = new Net20ProjectContext();
        public IActionResult Category(int? id, int? p)
        {
            int category_id = id ?? 0;
            //truyền biến category_id ra ngoài view thông qua ViewBag
            ViewBag.category_id = category_id;
            //liệt kê các sản phẩm (nếu có id truyền vào thì sẽ liệt kê danh mục theo category_id, nếu không thì liệt kê các sản phẩm)
            // quy định số bản ghi trên một trang
            int pageSize = 9;
            //tính trang hiện tại      
            int pageNumber = p ?? 1;
            List<Product> products = new List<Product>();
            //nếu có id truyền vào
            if (category_id > 0)
                products = (from product in db.Products
                            join category_product in db.CategoriesProducts
                          on product.Id equals category_product.ProductId
                            join category in db.Categories
                          on category_product.CategoryId equals category.Id
                            where category.Id == category_id
                            select product).ToList();
            else
            {
                products = (from product in db.Products orderby product.Id descending select product).ToList();
            }
            //---
            //orderby
            string orderby = !String.IsNullOrEmpty(Request.Query["orderby"]) ? Request.Query["orderby"] : "";
            switch (orderby)
            {
                case "name-asc":
                    products = db.Products.OrderBy(item => item.Name).ToList();
                    break;
                case "name-desc":
                    products = db.Products.OrderByDescending(item => item.Name).ToList();
                    break;
                case "price-asc":
                    products=db.Products.OrderBy(item=>item.Price).ToList();    
                    break;
                case "price-desc":
                    products = db.Products.OrderByDescending(item => item.Name).ToList();
                    break;
                case "discount-desc":
                    products = db.Products.OrderByDescending(item => item.Discount).ToList();
                    break;

            }
            //---
            //gọi view có phân trang
            return View(products.ToPagedList(pageNumber, pageSize));
        }
        public IActionResult detail(int? id)
        {
            id = id ?? 0;//để loại bỏ trường hợp id null;
            
            Product record = db.Products.Where(item => item.Id == id).FirstOrDefault();
          
            return View(record);
        }
        public IActionResult Rating(int? id, int? star)
        {
            id = id ?? 0;
            Rating record = new Rating();
            record.ProductId = (int)id;
            record.Star = star;
            db.Ratings.Add(record);
            db.SaveChanges();
            return Redirect("/Products/Detail/" + id);
        }
    }
}
