using GestorTareasUI.Models;
using GestorTareasUI.Models.Vista;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace GestorTareasUI.Servicios
{
    public class ServicioTareasApi : IServicioTareasApi
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ServicioTareasApi(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        private void AgregarTokenJwt()
        {
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["TokenAutenticacion"];
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<AuthResponseDto> IniciarSesionCompletoAsync(InicioSesionVistaModelo modelo)
        {
            var contenido = new StringContent(
                JsonSerializer.Serialize(modelo),
                Encoding.UTF8,
                "application/json");

            var respuesta = await _httpClient.PostAsync("api/autenticacion/iniciar-sesion", contenido);

            if (!respuesta.IsSuccessStatusCode)
                return null;

            var contenidoRespuesta = await respuesta.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AuthResponseDto>(contenidoRespuesta);
        }
        public async Task<bool> RegistrarAsync(RegistroVistaModelo modelo)
        {
            var contenido = new StringContent(
                JsonSerializer.Serialize(modelo),
                Encoding.UTF8,
                "application/json");

            var respuesta = await _httpClient.PostAsync("api/autenticacion/registro", contenido);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<TareaVistaModelo>> ObtenerTareasAsync()
        {
            AgregarTokenJwt();
            var respuesta = await _httpClient.GetAsync("api/tareas");

            if (!respuesta.IsSuccessStatusCode)
                return Enumerable.Empty<TareaVistaModelo>();

            var contenido = await respuesta.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<TareaVistaModelo>>(contenido);
        }

        public async Task<TareaVistaModelo> ObtenerTareaPorIdAsync(int id)
        {
            AgregarTokenJwt();
            var respuesta = await _httpClient.GetAsync($"api/tareas/{id}");

            if (!respuesta.IsSuccessStatusCode)
                return null;

            var contenido = await respuesta.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TareaVistaModelo>(contenido);
        }

        public async Task<bool> CrearTareaAsync(CrearTareaVistaModelo modelo)
        {
            AgregarTokenJwt();
            var contenido = new StringContent(
                JsonSerializer.Serialize(modelo),
                Encoding.UTF8,
                "application/json");

            var respuesta = await _httpClient.PostAsync("api/tareas", contenido);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<bool> ActualizarTareaAsync(ActualizarTareaVistaModelo modelo)
        {
            AgregarTokenJwt();
            var contenido = new StringContent(
                JsonSerializer.Serialize(modelo),
                Encoding.UTF8,
                "application/json");

            var respuesta = await _httpClient.PutAsync("api/tareas", contenido);
            return respuesta.IsSuccessStatusCode;
        }

        public async Task<bool> EliminarTareaAsync(int id)
        {
            AgregarTokenJwt();
            var respuesta = await _httpClient.DeleteAsync($"api/tareas/{id}");
            return respuesta.IsSuccessStatusCode;
        }
    }
}
