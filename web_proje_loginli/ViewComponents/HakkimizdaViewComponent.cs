using Microsoft.AspNetCore.Mvc;
using web_proje_loginli.Services;

namespace web_proje_loginli.ViewComponents
{
    public class HakkimizdaViewComponent : ViewComponent
    {
        private readonly ISiteDetayService siteDetayService;

        public HakkimizdaViewComponent(ISiteDetayService siteDetayService)
        {
            this.siteDetayService = siteDetayService;
        }

        public IViewComponentResult Invoke()
        {
            string hakkimizdaMetin = siteDetayService.GetAboutText();
            return View("Default", hakkimizdaMetin);
        }
    }
}
