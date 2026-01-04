using System.ComponentModel.DataAnnotations;

namespace IdentityAppTutorial.Core.Models.AppUserViewModels
{
    public class ForgetPasswordViewModel
    {
        [Display(Name ="E-Posta Adresi")]
        [Required(ErrorMessage ="{0} alanı boş geçilemez.")]
        [EmailAddress(ErrorMessage ="Lütfen geçerli bir {0} adresi giriniz.")]
        public string Email { get; set; } = null!;
    }
}
