using web_proje_loginli.EFModels;
using web_proje_loginli.Models.DTO;

namespace web_proje_loginli.Services
{
    public interface IProxyDetailsService
    {
        List<Proxy> GetProxies();
        List<CheckedProxy> GetCheckedProxies();
        List<Country> GetCountries();
        List<ProxyType> GetProxyTypes();
        Country GetCountry(int countryId);
        Proxy GetProxy(int id);
        ProxyType GetProxyType(int typeId);
        bool AddProxy(Proxy proxy);
        bool AddCountry(Country country);
        bool AddCheckedProxy(CheckedProxy proxy);
        bool EditProxy(Proxy proxy);
        bool EditCountry(Country country);
        bool RemoveProxy(int id);
        bool RemoveCountry(int id);
        bool RemoveCheckedProxy(int id);
        bool AcceptCheckedProxy(int id);
        bool HttpCheck(string ipPort);
        bool Socks4Check(string ipPort);
        bool Socks5Check(string ipPort);
        BotDTO BotuBaslat();

    }
}
