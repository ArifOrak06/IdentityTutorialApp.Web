using IdentityAppTutorial.Core.Models.EmailModels;
using IdentityTutorialApp.Repository.Extensions.Microsoft;
using IdentityTutorialApp.Service.Extensions.Microsoft;
using IdentityTutorialApp.Web.Extensions.Microsoft;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Dependencies added to IoC

builder.Services.AddDependenciesForRepositoryLayer(builder.Configuration);
builder.Services.AddServiceLayerDependencies();


// Framework, libraries

builder.Services.AddIdentityDependency();

builder.Services.AddCookieConfigurationDependency();

// GENEL CONFÝGURASYONLAR 

// IOption ile EmailSettings classýmýzý - appsettings.json'daki EmailSettings bölümüne baðladýk
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));


// Þifre Sýfýrlama Link Token Ömrü
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(2); // Ýki dkika geçerli olacak þekilde ayarladýk
});




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
