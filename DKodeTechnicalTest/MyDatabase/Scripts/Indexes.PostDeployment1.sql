/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

CREATE NONCLUSTERED INDEX Idx_Products_SKU ON Products(SKU)
CREATE NONCLUSTERED INDEX Idx_Stock_SKU ON Stock(SKU)
CREATE NONCLUSTERED INDEX Idx_Prices_SKU ON Prices(SKU)