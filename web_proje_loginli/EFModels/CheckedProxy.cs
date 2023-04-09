using System.ComponentModel.DataAnnotations;

namespace web_proje_loginli.EFModels
{
    public class CheckedProxy
    {
        [Key]
        public int CheckId { get; set; }
        public string IpAddress { get; set; }
        public string Port { get; set; }
        public int ProxyTypeId { get; set; }
        public ProxyType ProxyType { get; set; }
        public DateTime CheckTime { get; set; }
    }
}
