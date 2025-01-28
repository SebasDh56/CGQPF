using AguaApp.Models;
using Newtonsoft.Json;

namespace AguaApp;

public partial class UsuariosPage : ContentPage
{
    public UsuariosPage()
    {
        InitializeComponent();
        CargarUsuarios();
    }

    private async void CargarUsuarios()
    {
        try
        {
            // URL base de tu API
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7185/api/");

            // Llama al endpoint para obtener los usuarios
            var response = await client.GetAsync("Usuario");
            if (response.IsSuccessStatusCode)
            {
                // Lee el contenido de la respuesta
                var json = await response.Content.ReadAsStringAsync();

                // Deserializa los datos a una lista de objetos Usuario
                var usuariosList = JsonConvert.DeserializeObject<List<CGQUsuario>>(json);

                // Asigna la lista al ListView
                usuariosListView.ItemsSource = usuariosList;
            }
            else
            {
                await DisplayAlert("Error", "No se pudieron obtener los usuarios", "OK");
            }
        }
        catch (Exception ex)
        {
            // Muestra un mensaje de error si algo falla
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}
