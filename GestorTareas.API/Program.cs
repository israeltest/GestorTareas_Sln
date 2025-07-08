using GestorTareas.API.Configuracion;
using GestorTareas.API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSwaggerWithJwt();

builder.Services
    .AddAplicacion()
    .AddInfraestructura(builder.Configuration);

// Configurar CORS
builder.Services.AddCors(opciones =>
{
    opciones.AddPolicy("PermitirTodo", politica =>
    {
        politica.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("PermitirTodo");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
