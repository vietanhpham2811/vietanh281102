using Microsoft.AspNetCore.Mvc;
//muon su dung thu vien jSon thi phai them dong duoi
using Newtonsoft.Json;

namespace QlBanOpDaDienThoai.Controllers
{
    public class WishListController : Controller
    {
        public IActionResult Index()
        {
            string str_wish_list = HttpContext.Session.GetString("wish_list");
            if (!String.IsNullOrEmpty(str_wish_list))
            {
                List<int> list_wish_list = JsonConvert.DeserializeObject<List<int>>(str_wish_list);
                ViewBag.list_wish_list=list_wish_list;  
            }
            return View();
        }
        public IActionResult Add(int id)
        {
            string str_wish_list = HttpContext.Session.GetString("wish_lish");
            if(!string.IsNullOrEmpty(str_wish_list))
            {
                List<int> list_wish_list = JsonConvert.DeserializeObject<List<int>>(str_wish_list);
                if (!CheckIdExits(id)){
                    list_wish_list.Add(id);
                    string json_wish_lish = JsonConvert.SerializeObject(list_wish_list);
                    HttpContext.Session.SetString("wish_lish", json_wish_lish);
                }
            }
            else
            {
                List<int> list_wish_list=new List<int>();
                list_wish_list.Add(id);
                string json_wish_list = JsonConvert.SerializeObject(list_wish_list);
                HttpContext.Session.SetString("wish_list", json_wish_list);
            }
            return RedirectToAction("index");
        }
        public IActionResult Remove(int id)
        {
            string str_wish_list = HttpContext.Session.GetString("wish_list");
            if(String.IsNullOrEmpty(str_wish_list))
            {
                List<int> list_wish_list = JsonConvert.DeserializeObject<List<int>>(str_wish_list);
                list_wish_list.Remove(id);
                string json_wish_list = JsonConvert.SerializeObject(list_wish_list);
                HttpContext.Session.SetString("wish_list", json_wish_list);


            }
            return RedirectToAction("Index");

        }
        public bool CheckIdExits(int id)
        {
            string str_wish_list = HttpContext.Session.GetString("wish_list");
            if(!String.IsNullOrEmpty(str_wish_list)) {
                List<int> list_wish_list = JsonConvert.DeserializeObject<List<int>>(str_wish_list);
                foreach(var item in str_wish_list)
                {
                    if (item == id)
                    {
                        return true;
                    }
                }
            }
            return false;
        } 
    }
}
