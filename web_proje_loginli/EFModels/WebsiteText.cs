using System.ComponentModel.DataAnnotations;

namespace web_proje_loginli.EFModels
{
    public class WebsiteText
    {
        [Key]
        public int WebId { get; set; }
        public string MainPageText { get; set; }
        public string AboutText { get; set; }
    }
}
