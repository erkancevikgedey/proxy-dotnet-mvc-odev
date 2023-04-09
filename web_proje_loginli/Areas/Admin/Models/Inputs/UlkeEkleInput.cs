using System.ComponentModel.DataAnnotations;

namespace web_proje_loginli.Areas.Admin.Models.Inputs
{
    public class UlkeEkleInput
    {
        [Required]
        public string UlkeAdi { get; set; }
    }
}
