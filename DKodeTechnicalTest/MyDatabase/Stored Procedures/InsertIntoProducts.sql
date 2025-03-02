CREATE PROCEDURE [dbo].[InsertIntoProducts]
        @Id INT,
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
    SET NOCOUNT ON

    INSERT INTO [dbo].[Products]
        (
            Id,
            SKU,
            name,
            EAN,
            producer_name,
            category,
            is_wire,
            shipping,
            available,
            is_vendor,
            default_image
        )
        VALUES
        (
            @Id,
            @SKU,
            @name,
            @EAN,
            @producer_name,
            @category,
            @is_wire,
            @shipping,
            @available,
            @is_vendor,
            @default_image
        )

END

GO