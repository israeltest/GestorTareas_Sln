using GestorTareas.Aplicacion.DTOs;
using GestorTareas.Aplicacion.Interfaces.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestorTareas.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacionController : Controller
    {
        private readonly IServicioAutenticacion _servicioAutenticacion;

        public AutenticacionController(IServicioAutenticacion servicioAutenticacion)
        {
            _servicioAutenticacion = servicioAutenticacion;
        }

        //[HttpPost("registro")]
        //public async Task<IActionResult> Registrar(RegistroDTO registroDto)
        //{
        //    var usuario = await _servicioAutenticacion.RegistrarAsync(registroDto);
        //    return Ok(usuario);
        //}

        [HttpPost("iniciar-sesion")]
        public async Task<IActionResult> IniciarSesion(InicioSesionDTO inicioSesionDto)
        {
            var usuario = await _servicioAutenticacion.IniciarSesionAsync(inicioSesionDto);
            return Ok(usuario);
        }
    }
}
