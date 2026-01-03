using IdentityAppTutorial.Core.Entities;
using IdentityAppTutorial.Core.Models.AppUserViewModels;
using IdentityTutorialApp.Web.Extensions.Microsoft;
using IdentityTutorialApp.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IdentityTutorialApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel request)
        {
            if (!ModelState.IsValid)
                return View();
            var identityResult = await _userManager.CreateAsync(new()
            {
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.Phone,

            }, request.PasswordConfirm);

            if (identityResult.Succeeded)
            {
                TempData["SuccessMessage"] = "Üyelik kayýt iþlemi baþarýyla gerçekleþmiþtir.";
                return RedirectToAction(nameof(HomeController.SignUp));
            }
            // IdentityResult'tan hata dönerse
            ModelState.AddModelErrorList(identityResult.Errors.Select(x => x.Description).ToList());
            return View();

        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel request, string? returnUrl)
        {
            returnUrl = returnUrl ?? Url.Action("Index", "Home");
            AppUser? currentUser = await _userManager.FindByEmailAsync(request.Email);
            if (currentUser == null)
            {
                ModelState.AddModelError(string.Empty, "E-Posta veya Þifre hatalý");
            }
            var signInresult = await _signInManager.PasswordSignInAsync(currentUser, request.Password, request.RememberMe, true);
            if (signInresult.Succeeded)
                return Redirect(returnUrl);

            // Kullanýcý Hesabý Kilitlenmiþ ise; 
            if (signInresult.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Hesabýnýz 3 dakika süreliðine kilitlenmiþtir. Lütfen daha sonra tekrar deneyiniz.");
                return View();
            }

            ModelState.AddModelError(string.Empty, $"E-Posta veya Þifre hatalý, Baþarýsýz giriþ sayýsý : {await _userManager.GetAccessFailedCountAsync(currentUser)}");
            return View();
          
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
