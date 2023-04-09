using System.ComponentModel.DataAnnotations;

namespace web_proje_loginli.Areas.Admin.Models.Inputs
{
    public class SoruCevapDuzenleInput
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string DuzenlenmisSoru { get; set; }
        [Required]
        public string DuzenlenmisCevap { get; set; }
    }
}
