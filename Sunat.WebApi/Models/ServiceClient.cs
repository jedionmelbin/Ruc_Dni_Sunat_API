using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Sunat.WebApi.Models
{
    public class ServiceClient : IServiceClient
    {
        private readonly HttpClient _httpClient;
        public ServiceClient()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://e-consultaruc.sunat.gob.pe/") };
            //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));
        }


        public async Task PostAsync(string endpoint, object data, string args = null)
        {

            var payload = GetPayload(data);
            await _httpClient.PostAsync($"{endpoint}?{args}", payload);
        }


        private static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public async Task<HttpResponseMessage> PostAsync(string endpoint, object data)
        {

            var payload = GetPayload(data);
            var response = await _httpClient.PostAsync($"{endpoint}", payload);
            return response;
        }

        public async Task<HttpResponseMessage> PutAsync(string endpoint, object data)
        {

            var payload = GetPayload(data);
            var response = await _httpClient.PutAsync($"{endpoint}", payload);
            return response;
        }

        public async Task<HttpResponseMessage> GetAsync(string endpoint, string args = null)
        {

            var response = await _httpClient.GetAsync($"{endpoint}?{args}");
            if (!response.IsSuccessStatusCode)
                throw new NotImplementedException();

            return response;
        }

        public HttpResponseMessage Get(string endpoint, string args = null)
        {
            var response = _httpClient.GetAsync($"{endpoint}?{args}").Result;
            if (!response.IsSuccessStatusCode)
                throw new NotImplementedException();

            return response;
        }
    }
}
