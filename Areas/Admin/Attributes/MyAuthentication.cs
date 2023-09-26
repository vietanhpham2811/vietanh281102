using Microsoft.AspNetCore.Mvc.Filters;
using QlBanOpDaDienThoai.Models;
namespace QlBanOpDaDienThoai.Areas.Admin.Attributes
{
    public class MyAuthentication:ActionFilterAttribute
    {
    public Net20ProjectContext db=new Net20ProjectContext();
        public string controllerName { get; set; }
        public MyAuthentication(string _controllerName)
        {
            this.controllerName = _controllerName;
        }
        //public override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    //kiem tra xem user_id co duoc phep truy cap vao Controller nay khong
        //    int _UserId = Convert.ToInt32(context.HttpContext.Session.GetString("admin_user_id"));
        //    var check_record = (from item_menu in db.Menus join item_menu_group in db.MenusGroups on item_menu.Id equals item_menu_group.MenuId join item_group in db.Groups on item_menu_group.GroupId equals item_group.Id join item_user_group in db.UsersGroups on item_group.Id equals item_user_group.GroupId join item_user in db.Users on item_user_group.UserId equals item_user.Id where item_user.Id == _UserId && item_menu.ControllerName.Equals(controllerName) select item_menu).FirstOrDefault();
        //    if (check_record == null)
        //        context.HttpContext.Response.Redirect("/Admin/Home");
        //    base.OnActionExecuting(context);
        //}
    }
}
