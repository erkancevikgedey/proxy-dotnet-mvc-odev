namespace web_proje_loginli.Models.Api.Inputs
{
    public class GetProxylerInput
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public int Draw { get; set; }
        public SearchDto Search { get; set; }

        public string TurId { get; set; }
    }

    public class SearchDto
    {
        public string Value { get; set; }
        public string Regex { get; set; }
    }
}
