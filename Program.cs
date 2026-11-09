using EventDaysMvc.Infrastructure;
using EventDaysMvc.Repositories;
using EventDaysMvc.Services;

var builder = WebApplication.CreateBuilder(args);

// MVC регистрирует и API-контроллеры, и контроллеры с Razor-представлениями.
builder.Services.AddControllersWithViews();
builder.Services.AddProblemDetails();
builder.Services.AddSingleton<DayCalculator>();
builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
builder.Services.AddSingleton<IStudentRepository, InMemoryStudentRepository>();
builder.Services.AddLabRequestTimeouts();

var app = builder.Build();
app.UseExceptionHandler();
app.UseStaticFiles();
app.UseRouting();
app.UseRequestTimeouts();
app.MapControllers();
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
app.Run();
