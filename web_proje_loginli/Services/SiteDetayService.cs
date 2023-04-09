using web_proje_loginli.Data;
using web_proje_loginli.EFModels;

namespace web_proje_loginli.Services
{
    public class SiteDetayService : ISiteDetayService
    {
        private readonly ApplicationDbContext veriContext;

        public SiteDetayService(ApplicationDbContext veriContext)
        {
            this.veriContext = veriContext;
        }
        public List<QA> GetSSSs()
        {
            return veriContext.QA.ToList();
        }

        public bool AddSSS(string soru, string cevap)
        {
            QA eklenecek = new QA()
            {
                Question = soru,
                Answer = cevap,
            };

            try
            {
                veriContext.QA.Add(eklenecek);
                veriContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public QA GetSSS(int id)
        {
            return veriContext.QA.Where(x => x.QAId == id).FirstOrDefault();
        }

        public bool RemoveSSS(int id)
        {
            var sss = veriContext.QA.Where(x => x.QAId == id).FirstOrDefault();
            if (sss != null)
            {
                try
                {
                    veriContext.Remove(sss);
                    veriContext.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool EditSSS(QA sss)
        {
            try
            {
                veriContext.Update(sss);
                veriContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateWelcomeText(string metin)
        {
            var veri = veriContext.WebsiteText.FirstOrDefault();
            if(veri != null)
            {
                veri.MainPageText = metin;
                try
                {
                    veriContext.Update(veri);
                    veriContext.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool UpdateAboutText(string metin)
        {
            var veri = veriContext.WebsiteText.FirstOrDefault();
            if (veri != null)
            {
                veri.AboutText = metin;
                try
                {
                    veriContext.Update(veri);
                    veriContext.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public string GetWelcomeText()
        {
            var veri = veriContext.WebsiteText.FirstOrDefault();
            if (veri != null)
            {
                return veri.MainPageText;
            }
            else
            {
                return "Sisteme girilmemiş";
            }
        }

        public string GetAboutText()
        {
            var veri = veriContext.WebsiteText.FirstOrDefault();
            if (veri != null)
            {
                return veri.AboutText;
            }
            else
            {
                return "Sisteme girilmemiş";
            }
        }
    }
}
