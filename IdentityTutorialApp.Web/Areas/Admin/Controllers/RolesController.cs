using IdentityAppTutorial.Core.Entities;
using IdentityAppTutorial.Core.Models.AdminModels.RoleModels;
using IdentityTutorialApp.Web.Extensions.Microsoft;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
        [HttpGet]
        public async Task<IActionResult> RoleDelete(string roleId)
        {
            AppRole? deleteToRole = await _roleManager.FindByIdAsync(roleId);
            if (deleteToRole == null)
                throw new Exception("Silinmek istenen role sistemde bulunamadı.");
            IdentityResult? roleResult = await _roleManager.DeleteAsync(deleteToRole);
            if (!roleResult.Succeeded)
                ModelState.AddModelErrorList(roleResult.Errors.Select(e => e.Description).ToList());
            TempData["SuccessMessage"] = "Silme işlemi başarıyla gerçekleştirildi.";
            return Redirect(nameof(Index));
   
        }
    }
}
                                                                                                                                                                      