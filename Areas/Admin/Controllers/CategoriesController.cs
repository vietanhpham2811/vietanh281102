using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QlBanOpDaDienThoai.Areas.Admin.Attributes;
using QlBanOpDaDienThoai.Models;
using X.PagedList;

namespace QlBanOpDaDienThoai.Areas.Admin.Controllers
{
    [CheckLogin]
    [Area("admin")]
    public class CategoriesController : Controller
    {

        Net20ProjectContext db = new Net20ProjectContext();

        public IActionResult Index(int? p)
        {
            List<Category> list = db.Categories.Where(item => item.ParentId == 0).OrderByDescending(item => item.Id).ToList();
            int pageNumber = p ?? 1;
            int pageSize = 3;
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        public IActionResult Update(int? id)
        {

            Category record = db.Categories.Where(item => item.Id == id).FirstOrDefault();
            ViewBag.action = "/admin/categories/updatepost/" + id;
            return View("FormCreateUpdate", record);

        }
        public IActionResult UpdatePost(int? id, IFormCollection fc)
        {
            Category record = db.Categories.Where(item => item.Id == id).FirstOrDefault();
            string name = fc["name"].ToString().Trim();

            int parentid = Convert.ToInt32(fc["ParentId"].ToString());
            //cachs1:int displayhomepgae = fc["DisplayHomePage"] != "" && fc["DisplayHomePage"] == "on" ? 1 : 0;
            int displayhomepage = !string.IsNullOrEmpty(fc["displayhomepage"]) ? 1 : 0;
            record.Name = name;
            record.ParentId = parentid;
            record.DisplayHomePage = displayhomepage;
            db.Categories.Update(record);
            db.SaveChanges();
            return RedirectToAction("index", "categories");


        }
        public IActionResult Create()
        {
            ViewBag.action = "/admin/categories/createpost";
            return View("FormCreateUpdate");
        }
        public IActionResult CreatePost(IFormCollection fc)
        {
            Category record = new Category();
            string name = fc["name"].ToString().Trim();
            int parentId = Convert.ToInt32(fc["ParentId"]);
            int displayhomepage = !string.IsNullOrEmpty(fc["displayhomepage"]) ? 1 : 0;
            record.Name=name;
            record.ParentId = parentId; 
            record.DisplayHomePage=displayhomepage; 
            db.Categories.Add(record);
            db.SaveChanges();
            return RedirectToAction("index", "categories");
        }
        public IActionResult Delete(int? id)
        {
            Category record = db.Categories.Where(item => item.Id == id).FirstOrDefault();
            if (record.ParentId == 0)
            {
                List<Category> listCategorySub = db.Categories.Where(item => item.ParentId ==record.Id).ToList();
                foreach (var item in listCategorySub)
                {
                    db.Categories.Remove(item);
                    db.SaveChanges();
                }
            }

            db.Categories.Remove(record);
            db.SaveChanges();
            return RedirectToAction("index", "categories");

        }
    }
}
