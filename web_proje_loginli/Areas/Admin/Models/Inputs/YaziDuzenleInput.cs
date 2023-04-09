using System.ComponentModel.DataAnnotations;

namespace web_proje_loginli.Areas.Admin.Models.Inputs
{
    public class YaziDuzenleInput
    {
        [Required]
        public string AboutText { get; set; }
        [Required]
        public string MainText { get; set; } 
    }
}
