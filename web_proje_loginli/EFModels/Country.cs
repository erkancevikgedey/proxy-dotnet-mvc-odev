using System.ComponentModel.DataAnnotations;

namespace web_proje_loginli.EFModels
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }
}