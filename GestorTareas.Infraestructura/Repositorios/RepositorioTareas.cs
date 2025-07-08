using Dapper;
using GestorTareas.Dominio.Entidades;
using GestorTareas.Dominio.Interfaces.Repositorios;
using GestorTareas.Infraestructura.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GestorTareas.Infraestructura.Repositorios
{
    public class RepositorioTareas : IRepositorioTareas
    {
        private readonly ContextoDapper _contexto;

        public RepositorioTareas(ContextoDapper contexto)
        {
            _contexto = contexto;
        }

        public async Task<Tarea> ObtenerPorIdAsync(int id, int usuarioId)
        {
            const string consulta = "sp_ObtenerTareaPorId";

            using var conexion = _contexto.CrearConexion();
            var parametros = new DynamicParameters();
            parametros.Add("Id", id);
            parametros.Add("UsuarioId", usuarioId);

            return await conexion.QueryFirstOrDefaultAsync<Tarea>(
                consulta, parametros, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Tarea>> ObtenerTodasPorUsuarioAsync(int usuarioId)
        {
            const string consulta = "sp_ObtenerTareasPorUsuario";

            using var conexion = _contexto.CrearConexion();
            var parametros = new DynamicParameters();
            parametros.Add("UsuarioId", usuarioId);

            return await conexion.QueryAsync<Tarea>(
                consulta, parametros, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> AgregarAsync(Tarea tarea)
        {
            const string consulta = "sp_CrearTarea";

            using var conexion = _contexto.CrearConexion();
            var parametros = new DynamicParameters();
            parametros.Add("Titulo", tarea.Titulo);
            parametros.Add("Descripcion", tarea.Descripcion);
            parametros.Add("FechaVencimiento", tarea.FechaVencimiento);
            parametros.Add("UsuarioId", tarea.UsuarioId);
            parametros.Add("Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await conexion.ExecuteAsync(consulta, parametros, commandType: CommandType.StoredProcedure);
            return parametros.Get<int>("Id");
        }

        public async Task ActualizarAsync(Tarea tarea)
        {
            const string consulta = "sp_ActualizarTarea";

            using var conexion = _contexto.CrearConexion();
            var parametros = new DynamicParameters();
            parametros.Add("Id", tarea.Id);
            parametros.Add("Titulo", tarea.Titulo);
            parametros.Add("Descripcion", tarea.Descripcion);
            parametros.Add("FechaVencimiento", tarea.FechaVencimiento);
            parametros.Add("Completada", tarea.Completada);
            parametros.Add("UsuarioId", tarea.UsuarioId);

            await conexion.ExecuteAsync(consulta, parametros, commandType: CommandType.StoredProcedure);
        }

        public async Task EliminarAsync(int id, int usuarioId)
        {
            const string consulta = "sp_EliminarTarea";

            using var conexion = _contexto.CrearConexion();
            var parametros = new DynamicParameters();
            parametros.Add("Id", id);
            parametros.Add("UsuarioId", usuarioId);

            await conexion.ExecuteAsync(consulta, parametros, commandType: CommandType.StoredProcedure);
        }
    }
}
