using System.ComponentModel.DataAnnotations;

namespace IdentityAppTutorial.Core.Models.AppUserViewModels
{
    public class SignInViewModel
    {
        [Required(ErrorMessage ="E-Posta Adresi boş bırakılamaz.")]
        [EmailAddress(ErrorMessage ="E-Posta formatı hatalı girdiniz.")]
        [Display(Name ="E-Posta")]
        public string Email { get; set; } = null!;
        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz.")]
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }
    }
}
