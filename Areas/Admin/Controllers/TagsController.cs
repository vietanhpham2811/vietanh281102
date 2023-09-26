using Microsoft.AspNetCore.Mvc;
using QlBanOpDaDienThoai.Areas.Admin.Attributes;
using QlBanOpDaDienThoai.Models;
using X.PagedList;

namespace QlBanOpDaDienThoai.Areas.Admin.Controllers
{
    [CheckLogin]

    [Area("admin")]
    public class TagsController : Controller
    {

        Net20ProjectContext db = new Net20ProjectContext();
        [AdminAuthorize(idFunction = 1)]

        public IActionResult Index(int? p)
        {
            List<Tag> list = db.Tags.OrderBy(item => item.Id).ToList();
            int pageNumber = p ?? 1;
            int pageSize = 10;
            return View(list.ToPagedList(pageNumber, pageSize));
        }
        public IActionResult Update(int? id)
        {
            Tag record = db.Tags.Where(item => item.Id == id).FirstOrDefault();
            ViewBag.action = "/Admin/Tags/Update/" + id;
            return View("formcreateupdate", record);
        }
        [AdminAuthorize(idFunction = 3)]

        public IActionResult UpdatePost(int? id, IFormCollection fc)
        {
            Tag record = db.Tags.Where(item => item.Id == id).FirstOrDefault();
            string name = fc["name"].ToString().Trim();
            record.Name = name;
            db.Tags.Update(record);
            db.SaveChanges();

            return RedirectToAction("Index", "Tags");
        }

        public IActionResult Create()
        {
            ViewBag.action = "/Admin/Tags/CreatePost/";
            return View("FormCreateUpdate");
        }
        [AdminAuthorize(idFunction = 2)]

        public IActionResult CreatePost(IFormCollection fc)
        {
            Tag record =new Tag();
            string name = fc["name"].ToString().Trim();
            record.Name = name;

            db.Tags.Add(record);
            db.SaveChanges();
            return RedirectToAction("Index", "Tags");
        }
        [AdminAuthorize(idFunction = 4)]

        public IActionResult Delete(int? id)
        {
            Tag record=db.Tags.Where(item=>item.Id == id).FirstOrDefault();     
            List<TagsProduct> listTagProduct=db.TagsProducts.Where(item=>item.TagId == id).ToList();
            foreach(var item in listTagProduct)
            {
                db.TagsProducts.Remove(item);   
                db.SaveChanges();   
            }
            db.Tags.Remove(record);

            db.SaveChanges();
            return RedirectToAction("index","Tags");
        }
    }
}
