using System.ComponentModel.DataAnnotations;

namespace web_proje_loginli.EFModels
{
    public class BotAddress
    {
        [Key]
        public int AddressId { get; set; }
        public string Website { get; set; }
        public string Regex { get; set; }
        public int ProxyTypeId { get; set; }
        public ProxyType ProxyType { get; set; }
    }
}
