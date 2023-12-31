USE [LibreriaDB]
GO
/****** Object:  StoredProcedure [dbo].[ActualizarDetalleVenta]    Script Date: 1/08/2023 7:25:53 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[ActualizarDetalleVenta] 
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
