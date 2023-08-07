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
