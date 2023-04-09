using System.ComponentModel.DataAnnotations;

namespace web_proje_loginli.Models.Api.Inputs
{
	public class ApiCheckerInput
	{
        [Required]
        [RegularExpression(@"\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?):\d{1,5}\b")]
		public string ProxyIpPortData { get; set; }
		[Required]
		public string Tur { get; set; }
	}
}
