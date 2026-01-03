using Microsoft.AspNetCore.Identity;

namespace IdentityTutorialApp.Web.Localizations
{
    public class LocalizationsIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateUserName(string userName)
        {
            return new()
            {
                Code = "DuplicateUserName",
                Description = $"Bu {userName} kullanıcı adı daha önce başka bir kullanıcı tarafından kullanılmıştır.",
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new()
            {
                Code = "PasswordTooShort",
                Description = $"Şifre en az 6 karakterden oluşturulmalıdır, siz {length} karakter girdiniz.",
            };
        }
    }
}
