using AutoMapper;
using GestorTareas.Aplicacion.DTOs;
using GestorTareas.Aplicacion.Interfaces;
using GestorTareas.Aplicacion.Interfaces.Servicios;
using GestorTareas.Dominio.Entidades;
using GestorTareas.Dominio.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorTareas.Aplicacion.Servicios
{
    // Servicio de aplicación para gestionar tareas del usuario
    // Interactúa con la capa de repositorio y realiza la lógica de negocio necesaria
    public class ServicioTareas : IServicioTareas
    {
        private readonly IRepositorioTareas _repositorioTareas;
        private readonly IMapper _mapeador;

        // Constructor: inyecta el repositorio de tareas y el servicio de mapeo (AutoMapper)
        public ServicioTareas(IRepositorioTareas repositorioTareas, IMapper mapeador)
        {
            _repositorioTareas = repositorioTareas;
            _mapeador = mapeador;
        }

        /// <summary>
        /// Obtiene todas las tareas pertenecientes a un usuario específico.
        /// </summary>
        /// <param name="usuarioId">ID del usuario autenticado</param>
        /// <returns>Lista de tareas en formato DTO</returns>
        public async Task<IEnumerable<TareaDTO>> ObtenerTodasLasTareasAsync(int usuarioId)
        {
            var tareas = await _repositorioTareas.ObtenerTodasPorUsuarioAsync(usuarioId);
            return _mapeador.Map<IEnumerable<TareaDTO>>(tareas);
        }

        /// <summary>
        /// Obtiene una tarea específica por su ID, verificando que pertenezca al usuario.
        /// </summary>
        /// <param name="id">ID de la tarea</param>
        /// <param name="usuarioId">ID del usuario autenticado</param>
        /// <returns>Tarea en formato DTO</returns>
        public async Task<TareaDTO> ObtenerTareaPorIdAsync(int id, int usuarioId)
        {
            var tarea = await _repositorioTareas.ObtenerPorIdAsync(id, usuarioId);
            return _mapeador.Map<TareaDTO>(tarea);
        }

        /// <summary>
        /// Crea una nueva tarea asociada al usuario.
        /// </summary>
        /// <param name="tareaDto">DTO con los datos de la nueva tarea</param>
        /// <param name="usuarioId">ID del usuario autenticado</param>
        /// <returns>ID generado de la tarea creada</returns>
        public async Task<int> CrearTareaAsync(CrearTareaDTO tareaDto, int usuarioId)
        {
            var tarea = _mapeador.Map<Tarea>(tareaDto);
            tarea.UsuarioId = usuarioId;
            tarea.FechaCreacion = DateTime.UtcNow;

            return await _repositorioTareas.AgregarAsync(tarea);
        }

        /// <summary>
        /// Actualiza los datos de una tarea existente, validando que pertenezca al usuario.
        /// </summary>
        /// <param name="tareaDto">DTO con los datos actualizados</param>
        /// <param name="usuarioId">ID del usuario autenticado</param>
        public async Task ActualizarTareaAsync(ActualizarTareaDTO tareaDto, int usuarioId)
        {
            var tareaExistente = await _repositorioTareas.ObtenerPorIdAsync(tareaDto.Id, usuarioId);
            if (tareaExistente == null)
                throw new KeyNotFoundException("Tarea no encontrada");

            var tarea = _mapeador.Map<Tarea>(tareaDto);
            tarea.UsuarioId = usuarioId;
            tarea.FechaCreacion = tareaExistente.FechaCreacion;

            await _repositorioTareas.ActualizarAsync(tarea);
        }

        /// <summary>
        /// Elimina una tarea específica, asegurando que pertenezca al usuario.
        /// </summary>
        /// <param name="id">ID de la tarea</param>
        /// <param name="usuarioId">ID del usuario autenticado</param>
        public async Task EliminarTareaAsync(int id, int usuarioId)
        {
            await _repositorioTareas.EliminarAsync(id, usuarioId);
        }
    }
}
