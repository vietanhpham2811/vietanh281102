using Microsoft.AspNetCore.Mvc;
using QlBanOpDaDienThoai.Models;
using X.PagedList;

namespace QlBanOpDaDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController : Controller
    {
        Net20ProjectContext db=new Net20ProjectContext();
        public IActionResult Index(int? p)
        {
            int pageSize = 3;
            int pageNumber = p ?? 1;
            List<News> list = db.News.OrderByDescending(Item=>Item.Id).ToList();  
            return View(list.ToPagedList(pageNumber,pageSize));
        }
        public IActionResult Update(int? id)
        {
            News record =db.News.Where(item=>item.Id==id).FirstOrDefault();
            ViewBag.action = "/admin/News/UpdatePost/" + id;
            return View("FormCreateUpdate",record);
        }
        public IActionResult UpdatePost(int? id,IFormCollection fc)
        {
            News record = db.News.Where(item=>item.Id==id).FirstOrDefault();
            string name = fc["Name"].ToString().Trim();
            int hot = !String.IsNullOrEmpty(fc["Hot"]) ? 1:0;
            string description = fc["Description"].ToString();
            string content = fc["Content"].ToString();
            record.Name = name;
            record.Hot = hot;
            record.Description = description;
            record.Content = content;
            record.Hot = hot;   
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
                if (record.Photo != null && System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/News", record.Photo)))
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/News", record.Photo));
                }
                //upload anh moi
                //lay thoi gian gan vao ten file -> tranh cac file co ten trung ten voi file se upload
                var timestap = DateTime.Now.ToFileTime();
                filename = timestap + "_" + filename;
                //lay duong dan cua file
                string _Path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/News", filename);
                //upload file
                using (var stream = new FileStream(_Path, FileMode.Create))
                {
                    Request.Form.Files[0].CopyTo(stream);
                }
                //update gia tri vao cot Photo trong csdl
                record.Photo = filename;
            }
            db.News.Update(record);
            db.SaveChanges();   
            return RedirectToAction("Index","News");

        }
        public IActionResult Create()
        {
            ViewBag.action = "/Admin/News/createpost";
            return View("FormCreateUpdate");
        }
        public IActionResult CreatePost(IFormCollection fc)
        {
            News record = new News();
            string name = fc["name"].ToString().Trim();
            int hot = !String.IsNullOrEmpty(fc["hot"]) ? 1 : 0;
            string description= fc["description"].ToString().Trim();
            string content = fc["content"].ToString().Trim();
            string filename = "";
            record.Name = name;
            record.Hot = hot;   
            record.Description = description;
            record.Content = content;   
            try
            {
                //lay ten file
                filename = Request.Form.Files[0].FileName;
            }
            catch {; }
            if (!String.IsNullOrEmpty(filename))
            {
                //xoa anh cu
                if (record.Photo != null && System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/News", record.Photo)))
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/News", record.Photo));
                }
                //upload anh moi
                //lay thoi gian gan vao ten file -> tranh cac file co ten trung ten voi file se upload
                var timestap = DateTime.Now.ToFileTime();
                filename = timestap + "_" + filename;
                //lay duong dan cua file
                string _Path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/News", filename);
                //upload file
                using (var stream = new FileStream(_Path, FileMode.Create))
                {
                    Request.Form.Files[0].CopyTo(stream);
                }
                //update gia tri vao cot Photo trong csdl
                record.Photo = filename;
            }
            db.News.Add(record);    
            db.SaveChanges();   
            return RedirectToAction("Index", "News");

        }
        public IActionResult Delete(int? id)
        {
            News record = db.News.Where(item=>item.Id == id).FirstOrDefault();  
            db.News.Remove(record);
            db.SaveChanges();
            return RedirectToAction("Index", "News");

        }
    }
}
