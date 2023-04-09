using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text.RegularExpressions;
using web_proje_loginli.Data;
using web_proje_loginli.EFModels;
using web_proje_loginli.Models.DTO;

namespace web_proje_loginli.Services
{
    public class ProxyDetailsService : IProxyDetailsService
    {
        private readonly ApplicationDbContext veriContext;

        public ProxyDetailsService(ApplicationDbContext veriContext)
        {
            this.veriContext = veriContext;
        }

        public bool AcceptCheckedProxy(int id)
        {
            var checkEdilmisProxy = veriContext.CheckedProxy.Where(x => x.CheckId == id).FirstOrDefault();
            if (checkEdilmisProxy != null)
            {
                Proxy eklenecek = new Proxy()
                {
                    IpAddress = checkEdilmisProxy.IpAddress,
                    Port = checkEdilmisProxy.Port,
                    AddTime = DateTime.Now,
                    CountryId = 4, // 4 şuanki db'de diğer ülke seçeneği
                    ProxyTypeId = checkEdilmisProxy.ProxyTypeId,
                };
                var durum = this.AddProxy(eklenecek);
                return durum == true ? true : false;
            }
            else
            {
                return false;
            }
        }

        public bool AddCountry(Country country)
        {
            try
            {
                veriContext.Country.Add(country);
                veriContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddProxy(Proxy proxy)
        {
            try
            {
                veriContext.Proxy.Add(proxy);
                veriContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }



        public bool AddCheckedProxy(CheckedProxy proxy)
        {
            var mevcutMu = veriContext.CheckedProxy.Where(x => x.IpAddress == proxy.IpAddress && x.ProxyTypeId == proxy.ProxyTypeId).FirstOrDefault();
            if (mevcutMu == null)
            {
                try
                {
                    veriContext.CheckedProxy.Add(proxy);
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
                return true;
            }
        }


        public bool EditCountry(Country country)
        {
            try
            {
                veriContext.Update(country);
                veriContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool EditProxy(Proxy proxy)
        {
            try
            {
                veriContext.Update(proxy);
                veriContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<CheckedProxy> GetCheckedProxies()
        {
            return veriContext.CheckedProxy.ToList();
        }

        public List<Country> GetCountries()
        {
            return veriContext.Country.ToList();
        }

        public Country GetCountry(int countryId)
        {
            var ulke = veriContext.Country.Where(x => x.CountryId == countryId).FirstOrDefault();
            return ulke;
        }

        public List<Proxy> GetProxies()
        {
            var proxyler = veriContext.Proxy.Include("Country").Include("ProxyType");
            return proxyler.ToList();
        }

        public Proxy GetProxy(int id)
        {
            var proxy = veriContext.Proxy.Include("Country").Include("ProxyType").Where(x => x.ProxyId == id).FirstOrDefault();
            return proxy;
        }

        public ProxyType GetProxyType(int typeId)
        {
            var proxyTipi = veriContext.ProxyType.Where(x => x.ProxyTypeId == typeId).FirstOrDefault();
            return proxyTipi;
        }

        public List<ProxyType> GetProxyTypes()
        {
            var proxyTypes = veriContext.ProxyType;
            return proxyTypes.ToList();
        }

        public bool RemoveCheckedProxy(int id)
        {
            var checkedProxy = veriContext.CheckedProxy.Where(x => x.CheckId == id).FirstOrDefault();
            if (checkedProxy != null)
            {
                try
                {
                    veriContext.Remove(checkedProxy);
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

        public bool RemoveCountry(int id)
        {
            var ulke = veriContext.Country.Where(x => x.CountryId == id).FirstOrDefault();
            if (ulke != null)
            {
                try
                {
                    veriContext.Remove(ulke);
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
            throw new NotImplementedException();
        }

        public bool RemoveProxy(int id)
        {
            var proxy = veriContext.Proxy.Where(x => x.ProxyId == id).FirstOrDefault();
            if (proxy != null)
            {
                try
                {
                    veriContext.Remove(proxy);
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

        public bool HttpCheck(string ipPort)
        {
            var bolunmus = ipPort.Split(":");
            string proxyIP = bolunmus[0];
            int proxyPort = int.Parse(bolunmus[1]);
            var req = HttpWebRequest.Create("http://ip-api.com/json");
            req.Proxy = new WebProxy(proxyIP, proxyPort);
            req.Timeout = 7000;
            try
            {
                var resp = req.GetResponse();
                var json = new StreamReader(resp.GetResponseStream()).ReadToEnd();
                var myip = JObject.Parse(json)["query"].ToString();
                if (myip == proxyIP)
                {
                    CheckedProxy eklenecek = new CheckedProxy()
                    {
                        CheckTime = DateTime.Now,
                        IpAddress = proxyIP,
                        Port = proxyPort.ToString(),
                        ProxyTypeId = 1
                    };
                    this.AddCheckedProxy(eklenecek);
                }
                return myip == proxyIP ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Socks4Check(string ipPort)
        {
            var bolunmus = ipPort.Split(":");
            string proxyIP = bolunmus[0];
            int proxyPort = int.Parse(bolunmus[1]);
            var proxy = new WebProxy();
            proxy.Address = new Uri("socks4://" + proxyIP + ":" + proxyPort);
            var req = HttpWebRequest.Create("http://ip-api.com/json");
            req.Proxy = proxy;
            req.Timeout = 7000;
            try
            {
                var resp = req.GetResponse();
                var json = new StreamReader(resp.GetResponseStream()).ReadToEnd();
                var myip = JObject.Parse(json)["query"].ToString();
                if (myip == proxyIP)
                {
                    CheckedProxy eklenecek = new CheckedProxy()
                    {
                        CheckTime = DateTime.Now,
                        IpAddress = proxyIP,
                        Port = proxyPort.ToString(),
                        ProxyTypeId = 2
                    };
                    this.AddCheckedProxy(eklenecek);
                }
                return myip == proxyIP ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Socks5Check(string ipPort)
        {
            var bolunmus = ipPort.Split(":");
            string proxyIP = bolunmus[0];
            int proxyPort = int.Parse(bolunmus[1]);
            var proxy = new WebProxy();
            proxy.Address = new Uri("socks5://" + proxyIP + ":" + proxyPort);
            var req = HttpWebRequest.Create("http://ip-api.com/json");
            req.Proxy = proxy;
            req.Timeout = 7000;
            try
            {
                var resp = req.GetResponse();
                var json = new StreamReader(resp.GetResponseStream()).ReadToEnd();
                var myip = JObject.Parse(json)["query"].ToString();
                if (myip == proxyIP)
                {
                    CheckedProxy eklenecek = new CheckedProxy()
                    {
                        CheckTime = DateTime.Now,
                        IpAddress = proxyIP,
                        Port = proxyPort.ToString(),
                        ProxyTypeId = 3
                    };
                    this.AddCheckedProxy(eklenecek);
                }
                return myip == proxyIP ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public BotDTO BotuBaslat()
        {
            try
            {
                int sayac = 0;
                var veriCekilecekSiteler = veriContext.BotAddress;
                foreach (var veri in veriCekilecekSiteler)
                {
                    var webAdresi = veri.Website;
                    var regex = veri.Regex;
                    string html = string.Empty;
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(webAdresi);
                    request.AutomaticDecompression = DecompressionMethods.GZip;
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        html = reader.ReadToEnd();
                    }
                    Regex rg = new Regex(regex);
                    MatchCollection bulunan = rg.Matches(html);
                    var sonuclar = bulunan.Cast<Match>().Select(m => m.Value).ToList();
                    foreach (string item in sonuclar)
                    {
                        var proxy = item.Split(":");
                        var ip = proxy[0];
                        var port = proxy[1];

                        var mevcutMu = veriContext.CheckedProxy.Where(x => x.IpAddress == ip && x.ProxyTypeId == veri.ProxyTypeId).FirstOrDefault();
                        if (mevcutMu == null)
                        {
                            CheckedProxy eklenecek = new CheckedProxy
                            {
                                IpAddress = ip,
                                Port = port,
                                ProxyTypeId = veri.ProxyTypeId,
                                CheckTime = DateTime.Now
                            };
                            veriContext.CheckedProxy.Add(eklenecek);
                            sayac++;
                        }
                    }
                }
                veriContext.SaveChanges();
                return new BotDTO { Durum = true, Sayi = sayac};
            }
            catch (Exception ex)
            {
                return new BotDTO { Durum = false, Sayi = 0, Hata = ex.ToString() };
            }
        }
    }
}
