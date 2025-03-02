CREATE PROCEDURE [dbo].[UpdateProductBySKU]
        @SKU VARCHAR(16),
        @name VARCHAR(128),
        @EAN BIGINT,
        @producer_name VARCHAR(40),
        @category VARCHAR(512),
        @is_wire BIT,
        @shipping VARCHAR(32),
        @available BIT,
        @is_vendor BIT,
        @default_image VARCHAR(512)
AS
BEGIN
    UPDATE Products
    SET 
        SKU = @SKU,
        name = @name,
        EAN = @EAN,
        producer_name = @producer_name,
        category = @category,
        is_wire = @is_wire,
        shipping = @shipping,
        available = @available,
        is_vendor = @is_vendor,
        default_image = @default_image
    WHERE Products.SKU = @SKU
END