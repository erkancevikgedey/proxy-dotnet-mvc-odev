using System.ComponentModel.DataAnnotations;

namespace web_proje_loginli.Models.Inputs
{
    public class IletisimInput
    {
        [Required(ErrorMessage = "Hata: Mail Adresiniz Boş Olamaz")]
        [EmailAddress(ErrorMessage = "Hata: Geçerli Bir Mail Adresi Girmediniz")]
        public string MesajMail { get; set; }
        [Required(ErrorMessage = "Hata: Mesajınız Boş Olamaz")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Mesajınız {2} ile {1} karakter arasında olmalıdır.")]
        public string MesajIcerik { get; set; }
    }
}
