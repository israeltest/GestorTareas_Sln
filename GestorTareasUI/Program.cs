using GestorTareasUI.Servicios;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor(); 
//builder.Services.AddHttpClient<IServicioTareasApi, ServicioTareasApi>(client =>
//{
//    client.BaseAddress = new Uri(builder.Configuration["UrlApiBase"]);
//});
builder.Services.AddHttpClient<IServicioTareasApi, ServicioTareasApi>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["UrlApiBase"]);
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
});


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Autenticacion/IniciarSesion";
        options.AccessDeniedPath = "/Autenticacion/AccesoDenegado";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        options.Cookie.HttpOnly = true;
        options.SlidingExpiration = true;
    });


builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();  

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Autenticacion}/{action=IniciarSesion}/{id?}"); 

app.Run();