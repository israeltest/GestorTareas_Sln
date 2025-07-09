using GestorTareas.Aplicacion.DTOs;
using GestorTareas.Aplicacion.Interfaces.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GestorTareas.API.Controllers
{
    // Controlador de tareas: maneja las operaciones CRUD para las tareas del usuario actual
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TareasController : ControllerBase
    {
        private readonly IServicioTareas _servicioTareas;
        private readonly IServicioAutenticacion _servicioAutenticacion;

        public TareasController(IServicioTareas servicioTareas, IServicioAutenticacion servicioAutenticacion)
        {
            _servicioTareas = servicioTareas;
            _servicioAutenticacion = servicioAutenticacion;
        }

        /// <summary>
        /// Obtiene todas las tareas asociadas al usuario autenticado.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            var usuarioId = ObtenerIdUsuarioActual();
            var tareas = await _servicioTareas.ObtenerTodasLasTareasAsync(usuarioId);
            return Ok(tareas);
        }

        /// <summary>
        /// Obtiene una tarea específica por su ID, validando que pertenezca al usuario autenticado.
        /// </summary>
        /// <param name="id">ID de la tarea</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var usuarioId = ObtenerIdUsuarioActual();
            var tarea = await _servicioTareas.ObtenerTareaPorIdAsync(id, usuarioId);
            return Ok(tarea);
        }

        /// <summary>
        /// Crea una nueva tarea para el usuario autenticado.
        /// </summary>
        /// <param name="tareaDto">Datos de la nueva tarea</param>
        [HttpPost]
        public async Task<IActionResult> Crear(CrearTareaDTO tareaDto)
        {
            var usuarioId = ObtenerIdUsuarioActual();
            var id = await _servicioTareas.CrearTareaAsync(tareaDto, usuarioId);
            return CreatedAtAction(nameof(ObtenerPorId), new { id }, null);
        }

        /// <summary>
        /// Actualiza una tarea existente, asegurando que pertenezca al usuario autenticado.
        /// </summary>
        /// <param name="tareaDto">Datos actualizados de la tarea</param>
        [HttpPut]
        public async Task<IActionResult> Actualizar(ActualizarTareaDTO tareaDto)
        {
            var usuarioId = ObtenerIdUsuarioActual();
            await _servicioTareas.ActualizarTareaAsync(tareaDto, usuarioId);
            return NoContent();
        }

        /// <summary>
        /// Elimina una tarea por su ID, asegurando que pertenezca al usuario autenticado.
        /// </summary>
        /// <param name="id">ID de la tarea</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var usuarioId = ObtenerIdUsuarioActual();
            await _servicioTareas.EliminarTareaAsync(id, usuarioId);
            return NoContent();
        }

        /// <summary>
        /// Obtiene el ID del usuario autenticado a partir del token JWT.
        /// </summary>
        /// <returns>ID del usuario</returns>
        /// <exception cref="UnauthorizedAccessException">Si el usuario no está autenticado correctamente</exception>
        private int ObtenerIdUsuarioActual()
        {
            var usuarioIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (usuarioIdClaim == null || !int.TryParse(usuarioIdClaim.Value, out var usuarioId))
                throw new UnauthorizedAccessException("Usuario no válido");

            return usuarioId;
        }
    }
}
