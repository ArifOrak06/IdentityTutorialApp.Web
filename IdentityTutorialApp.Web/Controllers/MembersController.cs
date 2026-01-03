using IdentityAppTutorial.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityTutorialApp.Web.Controllers
{
    [Authorize]
    public class MembersController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public MembersController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
            //return Redirect(nameof(HomeController.Index));
        }
    }
}
