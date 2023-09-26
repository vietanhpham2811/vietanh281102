using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QlBanOpDaDienThoai.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QlBanOpDaDienThoai.Areas.Admin.Attributes
{
    [Area("admin")]
    [CheckLogin]
    public class AdminAuthorize : Attribute, IAuthorizationFilter
    {
        public int idFunction { set; get; }
        Net20ProjectContext db = new Net20ProjectContext();

        public void OnAuthorization(AuthorizationFilterContext context)
        
        {

       
            var idSession = context.HttpContext.Session.GetInt32("id");
         
            using (Net20ProjectContext db = new Net20ProjectContext())
            {
                try
                {
                    
                    
                    
                    var record = db.Permissions.Where(item =>( item.IdUser == idSession) && (item.IdFunction == idFunction)).Count();
                    if (record==0)
                    {
                        context.Result = new RedirectToRouteResult(new RouteValueDictionary
                    {
                        { "area", "Admin" },
                        { "controller", "Error" },
                        { "action", "errorPermission" }
                    });
                    }
                }
                catch (Exception)
                {
                    throw;
                }
              
            }



        }
    }
}
