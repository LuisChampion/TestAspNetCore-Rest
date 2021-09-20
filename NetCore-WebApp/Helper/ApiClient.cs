using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetCore_WebApp.Helper
{
    public partial class ApiClient
    {
        private readonly HttpClient _httpClient;
        private Uri BaseEndPoint { get; set; }

        public ApiClient(Uri baseEndPoint)
        {
            if (baseEndPoint == null)
            {
                throw new ArgumentNullException(nameof(baseEndPoint));
            }
            BaseEndPoint = baseEndPoint;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseEndPoint;
        }

        //Método para generar las llamadas GET
        private async Task<T> GetAsync<T>(Uri requestUri)
        {
            addHeaders();
            var response = await _httpClient.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

        public async Task<T> GetAsync<T>(string apiPath = "")
        {
            var requestUrl = CreateRequestUri(
                                                string.Format(System.Globalization.CultureInfo.InvariantCulture,
                                                apiPath));
            return await GetAsync<T>(requestUrl);
           
        }

        protected Uri CreateRequestUri(string relativeUri, string queryString = "")
        {
            var endPoint = new Uri(BaseEndPoint, relativeUri);
            var uriBuilder = new UriBuilder(endPoint);
            uriBuilder.Query = queryString;
            return uriBuilder.Uri;

        }

        protected HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonConvert.SerializeObject(content, MicrosoftDateFormatSettings);
            return new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        }

        private void addHeaders()
        {
            _httpClient.DefaultRequestHeaders.Remove("userIP");
            _httpClient.DefaultRequestHeaders.Add("userIP", "192.168.1.1");
        }

        private static JsonSerializerSettings MicrosoftDateFormatSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
            }
        }

    }
}
