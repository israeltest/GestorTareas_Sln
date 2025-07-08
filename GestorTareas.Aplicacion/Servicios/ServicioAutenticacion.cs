using AutoMapper;
using GestorTareas.Aplicacion.DTOs;
using GestorTareas.Aplicacion.Interfaces.Servicios;
using GestorTareas.Aplicacion.Uilidades;
using GestorTareas.Dominio.Entidades;
using GestorTareas.Dominio.Interfaces.Repositorios;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GestorTareas.Aplicacion.Servicios
{
    public class ServicioAutenticacion : IServicioAutenticacion
    {
        private readonly IRepositorioUsuarios _repositorioUsuarios;
        private readonly IMapper _mapeador;
        private readonly IConfiguration _configuration;

        public ServicioAutenticacion(IRepositorioUsuarios repositorioUsuarios, IMapper mapeador, IConfiguration configuration)
        {
            _repositorioUsuarios = repositorioUsuarios;
            _mapeador = mapeador;
            _configuration = configuration;
        }

        public async Task<UsuarioDTO> RegistrarAsync(RegistroDTO registroDto)
        {
            if (await _repositorioUsuarios.ObtenerPorNombreAsync(registroDto.NombreUsuario) != null)
                throw new Exception("El nombre de usuario ya existe");

            var usuario = new Usuario
            {
                NombreUsuario = registroDto.NombreUsuario,
                ContrasenaHash = PrepararContrasenaParaAlmacenar(registroDto.Contrasena),
                Correo = registroDto.Correo,
                FechaCreacion = DateTime.UtcNow
            };

            usuario.Id = await _repositorioUsuarios.AgregarAsync(usuario);
            return _mapeador.Map<UsuarioDTO>(usuario);
        }

        public async Task<UsuarioAutenticadoDTO> IniciarSesionAsync(InicioSesionDTO inicioSesionDto)
        {
            var usuario = await _repositorioUsuarios.ObtenerPorNombreAsync(inicioSesionDto.NombreUsuario);

            if (usuario == null || !VerificarContrasena(inicioSesionDto.Contrasena, usuario.ContrasenaHash))
                throw new Exception("Nombre de usuario o contraseña incorrectos");

            var usuarioDto = _mapeador.Map<UsuarioDTO>(usuario);
            var (token, expiracion) = JwtHelper.GenerarToken(usuario, _configuration);

            return new UsuarioAutenticadoDTO
            {
                Usuario = usuarioDto,
                Token = token,
                Expiracion = expiracion
            };
        }

        public async Task<UsuarioDTO> ObtenerUsuarioActualAsync(int usuarioId)
        {
            throw new NotImplementedException();
        }


        public bool VerificarContrasena(string contrasenaIngresada, string contrasenaAlmacenada)
        {
            string contrasenaIngresadaCodificada = Convert.ToBase64String(
                Encoding.UTF8.GetBytes(contrasenaIngresada));

            return contrasenaIngresadaCodificada == contrasenaAlmacenada;
        }
        public string PrepararContrasenaParaAlmacenar(string contrasenaPlana)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(contrasenaPlana));
        }
    }
}
