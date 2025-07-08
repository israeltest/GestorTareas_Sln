using GestorTareasUI.Models;
using GestorTareasUI.Models.Vista;

namespace GestorTareasUI.Servicios
{
    public interface IServicioTareasApi
    {
    Task<AuthResponseDto> IniciarSesionCompletoAsync(InicioSesionVistaModelo modelo);
    Task<bool> RegistrarAsync(RegistroVistaModelo modelo);
        Task<IEnumerable<TareaVistaModelo>> ObtenerTareasAsync();
        Task<TareaVistaModelo> ObtenerTareaPorIdAsync(int id);
        Task<bool> CrearTareaAsync(CrearTareaVistaModelo modelo);
        Task<bool> ActualizarTareaAsync(ActualizarTareaVistaModelo modelo);
        Task<bool> EliminarTareaAsync(int id);
    }
}
