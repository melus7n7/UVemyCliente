using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace UVemyCliente.Conexion
{
    public static class APIConexion
    {
        private static HttpClient _apiClient;

        public static HttpClient ObtenerClient()
        {
            if (_apiClient == null)
            {
                _apiClient = new HttpClient();
                _apiClient.DefaultRequestHeaders.Accept.Clear();
                _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _apiClient.BaseAddress = new Uri("http://localhost:3000/api/");
            }

            return _apiClient;
        }
    }
}
