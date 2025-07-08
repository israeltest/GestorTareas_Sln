using GestorTareasUI.Models.Vista;
using GestorTareasUI.Servicios;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GestorTareasUI.Controllers
{
    [AllowAnonymous]
    public class AutenticacionController : Controller
    {
        private readonly IServicioTareasApi _servicioTareasApi;

        public AutenticacionController(IServicioTareasApi servicioTareasApi)
        {
            _servicioTareasApi = servicioTareasApi;
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> IniciarSesion(InicioSesionVistaModelo modelo, string urlRetorno = null)
        {
            if (!ModelState.IsValid)
                return View(modelo);

            var authResponse = await _servicioTareasApi.IniciarSesionCompletoAsync(modelo);

            if (authResponse?.token == null)
            {
                ModelState.AddModelError("", "Credenciales inválidas o error en el servidor");
                return View(modelo);
            }

            if (authResponse.usuario == null || string.IsNullOrEmpty(authResponse.usuario.id.ToString()))
            {
                ModelState.AddModelError("", "Datos de usuario incompletos");
                return View(modelo);
            }

            Response.Cookies.Append("TokenAutenticacion", authResponse.token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = authResponse.expiracion
            });

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, authResponse.usuario.id.ToString()),
                new Claim(ClaimTypes.Name, authResponse.usuario.nombreUsuario ?? "Usuario"),
                new Claim("JWTToken", authResponse.token)
            };

            if (!string.IsNullOrEmpty(authResponse.usuario.correo))
            {
                claims.Add(new Claim(ClaimTypes.Email, authResponse.usuario.correo));
            }

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme,
                ClaimTypes.Name,  
                ClaimTypes.Role); 

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = authResponse.expiracion
                });

            return RedirectToAction("Index", "Tareas");
        }

        public IActionResult Registro()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Registro(RegistroVistaModelo modelo)
        //{
        //    if (!ModelState.IsValid)
        //        return View(modelo);

        //    var resultado = await _servicioTareasApi.RegistrarAsync(modelo);
        //    if (!resultado)
        //    {
        //        ModelState.AddModelError("", "Error en el registro");
        //        return View(modelo);
        //    }

        //    return RedirectToAction("IniciarSesion");
        //}

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete("TokenAutenticacion");
            return RedirectToAction("Index", "Inicio");
        }
    }
}
