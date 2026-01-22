
using IdentityAppTutorial.Core.Models.PictureModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace IdentityAppTutorial.Core.Helpers
{
    public class ImgHelper : IImgHelper
    {
        private readonly IFileProvider _fileProvider;

        public ImgHelper(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public async Task<PictureUploadViewModel> UploadPictureAsync(IFormFile pictureFile)
        {
            // Öncelikle parametre olarak gönderilen image dosyasının validasyon kontrolünü yapalım.

            if (pictureFile != null && pictureFile.Length > 0)
            {
                // base dizinini yani Uı katmanı içerisinde resimleri kaydedeceğimiz wwwroot dizinine ulaşalım.
                var wwrootFolder = _fileProvider.GetDirectoryContents("wwwroot");
                // Daha Sonra kaydedilecek olan image dosyasına random bir isim oluşturalım.
                var randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(pictureFile.FileName)}";
                // daha sonra yeni resmin kaydedileceği yeni dosya yolunu bulup, bu dosya yoluna hangi isimle kaydedileceğini bildirelim.
                var newPicturePath = Path.Combine(wwrootFolder.First(x => x.Name == "user-pictures").PhysicalPath, randomFileName);
                // Artık Resim kaydetme işlemini yapalım.
                using var stream = new FileStream(newPicturePath, FileMode.Create);


                await pictureFile.CopyToAsync(stream);
           
                 
            }
            return new PictureUploadViewModel()
            {
                FileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(pictureFile.FileName)}",
            };

        }
    }
}
