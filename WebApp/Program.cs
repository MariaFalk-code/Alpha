using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ProjectService>();



builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHsts();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Project}/{action=Dashboard}/{id?}")
    .WithStaticAssets();


app.Run();
