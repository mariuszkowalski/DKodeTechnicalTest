﻿CREATE PROCEDURE [dbo].[SelectAllSkusFromStock]
AS
SET NOCOUNT ON;
BEGIN
	SELECT SKU FROM Stock;
	RETURN;
END