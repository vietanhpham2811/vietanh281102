using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//su dung doi tuong thao tac IFormCollection
using Microsoft.AspNetCore.Http;
//nhin thay cac file .cs ben trong folder Models
using QlBanOpDaDienThoai.Models;
using Microsoft.EntityFrameworkCore;
//phan trang
using X.PagedList;
//nhin thay file CheckLogin.cs trong thu muc Attributes
using QlBanOpDaDienThoai.Areas.Admin.Attributes;
//doi tuong thao tac file
using System.IO;

namespace QlBanOpDaDienThoai.Areas.Admin.Controllers
{
    [Area("admin")]
    public class SlidesController : Controller
    {
        Net20ProjectContext db = new Net20ProjectContext();
        [AdminAuthorize(idFunction = 1)]

        public IActionResult Index(int? p)
        {
            int pageNumber = p ?? 1;
            int pageSize =  5; 
            List<Slide> list = db.Slides.OrderByDescending(item=>item.Id).ToList(); 
            
            return View(list.ToPagedList(pageNumber,pageSize));
        }
        public IActionResult Update(int? id)
        {
            Slide record = db.Slides.Where(item => item.Id == id).FirstOrDefault();
            ViewBag.action = "/admin/Slides/updatepost/" + id;
            return View("FormCreateUpdate", record);
        }
        [AdminAuthorize(idFunction = 3)]

        public IActionResult UpdatePost(int? id ,IFormCollection fc)
        {
            Slide record = db.Slides.Where(item => item.Id == id).FirstOrDefault();
            string name = fc["name"].ToString().Trim();
            string title = fc["Title"].ToString().Trim();
            string subtitle = fc["SubTitle"].ToString().Trim();
            string info = fc["Info"].ToString().Trim();
            string link = fc["link"].ToString().Trim();
            record.Name=name;
            record.Title=title;
            record.SubTitle=subtitle;   
            record.Info=info;   
            record.Link=link;
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
                if (record.Photo != null && System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot /Upload/Slides", record.Photo)))
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Slides", record.Photo));
                }
                //upload anh moi
                //lay thoi gian gan vao ten file -> tranh cac file co ten trung ten voi file se upload
                var timestap = DateTime.Now.ToFileTime();
                filename = timestap + "_" + filename;
                //lay duong dan cua file
                string _Path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Slides", filename);
                //upload file
                using (var stream = new FileStream(_Path, FileMode.Create))
                {
                    Request.Form.Files[0].CopyTo(stream);
                }
                //update gia tri vao cot Photo trong csdl
                record.Photo = filename;
            }
            db.Slides.Update(record);
            db.SaveChanges();
            return RedirectToAction("index", "Slides");
        }
        public IActionResult Create() {
            ViewBag.action = "/Admin/Slides/CreatePost";
            return View("FormCreateUpdate");

        }
        [AdminAuthorize(idFunction = 2)]

        public IActionResult CreatePost(IFormCollection fc)
        {
            Slide record = new Slide();
            string name = fc["name"].ToString().Trim();
            string title = fc["Title"].ToString().Trim();
            string subtitle = fc["SubTitle"].ToString().Trim();
            string info = fc["Info"].ToString().Trim();
            string link = fc["link"].ToString().Trim();
            record.Name = name;
            record.Title = title;
            record.SubTitle = subtitle;
            record.Info = info;
            record.Link = link;
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
                if (record.Photo != null && System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot /Upload/Slides", record.Photo)))
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Slides", record.Photo));
                }
                //upload anh moi
                //lay thoi gian gan vao ten file -> tranh cac file co ten trung ten voi file se upload
                var timestap = DateTime.Now.ToFileTime();
                filename = timestap + "_" + filename;
                //lay duong dan cua file
                string _Path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Slides", filename);
                //upload file
                using (var stream = new FileStream(_Path, FileMode.Create))
                {
                    Request.Form.Files[0].CopyTo(stream);
                }
                //update gia tri vao cot Photo trong csdl
                record.Photo = filename;
            }
             db.Slides.Add(record);
            db.SaveChanges();
            return RedirectToAction("index", "Slides");
        }
        [AdminAuthorize(idFunction = 4)]

        public IActionResult Delete(int? id)
        {
            Slide record= db.Slides.Where(item=>item.Id==id).FirstOrDefault();
            db.Slides.Remove(record);
            db.SaveChanges();
            return RedirectToAction("index", "Slides");
        }
    }
}
