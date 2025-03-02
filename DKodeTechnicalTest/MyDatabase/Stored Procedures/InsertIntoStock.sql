CREATE PROCEDURE [dbo].[InsertIntoStock]
        @product_id BIGINT,
        @SKU VARCHAR(16),
        @unit VARCHAR(16),
        @qty DECIMAL(10,4),
        @manufacturer VARCHAR(128),
        @shipping VARCHAR(32),
        @shipping_cost DECIMAL(10,4)
AS
BEGIN
    SET NOCOUNT ON

    INSERT INTO [dbo].[Stock]
        (
            product_id,
            SKU,
            unit,
            qty,
            manufacturer,
            shipping,
            shipping_cost
        )
        VALUES
        (
            @product_id,
            @SKU,
            @unit,
            @qty,
            @manufacturer,
            @shipping,
            @shipping_cost
        )

END

GO