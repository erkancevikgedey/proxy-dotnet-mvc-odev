using web_proje_loginli.EFModels;

namespace web_proje_loginli.Services
{
    public interface ISiteDetayService
    {
        bool EditSSS(QA sss);
        QA GetSSS(int id);
        List<QA> GetSSSs();
        bool RemoveSSS(int id);
        public bool AddSSS(string soru, string cevap);
        public bool UpdateWelcomeText(string metin);
        public bool UpdateAboutText(string metin);
        public string GetWelcomeText();
        public string GetAboutText();
    }
}