using GestorTareas.Aplicacion.DTOs;
using GestorTareas.Dominio.Entidades;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GestorTareas.Aplicacion.Uilidades
{
    public class JwtHelper
    {
        public static (string Token, DateTime Expiracion) GenerarToken(Usuario usuario, IConfiguration _configuration)
        {
            var claims = new[]
            {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                    new Claim(ClaimTypes.Email, usuario.Correo ?? string.Empty)
                };

            var claveSecreta = _configuration["JwtSettings:SecretKey"];
            if (string.IsNullOrEmpty(claveSecreta) || Encoding.UTF8.GetByteCount(claveSecreta) < 16)
            {
                throw new ArgumentException("La clave secreta JWT no es válida o es demasiado corta");
            }

            var claveBytes = Encoding.UTF8.GetBytes(claveSecreta);
            var claveSeguridad = new SymmetricSecurityKey(claveBytes);

            var credenciales = new SigningCredentials(claveSeguridad, SecurityAlgorithms.HmacSha256);

            int duracionHoras;
            if (!int.TryParse(_configuration["JwtSettings:DuracionHoras"], out duracionHoras))
            {
                throw new ArgumentException(" 'Jwt:DuracionHoras' no es válido");
            }
            var data = DateTime.Now;
            data = DateTime.UtcNow.AddMinutes(duracionHoras);
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Emisor"],
                audience: _configuration["JwtSettings:Audiencia"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(duracionHoras),
                signingCredentials: credenciales);
            
            var expClaim = token.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;
            if (expClaim != null && long.TryParse(expClaim, out var expUnixTime))
            {
                var expirationDate = DateTimeOffset.FromUnixTimeSeconds(expUnixTime).DateTime;
                Console.WriteLine($"expira en: {expirationDate} (UTC)");
                Console.WriteLine($"restant: {expirationDate - DateTime.UtcNow}");
            }
            else
            {
                Console.WriteLine("claim o no es válido.");
            }
            return (new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);
        }
    }
}
