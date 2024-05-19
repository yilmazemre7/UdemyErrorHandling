using UdemyErrorHandling.Filter;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

//Tüm projede ilgili hata ekranýný basmaya yarar.
builder.Services.AddMvc(opt =>
{
	opt.Filters.Add(new CustomHandleExceptionFilterAttribute() { ErrorPage = "error1" });
}).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);
var app = builder.Build();

//Request ]----------DeveloperExceptionPage]----------[ExceptionHandler]----------[UseStatusCodePages]----------[DatabaseErrorPage]]----------Response//
if (app.Environment.IsDevelopment())
{
	//Developer ExceptionFilteri aktif eder. 
	app.UseDeveloperExceptionPage();
	//1.Yol
	app.UseStatusCodePages("text/plain", "Bir hata olustu.Durum kodu {0}");
	//2.Yol
	app.UseStatusCodePages(async context =>
	{
		context.HttpContext.Response.ContentType = "text/plain";
		await context.HttpContext.Response.WriteAsync($"Bir hata var.Durum Kodu {context.HttpContext.Response.StatusCode} ");
	});
	//3.Yol
	app.UseStatusCodePages();
}
else
{
	app.UseHsts();
}

//Uygulama bazýnda hata sayfasý.
//app.UseExceptionHandler(context =>
//{
//	context.Run(async page =>
//	{
//		page.Response.StatusCode = 500;
//		page.Response.ContentType = "text/html";
//		await page.Response.WriteAsync($"<html>Hata var {page.Response.StatusCode} <head><h1></h1></head></html>");
//	});
//});
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
