using System.ComponentModel.DataAnnotations;

namespace IdentityAppTutorial.Core.Models.AppUserViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Şifre alanı zorunlu bir alandır.")]
        [Display(Name = "Yeni Şifre")]
        public string? Password { get; set; }
        [Compare(nameof(Password), ErrorMessage = "Şifre aynı değildir.")]
        [Required(ErrorMessage = "Şifre tekrar alanı zorunlu bir alandır.")]
        [Display(Name = "Yeni Şifre Tekrar")]
        public string? PasswordConfirm { get; set; }
    }
}
