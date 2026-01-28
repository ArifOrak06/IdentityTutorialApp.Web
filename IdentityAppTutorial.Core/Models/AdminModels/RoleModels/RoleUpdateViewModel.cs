using System.ComponentModel.DataAnnotations;

namespace IdentityAppTutorial.Core.Models.AdminModels.RoleModels
{
    public class RoleUpdateViewModel
    {
        public string Id { get; set; } = null!;
        [Required(ErrorMessage = "Role Adı alanı boş geçilemez.")]
        [Display(Name = "Role Adı")]
        public string Name { get; set; } = null!;
    }
}
