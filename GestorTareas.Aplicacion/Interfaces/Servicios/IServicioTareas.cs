using GestorTareas.Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorTareas.Aplicacion.Interfaces.Servicios
{
    public interface IServicioTareas
    {
        Task<IEnumerable<TareaDTO>> ObtenerTodasLasTareasAsync(int usuarioId);
        Task<TareaDTO> ObtenerTareaPorIdAsync(int id, int usuarioId);
        Task<int> CrearTareaAsync(CrearTareaDTO tareaDto, int usuarioId);
        Task ActualizarTareaAsync(ActualizarTareaDTO tareaDto, int usuarioId);
        Task EliminarTareaAsync(int id, int usuarioId);
    }
}
