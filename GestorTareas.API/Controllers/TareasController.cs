using GestorTareas.Aplicacion.DTOs;
using GestorTareas.Aplicacion.Interfaces.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GestorTareas.API.Controllers
{
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

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            var usuarioId = ObtenerIdUsuarioActual();
            var tareas = await _servicioTareas.ObtenerTodasLasTareasAsync(usuarioId);
            return Ok(tareas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var usuarioId = ObtenerIdUsuarioActual();
            var tarea = await _servicioTareas.ObtenerTareaPorIdAsync(id, usuarioId);
            return Ok(tarea);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearTareaDTO tareaDto)
        {
            var usuarioId = ObtenerIdUsuarioActual();
            var id = await _servicioTareas.CrearTareaAsync(tareaDto, usuarioId);
            return CreatedAtAction(nameof(ObtenerPorId), new { id }, null);
        }

        [HttpPut]
        public async Task<IActionResult> Actualizar(ActualizarTareaDTO tareaDto)
        {
            var usuarioId = ObtenerIdUsuarioActual();
            await _servicioTareas.ActualizarTareaAsync(tareaDto, usuarioId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var usuarioId = ObtenerIdUsuarioActual();
            await _servicioTareas.EliminarTareaAsync(id, usuarioId);
            return NoContent();
        }

        private int ObtenerIdUsuarioActual()
        {
            var usuarioIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (usuarioIdClaim == null || !int.TryParse(usuarioIdClaim.Value, out var usuarioId))
                throw new UnauthorizedAccessException("Usuario no válido");

            return usuarioId;
        }
    }
}
