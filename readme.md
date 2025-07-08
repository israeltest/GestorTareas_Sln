# Portal de Tareas con Autenticación JWT - .NET 8

Este proyecto consiste en una API RESTful desarrollada en ASP.NET Core 8 y un portal web MVC también en ASP.NET Core 8, que permite gestionar tareas con autenticación basada en JWT.

## 🔧 Tecnologías utilizadas
- ASP.NET Core 8 (API + MVC)
- Entity Framework Core
- SQL Server + Procedimientos almacenados
- Autenticación JWT
- Bootstrap 5
- Session para token JWT en el portal

## ▶️ Instrucciones de ejecución

1. Abrir la solución en Visual Studio 2022.
2. Restaurar paquetes NuGet automáticamente.
3. Ejecutar el script `GestorTareasDb.sql` en tu instancia de SQL Server.
4. Configurar la cadena de conexión en `appsettings.json` en proyecto GestorTareas.API.
    ![alt text](image-1.png)
5. Configurar en `appsettings.json` en proyecto GestorTareasUI la url de la Api para que eista conexión entre el sitio y la Api.
      ![alt text](image.png)
5. Establecer el proyecto de inicio (API o portal).
6. Iniciar sesión en el portal con: 
    Usuario:    juan.perez
	Contraseña: Prueba123
7. Capturas API en swagger:
    ![alt text](image-2.png)
    Consumo de Api Iniciar sesión  generación de Token: 
    ![alt text](image-4.png)
    Consumo de los demás métodos con el token generado:
        Agregando el Token: ![alt text](image-5.png)
        Consumiento api para obtener todas las Tareas registradas: ![alt text](image-6.png)
8. Capturas portal: 
    Inicio de sesión: 
      ![alt text](image-3.png)
    Página Inicio que lista todas las tareas  muestra las opciones a realizar:
      ![alt text](image-7.png)
    Creación de una Tarea:
      ![alt text](image-8.png) ![alt text](image-9.png)
    Edición de una Tarea: 
     ![alt text](image-10.png) ![alt text](image-11.png)
    Eliminación de la Tarea: 
     ![alt text](image-12.png) ![alt text](image-13.png)
    



  