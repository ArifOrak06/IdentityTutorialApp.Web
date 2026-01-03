using IdentityAppTutorial.Core.Entities;
using IdentityTutorialApp.Repository.Contexts.EfCore;
using IdentityTutorialApp.Web.CustomValidations;
using IdentityTutorialApp.Web.Localizations;

namespace IdentityTutorialApp.Web.Extensions.Microsoft
{
    public static class StartupExtension
    {
        public static void AddIdentityDependency(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnoprstuvwxyz1234567890_-";
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit = false;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                opt.Lockout.MaxFailedAccessAttempts = 3;



            }).AddPasswordValidator<PasswordValidator>()
            .AddErrorDescriber<LocalizationsIdentityErrorDescriber>()
            .AddEntityFrameworkStores<AppDbContext>();
        }
        public static void AddCookieConfigurationDependency(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(opt =>
            {
                var cookieBuilder = new CookieBuilder();
                cookieBuilder.Name = "IdentityWebApp";
                opt.LoginPath = new PathString("/Home/SignIn");
                opt.LogoutPath = new PathString("/Members/LogOut");
                opt.Cookie = cookieBuilder;
                opt.ExpireTimeSpan = TimeSpan.FromDays(60);
                opt.SlidingExpiration = true;


            });
        }
    }
}
