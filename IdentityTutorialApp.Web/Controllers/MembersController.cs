using IdentityAppTutorial.Core.Entities;
using IdentityAppTutorial.Core.Helpers;
using IdentityAppTutorial.Core.Models.AppUserViewModels;
using IdentityTutorialApp.Web.Extensions.Microsoft;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IdentityTutorialApp.Web.Controllers
{
    [Authorize]
    public class MembersController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IImgHelper _imgHelper;

        public MembersController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IImgHelper imgHelper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _imgHelper = imgHelper;
        }

        public async Task<IActionResult> Index()
        {
            AppUser? user = await _userManager.FindByNameAsync(User.Identity!.Name!);
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
            if (!checkOldPassword)
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
            await _signInManager.PasswordSignInAsync(currentUser, request.NewPassword, true, false);

            TempData["SuccessMessage"] = "Şifreniz başarıyla değiştirilmiştir.";

            return Redirect(nameof(PasswordChange));

        }
        [HttpGet]
        public async Task<IActionResult> UpdateUser()
        {
            // Kullanıcı cinsiyet bilgisinde güncelleme yapmak isterse bunu Dropdown List şeklinde sunabilmek için  ÖSelectList oluşturduk ve 
            ViewBag.genderList = new SelectList(Enum.GetNames(typeof(Gender)));
            var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name!);
            var userEditViewModel = new UserEditViewModel
            {
                Email = currentUser!.Email,
                Phone = currentUser!.PhoneNumber,
                City = currentUser!.City,
                BirthDate = currentUser!.BirtDate,
                Gender = currentUser.Gender,
                UserName = currentUser!.UserName,

            };


            return View(userEditViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserEditViewModel request)
        {
            if (!ModelState.IsValid)
                return View();

            var currentUser = await _userManager.FindByNameAsync(User.Identity!.Name!);
            currentUser.UserName = request.UserName;
            currentUser.Email = request.Email;
            currentUser.PhoneNumber = request.Phone;
            currentUser.City = request.City;
            currentUser.BirtDate = request.BirthDate;
            currentUser.Gender = request.Gender;

            // Kullanıcının hassas bilgileri güncellendikten sonra resmini güncelleyeceğiz.

            if (request.Picture != null)
            {

                var uploadModel = await _imgHelper.UploadPictureAsync(request.Picture);
                currentUser.Picture = uploadModel.FileName;
            }

            var identityResult = await _userManager.UpdateAsync(currentUser);
            if (!identityResult.Succeeded)
            {
                ModelState.AddModelErrorList(identityResult.Errors.Select(x => x.Description).ToList());
                return View();
            }

            await _userManager.UpdateSecurityStampAsync(currentUser);
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(currentUser, true);
            TempData["SuccessMessage"] = "Üye Bilgileri Başarılı bir şekilde güncellenmiştir.";
            UserEditViewModel? userEditViewModel = new()
            {
                BirthDate = currentUser.BirtDate,
                Gender = currentUser.Gender,
                City = currentUser.City,
                Phone = currentUser.PhoneNumber,
                UserName = currentUser.UserName,
                Email = currentUser.Email,
                PictureUrl = currentUser.Picture
            };

            return View(userEditViewModel);

        }



    }

}
