using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using QlBanOpDaDienThoai.Areas.Admin.Attributes;
using QlBanOpDaDienThoai.Models;
using System.IO;
using X.PagedList;

namespace QlBanOpDaDienThoai.Areas.Admin.Controllers
{
    [CheckLogin]
    [Area("admin")]


    public class ProductsController : Controller
    {
        Net20ProjectContext db = new Net20ProjectContext();
        public IActionResult Index(int? p)
        {
            int pageNumber = p ?? 1;
            int pageSize = 8;
            List<Product> list = db.Products.OrderByDescending(item => item.Id).ToList();
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        public IActionResult SearchProducts(int? p) {
            string key = Request.Query["key"];
            ViewBag.key = key;
            int pageNumber = p ?? 1;
            int pageSize = 8;
            List<Product> list=db.Products.Where(item=>item.Name.Contains(key)).ToList();
            return View(list.ToPagedList(pageNumber, pageSize));


        }

        public IActionResult Update(int? id)
        {
            Product record = db.Products.Where(item => item.Id == id).FirstOrDefault();
            ViewBag.action = "/admin/products/updatepost/" + id;
            return View("FormCreateUpdate", record);

        }
        [HttpPost]
        public IActionResult UpdatePost(int? id, IFormCollection fc)
        {
            Product record = db.Products.Where(item => item.Id == id).FirstOrDefault();
            string name = fc["name"].ToString().Trim();
            string[] lstcategories = fc["category"];
            string[] lsttags = fc["tag"];
            double price = Convert.ToDouble(fc["price"]);
            double discount = !String.IsNullOrEmpty(fc["discount"]) ? Convert.ToDouble(fc["discount"]) : 0;
            string content = fc["content"].ToString().Trim();
            string description = fc["Description"].ToString().Trim();
            int hot = !string.IsNullOrEmpty(fc["hot"]) ? 1 : 0;
            record.Name = name;
            record.Price = price;
            record.Discount = discount;
            record.Hot = hot;
            record.Description = description;
            record.Content = content;
            string filename = "";
            try
            {
                filename = Request.Form.Files[0].FileName;

            }
            catch {; }
            if (!string.IsNullOrEmpty(filename))
            {
                var timestamp = DateTime.Now.ToFileTime();
                filename = timestamp + "_" + filename;
                //lấy đường dẫn của file
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Products", filename);

                //upload file
                using (var stream = new FileStream(path, FileMode.Create))
                {

                    Request.Form.Files[0].CopyTo(stream);
                }
                record.Photo = filename;
            }
           
            db.Products.Update(record);
            db.SaveChanges();
         
            List<CategoriesProduct> categories_products = db.CategoriesProducts.Where(item => item.ProductId == id).ToList();
            foreach (var item in categories_products)
                db.CategoriesProducts.Remove(item);
            db.SaveChanges();
            foreach (string _CategoryId in lstcategories)
            {
                CategoriesProduct category_product = new CategoriesProduct();
                category_product.CategoryId = Convert.ToInt32(_CategoryId);
                category_product.ProductId = id ?? 0;
                db.CategoriesProducts.Add(category_product);
                db.SaveChanges();
            }
            //tag_product

            List<TagsProduct> tags_product=db.TagsProducts.Where(item=>item.ProductId==id).ToList();  
            foreach(var item in tags_product) {
                db.TagsProducts.Remove(item);
                db.SaveChanges();   
            }
            foreach(string tag_id in lsttags)
            {
                TagsProduct tag_product =new TagsProduct();
                tag_product.TagId = Convert.ToInt32(tag_id);  
                tag_product.ProductId= id ?? 0; 
                db.TagsProducts.Add(tag_product);
                db.SaveChanges();
            }

            return RedirectToAction("index", "products");

        }
        public IActionResult create()
        {
            ViewBag.action = "/admin/products/createpost";
            return View("FormCreateUpdate");
        }
        public IActionResult createpost(IFormCollection fc)
        {
            Product record=new Product();   
            string name = fc["name"].ToString().Trim();
            double price = Convert.ToDouble(fc["price"]);
            double discount = !string.IsNullOrEmpty(fc["discount"]) ? Convert.ToDouble(fc["discount"]) : 0;
            int hot = !string.IsNullOrEmpty(fc["hot"]) ?1 :0;
            string[] lstcategories = fc["category"];
            string[] lsttags = fc["tag"];
            record.Name = name;
            record.Price = price;
            record.Discount = discount;
            string filename = "";
            try
            {
                filename = Request.Form.Files[0].FileName;
            }
            catch {; }
            if(!string.IsNullOrEmpty(filename)) {
                var timestamp = DateTime.Now.ToFileTime();
                filename=timestamp+"_"+filename;
                string path=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/upload/products" ,filename);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    Request.Form.Files[0].CopyTo(stream);
                }
                //update gia tri vao cot Photo trong csdl
                record.Photo = filename;

            }
            db.Add(record);
            db.SaveChanges();
            foreach(string categoryid in lstcategories)
            {
                CategoriesProduct category_product = new CategoriesProduct();
                category_product.CategoryId = Convert.ToInt32(categoryid) ;
                category_product.ProductId = record.Id;
                db.CategoriesProducts.Add(category_product);
                db.SaveChanges();   
            }
            foreach(string tagid in lsttags)
            {
                TagsProduct tag_product= new TagsProduct(); 
                tag_product.TagId= Convert.ToInt32(tagid) ; 
                tag_product.ProductId= record.Id;   
                db.TagsProducts.Add(tag_product);
                db.SaveChanges();
            }
            return RedirectToAction("index","Products");
        }
        public IActionResult Delete(int? id)
        {
         
            List<CategoriesProduct> category_product = db.CategoriesProducts.Where(item => item.ProductId == id).ToList();
            foreach(var item in category_product)
            {
                db.CategoriesProducts.Remove(item); 
                db.SaveChanges();
            }
            List<TagsProduct> tag_product=db.TagsProducts.Where(item=>item.ProductId==id).ToList();
            foreach(var item in tag_product)
            {
                db.TagsProducts.Remove(item);
                db.SaveChanges();
            } 
            Product record = db.Products.Where(item => item.Id == id).FirstOrDefault();
            db.Products.Remove(record);
            db.SaveChanges();
            return RedirectToAction("index", "products");
        }
    }
}
