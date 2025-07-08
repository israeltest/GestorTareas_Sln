namespace GestorTareasUI.Models.Vista
{
    public class TareaVistaModelo
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime? fechaVencimiento { get; set; }
        public bool completada { get; set; }
    }
    public class CrearTareaVistaModelo
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaVencimiento { get; set; }
    }
    public class ActualizarTareaVistaModelo
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public bool Completada { get; set; }
    }
    public class InicioSesionVistaModelo
    {
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
    }
    public class RegistroVistaModelo
    {
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public string Correo { get; set; }
    }
}
