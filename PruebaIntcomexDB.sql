USE [master]
GO
/****** Object:  Database [PruebaIntcomexDB]    Script Date: 09/03/2023 4:44:22 p. m. ******/
CREATE DATABASE [PruebaIntcomexDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PruebaIntcomexDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\PruebaIntcomexDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PruebaIntcomexDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\PruebaIntcomexDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [PruebaIntcomexDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PruebaIntcomexDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PruebaIntcomexDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PruebaIntcomexDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PruebaIntcomexDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PruebaIntcomexDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PruebaIntcomexDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [PruebaIntcomexDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PruebaIntcomexDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PruebaIntcomexDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PruebaIntcomexDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PruebaIntcomexDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PruebaIntcomexDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PruebaIntcomexDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PruebaIntcomexDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PruebaIntcomexDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PruebaIntcomexDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PruebaIntcomexDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PruebaIntcomexDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PruebaIntcomexDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PruebaIntcomexDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PruebaIntcomexDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PruebaIntcomexDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PruebaIntcomexDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PruebaIntcomexDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PruebaIntcomexDB] SET  MULTI_USER 
GO
ALTER DATABASE [PruebaIntcomexDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PruebaIntcomexDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PruebaIntcomexDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PruebaIntcomexDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PruebaIntcomexDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PruebaIntcomexDB] SET QUERY_STORE = OFF
GO
USE [PruebaIntcomexDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 09/03/2023 4:44:23 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cargos]    Script Date: 09/03/2023 4:44:24 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cargos](
	[IdCargo] [int] IDENTITY(1,1) NOT NULL,
	[NombreCargo] [nvarchar](max) NULL,
	[Estado] [int] NOT NULL,
 CONSTRAINT [PK_Cargos] PRIMARY KEY CLUSTERED 
(
	[IdCargo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 09/03/2023 4:44:24 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[CodigoCliente] [int] IDENTITY(1,1) NOT NULL,
	[NombreCompleto] [nvarchar](max) NULL,
	[Telefono] [int] NOT NULL,
	[CorreoElectronico] [nvarchar](max) NULL,
	[IdUsuario] [int] NOT NULL,
	[IdCargo] [int] NOT NULL,
	[IdTipoContacto] [int] NOT NULL,
	[Estado] [int] NOT NULL,
 CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED 
(
	[CodigoCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TiposContactos]    Script Date: 09/03/2023 4:44:24 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TiposContactos](
	[IdTipoContacto] [int] IDENTITY(1,1) NOT NULL,
	[NombreTipoContacto] [nvarchar](max) NULL,
	[Estado] [int] NOT NULL,
 CONSTRAINT [PK_TiposContactos] PRIMARY KEY CLUSTERED 
(
	[IdTipoContacto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 09/03/2023 4:44:24 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[NombreUsuario] [nvarchar](max) NULL,
	[Estado] [int] NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230307191224_AgregarModelosIniciales1', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230307205124_ActualizarModelosIniciales2', N'5.0.17')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230307235340_ActualizarModelosIniciales3', N'5.0.17')
GO
SET IDENTITY_INSERT [dbo].[Cargos] ON 

INSERT [dbo].[Cargos] ([IdCargo], [NombreCargo], [Estado]) VALUES (1, N'MEDICO', 1)
INSERT [dbo].[Cargos] ([IdCargo], [NombreCargo], [Estado]) VALUES (2, N'PROGRAMADOR', 1)
INSERT [dbo].[Cargos] ([IdCargo], [NombreCargo], [Estado]) VALUES (3, N'CELULAR', 0)
INSERT [dbo].[Cargos] ([IdCargo], [NombreCargo], [Estado]) VALUES (4, N'CONSTRUCTOR', 1)
SET IDENTITY_INSERT [dbo].[Cargos] OFF
GO
SET IDENTITY_INSERT [dbo].[Clientes] ON 

INSERT [dbo].[Clientes] ([CodigoCliente], [NombreCompleto], [Telefono], [CorreoElectronico], [IdUsuario], [IdCargo], [IdTipoContacto], [Estado]) VALUES (2, N'BRUNITO', 317, N'brunito@gmail.com', 2, 2, 1, 0)
INSERT [dbo].[Clientes] ([CodigoCliente], [NombreCompleto], [Telefono], [CorreoElectronico], [IdUsuario], [IdCargo], [IdTipoContacto], [Estado]) VALUES (3, N'Samy', 318, N'sammy@gmail.com', 2, 2, 2, 1)
INSERT [dbo].[Clientes] ([CodigoCliente], [NombreCompleto], [Telefono], [CorreoElectronico], [IdUsuario], [IdCargo], [IdTipoContacto], [Estado]) VALUES (4, N'ROCKY', 123, N'rocky@gmail.com', 1, 1, 2, 1)
INSERT [dbo].[Clientes] ([CodigoCliente], [NombreCompleto], [Telefono], [CorreoElectronico], [IdUsuario], [IdCargo], [IdTipoContacto], [Estado]) VALUES (5, N'kenay', 1234, N'kenay@gmail.com', 1, 1, 1, 0)
INSERT [dbo].[Clientes] ([CodigoCliente], [NombreCompleto], [Telefono], [CorreoElectronico], [IdUsuario], [IdCargo], [IdTipoContacto], [Estado]) VALUES (6, N'LILIANA', 313, N'liliana@gmail.com', 1, 1, 6, 1)
INSERT [dbo].[Clientes] ([CodigoCliente], [NombreCompleto], [Telefono], [CorreoElectronico], [IdUsuario], [IdCargo], [IdTipoContacto], [Estado]) VALUES (7, N'anita', 311, N'anita@gmail.com', 4, 1, 2, 0)
SET IDENTITY_INSERT [dbo].[Clientes] OFF
GO
SET IDENTITY_INSERT [dbo].[TiposContactos] ON 

INSERT [dbo].[TiposContactos] ([IdTipoContacto], [NombreTipoContacto], [Estado]) VALUES (1, N'CELULAR1', 0)
INSERT [dbo].[TiposContactos] ([IdTipoContacto], [NombreTipoContacto], [Estado]) VALUES (2, N'CORREO', 1)
INSERT [dbo].[TiposContactos] ([IdTipoContacto], [NombreTipoContacto], [Estado]) VALUES (3, N'HOJA', 0)
INSERT [dbo].[TiposContactos] ([IdTipoContacto], [NombreTipoContacto], [Estado]) VALUES (4, N'CELULAR', 0)
INSERT [dbo].[TiposContactos] ([IdTipoContacto], [NombreTipoContacto], [Estado]) VALUES (5, N'TELEFONO', 0)
INSERT [dbo].[TiposContactos] ([IdTipoContacto], [NombreTipoContacto], [Estado]) VALUES (6, N'CELULAR1', 1)
SET IDENTITY_INSERT [dbo].[TiposContactos] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([IdUsuario], [NombreUsuario], [Estado]) VALUES (1, N'JUANITO', 1)
INSERT [dbo].[Usuarios] ([IdUsuario], [NombreUsuario], [Estado]) VALUES (2, N'DAYANITA', 0)
INSERT [dbo].[Usuarios] ([IdUsuario], [NombreUsuario], [Estado]) VALUES (3, N'LILI', 1)
INSERT [dbo].[Usuarios] ([IdUsuario], [NombreUsuario], [Estado]) VALUES (4, N'FRIJOL', 1)
INSERT [dbo].[Usuarios] ([IdUsuario], [NombreUsuario], [Estado]) VALUES (5, N'SAMSUNG', 0)
INSERT [dbo].[Usuarios] ([IdUsuario], [NombreUsuario], [Estado]) VALUES (6, N'IPHONE', 0)
INSERT [dbo].[Usuarios] ([IdUsuario], [NombreUsuario], [Estado]) VALUES (7, N'APPLE', 0)
INSERT [dbo].[Usuarios] ([IdUsuario], [NombreUsuario], [Estado]) VALUES (8, N'COCACOLA', 1)
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
ALTER TABLE [dbo].[Cargos] ADD  DEFAULT ((0)) FOR [Estado]
GO
ALTER TABLE [dbo].[Clientes] ADD  DEFAULT ((0)) FOR [Estado]
GO
ALTER TABLE [dbo].[TiposContactos] ADD  DEFAULT ((0)) FOR [Estado]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT ((0)) FOR [Estado]
GO
USE [master]
GO
ALTER DATABASE [PruebaIntcomexDB] SET  READ_WRITE 
GO
