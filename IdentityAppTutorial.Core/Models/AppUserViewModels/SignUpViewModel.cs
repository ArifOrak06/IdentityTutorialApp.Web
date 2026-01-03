using System.ComponentModel.DataAnnotations;

namespace IdentityAppTutorial.Core.Models.AppUserViewModels
{
    public class SignUpViewModel
    {
        [Display(Name ="Kullanıcı Adı")]
        [Required(ErrorMessage ="Kullanıcı adı alanı zorunlu bir alandır.")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "E-posta alanı zorunlu bir alandır.")]
        [EmailAddress(ErrorMessage ="E-posta formatı yanlıştır.")]
        [Display(Name = "E-Posta")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Telofon alanı zorunlu bir alandır.")]
        [Display(Name = "GSM")]
        public string? Phone { get; set; }
        [Required(ErrorMessage ="Şifre alanı zorunlu bir alandır.")]
        [Display(Name = "Şifre")]
        public string? Password { get; set; }
        [Compare(nameof(Password),ErrorMessage ="Şifre aynı değildir.")]
        [Required(ErrorMessage ="Şifre tekrar alanı zorunlu bir alandır.")]
        [Display(Name ="Şifre Tekrar")]
        public string? PasswordConfirm { get; set; }
    }
}
