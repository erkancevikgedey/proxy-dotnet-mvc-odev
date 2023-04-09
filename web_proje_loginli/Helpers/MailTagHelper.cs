using Microsoft.AspNetCore.Razor.TagHelpers;

namespace web_proje_loginli.Helpers
{
    public class MailTagHelper : TagHelper
    {
        public string MailAdresi { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", $"mailto:{MailAdresi}");
            output.Content.SetContent("Yönetici Mail");
        }
    }
}
