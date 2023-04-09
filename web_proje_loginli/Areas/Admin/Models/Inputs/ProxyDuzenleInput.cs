using System.ComponentModel.DataAnnotations;

namespace web_proje_loginli.Areas.Admin.Models.Inputs
{
    public class ProxyDuzenleInput
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string IpAddress { get; set; }
        [Required]
        public string Port { get; set; }
        [Required]
        public int CountryId { get; set; }
        [Required]
        public int ProxyTypeId { get; set; }

    }
}
