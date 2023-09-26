using Microsoft.AspNetCore.Mvc.Filters;

namespace QlBanOpDaDienThoai.Areas.Admin.Attributes
{
    public class CheckLogin: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        
        {
            string email=context.HttpContext.Session.GetString("email");    
            if (string.IsNullOrEmpty(email))
            {
                context.HttpContext.Response.Redirect("/Admin/Account/Login");

            }
            base.OnActionExecuting(context);
        }
    }
}
