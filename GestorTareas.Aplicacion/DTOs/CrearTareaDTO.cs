using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorTareas.Aplicacion.DTOs
{
    public class CrearTareaDTO
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaVencimiento { get; set; }
    }
}
