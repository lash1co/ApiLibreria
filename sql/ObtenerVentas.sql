USE [LibreriaDB]
GO
/****** Object:  StoredProcedure [dbo].[ObtenerVentas]    Script Date: 19/07/2023 9:31:57 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerVentas]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	-- Select Venta.Id, Venta.CodVenta, Venta.PuntoVenta, Venta.Cliente, Cliente.Nombres, Venta.Total from Venta inner join Cliente on Venta.Cliente = Cliente.Id;
	Select * from Venta;
END
