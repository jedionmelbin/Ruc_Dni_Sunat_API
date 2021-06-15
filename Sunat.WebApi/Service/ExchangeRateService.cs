using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sunat.WebApi.Service
{
    public interface IExchangeRateService
    {
       decimal ExchangeRate();
    }
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly HttpClient _httpClient;
        public ExchangeRateService()
        {
            _httpClient = new HttpClient();
        }

        public decimal ExchangeRate()
        {
            string cambio = string.Empty;
            try
            {
                HtmlWeb htmlWeb = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc = htmlWeb.Load("http://www2.deltron.com.pe/index_2.php");
                IEnumerable<HtmlNode> nodes = doc.DocumentNode.Descendants().Where(n => n.HasClass("address-content"));
                string divCambio = string.Empty;
                string tipoCambio = string.Empty;
             
                foreach (var item in nodes)
                {
                    doc.LoadHtml(item.InnerHtml);
                    foreach (var div in doc.DocumentNode.SelectNodes("//ul//li[@class='address wow  lightSpeedIn']"))
                    {
                        divCambio = div.InnerText?.TrimStart()?.TrimEnd();
                        tipoCambio = divCambio.Split(':')[1];
                        cambio = tipoCambio.TrimStart().TrimEnd();
                    }
                }

                return Convert.ToDecimal(cambio);
            }
            catch (Exception)
            {
                return Convert.ToDecimal(cambio);
            }
        }
    }
}
