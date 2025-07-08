using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorTareas.Dominio.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string ContrasenaHash { get; set; }
        public string Correo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
