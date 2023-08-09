using Microsoft.AspNetCore.Mvc;
using QlBanOpDaDienThoai.Models;
using X.PagedList;
using Microsoft.AspNetCore.Http;
//nhin thay cac file .cs ben trong folder Models
using QlBanOpDaDienThoai.Models;
//su dung entity framework
using Microsoft.EntityFrameworkCore;
//nhin thay file CheckLogin.cs tron thu muc Attributes
using QlBanOpDaDienThoai.Areas.Admin.Attributes;
//doi tuong thao tac file
using System.IO;
//su dung kieu List
using System.Collections.Generic;
//doi tuong ma hoa password
using BC = BCrypt.Net.BCrypt;

namespace QlBanOpDaDienThoai.Controllers
{
    public class SearchController : Controller
    {
        Net20ProjectContext db = new Net20ProjectContext();
        public IActionResult SearchProducts(int? p)
        {
            string key = Request.Query["key"];
            ViewBag._key = key;

            int pageNumber = p ?? 1;
            int pageSize = 9;
            List<Product> list_product = db.Products.Where(item => item.Name.Contains(key)).ToList();

            return View(list_product.ToPagedList(pageNumber, pageSize));
        }
        public IActionResult SearchPrice(int? p)
        {
            double toPrice = Convert.ToDouble(Request.Query["toprice"]);
            double fromPrice = Convert.ToDouble(Request.Query["fromPrice"]);
            ViewBag.toPrice = toPrice;
            ViewBag.fromPrice = fromPrice;
            int pageNumber = p ?? 1;
            int pageSize = 9;
            List<Product> list_product = db.Products.Where(item => item.Price >= fromPrice && item.Price <= toPrice).ToList();
            return View(list_product.ToPagedList(pageNumber, pageSize));
        }
        public IActionResult SearchTag(int? p,int? id)
        {
            int tag_id = id ?? 0;
            ViewBag._tag_id = tag_id;
            int pageNumber = p ?? 1;
            int pageSize = 9;
            var list_record = from product in db.Products
                              join tag_product in db.TagsProducts
                            on product.Id equals tag_product.ProductId
                              join tag in db.Tags
                            on tag_product.TagId equals tag.Id
                              where tag.Id == tag_id
                              select product;
            return View(list_record.ToPagedList(pageNumber, pageSize));
        }
    }
}
