CREATE TABLE [dbo].[Products]
(
        Id INT PRIMARY KEY NOT NULL,
        SKU VARCHAR(16) NOT NULL,
        name VARCHAR(128) NULL,
        EAN BIGINT NULL,
        producer_name VARCHAR(40) NOT NULL,
        category VARCHAR(512) NULL,
        is_wire BIT NULL,
        shipping VARCHAR(32) NOT NULL,
        available BIT NOT NULL,
        is_vendor BIT NOT NULL,
        default_image VARCHAR(512) NULL,
        CONSTRAINT UC_Products_SKU UNIQUE (SKU)
)
