using Hafta11.MyExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Web;
using web_proje_loginli.Data;
using web_proje_loginli.EFModels;
using web_proje_loginli.Models.Api;
using web_proje_loginli.Models.Api.Inputs;
using web_proje_loginli.Services;

namespace web_proje_loginli.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class ApiController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<IdentityUser> _userManager;
        private readonly IProxyDetailsService proxyDetailsService;
        private readonly ISiteDetayService siteDetayService;

        public ApiController(RoleManager<IdentityRole> roleMgr, UserManager<IdentityUser> userManager, IProxyDetailsService proxyDetailsService, ISiteDetayService siteDetayService)
        {
            _roleManager = roleMgr;
            _userManager = userManager;
            this.proxyDetailsService = proxyDetailsService;
            this.siteDetayService = siteDetayService;
        }
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }


        public IActionResult GetSayilar()
        {
            var httpSayisi = proxyDetailsService.GetProxies().Where(x => x.ProxyTypeId == 1).Count();
            var socks4Sayisi = proxyDetailsService.GetProxies().Where(x => x.ProxyTypeId == 2).Count();
            var socks5Sayisi = proxyDetailsService.GetProxies().Where(x => x.ProxyTypeId == 3).Count();
            var toplam = httpSayisi + socks4Sayisi + socks5Sayisi;
            var kacSaatOnce = Convert.ToInt32((DateTime.Now - proxyDetailsService.GetProxies().OrderByDescending(x => x.AddTime).FirstOrDefault().AddTime).TotalHours);

            AnasayfaSayilar anasayfaSayilar = new AnasayfaSayilar() { HttpSayisi = httpSayisi, Socks4Sayisi = socks4Sayisi, Socks5Sayisi = socks5Sayisi, Toplam = toplam, SonGuncelleme = kacSaatOnce };
            return Json(anasayfaSayilar);
        }

        [HttpPost]
        public IActionResult GetProxyler(GetProxylerInput giris)
        {
            var urunler = proxyDetailsService.GetProxies().AsQueryable();
            var toplamKayitSayisi = urunler.Count();
            urunler = urunler
                .WhereIf(!string.IsNullOrEmpty(giris.TurId), x => x.ProxyTypeId.ToString().ToLower().Contains(giris.TurId.ToLower()))
                .WhereIf(giris.Search is not null &&
                    !string.IsNullOrEmpty(giris.Search.Value),
                    x => x.IpAddress.ToLower().Contains(giris.Search.Value.ToLower())
                    || x.Port.ToLower().Contains(giris.Search.Value.ToLower())
                    || x.ProxyType.Type.ToLower().Contains(giris.Search.Value.ToLower())
                    || x.Country.CountryName.ToLower().Contains(giris.Search.Value.ToLower())
                    );
            var test = giris.Search;
            var filtrelenmisKayitSayisi = urunler.Count();
            var sonuc = urunler
                .Skip(giris.Start)
                .Take(giris.Length)
                .ToList();
            return Json(new {
                Data = sonuc,
                Draw = giris.Draw,
                RecordsTotal = toplamKayitSayisi,
                RecordsFiltered = filtrelenmisKayitSayisi
            });
        }

        [HttpPost]
        public IActionResult GetProxyChecker(ApiCheckerInput input)
        {
            if (ModelState.IsValid)
            {
                if (input.Tur == "Http")
                {
                    var durum = proxyDetailsService.HttpCheck(input.ProxyIpPortData);
                    return durum == true ? Json(new { Ip = input.ProxyIpPortData }) : Json(new { Error = "Hata", Ip = input.ProxyIpPortData });
                }
                else if (input.Tur == "Socks4")
                {
                    var durum = proxyDetailsService.Socks4Check(input.ProxyIpPortData);
                    return durum == true ? Json(new { Ip = input.ProxyIpPortData }) : Json(new { Error = "Hata", Ip = input.ProxyIpPortData });
                }
                else if (input.Tur == "Socks5")
                {
                    var durum = proxyDetailsService.Socks5Check(input.ProxyIpPortData);
                    return durum == true ? Json(new { Ip = input.ProxyIpPortData }) : Json(new { Error = "Hata", Ip = input.ProxyIpPortData });
                }
                else
                {
                    return Json(new { Error = "Geçersiz tür", Ip = input.ProxyIpPortData });
                }
            }
            else
            {
                return Json(new{Error = "Geçersiz format",Ip = input.ProxyIpPortData});
            }
        }

        public IActionResult Liste(ApiHizmetInput giris)
        {
            if (ModelState.IsValid)
            {
                var turler = new List<string> { "http", "socks4", "socks5", "all" };
                if (giris.ApiKey != "testKey" && !turler.Contains(giris.Tur, StringComparer.OrdinalIgnoreCase))
                {
                    return Content("Geçersiz bilgiler");
                }
                List<Proxy> proxyList = proxyDetailsService.GetProxies();
                var donecekMetin = "";
                if (giris.Tur != "all")
                {
                    foreach (var proxy in proxyList.Where(x => x.ProxyType.Type == giris.Tur).ToList())
                    {
                        donecekMetin = donecekMetin + proxy.IpAddress + ":" + proxy.Port + "\n";
                    }
                    return Content(donecekMetin);
                }
                else
                {
                    foreach (var proxy in proxyList.ToList())
                    {
                        donecekMetin = donecekMetin + proxy.IpAddress + ":" + proxy.Port + ":" + proxy.ProxyType.Type + "\n";
                    }
                    return Content(donecekMetin);
                }

            }
            else
            {
                return Content("Geçersiz bilgiler");
            }

        }

        public IActionResult GetApiUrls()
        {
            //şuanki id _userManager.GetUserId(User)
            var all = HttpUtility.UrlEncode(this.Url.Action("Liste", "Api", new { Tur = "all", ApiKey = _userManager.GetUserId(User) }, this.Request.Scheme));
            var http = HttpUtility.UrlEncode(this.Url.Action("Liste", "Api", new { Tur = "http", ApiKey = _userManager.GetUserId(User) }, this.Request.Scheme));
            var socks4 = HttpUtility.UrlEncode(this.Url.Action("Liste", "Api", new { Tur = "socks4", ApiKey = _userManager.GetUserId(User) }, this.Request.Scheme));
            var socks5 = HttpUtility.UrlEncode(this.Url.Action("Liste", "Api", new { Tur = "socks5", ApiKey = _userManager.GetUserId(User) }, this.Request.Scheme));
            return Json(new { All = all, Http = http, Socks4 = socks4, Socks5 = socks5 });
        }

        public IActionResult GetSSSList()
        {
            var sorular = siteDetayService.GetSSSs();
            return Json(sorular);
        }

        public IActionResult Test()
        {
            Proxy proxy = new Proxy()
            {
                AddTime = DateTime.Now,
                Country = proxyDetailsService.GetCountry(1),
                IpAddress = "192.168.1.1",
                Port = "3128",
                ProxyType = proxyDetailsService.GetProxyType(1)
            };
            proxyDetailsService.AddProxy(proxy);
            return View();
        }
    }
}
