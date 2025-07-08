namespace GestorTareasUI.Models
{
    public class AuthResponseDto
    {
        public UsuarioDto usuario { get; set; }
        public string token { get; set; }
        public DateTime expiracion { get; set; }
    }

    public class UsuarioDto
    {
        public int id { get; set; }
        public string nombreUsuario { get; set; }
        public string correo { get; set; }
    }
}
