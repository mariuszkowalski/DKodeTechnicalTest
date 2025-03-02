CREATE PROCEDURE [dbo].[InsertIntoPrices]
        @warehouse_id VARCHAR(16),
        @SKU VARCHAR(16),
        @net_product_price DECIMAL(15,2),
        @net_product_price_after_discount DECIMAL(15,2),
        @VAT_rate INT,
        @net_product_price_after_discount_logistic_unit DECIMAL(15,2)
AS
BEGIN
    SET NOCOUNT ON

    INSERT INTO [dbo].[Prices]
        (
            warehouse_id,
            SKU,
            net_product_price,
            net_product_price_after_discount,
            VAT_rate,
            net_product_price_after_discount_logistic_unit
        )
        VALUES
        (
            @warehouse_id,
            @SKU,
            @net_product_price,
            @net_product_price_after_discount,
            @VAT_rate,
            @net_product_price_after_discount_logistic_unit
        )

END

GO