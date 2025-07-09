using GestorTareas.Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorTareas.Aplicacion.Interfaces.Servicios
{
    // Define las operaciones de acceso a datos para las tareas.
    // Es utilizada por la capa de aplicación (ServicioTareas) para mantenerse desacoplada de la implementación concreta.
    public interface IServicioTareas
    {
        Task<IEnumerable<TareaDTO>> ObtenerTodasLasTareasAsync(int usuarioId);   // Listar todas las tareas del usuario
        Task<TareaDTO> ObtenerTareaPorIdAsync(int id, int usuarioId);           // Obtener una tarea específica por ID y usuario
        Task<int> CrearTareaAsync(CrearTareaDTO tareaDto, int usuarioId);       // Insertar nueva tarea y retornar ID
        Task ActualizarTareaAsync(ActualizarTareaDTO tareaDto, int usuarioId);  // Actualizar una tarea existente
        Task EliminarTareaAsync(int id, int usuarioId);                         // Eliminar tarea por ID (validando usuario)
    }
}
