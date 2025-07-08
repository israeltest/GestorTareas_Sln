using Dapper;
using GestorTareas.Dominio.Entidades;
using GestorTareas.Dominio.Interfaces;
using GestorTareas.Dominio.Interfaces.Repositorios;
using GestorTareas.Infraestructura.Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorTareas.Infraestructura.Repositorios
{
    public class RepositorioUsuarios : IRepositorioUsuarios
    {
        private readonly ContextoDapper _contexto;

        public RepositorioUsuarios(ContextoDapper contexto)
        {
            _contexto = contexto;
        }

        public async Task<Usuario> ObtenerPorNombreAsync(string nombreUsuario)
        {
            const string consulta = "sp_ObtenerUsuarioPorNombre";

            using var conexion = _contexto.CrearConexion();
            var parametros = new DynamicParameters();
            parametros.Add("NombreUsuario", nombreUsuario);

            return await conexion.QueryFirstOrDefaultAsync<Usuario>(
                consulta, parametros, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> AgregarAsync(Usuario usuario)
        {
            const string consulta = "sp_CrearUsuario";

            using var conexion = _contexto.CrearConexion();
            var parametros = new DynamicParameters();
            parametros.Add("NombreUsuario", usuario.NombreUsuario);
            parametros.Add("ContrasenaHash", usuario.ContrasenaHash);
            parametros.Add("Correo", usuario.Correo);
            parametros.Add("Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await conexion.ExecuteAsync(consulta, parametros, commandType: CommandType.StoredProcedure);
            return parametros.Get<int>("Id");
        }
    }
}
