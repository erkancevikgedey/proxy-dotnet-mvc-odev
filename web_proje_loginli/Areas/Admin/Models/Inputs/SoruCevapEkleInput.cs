using System.ComponentModel.DataAnnotations;

namespace web_proje_loginli.Areas.Admin.Models.Inputs
{
    public class SoruCevapEkleInput
    {
        [Required]
        public string Soru { get; set; }
        [Required]
        public string Cevap { get; set; }
    }
}
