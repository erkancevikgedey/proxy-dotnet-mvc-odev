using System.ComponentModel.DataAnnotations;

namespace web_proje_loginli.Areas.Admin.Models.Inputs
{
    public class BotSitesiDuzenleInput
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)", ErrorMessage = "Geçersiz regex")]
        public string DuzenlenecekSiteAdresi { get; set; }
        [Required]
        [RegularExpression(@"^(?:(?:[^?+*{}()[\]\\|]+|\\.|\[(?:\^?\\.|\^[^\\]|[^\\^])(?:[^\]\\]+|\\.)*\]|\((?:\?[:=!]|\?<[=!]|\?>|\?<[^\W\d]\w*>|\?'[^\W\d]\w*')?(?<N>)|\)(?<-N>))(?:(?:[?+*]|\{\d+(?:,\d*)?\})[?+]?)?|\|)*$(?(N)(?!))", ErrorMessage = "Geçersiz regex")]
        public string DuzenlenecekRegex { get; set; }

        [Required]
        public int EklenecekTurId { get; set; }
    }
}
