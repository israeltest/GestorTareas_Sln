using GestorTareas.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorTareas.Dominio.Interfaces.Repositorios
{
    public interface IRepositorioUsuarios
    {
        Task<Usuario> ObtenerPorNombreAsync(string nombreUsuario);
        Task<int> AgregarAsync(Usuario usuario);
    }
}
