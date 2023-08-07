-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
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
