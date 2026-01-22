using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace IdentityAppTutorial.Core.Models.AppUserViewModels
{
    public class UserEditViewModel
    {
        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı adı alanı zorunlu bir alandır.")]
        public string?UserName { get; set; } = null!;
        [Required(ErrorMessage = "E-posta alanı zorunlu bir alandır.")]
        [EmailAddress(ErrorMessage = "E-posta formatı yanlıştır.")]
        [Display(Name = "E-Posta")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Telofon alanı zorunlu bir alandır.")]
        [Display(Name = "GSM")]
        public string Phone { get; set; } = null!;
        [DataType(DataType.Date)]
        [Display(Name = "Doğum Tarihi")]
        public DateTime? BirthDate { get; set; }
        [Display(Name ="Şehir")]
        public string?  City  { get; set; }
        [Display(Name ="Fotoğraf")]
        public IFormFile? Picture { get; set; }
        [Display(Name ="Cinsiyet")]
        public Gender? Gender { get; set; }
        public string? PictureUrl { get; set; }

    }
}
