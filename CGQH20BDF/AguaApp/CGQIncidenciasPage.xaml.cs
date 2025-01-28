using AguaApp.Models;
using Newtonsoft.Json;

namespace AguaApp
{
    public partial class IncidenciasPage : ContentPage
    {
        public IncidenciasPage()
        {
            InitializeComponent();
            CargarIncidencias();
        }

        private async void CargarIncidencias()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7185/api/");

                var response = await client.GetAsync("Incidencium");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    // Log para verificar los datos recibidos
                    System.Diagnostics.Debug.WriteLine("Incidencias JSON: " + json);

                    var incidenciasList = JsonConvert.DeserializeObject<List<CGQIncidencias>>(json);

                    // Verifica la cantidad de incidencias cargadas
                    System.Diagnostics.Debug.WriteLine("Incidencias cargadas: " + incidenciasList?.Count);

                    incidenciasListView.ItemsSource = incidenciasList;
                }
                else
                {
                    await DisplayAlert("Error", $"Error al obtener incidencias: {response.StatusCode}", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
