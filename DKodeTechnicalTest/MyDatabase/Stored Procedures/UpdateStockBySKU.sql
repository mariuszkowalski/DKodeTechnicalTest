CREATE PROCEDURE [dbo].[UpdateStockBySKU]
        @product_id BIGINT,
        @SKU VARCHAR(16),
        @unit VARCHAR(16),
        @qty DECIMAL(10,4),
        @manufacturer VARCHAR(128),
        @shipping VARCHAR(32),
        @shipping_cost DECIMAL(10,4)
AS
BEGIN
    UPDATE Stock
    SET 
        product_id = @product_id,
        SKU = @SKU,
        unit = @unit,
        qty = @qty,
        manufacturer = @manufacturer,
        shipping = @shipping,
        shipping_cost = @shipping_cost
    WHERE Stock.SKU = @SKU
END