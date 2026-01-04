using IdentityAppTutorial.Core.Models.EmailModels;
using IdentityAppTutorial.Core.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace IdentityTutorialApp.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;   
        }
        public async Task SendResetPasswordEmail(string resetPasswordEmailLink, string toEmail)
        {
            // Öncelikle smtp service'imizi oluşturalım.
            var smtpClient = new SmtpClient();
            smtpClient.Host = _emailSettings.Host;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);
            smtpClient.EnableSsl = true;

            // MailMessage oluşturma
            var mailMessage = new MailMessage();
            // mail kimden gidiyor ? 
            mailMessage.From = new MailAddress(_emailSettings.Email);
            // Mail Kime gidecek ? 
            mailMessage.To.Add(toEmail);
            // mail konusu veya başlığı
            mailMessage.Subject = "Localhos | Şifre Sıfırlama Linki";
            // mail içeriği
            mailMessage.Body = @$"

          <h4>Şifrenizi yenilemek için aşağıdaki linke tıklayınız.</h4>
          <p><a href='{resetPasswordEmailLink}'>Şifre Sıfırlama Linki</a></p>     ";

            // mail body/içeriğinde html etiketleri kullandığımız için mailin html okuma modunu açalım
            mailMessage.IsBodyHtml = true;

            //maili gönderelim.

            await smtpClient.SendMailAsync(mailMessage);


        }
    }
}
