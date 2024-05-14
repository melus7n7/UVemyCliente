using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using UVemyCliente.DTO;

namespace UVemyCliente.Conexion
{
    public static class ClaseClient
    {
        private static string URL_PATH = "clases";
        public static async Task<int> GuardarClase(ClaseDTO clase)
        {
            int codigoRespuesta = 500;
            var json = JsonSerializer.Serialize(clase);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            /*try
            {
                using (HttpResponseMessage respuesta = await APIConexion.ObtenerClient().PostAsync(URL_PATH, content))
                {
                    codigoRespuesta = (int)respuesta.StatusCode;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine(ex);
                codigoRespuesta = 500;
            }
            */
            return codigoRespuesta;
        }

    }
}
