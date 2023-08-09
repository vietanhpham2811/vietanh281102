using  Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QlBanOpDaDienThoai.Areas.Admin.Attributes;
using QlBanOpDaDienThoai.Models;
using X.PagedList;
namespace QlBanOpDaDienThoai.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AdvsController : Controller
    {
        Net20ProjectContext db = new Net20ProjectContext();
        public IActionResult Index(int? p)
        {
            int pageNumber = p ?? 1;
            int pageSize = 10; 
            List<Adv> list= db.Advs.OrderBy(item=>item.Id).ToList();  
            return View(list.ToPagedList(pageNumber,pageSize));
        }
        public IActionResult Update(int? id)
        {
            Adv record =db.Advs.Where(item=>item.Id == id).FirstOrDefault();
            ViewBag.action = "/admin/advs/updatepost/" + id;
            return View("FormCreateUpdate",record);
        }
        public IActionResult UpdatePost(int? id,IFormCollection fc)
        {
            Adv record = db.Advs.Where(item => item.Id == id).FirstOrDefault();
            string name = fc["name"].ToString().Trim();
            int Position = Convert.ToInt32(fc["position"]);
            record.Name = name;
            record.Position = Position;
            string filename = "";
            try
            {
                //lay ten file
                filename = Request.Form.Files[0].FileName;
            }
            catch {; }
            if (!String.IsNullOrEmpty(filename))
            {
                //xoa anh cu
                if (record.Photo != null && System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Adv", record.Photo)))
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Adv", record.Photo));
                }
                //upload anh moi
                //lay thoi gian gan vao ten file -> tranh cac file co ten trung ten voi file se upload
                var timestap = DateTime.Now.ToFileTime();
                filename = timestap + "_" + filename;
                //lay duong dan cua file
                string _Path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Adv", filename);
                //upload file
                using (var stream = new FileStream(_Path, FileMode.Create))
                {
                    Request.Form.Files[0].CopyTo(stream);
                }
                //update gia tri vao cot Photo trong csdl
                record.Photo = filename;
            }
            db.Advs.Update(record);
            db.SaveChanges();
            return RedirectToAction("Index", "Advs");
        }
        public IActionResult Create()
        {
            ViewBag.action = "/admin/Advs/createpost";
            return View("FormCreateUpdate");
        }
        public IActionResult CreatePost(IFormCollection fc)
        {
            Adv record = new Adv();
            string name= fc["name"].ToString().Trim();
            int Position = Convert.ToInt32(fc["position"]);
            record.Name = name; 
            record.Position = Position; 
            string filename = "";
            try
            {
                //lay ten file
                filename = Request.Form.Files[0].FileName;
            }
            catch {; }
            if (!String.IsNullOrEmpty(filename))
            {
                //xoa anh cu
                if (record.Photo != null && System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Adv", record.Photo)))
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Adv", record.Photo));
                }
                //upload anh moi
                //lay thoi gian gan vao ten file -> tranh cac file co ten trung ten voi file se upload
                var timestap = DateTime.Now.ToFileTime();
                filename = timestap + "_" + filename;
                //lay duong dan cua file
                string _Path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Adv", filename);
                //upload file
                using (var stream = new FileStream(_Path, FileMode.Create))
                {
                    Request.Form.Files[0].CopyTo(stream);
                }
                //update gia tri vao cot Photo trong csdl
                record.Photo = filename;
            }
            db.Advs.Add(record);
            db.SaveChanges();   
            return RedirectToAction("Index","Advs");

        }
        public IActionResult Delete(int? id)
        {
            Adv record = db.Advs.Where(item => item.Id == id).FirstOrDefault();
            db.Advs.Remove(record);
            db.SaveChanges();
            return RedirectToAction("Index", "Advs");

        }
    }
}
