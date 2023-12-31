USE [master]
GO
/****** Object:  Database [LibreriaDB]    Script Date: 7/08/2023 1:11:58 p. m. ******/
CREATE DATABASE [LibreriaDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LibreriaDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\LibreriaDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LibreriaDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\LibreriaDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [LibreriaDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LibreriaDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LibreriaDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LibreriaDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LibreriaDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LibreriaDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LibreriaDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [LibreriaDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LibreriaDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LibreriaDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LibreriaDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LibreriaDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LibreriaDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LibreriaDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LibreriaDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LibreriaDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LibreriaDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LibreriaDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LibreriaDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LibreriaDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LibreriaDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LibreriaDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LibreriaDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LibreriaDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LibreriaDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [LibreriaDB] SET  MULTI_USER 
GO
ALTER DATABASE [LibreriaDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LibreriaDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LibreriaDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LibreriaDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LibreriaDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [LibreriaDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [LibreriaDB] SET QUERY_STORE = OFF
GO
USE [LibreriaDB]
GO
/****** Object:  User [admin]    Script Date: 7/08/2023 1:11:58 p. m. ******/
CREATE USER [admin] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 7/08/2023 1:11:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Cedula] [nvarchar](10) NOT NULL,
	[Nombres] [nvarchar](50) NOT NULL,
	[Telefono] [nvarchar](30) NOT NULL,
	[Direccion] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleVenta]    Script Date: 7/08/2023 1:11:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleVenta](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Venta] [bigint] NOT NULL,
	[Libro] [bigint] NOT NULL,
	[Cantidad] [int] NOT NULL,
 CONSTRAINT [PK_DetalleVenta] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Libro]    Script Date: 7/08/2023 1:11:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Libro](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Autor] [nvarchar](30) NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Precio] [money] NOT NULL,
 CONSTRAINT [PK_Libro] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta]    Script Date: 7/08/2023 1:11:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CodVenta] [nvarchar](10) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[PuntoVenta] [nvarchar](20) NOT NULL,
	[Cliente] [bigint] NOT NULL,
	[Total] [money] NOT NULL,
 CONSTRAINT [PK_Venta] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cliente] ON 

INSERT [dbo].[Cliente] ([Id], [Cedula], [Nombres], [Telefono], [Direccion]) VALUES (1, N'1110460048', N'Luis Alejandro Hernández Carrillo', N'3162554077', N'Carrera 3 E Norte # 19 - 26')
INSERT [dbo].[Cliente] ([Id], [Cedula], [Nombres], [Telefono], [Direccion]) VALUES (3, N'38228126', N'Blanca Luz Carrillo Amaya', N'3185647436', N'Carrera 3 E N # 19 - 26')
INSERT [dbo].[Cliente] ([Id], [Cedula], [Nombres], [Telefono], [Direccion]) VALUES (4, N'6256325', N'Fercha Gallego', N'311456789', N'Avenida 30 de Agosto Pereira')
INSERT [dbo].[Cliente] ([Id], [Cedula], [Nombres], [Telefono], [Direccion]) VALUES (5, N'123456', N'Anonimo Perez II', N'3112234567', N'Avenida siempre viva, calle falsa 123')
SET IDENTITY_INSERT [dbo].[Cliente] OFF
GO
SET IDENTITY_INSERT [dbo].[Libro] ON 

INSERT [dbo].[Libro] ([Id], [Nombre], [Autor], [Cantidad], [Precio]) VALUES (1, N'Learning C# by Developing Games with Unity', N'Harrison Ferrone', 5, 128000.0000)
INSERT [dbo].[Libro] ([Id], [Nombre], [Autor], [Cantidad], [Precio]) VALUES (2, N'Patrones de diseño en C#', N'Laurent Debrauwer', 4, 171000.0000)
INSERT [dbo].[Libro] ([Id], [Nombre], [Autor], [Cantidad], [Precio]) VALUES (3, N'La biblia de la programación', N'Elixir Santos', 10, 200000.0000)
SET IDENTITY_INSERT [dbo].[Libro] OFF
GO
ALTER TABLE [dbo].[DetalleVenta]  WITH CHECK ADD  CONSTRAINT [FK_DetalleVenta_Libro] FOREIGN KEY([Libro])
REFERENCES [dbo].[Libro] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DetalleVenta] CHECK CONSTRAINT [FK_DetalleVenta_Libro]
GO
ALTER TABLE [dbo].[DetalleVenta]  WITH CHECK ADD  CONSTRAINT [FK_DetalleVenta_Venta] FOREIGN KEY([Venta])
REFERENCES [dbo].[Venta] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DetalleVenta] CHECK CONSTRAINT [FK_DetalleVenta_Venta]
GO
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD  CONSTRAINT [FK_Venta_Cliente] FOREIGN KEY([Cliente])
REFERENCES [dbo].[Cliente] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Venta] CHECK CONSTRAINT [FK_Venta_Cliente]
GO
/****** Object:  StoredProcedure [dbo].[ActualizarCliente]    Script Date: 7/08/2023 1:11:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ActualizarCliente] 
	-- Add the parameters for the stored procedure here
	@IdCliente bigint,
	@Cedula nvarchar(10),
	@Nombres nvarchar(50),
	@Telefono nvarchar(30),
	@Direccion nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update Cliente
	set Cedula = @Cedula,
	Nombres = @Nombres,
	Telefono = @Telefono,
	Direccion = @Direccion
	where Id = @IdCliente
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarDetalleVenta]    Script Date: 7/08/2023 1:11:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ActualizarDetalleVenta] 
	-- Add the parameters for the stored procedure here
	@Id bigint,
	@Venta bigint,
	@Libro bigint,
	@Cantidad int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update DetalleVenta
	set Venta = @Venta,
	Libro = @Libro,
	Cantidad = @Cantidad
	where Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarLibro]    Script Date: 7/08/2023 1:11:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ActualizarLibro]
	-- Add the parameters for the stored procedure here
	@IdLibro bigint,
	@Nombre nvarchar(30),
	@Autor nvarchar(30),
	@Cantidad int,
	@Precio money
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update Libro
	set Nombre = @Nombre,
	Autor = @Autor,
	Cantidad = @Cantidad,
	Precio = @Precio
	where Id = @IdLibro;
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarVenta]    Script Date: 7/08/2023 1:11:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ActualizarVenta]
	-- Add the parameters for the stored procedure here
	@IdVenta bigint,
	@CodVenta nvarchar(10),
	@Fecha Datetime,
	@PuntoVenta nvarchar(20),
	@Cliente Bigint,
	@Total money
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update Venta
	set CodVenta = @CodVenta,
	Fecha = @Fecha,
	PuntoVenta = @PuntoVenta,
	Cliente = @Cliente,
	Total = @Total
	where Id = @IdVenta;
END
GO
/****** Object:  StoredProcedure [dbo].[AgregarCliente]    Script Date: 7/08/2023 1:11:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Luis Alejandro Hernández Carrillo
-- Create date: 15/07/2023
-- Description:	Procedimiento para crear cliente en la libreria
-- =============================================
CREATE PROCEDURE [dbo].[AgregarCliente]
	-- Add the parameters for the stored procedure here
	@Cedula nvarchar(10),
	@Nombres nvarchar(50),
	@Telefono nvarchar(30),
	@Direccion nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert into LibreriaDB.dbo.Cliente(Cedula,Nombres,Telefono,Direccion) values(@Cedula,@Nombres,@Telefono,@Direccion);
END
GO
/****** Object:  StoredProcedure [dbo].[AgregarDetalleVenta]    Script Date: 7/08/2023 1:11:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AgregarDetalleVenta] 
	-- Add the parameters for the stored procedure here
	@Venta bigint, 
	@Libro bigint,
	@Cantidad int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert into DetalleVenta(Venta,Libro,Cantidad) values(@Venta,@Libro,@Cantidad);
END
GO
/****** Object:  StoredProcedure [dbo].[AgregarLibro]    Script Date: 7/08/2023 1:11:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AgregarLibro]
	-- Add the parameters for the stored procedure here
	@Nombre nvarchar(30), 
	@Autor nvarchar(30),
	@Cantidad int,
	@Precio money
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert into Libro(Nombre,Autor,Cantidad,Precio) values(@Nombre,@Autor,@Cantidad,@Precio);
END
GO
/****** Object:  StoredProcedure [dbo].[AgregarVenta]    Script Date: 7/08/2023 1:11:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Luis Alejandro Hernández Carrillo
-- Create date: 15/07/2023
-- Description:	Procedimiento de agregar venta
-- =============================================
CREATE PROCEDURE [dbo].[AgregarVenta]
	-- Add the parameters for the stored procedure here
	@CodVenta nvarchar(10),
	@Fecha Datetime,
	@PuntoVenta nvarchar(20),
	@Cliente Bigint,
	@Total money
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert into dbo.Venta(CodVenta,Fecha,PuntoVenta,Cliente,Total) values(@CodVenta,@Fecha,@PuntoVenta,@Cliente,@Total);
END
GO
/****** Object:  StoredProcedure [dbo].[EliminarCliente]    Script Date: 7/08/2023 1:11:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[EliminarCliente] 
	-- Add the parameters for the stored procedure here
	@IdCliente bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Delete from Cliente
	where Id = @IdCliente;
END
GO
/****** Object:  StoredProcedure [dbo].[EliminarDetalleVenta]    Script Date: 7/08/2023 1:11:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[EliminarDetalleVenta] 
	-- Add the parameters for the stored procedure here
	@Id bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	delete from DetalleVenta
	where Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[EliminarLibro]    Script Date: 7/08/2023 1:11:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[EliminarLibro] 
	-- Add the parameters for the stored procedure here
	@IdLibro bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Delete from Libro
	where Id = @IdLibro;
END
GO
/****** Object:  StoredProcedure [dbo].[EliminarVenta]    Script Date: 7/08/2023 1:11:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[EliminarVenta] 
	-- Add the parameters for the stored procedure here
	@IdVenta Bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Delete from Venta
	where Id = @IdVenta
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerCliente]    Script Date: 7/08/2023 1:11:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerCliente]
	-- Add the parameters for the stored procedure here
	@Id bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from Cliente where Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerClientes]    Script Date: 7/08/2023 1:11:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerClientes]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from Cliente;
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerDetallesVenta]    Script Date: 7/08/2023 1:11:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerDetallesVenta]
	-- Add the parameters for the stored procedure here
	@Venta bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from DetalleVenta where Venta = @Venta;
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerDetalleVenta]    Script Date: 7/08/2023 1:11:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerDetalleVenta]
	-- Add the parameters for the stored procedure here
	@Id bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from DetalleVenta where Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerLibro]    Script Date: 7/08/2023 1:11:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerLibro]
	-- Add the parameters for the stored procedure here
	@Id bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from Libro where Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerLibros]    Script Date: 7/08/2023 1:11:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerLibros]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from Libro;
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerVenta]    Script Date: 7/08/2023 1:11:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerVenta]
	-- Add the parameters for the stored procedure here
	@IdVenta bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	--Select Venta.Id, Venta.CodVenta, Venta.PuntoVenta, Venta.Cliente, Cliente.Nombres, Venta.Total from Venta inner join Cliente 
	--on Venta.Cliente = Cliente.Id
	select * from Venta
	where Id = @IdVenta;
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerVentas]    Script Date: 7/08/2023 1:11:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerVentas]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	-- Select Venta.Id, Venta.CodVenta, Venta.PuntoVenta, Venta.Cliente, Cliente.Nombres, Venta.Total from Venta inner join Cliente on Venta.Cliente = Cliente.Id;
	Select * from Venta;
END
GO
USE [master]
GO
ALTER DATABASE [LibreriaDB] SET  READ_WRITE 
GO
