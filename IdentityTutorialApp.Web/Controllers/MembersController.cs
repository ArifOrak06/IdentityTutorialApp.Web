using IdentityAppTutorial.Core.Entities;
using IdentityAppTutorial.Core.Models.AppUserViewModels;
using IdentityTutorialApp.Web.Extensions.Microsoft;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityTutorialApp.Web.Controllers
{
    [Authorize] 
    public class MembersController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
       
        public MembersController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            AppUser? user  = await _userManager.FindByNameAsync(User.Identity!.Name!);
            var userViewModel = new UiUserViewModel
            {
                Email = user!.Email,
                PhoneNumber = user!.PhoneNumber,
                UserName = user!.UserName
            };
            return View(userViewModel);
        }
        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
            //return Redirect(nameof(HomeController.Index));
        }
        public IActionResult PasswordChange()
        {
            return View();
            
        }
  
        [HttpPost]
        public async Task<IActionResult> PasswordChange(PasswordChangeViewModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name!);
            if (currentUser == null)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı Bulunamadı.");
                return Redirect(nameof(Index));
            }
            var checkOldPassword = await _userManager.CheckPasswordAsync(currentUser, request.PasswordOld); // bool değer döner.
            if(!checkOldPassword)
            {
                ModelState.AddModelError(string.Empty, "Eski Parola Hatalı.");
                return View();
            }
            var result = await _userManager.ChangePasswordAsync(currentUser, request.PasswordOld, request.NewPassword);
            if (!result.Succeeded)
            {
                ModelState.AddModelErrorList(result.Errors.Select(e => e.Description).ToList());
                return View();
            }
            // Kullanıcının şifresini yani hassas verisini değiştirdiğimiz için securityStamp değerini güncellememiz gerekir ve parola değiştirdiğimiz için mutlaka   sistemden çıkartıp tekrar giriş yaptırıyoruz.
            await _userManager.UpdateSecurityStampAsync(currentUser);
            await _signInManager.SignOutAsync();
            await _signInManager.PasswordSignInAsync(currentUser, request.NewPassword,true,false);

            TempData["SuccessMessage"] = "Şifreniz başarıyla değiştirilmiştir.";

            return Redirect(nameof(PasswordChange));

        }
    }





}
