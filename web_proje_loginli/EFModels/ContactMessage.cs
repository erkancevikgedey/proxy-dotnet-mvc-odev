using System.ComponentModel.DataAnnotations;

namespace web_proje_loginli.EFModels
{
    public class ContactMessage
    {
        [Key]
        public int MessageId { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }

    }
}
