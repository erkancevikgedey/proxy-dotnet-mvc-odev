using System.ComponentModel.DataAnnotations;

namespace web_proje_loginli.Areas.Admin.Models.Inputs
{
	public class ProxyEkleInput
	{
		[Required]
		[RegularExpression(@"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4}$")]
		public string IpAddress { get; set; }
		[Required]
		[RegularExpression(@"^([1-9][0-9]{0,3}|[1-5][0-9]{4}|6[0-4][0-9]{3}|65[0-4][0-9]{2}|655[0-2][0-9]|6553[0-5])$")]
		public string Port { get; set; }
		[Required]
		public int CountryId { get; set; }
		[Required]
		public int ProxyTypeId { get; set; }
	}
}
