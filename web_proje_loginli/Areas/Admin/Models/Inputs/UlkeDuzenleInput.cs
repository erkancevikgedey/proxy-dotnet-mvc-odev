using System.ComponentModel.DataAnnotations;

namespace web_proje_loginli.Areas.Admin.Models.Inputs
{
    public class UlkeDuzenleInput
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string DuzenlenmisAd { get; set; }
    }
}
