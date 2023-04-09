using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace web_proje_loginli.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UlkeListesi()
        {
            return View();
        }

        public IActionResult VeriCekmeBotu()
        {
            return View();
        }
        public IActionResult MesajlariGoruntule()
        {
            return View();
        }
        public IActionResult BekleyenProxyler()
        {
            return View();
        }
        public IActionResult TasarimDuzenle()
        {
            return View();
        }
    }
}
