using GestorTareas.Aplicacion.DTOs;

namespace GestorTareas.Aplicacion.Interfaces.Servicios
{
    public class UsuarioAutenticadoDTO
    {
        public UsuarioDTO Usuario { get; set; }
        public string Token { get; set; }
        public DateTime Expiracion { get; set; }
    }
}