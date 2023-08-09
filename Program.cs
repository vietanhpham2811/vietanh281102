var builder = WebApplication.CreateBuilder(args);
//---
//đăng ký sử dụng mô hình mvc
builder.Services.AddControllersWithViews();
//đăng ký sử dụng session
builder.Services.AddSession();
//---
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
//---
//sử dụng session
app.UseSession();
app.UseStaticFiles(); // muốn truy cập được các file trong thư mục wwwroot thì phải có dòng này
//sử dụng url
app.UseRouting();
app.MapControllerRoute(
            name: "area",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
//---

app.Run();
