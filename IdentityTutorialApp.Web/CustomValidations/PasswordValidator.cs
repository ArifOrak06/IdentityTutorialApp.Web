using IdentityAppTutorial.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityTutorialApp.Web.CustomValidations
{
    public class PasswordValidator : IPasswordValidator<AppUser>
    {
        public async Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string? password)
        {
            var errors = new List<IdentityError>();
            if (password!.ToLower().Contains(user.UserName!.ToLower()))
                errors.Add(new IdentityError()
                {
                    Code = "PasswordNoContainsUserName",
                    Description = "Şifre alanı kullanıcı adı içeremez."
                });
            if (password!.ToLower().StartsWith("1234"))
                errors.Add(new()
                {
                    Code = "PasswordNoContain1234",
                    Description = "Parola ardışık sayı içeremez",
                });
            // Parolada custom olarak hazırlanan yukarıdaki ihlaller oluşursa Failed olarak matchleyip içerisine meydana gelen ihlal mesajlarını yerleştiriyoruz..
            if (errors.Any())
                return await Task.FromResult(IdentityResult.Failed(errors.ToArray()));

            // Hata Yoksa 

            return await Task.FromResult(IdentityResult.Success);
        }

    }
}
