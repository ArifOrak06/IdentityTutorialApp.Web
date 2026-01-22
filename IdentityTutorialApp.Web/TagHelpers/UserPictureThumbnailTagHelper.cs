using Microsoft.AspNetCore.Razor.TagHelpers;

namespace IdentityTutorialApp.Web.TagHelpers
{
    public class UserPictureThumbnailTagHelper : TagHelper
    {
        public string? PictureUrl { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";

            if (string.IsNullOrEmpty(PictureUrl))
            {
                output.Attributes.SetAttribute("src", "./user-pictures/default-user-picture.png");
            }
            else
            {
                output.Attributes.SetAttribute("src", $"./user-pictures/{PictureUrl}");
            }

            // Output.TagName ile hangi etiket üzerinde işlem yapacaksak onu belirliyoruz, daha sonra ise parametre olarak alacağımız PictureUrl View sayfasındaki Model'in Picture propertysine 
            //karşılık gelen fieldı ifade eder.
        }
    }
}
