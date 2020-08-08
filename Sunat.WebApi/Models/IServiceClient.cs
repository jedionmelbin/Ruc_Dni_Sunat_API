using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sunat.WebApi.Models
{
    public interface IServiceClient
    {
        Task<HttpResponseMessage> PostAsync(string endpoint, object data);
        Task<HttpResponseMessage> PutAsync(string endpoint, object data);
        Task<HttpResponseMessage> GetAsync(string endpoint, string args = null);
        HttpResponseMessage Get(string endpoint, string args = null);
    }
}
