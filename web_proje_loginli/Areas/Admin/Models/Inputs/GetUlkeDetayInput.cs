using System.ComponentModel.DataAnnotations;

namespace web_proje_loginli.Areas.Admin.Models.Inputs
{
	public class GetUlkeDetayInput
	{
		[Required]
		public int Id { get; set; }
	}
}
