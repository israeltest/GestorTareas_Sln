CREATE DATABASE GestorTareasDb;
GO
USE [GestorTareasDb]
GO
/****** Object:  Table [dbo].[Tareas]    Script Date: 6/7/2025 11:36:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tareas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [nvarchar](100) NOT NULL,
	[Descripcion] [nvarchar](500) NULL,
	[FechaCreacion] [datetime2](7) NOT NULL,
	[FechaVencimiento] [datetime2](7) NULL,
	[Completada] [bit] NOT NULL,
	[UsuarioId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 6/7/2025 11:36:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NombreUsuario] [nvarchar](50) NOT NULL,
	[ContrasenaHash] [nvarchar](255) NOT NULL,
	[Correo] [nvarchar](100) NULL,
	[FechaCreacion] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Tareas] ON 

INSERT [dbo].[Tareas] ([Id], [Titulo], [Descripcion], [FechaCreacion], [FechaVencimiento], [Completada], [UsuarioId]) VALUES (4, N'Taréa 1', N'Prueba del sistema', CAST(N'2025-07-06T10:51:52.3000000' AS DateTime2), CAST(N'2025-07-13T00:00:00.0000000' AS DateTime2), 1, 1)
INSERT [dbo].[Tareas] ([Id], [Titulo], [Descripcion], [FechaCreacion], [FechaVencimiento], [Completada], [UsuarioId]) VALUES (5, N'Tarea 2', N'Pruebas Unitareas', CAST(N'2025-07-06T11:01:52.1466667' AS DateTime2), CAST(N'2025-07-10T00:00:00.0000000' AS DateTime2), 0, 1)
SET IDENTITY_INSERT [dbo].[Tareas] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([Id], [NombreUsuario], [ContrasenaHash], [Correo], [FechaCreacion]) VALUES (1, N'juan.perez', N'UHJ1ZWJhMTIz', N'use1@example.com', CAST(N'2025-07-05T19:32:22.1866667' AS DateTime2))
INSERT [dbo].[Usuarios] ([Id], [NombreUsuario], [ContrasenaHash], [Correo], [FechaCreacion]) VALUES (2, N'maria.garcia', N'UHJ1ZWJhMTIz', N'user2@example.com', CAST(N'2025-07-05T19:32:22.1866667' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Usuarios__6B0F5AE0574E5113]    Script Date: 6/7/2025 11:36:54 ******/
ALTER TABLE [dbo].[Usuarios] ADD UNIQUE NONCLUSTERED 
(
	[NombreUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Tareas] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Tareas] ADD  DEFAULT ((0)) FOR [Completada]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Tareas]  WITH CHECK ADD FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarTarea]    Script Date: 6/7/2025 11:36:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ActualizarTarea]
    @Id INT,
    @Titulo NVARCHAR(100),
    @Descripcion NVARCHAR(500),
    @FechaVencimiento DATETIME2,
    @Completada BIT,
    @UsuarioId INT
AS
BEGIN
    UPDATE Tareas
    SET 
        Titulo = @Titulo,
        Descripcion = @Descripcion,
        FechaVencimiento = @FechaVencimiento,
        Completada = @Completada
    WHERE Id = @Id AND UsuarioId = @UsuarioId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CrearTarea]    Script Date: 6/7/2025 11:36:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimientos Almacenados
CREATE PROCEDURE [dbo].[sp_CrearTarea]
    @Titulo NVARCHAR(100),
    @Descripcion NVARCHAR(500),
    @FechaVencimiento DATETIME2,
    @UsuarioId INT,
    @Id INT OUTPUT
AS
BEGIN
    INSERT INTO Tareas (Titulo, Descripcion, FechaVencimiento, UsuarioId)
    VALUES (@Titulo, @Descripcion, @FechaVencimiento, @UsuarioId);
    
    SET @Id = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CrearUsuario]    Script Date: 6/7/2025 11:36:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_CrearUsuario]
    @NombreUsuario NVARCHAR(50),
    @ContrasenaHash NVARCHAR(255),
    @Correo NVARCHAR(100) = NULL,
    @Id INT OUTPUT
AS
BEGIN
    INSERT INTO Usuarios (NombreUsuario, ContrasenaHash, Correo)
    VALUES (@NombreUsuario, @ContrasenaHash, @Correo);
    
    SET @Id = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarTarea]    Script Date: 6/7/2025 11:36:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_EliminarTarea]
    @Id INT,
    @UsuarioId INT
AS
BEGIN
    DELETE FROM Tareas WHERE Id = @Id AND UsuarioId = @UsuarioId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerTareaPorId]    Script Date: 6/7/2025 11:36:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ObtenerTareaPorId]
    @Id INT,
    @UsuarioId INT
AS
BEGIN
    SELECT * FROM Tareas WHERE Id = @Id AND UsuarioId = @UsuarioId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerTareasPorUsuario]    Script Date: 6/7/2025 11:36:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ObtenerTareasPorUsuario]
    @UsuarioId INT
AS
BEGIN
    SELECT * FROM Tareas WHERE UsuarioId = @UsuarioId;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerUsuarioPorNombre]    Script Date: 6/7/2025 11:36:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ObtenerUsuarioPorNombre]
    @NombreUsuario NVARCHAR(50)
AS
BEGIN
    SELECT * FROM Usuarios WHERE NombreUsuario = @NombreUsuario;
END
GO
