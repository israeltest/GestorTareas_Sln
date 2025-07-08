using AutoMapper;
using GestorTareas.Aplicacion.DTOs;
using GestorTareas.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorTareas.Aplicacion.Perfiles
{
    public class MapeoPerfil : Profile
    {
        public MapeoPerfil()
        {
            CreateMap<Tarea, TareaDTO>();
            CreateMap<CrearTareaDTO, Tarea>();
            CreateMap<ActualizarTareaDTO, Tarea>();

            CreateMap<Usuario, UsuarioDTO>();
            CreateMap<RegistroDTO, Usuario>();
        }
    }
}
