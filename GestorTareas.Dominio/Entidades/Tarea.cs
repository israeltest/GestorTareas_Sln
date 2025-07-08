using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorTareas.Dominio.Entidades
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public bool Completada { get; set; }
        public int UsuarioId { get; set; }
    }
}
