using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCore_WebApp.Helper;

namespace NetCore_WebApp.Factory
{
    //Cliente Api basado en el Patrón de Diseño Singleton
    public class ApiClientFactory
    {
        private static Uri apiUri;

        private static readonly Lazy<ApiClient> restClient = new Lazy<ApiClient>(
            ()=> new ApiClient(apiUri),System.Threading.LazyThreadSafetyMode.ExecutionAndPublication
            );

        //Constructor privado para evitar la instanciación directa
        static ApiClientFactory()
        {
            apiUri = new Uri(Utility.ApplicationSettings.WebApiUrl);
        }

        //Devuelva la instancia única
        public static ApiClient Instance
        { 
            get { return restClient.Value; }
        }
    }
}
