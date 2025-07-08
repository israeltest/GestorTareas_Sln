using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorTareas.Aplicacion.DTOs
{
    public class ActualizarTareaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public bool Completada { get; set; }
    }
}
