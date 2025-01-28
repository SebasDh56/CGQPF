using AguaApp.Models;
using Newtonsoft.Json;

namespace AguaApp
{
    public partial class PagosPage : ContentPage
    {
        public PagosPage()
        {
            InitializeComponent();
            CargarPagos();
        }

        private async void CargarPagos()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7185/api/");

                var response = await client.GetAsync("Pago");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    // Log para verificar los datos recibidos
                    System.Diagnostics.Debug.WriteLine("Pagos JSON: " + json);

                    var pagosList = JsonConvert.DeserializeObject<List<CGQPago>>(json);

                    // Verifica la cantidad de pagos cargados
                    System.Diagnostics.Debug.WriteLine("Pagos cargados: " + pagosList?.Count);

                    pagosListView.ItemsSource = pagosList;
                }
                else
                {
                    await DisplayAlert("Error", $"Error al obtener pagos: {response.StatusCode}", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
