USE [LibreriaDB]
GO
/****** Object:  StoredProcedure [dbo].[ObtenerVenta]    Script Date: 19/07/2023 9:26:52 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[ObtenerVenta]
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
