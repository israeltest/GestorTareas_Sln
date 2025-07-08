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
    public class ServicioTareas : IServicioTareas
    {
        private readonly IRepositorioTareas _repositorioTareas;
        private readonly IMapper _mapeador;

        public ServicioTareas(IRepositorioTareas repositorioTareas, IMapper mapeador)
        {
            _repositorioTareas = repositorioTareas;
            _mapeador = mapeador;
        }

        public async Task<IEnumerable<TareaDTO>> ObtenerTodasLasTareasAsync(int usuarioId)
        {
            var tareas = await _repositorioTareas.ObtenerTodasPorUsuarioAsync(usuarioId);
            return _mapeador.Map<IEnumerable<TareaDTO>>(tareas);
        }

        public async Task<TareaDTO> ObtenerTareaPorIdAsync(int id, int usuarioId)
        {
            var tarea = await _repositorioTareas.ObtenerPorIdAsync(id, usuarioId);
            return _mapeador.Map<TareaDTO>(tarea);
        }

        public async Task<int> CrearTareaAsync(CrearTareaDTO tareaDto, int usuarioId)
        {
            var tarea = _mapeador.Map<Tarea>(tareaDto);
            tarea.UsuarioId = usuarioId;
            tarea.FechaCreacion = DateTime.UtcNow;

            return await _repositorioTareas.AgregarAsync(tarea);
        }

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

        public async Task EliminarTareaAsync(int id, int usuarioId)
        {
            await _repositorioTareas.EliminarAsync(id, usuarioId);
        }
    }
}
