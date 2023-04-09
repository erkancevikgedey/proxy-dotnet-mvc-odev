using System.ComponentModel.DataAnnotations;

namespace web_proje_loginli.EFModels
{
    public class ProxyType
    {
        [Key]
        public int ProxyTypeId { get; set; }
        public string Type { get; set; }
    }
}