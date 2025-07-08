using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorTareas.Aplicacion.DTOs
{
    public class UsuarioDTO
    {        
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Correo { get; set; }
    }
}
