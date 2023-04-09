using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web_proje_loginli.Areas.Admin.Models.Inputs;
using web_proje_loginli.EFModels;
using web_proje_loginli.Services;

namespace web_proje_loginli.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ApiController : Controller
    {
        private readonly IProxyDetailsService proxyDetailsService;
        private readonly IBotDetailsService botDetailsService;
        private readonly IMessageDetailsService messageDetailsService;
        private readonly ISiteDetayService siteDetayService;

        public ApiController(IProxyDetailsService proxyDetailsService, IBotDetailsService botDetailsService, IMessageDetailsService messageDetailsService, ISiteDetayService siteDetayService)
        {
            this.proxyDetailsService = proxyDetailsService;
            this.botDetailsService = botDetailsService;
            this.messageDetailsService = messageDetailsService;
            this.siteDetayService = siteDetayService;
        }
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public IActionResult GetAnasayfaSayisalVeriler()
        {
            var kacSaatOnceProxyEklendi = Convert.ToInt32((DateTime.Now - proxyDetailsService.GetProxies().OrderByDescending(x => x.AddTime).FirstOrDefault().AddTime).TotalHours);
            var kacSaatOnceCheckerCalisti = Convert.ToInt32((DateTime.Now - proxyDetailsService.GetCheckedProxies().OrderByDescending(x => x.CheckTime).FirstOrDefault().CheckTime).TotalHours);
            var kacAdetVeriBotSitesi = botDetailsService.GetBotAddresses().Count();
            var kacAdetMesaj = messageDetailsService.GetAllMessages().Count();
            return Json(new { kacSaatOnceProxyEklendi= kacSaatOnceProxyEklendi, kacSaatOnceCheckerCalisti= kacSaatOnceCheckerCalisti, kacAdetVeriBotSitesi= kacAdetVeriBotSitesi, kacAdetMesaj= kacAdetMesaj });
        }

        [HttpPost]
        public IActionResult GetProxyler()
        {
            List<Proxy> proxyList = proxyDetailsService.GetProxies();
            return Json(proxyList);
        }

        [HttpPost]
        public IActionResult GetBotSiteleri()
        {
            List<BotAddress> botList = botDetailsService.GetBotAddresses();
            return Json(botList);
        }

        [HttpPost]
        public IActionResult GetMesajlar()
        {
            List<ContactMessage> messageList = messageDetailsService.GetAllMessages();
            return Json(messageList);
        }

        [HttpPost]
        public IActionResult GetSiteDetaylar()
        {
            string anasayfa = siteDetayService.GetWelcomeText();
            string hakkimizda = siteDetayService.GetAboutText();
            return Json(new {anasayfa=anasayfa, hakkimizda=hakkimizda});
        }

        [HttpPost]
        public IActionResult GetBotSitesiDetay(GetBotSitesiDetayInput giris)
        {
            if (ModelState.IsValid)
            {
                BotAddress botSitesi = botDetailsService.GetBotAddress(giris.Id);
                return Json(botSitesi);
            }
            else
            {
                return Json(new { Error = "Lütfen bir proxy bilgisi giriniz." });
            }
        }

        [HttpPost]
        public IActionResult GetProxyDetay(GetProxyDetayInput giris)
        {
            if (ModelState.IsValid)
            {
                Proxy proxy = proxyDetailsService.GetProxy(giris.Id);
                return Json(proxy);
            }
            else
            {
                return Json(new { Error = "Lütfen bir proxy bilgisi giriniz." });
            }
        }

        [HttpPost]
        public IActionResult GetUlkeDetay(GetUlkeDetayInput giris)
        {
            if (ModelState.IsValid)
            {
                Country country = proxyDetailsService.GetCountry(giris.Id);
                return Json(country);
            }
            else
            {
                return Json(new { Error = "Lütfen bir ülke bilgisi giriniz." });
            }
        }

        [HttpPost]
        public IActionResult GetSSSDetay(GetSSSDetayInput giris)
        {
            if (ModelState.IsValid)
            {
                QA sss = siteDetayService.GetSSS(giris.Id);
                return Json(sss);
            }
            else
            {
                return Json(new { Error = "Lütfen bir ülke bilgisi giriniz." });
            }
        }

        [HttpPost]
        public IActionResult GetTurler()
        {
            List<ProxyType> proxyList = proxyDetailsService.GetProxyTypes();
            return Json(proxyList);
        }

        [HttpPost]
        public IActionResult GetUlkeler()
        {
            List<Country> proxyList = proxyDetailsService.GetCountries();
            return Json(proxyList);
        }

        [HttpPost]
        public IActionResult GetBeklenProxyler()
        {
            List<CheckedProxy> proxyCheckedList = proxyDetailsService.GetCheckedProxies();
            return Json(proxyCheckedList);
        }

        [HttpPost]
        public IActionResult GetSoruCevaplar()
        {
            List<QA> sssListesi = siteDetayService.GetSSSs();
            return Json(sssListesi);
        }

        [HttpPost]
        public IActionResult UlkeEkle(UlkeEkleInput giris)
        {
            if (ModelState.IsValid)
            {
                Country eklenecekUlke = new Country();
                eklenecekUlke.CountryName = giris.UlkeAdi;
                var durum = proxyDetailsService.AddCountry(eklenecekUlke);
				if (durum)
				{
                    return Json(new { Success = "Başarıyla eklendi" });
				}
				else
				{
                    return Json(new { Error = "Eklenirken bir hata oluştu." });
                }
            }
            else
            {
                return Json(new { Error = "Lütfen bir ülke adı giriniz." });
            }
        }

        [HttpPost]
        public IActionResult ProxyEkle(ProxyEkleInput giris)
        {
            if (ModelState.IsValid)
            {
                Proxy proxy = new Proxy()
                {
                    AddTime = DateTime.Now,
                    CountryId = giris.CountryId,
                    IpAddress = giris.IpAddress,
                    Port = giris.Port,
                    ProxyTypeId = giris.ProxyTypeId
                };

                var durum = proxyDetailsService.AddProxy(proxy);
				if (durum)
				{
                    return Json(new { Success = "Başarıyla eklendi" });
				}
				else
				{
                    return Json(new { Error = "Eklenirken bir hata oluştu" });
                }
               
            }
            else
            {
                return Json(new { Error = "Geçerli bilgiler giriniz." });
            }
        }

        [HttpPost]
        public IActionResult ProxyDuzenle(ProxyDuzenleInput giris)
        {
            if (ModelState.IsValid)
            {
                var duzenlenecek = proxyDetailsService.GetProxy(giris.Id);
                if(duzenlenecek != null)
				{
                    duzenlenecek.ProxyTypeId = giris.ProxyTypeId;
                    duzenlenecek.CountryId = giris.CountryId;
                    duzenlenecek.IpAddress = giris.IpAddress;
                    duzenlenecek.Port = giris.Port;

                    var durum = proxyDetailsService.EditProxy(duzenlenecek);
					if (durum)
					{
                        return Json(new { Success = "Başarıyla duzenlendi" });
					}
					else
					{
                        return Json(new { Error = "Düzenlenemedi." });
                    }
                    
                }
				else
				{
                    return Json(new { Error = "Yanlış bir şeyler gitti ve veriler boş olmamalı." });
                }
            }
            else
            {
                return Json(new { Error = "Yanlış bir şeyler gitti ve veriler boş olmamalı." });
            }
        }

        [HttpPost]
        public IActionResult UlkeDuzenle(UlkeDuzenleInput giris)
        {
            if (ModelState.IsValid)
            {
                var duzenlenecek = proxyDetailsService.GetCountry(giris.Id);
                if (duzenlenecek != null)
                {
                    duzenlenecek.CountryName = giris.DuzenlenmisAd;

                    var durum = proxyDetailsService.EditCountry(duzenlenecek);
					if (durum)
                    {
                        return Json(new { Success = "Başarıyla duzenlendi"});
					}
					else
					{
                        return Json(new { Error = "Düzenlenemedi." });
                    }
				}
				else
				{
                    return Json(new { Error = "Yanlış bir şeyler gitti ve veriler boş olmamalı." });
                } 
            }
            else
            {
                return Json(new { Error = "Yanlış bir şeyler gitti ve veriler boş olmamalı." });
            }
        }

        [HttpPost]
        public IActionResult Ulkesil(UlkeSilInput giris)
        {
            if (ModelState.IsValid)
            {
                var durum = proxyDetailsService.RemoveCountry(giris.Id);
				if (durum)
				{
                    return Json(new { Success = "Başarıyla silindi"});
				}
				else
				{
                    return Json(new { Error = "Silinirken bir hata oluştu" });
                }
            }
            else
            {
                return Json(new { Error = "Yanlış bir şeyler gitti ve veriler boş olmamalı." });
            }
        }

        public IActionResult ProxySil(ProxySilInput giris)
        {
            if (ModelState.IsValid)
            {
                var id = giris.Id;
                var durum = proxyDetailsService.RemoveProxy(id);
                if (durum)
                {
                    return Json(new { Success = "Başarıyla silindi"});
                }
                else
                {
                    return Json(new { Error = "Silinemedi." });
                }
            }
            else
            {
                return Json(new { Error = "Yanlış bir şeyler gitti ve veriler boş olmamalı." });
            }
        }

        [HttpPost]
        public IActionResult BotSitesiEkle(BotSitesiEkleInput giris)
        {
            if (ModelState.IsValid)
            {
                var durum = botDetailsService.AddBotSitesi(giris.EklenecekSite, giris.EklenecekRegex, giris.EklenecekTurId);
                if (durum)
                {
                    return Json(new { Success = "Başarıyla eklendi"});
                }
                else
                {
                    return Json(new { Error = "Eklenirken bir hata oluştu" });
                }
                
            }
            else
            {
                return Json(new { Error = "Yanlış bir şeyler gitti, regex doğru ve veriler boş olmamalı." });
            }
        }
        [HttpPost]
        public IActionResult BotCalistir()
        {
            var dto = proxyDetailsService.BotuBaslat();

            if (dto.Durum)
            {
                return Json(new { Success = "Başarıyla bot çalıştırıldı " +dto.Sayi+ " adet yeni veri eklendi." });
            }
            else
            {
                return Json(new { Error = "Hata: "+dto.Hata });
            }
            
            //TODO: SERVİS BAĞLANIP GET İSTEKLERİYLE REGEXLER BİRER BİRER ÇALIŞTIRILACAK
        }

        [HttpPost]
        public IActionResult BotSitesiDuzenle(BotSitesiDuzenleInput giris)
        {
            if (ModelState.IsValid)
            {
                var duzenlenecek = botDetailsService.GetBotAddress(giris.Id);
                duzenlenecek.Website = giris.DuzenlenecekSiteAdresi;
                duzenlenecek.Regex = giris.DuzenlenecekRegex;
                duzenlenecek.ProxyTypeId = giris.EklenecekTurId;

                var durum = botDetailsService.EditBotSitesi(duzenlenecek);
                if (durum)
                {
                    return Json(new { Success = "Başarıyla düzenlendi"});
                }
                else
                {
                    return Json(new { Error = "Düzenlenme sırasında bir sorun oluştu" });
                }
            }
            else
            {
                return Json(new { Error = "Yanlış bir şeyler gitti, regex doğru ve veriler boş olmamalı." });
            }
        }

        [HttpPost]
        public IActionResult BotSitesiSil(BotSitesiSilInput giris)
        {
            if (ModelState.IsValid)
            {
                var durum = botDetailsService.RemoveBotSitesi(giris.Id);
                if (durum)
                {
                    return Json(new { Success = "Başarıyla silindi"});

                }
                else
                {
                    return Json(new { Error = "Silinirken bir hata oluştu" });
                }
            }
            else
            {
                return Json(new { Error = "Yanlış bir şeyler gitti." });
            }
        }

        [HttpPost]

        public IActionResult MesajSil(MesajSilInput giris)
        {
            if (ModelState.IsValid)
            {
                var durum = messageDetailsService.RemoveMessage(giris.Id);
                if (durum)
                {
                    return Json(new { Success = "Başarıyla silindi"});
                }
                else
                {
                    return Json(new { Error = "Silinirken bir hata oluştu" });
                }
            }
            else
            {
                return Json(new { Error = "Yanlış bir şeyler gitti." });
            }
        }

        [HttpPost]

        public IActionResult MesajGoruntule(MesajGoruntuleInput giris)
        {
            if (ModelState.IsValid)
            {
                var mesaj = messageDetailsService.GetMessage(giris.Id);
                if(mesaj != null)
                {
                    return Json(mesaj);
                }
                else
                {
                    return Json(new { Error = "Mesaj bulunamadı." });
                }
            }
            else
            {
                return Json(new { Error = "Yanlış bir şeyler gitti." });
            }
        }

        [HttpPost]

        public IActionResult BekleyenProxySil(BekleyenProxySilInput giris)
        {
            if (ModelState.IsValid)
            {
                var durum = proxyDetailsService.RemoveCheckedProxy(giris.Id);
                if (durum)
                {
                    return Json(new { Success = "Başarıyla silindi"});
                }
                else
                {
                    return Json(new { Error = "Yanlış bir şeyler gitti." });
                }
            }
            else
            {
                return Json(new { Error = "Veri girişi sorunu." });
            }
        }

        [HttpPost]

        public IActionResult BekleyenProxyOnayla(BekleyenProxyOnaylaInput giris)
        {
            if (ModelState.IsValid)
            {
                var durum = proxyDetailsService.AcceptCheckedProxy(giris.Id);
                if (durum)
                {
                    var durum2 = proxyDetailsService.RemoveCheckedProxy(giris.Id);
                    if (durum2)
                    {
                        return Json(new { Success = "Başarıyla onaylandı" });
                    }
                    else
                    {
                        return Json(new { Error = "Yanlış bir şeyler gitti." });
                    }
                }
                else
                {
                    return Json(new { Error = "Yanlış bir şeyler gitti." });
                }
            }
            else
            {
                return Json(new { Error = "Veri girişi yanlış." });
            }
        }

        [HttpPost]

        public IActionResult SoruCevapEkle(SoruCevapEkleInput giris)
        {
            if (ModelState.IsValid)
            {
                var durum = siteDetayService.AddSSS(giris.Soru, giris.Cevap);
                if (durum)
                {
                    return Json(new { Success = "Başarıyla eklendi"});
                }
                else
                {
                    return Json(new { Error = "Eklenirken bir hata oluştu" });
                }
            }
            else
            {
                return Json(new { Error = "Yanlış bir şeyler gitti." });
            }
        }

        [HttpPost]

        public IActionResult SoruCevapDuzenle(SoruCevapDuzenleInput giris)
        {
            if (ModelState.IsValid)
            {
                var duzenlenecek = siteDetayService.GetSSS(giris.Id);
                duzenlenecek.Question = giris.DuzenlenmisSoru;
                duzenlenecek.Answer = giris.DuzenlenmisCevap;
                var durum = siteDetayService.EditSSS(duzenlenecek);
                if (durum)
                {
                    return Json(new { Success = "Başarıyla duzenlendi" });
                }
                else
                {
                    return Json(new { Error = "Düzenleme sırasında hata oluştu." });
                }
            }
            else
            {
                return Json(new { Error = "Yanlış bir şeyler gitti." });
            }
        }

        [HttpPost]

        public IActionResult SoruCevapSil(SoruCevapSilInput giris)
        {
            if (ModelState.IsValid)
            {
                var durum = siteDetayService.RemoveSSS(giris.Id);
                if (durum)
                {
                    return Json(new { Success = "Başarıyla silindi"});
                }
                else
                {
                    return Json(new { Error = "Silinirken bir hata oluştu." });
                }
            }
            else
            {
                return Json(new { Error = "Yanlış bir şeyler gitti." });
            }
        }

        [HttpPost]

        public IActionResult YaziDuzenle(YaziDuzenleInput giris)
        {
            if (ModelState.IsValid)
            {
                var durum1 = siteDetayService.UpdateWelcomeText(giris.MainText);
                var durum2 = siteDetayService.UpdateAboutText(giris.AboutText);
                if (durum1 && durum2)
                {
                    return Json(new { Success = "Başarıyla güncellendi" });
                }
                else
                {
                    return Json(new { Error = "Güncelleme sırasında hata oluştu." });
                }
            }
            else
            {
                return Json(new { Error = "Yanlış bir şeyler gitti." });
            }
        }
    }
}
