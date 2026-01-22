using IdentityAppTutorial.Core.Models.PictureModels;
using Microsoft.AspNetCore.Http;

namespace IdentityAppTutorial.Core.Helpers
{
    public interface IImgHelper
    {
        Task<PictureUploadViewModel> UploadPictureAsync(IFormFile pictureFile);


    }
}
