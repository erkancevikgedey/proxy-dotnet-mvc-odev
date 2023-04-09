using web_proje_loginli.EFModels;

namespace web_proje_loginli.Services
{
    public interface IBotDetailsService
    {
        bool AddBotSitesi(string site, string regex, int turId);
        bool EditBotSitesi( BotAddress duzenlenecek);
        BotAddress GetBotAddress(int id);
        List<BotAddress> GetBotAddresses();
        bool RemoveBotSitesi(int id);
    }
}