using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Net.Http.Json;
using System.Web.Mvc;
using Entities.Helper;

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
            _httpClient.DefaultRequestHeaders.ExpectContinue = false;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            
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

        //Método para generar las llamadas POST
        private async Task<Message<T>> PostAsync<T>(Uri requestUrl, T content)
        {

            //addHeaders();
            var response = await _httpClient.PostAsync(requestUrl, CreateHttpContent<T>(content));
            bool estatus = response.IsSuccessStatusCode;
            Message<T> newT = default;
            if (estatus == true)
            {
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                newT = JsonConvert.DeserializeObject<Message<T>>(data);
            }
            return newT;
        }

        //Método para generar las llamadas PUT
        private async Task<Message<T>> PutAsync<T>(Uri requestUrl, T content)
        {

            //addHeaders();
            var response = await _httpClient.PutAsync(requestUrl, CreateHttpContent<T>(content));
            bool estatus = response.IsSuccessStatusCode;
            Message<T> newT = default;
            if (estatus == true)
            {
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                newT = JsonConvert.DeserializeObject<Message<T>>(data);
            }
            return newT;
        }

        public async Task<T> GetAsync<T>(string apiPath = "")
        {
            var requestUrl = CreateRequestUri(
                                                string.Format(System.Globalization.CultureInfo.InvariantCulture,
                                                apiPath));
            return await GetAsync<T>(requestUrl);
           
        }

        public async Task<Message<T>> PostAsync<T>(string apiPath, T content)
        {
            var requestUrl = CreateRequestUri(
                                                string.Format(System.Globalization.CultureInfo.InvariantCulture,
                                                apiPath));
            return await PostAsync(requestUrl,content);

        }

        public async Task<Message<T>> PutAsync<T>(string apiPath, T content)
        {
            var requestUrl = CreateRequestUri(
                                                string.Format(System.Globalization.CultureInfo.InvariantCulture,
                                                apiPath));
            return await PutAsync(requestUrl, content);

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

        protected StringContent CreateStringContent<T>(T content)
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
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    PreserveReferencesHandling= PreserveReferencesHandling.Objects,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
            }
        }

    }
}
