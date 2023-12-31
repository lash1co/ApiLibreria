USE [LibreriaDB]
GO
/****** Object:  StoredProcedure [dbo].[AgregarDetalleVenta]    Script Date: 1/08/2023 7:27:05 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[AgregarDetalleVenta] 
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
