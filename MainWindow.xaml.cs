using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UVemyCliente
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //_ = EnviarPeticionGetAsync();
            //_ = EnviarPeticionPutAsync();
            _ = EnviarPeticionPutYGetAlMismoTiempoAsync();
        }

        public async Task EnviarPeticionGetAsync()
        {
            Debug.WriteLine("Entrando");
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:3000/");
                try
                {
                    HttpResponseMessage response = await client.GetAsync("api/cursos");
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(content);
                        Debug.WriteLine(content);
                        Debug.WriteLine("If");
                    }
                    else
                    {
                        Debug.WriteLine("Código de estado: " + response.StatusCode);
                        Debug.WriteLine("Else");
                    }
                }
                catch (HttpRequestException e)
                {
                    Debug.WriteLine("Error de conexión: " + e.Message);
                }
            }
            Debug.WriteLine("Finalizando");
        }

        public async Task EnviarPeticionPutAsync()
        {
            Debug.WriteLine("Entrando");
            string jsonData = @"
            {
                ""titulo"": ""Curso de prueba2"",
                ""descripcion"": ""Curso de prueba2"",
                ""objetivos"": ""Curso de prueba2"",
                ""requisitos"": ""Curso de prueba2""
            }";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:3000/");
                try
                {
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("api/cursos", content);

                    if (response.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("Solicitud POST exitosa.");
                    }
                    else
                    {
                        Debug.WriteLine("Código de estado: " + response.StatusCode);
                    }
                }
                catch (HttpRequestException e)
                {
                    Debug.WriteLine("Error de conexión: " + e.Message);
                    Debug.WriteLine("Catch");
                }
                Debug.WriteLine("Finalizando");
            }
        }

        public async Task EnviarPeticionPutYGetAlMismoTiempoAsync()
        {
            //Podemos meter el http://localhost:3000/api/ a resources o como siempre es el mismo hacer un singleton en la clase ServicioHttp
            ServicioHttp httpClientService = new ServicioHttp("http://localhost:3000/api/");

            HttpResponseMessage respuestaGet = await httpClientService.SendRequestAsync(HttpMethod.Get, "cursos");
            string respuestaContentGet = await respuestaGet.Content.ReadAsStringAsync();
            Debug.WriteLine(respuestaContentGet);


            string jsonData = @"
            {
                ""titulo"": ""Curso de prueba3"",
                ""descripcion"": ""Curso de prueba3"",
                ""objetivos"": ""Curso de prueba3"",
                ""requisitos"": ""Curso de prueba3""
            }";
            var jsonContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage respuestaPut = await httpClientService.SendRequestAsync(HttpMethod.Post, "cursos", jsonContent);
            string respuestaContentPut = await respuestaPut.Content.ReadAsStringAsync();
            Debug.WriteLine(respuestaContentPut);

        }

        public class ServicioHttp
        {
            private readonly HttpClient _cliente;
            public ServicioHttp(string baseUri)
            {
                _cliente = new HttpClient
                {
                    BaseAddress = new Uri(baseUri)
                };
            }
            public async Task<HttpResponseMessage> SendRequestAsync(HttpMethod method, string relativeUri, HttpContent content = null)
            {
                return await _cliente.SendAsync(new HttpRequestMessage(method, relativeUri)
                {
                    Content = content
                });
            }
        }

    }
}
