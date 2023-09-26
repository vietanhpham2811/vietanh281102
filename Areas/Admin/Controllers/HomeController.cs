﻿using Microsoft.AspNetCore.Mvc;
using QlBanOpDaDienThoai.Areas.Admin.Attributes;
using QlBanOpDaDienThoai.Models;

namespace QlBanOpDaDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    [CheckLogin]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
