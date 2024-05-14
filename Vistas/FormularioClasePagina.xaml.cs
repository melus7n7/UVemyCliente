using Grpc.Net.Client;
using System;
using System.Collections.Generic;
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
using UVemyCliente.Conexion;
using UVemyCliente.DTO;
using UVemyCliente.Utilidades;

namespace UVemyCliente.Vistas
{
    /// <summary>
    /// Lógica de interacción para FormularioClasePagina.xaml
    /// </summary>
    public partial class FormularioClase : Page
    {
        public FormularioClase()
        {
            InitializeComponent();
        }

        private void ClicGuardarClase(object sender, RoutedEventArgs e)
        {
            bool esValido = ValidarInformacion();

            if (!esValido)
            {
                MostrarCamposNoValidos();
            }
            else
            {
                _ = GuardarClaseAsync();
            }
        }

        private bool ValidarInformacion()
        {
            return true;
        }

        private void MostrarCamposNoValidos()
        {

        }

        private async Task GuardarClaseAsync()
        {
            /*ClaseDTO clase = new ClaseDTO
            {
                Nombre = "Clase Prueba",
                Descripcion = "Descripcion",
                IdCurso = 1
            };

            int respuesta = await ClaseClient.GuardarClase(clase);

            if (respuesta >= 500)
            {
                ErrorMensaje error = new ErrorMensaje();
                error.Show();
            }
            else
            {
                ExitoMensaje exito = new ExitoMensaje();
                exito.Show();
            }*/
            try
            {
                var channel = GrpcChannel.ForAddress("http://localhost:3001");
                
                var cliente = new VideoService.VideoServiceClient(channel);
                var peticion = new EnviarVideoPeticion { Datos = "" };

                var respuesta = cliente.enviarVideoDesdeCliente(peticion);


                
                Console.WriteLine("\nRecepción de datos correcta. " + respuesta.Respuesta);

            }
            catch(Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.StackTrace);
                await Console.Out.WriteLineAsync(ex.Message);


            }

            


        }
    }
}
