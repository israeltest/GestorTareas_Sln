using GestorTareas.Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorTareas.Aplicacion.Interfaces.Servicios
{
    public interface IServicioAutenticacion
    {
        Task<UsuarioDTO> RegistrarAsync(RegistroDTO registroDto);
        Task<UsuarioAutenticadoDTO> IniciarSesionAsync(InicioSesionDTO inicioSesionDto);
        Task<UsuarioDTO> ObtenerUsuarioActualAsync(int usuarioId);
    }
}
