using System.ComponentModel.DataAnnotations;

namespace web_proje_loginli.Areas.Admin.Models.Inputs
{
    public class SoruCevapSilInput
    {
        [Required]
        public int Id { get; set; }
    }
}
