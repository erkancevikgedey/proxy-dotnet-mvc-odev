using System.ComponentModel.DataAnnotations;

namespace web_proje_loginli.EFModels
{
    public class QA
    {
        [Key]
        public int QAId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
