using GestorTareas.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorTareas.Dominio.Interfaces.Repositorios
{
    public interface IRepositorioTareas
    {
        Task<Tarea> ObtenerPorIdAsync(int id, int usuarioId);
        Task<IEnumerable<Tarea>> ObtenerTodasPorUsuarioAsync(int usuarioId);
        Task<int> AgregarAsync(Tarea tarea);
        Task ActualizarAsync(Tarea tarea);
        Task EliminarAsync(int id, int usuarioId);
    }
}
