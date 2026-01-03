using IdentityTutorialApp.Repository.Extensions.Microsoft;
using IdentityTutorialApp.Web.Extensions.Microsoft;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Dependencies added to IoC

builder.Services.AddDependenciesForRepositoryLayer(builder.Configuration);
//builder.Services.AddCookieConfigurationDependency();


// Framework, libraries

builder.Services.AddIdentityDependency();

builder.Services.AddCookieConfigurationDependency();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
