using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using web_proje_loginli.Models;
using web_proje_loginli.Models.Inputs;
using web_proje_loginli.Services;

namespace web_proje_loginli.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<IdentityUser> _userManager;
        private readonly IMessageDetailsService messageDetailsService;
        private readonly ISiteDetayService siteDetayService;

        public HomeController(ILogger<HomeController> logger, RoleManager<IdentityRole> roleMgr, UserManager<IdentityUser> userManager, IMessageDetailsService messageDetailsService, ISiteDetayService siteDetayService)
        {
            _logger = logger;
            _roleManager = roleMgr;
            _userManager = userManager;
            this.messageDetailsService = messageDetailsService;
            this.siteDetayService = siteDetayService;
        }

        public IActionResult Index()
        {
            string karsilamaMetin = siteDetayService.GetWelcomeText();
            ViewData["KarsilamaMetin"] = karsilamaMetin;
            return View();
        }

        public async Task<IActionResult> Olustur()
        {
            IdentityResult result = await _roleManager.CreateAsync(new IdentityRole("User"));
            return Content(result.ToString());
        }

        
        public async Task<IActionResult> Bagla()
        {
            var userId = await _userManager.GetUserAsync(User);
            var result = await _userManager.AddToRoleAsync(userId, "User");
            return Content(result.ToString());
        }
        // sadece login sayfasına [AllowAnonymous] diyebilirmişiz
        public IActionResult ProxyListesi()
        {
            return View();
        }

        public IActionResult ProxyChecker()
        {
            return View();
        }

        public IActionResult ApiHizmeti()
        {
            return View();
        }

        public IActionResult Sss()
        {
            return View();
        }

        public IActionResult Iletisim()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Iletisim(IletisimInput mesaj)
        {
            if (ModelState.IsValid)
            {
                
                var durum = messageDetailsService.AddMessage(mesaj.MesajMail, mesaj.MesajIcerik);
                if (durum)
                {
                    ViewBag.BasariMesaji = "Mesajınız başarıyla gönderildi. Lütfen yanıt bekleyiniz.";
                }
                else
                {
                    ViewBag.BasariMesaji = "Mesaj kaydedilirken bir hata oluştu, adminle görüşün.";
                }
                return View();
            }
            else
            {
                return View(mesaj);
            }
        }

        public IActionResult Hakkimizda()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}