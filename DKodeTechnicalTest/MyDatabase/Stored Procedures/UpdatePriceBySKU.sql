CREATE PROCEDURE [dbo].[UpdatePriceBySKU]
        @warehouse_id VARCHAR(16),
        @SKU VARCHAR(16),
        @net_product_price DECIMAL(15,2),
        @net_product_price_after_discount DECIMAL(15,2),
        @VAT_rate INT,
        @net_product_price_after_discount_logistic_unit DECIMAL(15,2)
AS
BEGIN
    UPDATE Prices
    SET 
        warehouse_id = @warehouse_id,
        SKU = @SKU,
        net_product_price = @net_product_price,
        net_product_price_after_discount = @net_product_price_after_discount,
        VAT_rate = @VAT_rate,
        net_product_price_after_discount_logistic_unit = @net_product_price_after_discount_logistic_unit
    WHERE Prices.SKU = @SKU
END