USE [Prueba_Desarrollador]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inscripcion_Asignaturas]') AND type in (N'U'))
ALTER TABLE [dbo].[Inscripcion_Asignaturas] DROP CONSTRAINT IF EXISTS [FK_Inscripcion_Asignaturas_Usuarios]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inscripcion_Asignaturas]') AND type in (N'U'))
ALTER TABLE [dbo].[Inscripcion_Asignaturas] DROP CONSTRAINT IF EXISTS [FK_Inscripcion_Asignaturas_Ciclos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inscripcion_Asignaturas]') AND type in (N'U'))
ALTER TABLE [dbo].[Inscripcion_Asignaturas] DROP CONSTRAINT IF EXISTS [FK_Inscripcion_Asignaturas_Asignaturas]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Calificaciones]') AND type in (N'U'))
ALTER TABLE [dbo].[Calificaciones] DROP CONSTRAINT IF EXISTS [FK_Calificaciones_Asignaturas]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Asignaturas]') AND type in (N'U'))
ALTER TABLE [dbo].[Asignaturas] DROP CONSTRAINT IF EXISTS [FK_Asignaturas_Usuarios]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 12/03/2021 03:31:05 p. m. ******/
DROP TABLE IF EXISTS [dbo].[Usuarios]
GO
/****** Object:  Table [dbo].[Inscripcion_Asignaturas]    Script Date: 12/03/2021 03:31:05 p. m. ******/
DROP TABLE IF EXISTS [dbo].[Inscripcion_Asignaturas]
GO
/****** Object:  Table [dbo].[Ciclos]    Script Date: 12/03/2021 03:31:05 p. m. ******/
DROP TABLE IF EXISTS [dbo].[Ciclos]
GO
/****** Object:  Table [dbo].[Calificaciones]    Script Date: 12/03/2021 03:31:05 p. m. ******/
DROP TABLE IF EXISTS [dbo].[Calificaciones]
GO
/****** Object:  Table [dbo].[Asignaturas]    Script Date: 12/03/2021 03:31:05 p. m. ******/
DROP TABLE IF EXISTS [dbo].[Asignaturas]
GO
USE [master]
GO
/****** Object:  Database [Prueba_Desarrollador]    Script Date: 12/03/2021 03:31:05 p. m. ******/
DROP DATABASE IF EXISTS [Prueba_Desarrollador]
GO
/****** Object:  Database [Prueba_Desarrollador]    Script Date: 12/03/2021 03:31:05 p. m. ******/
CREATE DATABASE [Prueba_Desarrollador]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Prueba_Desarrollador', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Prueba_Desarrollador.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Prueba_Desarrollador_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Prueba_Desarrollador_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Prueba_Desarrollador] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Prueba_Desarrollador].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Prueba_Desarrollador] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Prueba_Desarrollador] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Prueba_Desarrollador] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Prueba_Desarrollador] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Prueba_Desarrollador] SET ARITHABORT OFF 
GO
ALTER DATABASE [Prueba_Desarrollador] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Prueba_Desarrollador] SET AUTO_SHRINK ON 
GO
ALTER DATABASE [Prueba_Desarrollador] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Prueba_Desarrollador] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Prueba_Desarrollador] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Prueba_Desarrollador] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Prueba_Desarrollador] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Prueba_Desarrollador] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Prueba_Desarrollador] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Prueba_Desarrollador] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Prueba_Desarrollador] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Prueba_Desarrollador] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Prueba_Desarrollador] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Prueba_Desarrollador] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Prueba_Desarrollador] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Prueba_Desarrollador] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Prueba_Desarrollador] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Prueba_Desarrollador] SET RECOVERY FULL 
GO
ALTER DATABASE [Prueba_Desarrollador] SET  MULTI_USER 
GO
ALTER DATABASE [Prueba_Desarrollador] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Prueba_Desarrollador] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Prueba_Desarrollador] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Prueba_Desarrollador] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Prueba_Desarrollador] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Prueba_Desarrollador] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Prueba_Desarrollador', N'ON'
GO
ALTER DATABASE [Prueba_Desarrollador] SET QUERY_STORE = OFF
GO
USE [Prueba_Desarrollador]
GO
/****** Object:  Table [dbo].[Asignaturas]    Script Date: 12/03/2021 03:31:09 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Asignaturas](
	[Id_Asignatura] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](250) NOT NULL,
	[Creditos] [int] NOT NULL,
	[Id_Usuario] [int] NOT NULL,
 CONSTRAINT [PK_Asignaturas] PRIMARY KEY CLUSTERED 
(
	[Id_Asignatura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Calificaciones]    Script Date: 12/03/2021 03:31:09 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Calificaciones](
	[Id_Calificacion] [int] IDENTITY(1,1) NOT NULL,
	[Id_Asignatura] [int] NOT NULL,
	[Nota] [numeric](2, 1) NOT NULL,
	[Fecha_Calificacion] [date] NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_Calificaciones] PRIMARY KEY CLUSTERED 
(
	[Id_Calificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ciclos]    Script Date: 12/03/2021 03:31:09 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ciclos](
	[Id_Ciclo] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](250) NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_Ciclos] PRIMARY KEY CLUSTERED 
(
	[Id_Ciclo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inscripcion_Asignaturas]    Script Date: 12/03/2021 03:31:09 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inscripcion_Asignaturas](
	[Id_Asignatura] [int] IDENTITY(1,1) NOT NULL,
	[Id_Materia] [int] NOT NULL,
	[Id_Usuario] [int] NOT NULL,
	[Id_Ciclo] [int] NOT NULL,
 CONSTRAINT [PK_Inscripcion_Asignaturas] PRIMARY KEY CLUSTERED 
(
	[Id_Asignatura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 12/03/2021 03:31:09 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id_Usuario] [int] IDENTITY(1,1) NOT NULL,
	[Numero_Identificacion] [int] NOT NULL,
	[Nombre_Completo] [nvarchar](250) NOT NULL,
	[Tipo_Usuario] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id_Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Asignaturas]  WITH CHECK ADD  CONSTRAINT [FK_Asignaturas_Usuarios] FOREIGN KEY([Id_Usuario])
REFERENCES [dbo].[Usuarios] ([Id_Usuario])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Asignaturas] CHECK CONSTRAINT [FK_Asignaturas_Usuarios]
GO
ALTER TABLE [dbo].[Calificaciones]  WITH CHECK ADD  CONSTRAINT [FK_Calificaciones_Asignaturas] FOREIGN KEY([Id_Asignatura])
REFERENCES [dbo].[Inscripcion_Asignaturas] ([Id_Asignatura])
GO
ALTER TABLE [dbo].[Calificaciones] CHECK CONSTRAINT [FK_Calificaciones_Asignaturas]
GO
ALTER TABLE [dbo].[Inscripcion_Asignaturas]  WITH CHECK ADD  CONSTRAINT [FK_Inscripcion_Asignaturas_Asignaturas] FOREIGN KEY([Id_Materia])
REFERENCES [dbo].[Asignaturas] ([Id_Asignatura])
GO
ALTER TABLE [dbo].[Inscripcion_Asignaturas] CHECK CONSTRAINT [FK_Inscripcion_Asignaturas_Asignaturas]
GO
ALTER TABLE [dbo].[Inscripcion_Asignaturas]  WITH CHECK ADD  CONSTRAINT [FK_Inscripcion_Asignaturas_Ciclos] FOREIGN KEY([Id_Ciclo])
REFERENCES [dbo].[Ciclos] ([Id_Ciclo])
GO
ALTER TABLE [dbo].[Inscripcion_Asignaturas] CHECK CONSTRAINT [FK_Inscripcion_Asignaturas_Ciclos]
GO
ALTER TABLE [dbo].[Inscripcion_Asignaturas]  WITH CHECK ADD  CONSTRAINT [FK_Inscripcion_Asignaturas_Usuarios] FOREIGN KEY([Id_Usuario])
REFERENCES [dbo].[Usuarios] ([Id_Usuario])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Inscripcion_Asignaturas] CHECK CONSTRAINT [FK_Inscripcion_Asignaturas_Usuarios]
GO
USE [master]
GO
ALTER DATABASE [Prueba_Desarrollador] SET  READ_WRITE 
GO
