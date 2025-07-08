using Microsoft.Extensions.DependencyInjection;
namespace GestorTareas.API.Configuracion
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddAplicacion(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<Aplicacion.Perfiles.MapeoPerfil>();
            });

            services.AddScoped<Aplicacion.Interfaces.Servicios.IServicioTareas, Aplicacion.Servicios.ServicioTareas>();
            services.AddScoped<Aplicacion.Interfaces.Servicios.IServicioAutenticacion, Aplicacion.Servicios.ServicioAutenticacion>();

            return services;
        }

        public static IServiceCollection AddInfraestructura(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<Infraestructura.Datos.ContextoDapper>();
            services.AddScoped<Dominio.Interfaces.Repositorios.IRepositorioTareas, Infraestructura.Repositorios.RepositorioTareas>();
            services.AddScoped<Dominio.Interfaces.Repositorios.IRepositorioUsuarios, Infraestructura.Repositorios.RepositorioUsuarios>();

            return services;
        }
    }
}
