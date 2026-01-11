using System.ComponentModel.DataAnnotations;

namespace IdentityAppTutorial.Core.Models.AppUserViewModels
{
    public class PasswordChangeViewModel
    {
        public string PasswordOld { get; set; }
        [Required(ErrorMessage = "Şifre alanı boş geçilemez.")]
        [Display(Name = "Yeni Şifre")]
        [MinLength(6,ErrorMessage = "Şifreniz En az 6 karakterden oluşturulmalıdır.")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Yeni Şifre Tekrar alanı boş geçilemez.")]
        [Display(Name = "Yeni Şifre Tekrar")]
        [Compare(nameof(NewPassword),ErrorMessage ="Yeni şifreler uyuşmuyor.")]
        [MinLength(6, ErrorMessage = "Şifreniz En az 6 karakterden oluşturulmalıdır.")]
        public string NewPasswordConfirm { get; set; }
    }
}
