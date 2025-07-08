using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorTareas.Infraestructura.Datos
{
    public class ContextoDapper
    {
        private readonly IConfiguration _configuracion;
        private readonly string _cadenaConexion;

        public ContextoDapper(IConfiguration configuracion)
        {
            _configuracion = configuracion;
            _cadenaConexion = _configuracion.GetConnectionString("GestorTareasDb");
        }

        public IDbConnection CrearConexion() => new SqlConnection(_cadenaConexion);
    }
}
