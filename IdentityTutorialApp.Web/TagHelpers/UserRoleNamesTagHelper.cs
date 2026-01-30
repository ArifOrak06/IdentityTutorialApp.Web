using IdentityAppTutorial.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace IdentityTutorialApp.Web.TagHelpers
{
    public class UserRoleNamesTagHelper : TagHelper
    {
        public string UserId { get; set; }
        private readonly UserManager<AppUser> _userManager;

        public UserRoleNamesTagHelper(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            // Öncelikle rolleri listelenecek kullanıcıyı bulalım
            var user = await _userManager.FindByIdAsync(UserId);

            // Daha sonra kullanıcının rollerini getirelim.
            var userRoles = await _userManager.GetRolesAsync(user!);

            var stringBuilder = new StringBuilder();

            userRoles.ToList().ForEach(x =>
            {

                stringBuilder.Append(@$"<span class='badge bg-secondary ml-1'>{x.ToLower()}</span>");
                
            });

            output.Content.SetHtmlContent(stringBuilder.ToString());

        }
    }
}
