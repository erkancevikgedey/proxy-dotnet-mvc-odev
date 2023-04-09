using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using web_proje_loginli.EFModels;

namespace web_proje_loginli.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //public DbSet<Proxy> ProxyBilgi { get; set; }
        public DbSet<BotAddress> BotAddress { get; set; }
        public DbSet<CheckedProxy> CheckedProxy { get; set; }
        public DbSet<ContactMessage> ContactMessage { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Proxy> Proxy { get; set; }
        public DbSet<ProxyType> ProxyType { get; set; }
        public DbSet<QA> QA { get; set; }
        public DbSet<WebsiteText> WebsiteText { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}