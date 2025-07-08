using GestorTareasUI.Models.Vista;
using GestorTareasUI.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestorTareasUI.Controllers
{
    [Authorize]
    public class TareasController : Controller
    {
        private readonly IServicioTareasApi _servicioTareasApi;

        public TareasController(IServicioTareasApi servicioTareasApi)
        {
            _servicioTareasApi = servicioTareasApi;
        }

        public async Task<IActionResult> Index()
        {
            var tareas = await _servicioTareasApi.ObtenerTareasAsync();
            return View(tareas);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CrearTareaVistaModelo modelo)
        {
            if (!ModelState.IsValid)
                return View(modelo);

            var resultado = await _servicioTareasApi.CrearTareaAsync(modelo);
            if (!resultado)
            {
                ModelState.AddModelError("", "Error al crear la tarea");
                return View(modelo);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Editar(int id)
        {
            var tarea = await _servicioTareasApi.ObtenerTareaPorIdAsync(id);
            if (tarea == null)
                return NotFound();

            var modelo = new ActualizarTareaVistaModelo
            {
                Id = tarea.id,
                Titulo = tarea.titulo,
                Descripcion = tarea.descripcion,
                FechaVencimiento = tarea.fechaVencimiento,
                Completada = tarea.completada
            };

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ActualizarTareaVistaModelo modelo)
        {
            if (!ModelState.IsValid)
                return View(modelo);

            var resultado = await _servicioTareasApi.ActualizarTareaAsync(modelo);
            if (!resultado)
            {
                ModelState.AddModelError("", "Error al actualizar la tarea");
                return View(modelo);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var resultado = await _servicioTareasApi.EliminarTareaAsync(id);
            if (!resultado)
                return BadRequest();

            return RedirectToAction("Index");
        }
    }
}