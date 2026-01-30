using Azure.Core;
using IdentityAppTutorial.Core.Entities;
using IdentityAppTutorial.Core.Models.AdminModels.RoleModels;
using IdentityTutorialApp.Web.Extensions.Microsoft;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityTutorialApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class RolesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public RolesController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var roleViewModels = await _roleManager.Roles.Select(x => new RoleViewModel
            {
                Id = x.Id,
                Name = x.Name!
            }).ToListAsync();

            return View(roleViewModels);
        }
        public IActionResult RoleCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleCreateViewModel request)
        {
            if (!ModelState.IsValid)
                return View();
            var roleResult = await _roleManager.CreateAsync(new AppRole() { Name = request.Name });
            if(!roleResult.Succeeded)
                ModelState.AddModelErrorList(roleResult.Errors.Select(e=>e.Description).ToList());
            TempData["SuccessMessage"] = "Role ekleme işlemi başarılı bir şekilde gerçekleştirildi.";
            return View();
        }
        public async Task<IActionResult> RoleUpdate(string roleId)
        {
            AppRole? roleToUpdate = await _roleManager.FindByIdAsync(roleId);
            if(roleToUpdate == null)
                throw new Exception("Güncellenecek rol bulunamadı.");

            return View(new RoleUpdateViewModel
            {
                Id = roleToUpdate!.Id,
                Name = roleToUpdate!.Name!
            });
        }
        [HttpPost]
        public async Task<IActionResult> RoleUpdate(RoleUpdateViewModel request)
        {
            if (!ModelState.IsValid)
                return View();
            AppRole? roleToUpdate = await _roleManager.FindByIdAsync(request.Id);
            roleToUpdate!.Name = request.Name;
            var updateResult = await _roleManager.UpdateAsync(roleToUpdate);
            if(!updateResult.Succeeded)
                ModelState.AddModelErrorList(updateResult.Errors.Select(e=>e.Description).ToList());
            TempData["SuccessMessage"] = "Role güncelleme işlemi başarılı bir şekilde gerçekleştirildi.";
            return View();
        }
        public async Task<IActionResult> AssignRoleToAppUser(string userId)
        {
            // öncelikle hangi kullanıcıya role ataması yapılacak bulalım.
            AppUser? user = await _userManager.FindByIdAsync(userId);
            ViewBag.UserId = userId;
            // Diğer önemli husus kullanıcının karşısına tüm rolleri listelemek ! buradan hangi rolleri atamak isterse onu seçecek
            List<AppRole>? roleList = (await _roleManager.Roles.ToListAsync())!;

            List<AssignRoleToAppUserViewModel> assignRoleList = new();
            // Kullanıcının rolleri var mı varsa Exist = true'ya setleyelim.

            var userRoles = await _userManager.GetRolesAsync(user); // içerisine parametre olarak verilen kullanıcının rollerini string olarak döner.

            foreach (AppRole role in roleList)
            {
                // role entitylerinin tamamını dto'ya mapleyelim.
                var assignRoleToAppUserModel = new AssignRoleToAppUserViewModel
                {
                    Id = role.Id,
                    Name = role.Name,
                };
                // Şimdi Döngünün her turunda role atanması yapılacak kullanıcının sahip olduğu role var mı ona bakalım bu duruma göre existi true'ya setleyelim.
                if (userRoles.Contains(role.Name))
                    assignRoleToAppUserModel.Exist = true;

                assignRoleList.Add(assignRoleToAppUserModel);

            }
            return View(assignRoleList);


        }
        [HttpPost]
        public async Task<IActionResult> AssignRoleToAppUser(string userId,List<AssignRoleToAppUserViewModel> matchedRoleList)
        {
            // Rolleri atanacak kullanıcıyı bulalım.
            AppUser? user = (await _userManager.FindByIdAsync(userId))!;
            foreach (var role in matchedRoleList)
            {
                //Parametre olarak gelen atanması için seçilen ve seçilmeyen tüm roller üzerinde bir döngü kurduk,
                //bu sayede hangi role kullanıcı tarafından seçilmiş, hangisi seçilmemiş seçilenleri atayıp, seçilmeyenleri kullanıcıda varsa sileceğiz.

                if (role.Exist)
                    await _userManager.AddToRoleAsync(user,role.Name);
                
                else
                    await _userManager.RemoveFromRoleAsync(user,role.Name);  
                   
            }

            await _userManager.UpdateSecurityStampAsync(user);

            TempData["SuccessMessage"] = "Kullanıcıya rol atama işlemi başarılı bir şekilde gerçekleştirildi.";
            return RedirectToAction(nameof(HomeController.UserList),"Home");
        }
    
    }
}
                                                                                                                                                                      