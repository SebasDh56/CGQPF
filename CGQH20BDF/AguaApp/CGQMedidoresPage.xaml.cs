using AguaApp.Models;
using Newtonsoft.Json;

namespace AguaApp
{
    public partial class MedidoresPage : ContentPage
    {
        public MedidoresPage()
        {
            InitializeComponent();
            CargarMedidores();
        }

        private async void CargarMedidores()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7185/api/");

                var response = await client.GetAsync("Medidor");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    // Log para verificar los datos recibidos
                    System.Diagnostics.Debug.WriteLine("Medidores JSON: " + json);

                    var medidoresList = JsonConvert.DeserializeObject<List<CGQMedidor>>(json);

                    // Verifica la cantidad de medidores cargados
                    System.Diagnostics.Debug.WriteLine("Medidores cargados: " + medidoresList?.Count);

                    medidoresListView.ItemsSource = medidoresList;
                }
                else
                {
                    await DisplayAlert("Error", $"Error al obtener medidores: {response.StatusCode}", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
