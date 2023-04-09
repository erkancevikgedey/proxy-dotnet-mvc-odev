using System.ComponentModel.DataAnnotations;

namespace web_proje_loginli.EFModels
{
    public class Proxy
    {
        [Key]
        public int ProxyId { get; set; }
        public string IpAddress { get; set; }

        public string Port { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int ProxyTypeId { get; set; }
        public ProxyType ProxyType { get; set; }

        public DateTime AddTime { get; set; }
    }
}
