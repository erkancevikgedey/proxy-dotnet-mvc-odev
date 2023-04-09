using Microsoft.EntityFrameworkCore;
using web_proje_loginli.Data;
using web_proje_loginli.EFModels;

namespace web_proje_loginli.Services
{
    public class BotDetailsService : IBotDetailsService
    {
        private readonly ApplicationDbContext veriContext;

        public BotDetailsService(ApplicationDbContext veriContext)
        {
            this.veriContext = veriContext;
        }

        public BotAddress GetBotAddress(int id)
        {
            return veriContext.BotAddress.Where(x => x.AddressId == id).FirstOrDefault();
        }

        public List<BotAddress> GetBotAddresses()
        {
            return veriContext.BotAddress.Include("ProxyType").ToList();
        }

        public bool AddBotSitesi(string site, string regex, int turId)
        {
            BotAddress eklenecek = new BotAddress()
            {
                Website = site,
                Regex = regex,
                ProxyTypeId = turId
            };

            try
            {
                veriContext.BotAddress.Add(eklenecek);
                veriContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveBotSitesi(int id)
        {
            var silinecek = this.GetBotAddress(id);
            if(silinecek != null)
            {
                try
                {
                    veriContext.Remove(silinecek);
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

        public bool EditBotSitesi(BotAddress duzenlenecek)
        {
            try
            {
                veriContext.Update(duzenlenecek);
                veriContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
