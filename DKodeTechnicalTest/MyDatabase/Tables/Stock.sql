CREATE TABLE [dbo].[Stock]
(
        Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
        product_id BIGINT UNIQUE NOT NULL,
        SKU VARCHAR(16) NOT NULL,
        unit VARCHAR(16) NULL,
        qty DECIMAL(10,4) NOT NULL,
        manufacturer VARCHAR(128) NULL,
        shipping VARCHAR(32) NOT NULL,
        shipping_cost DECIMAL(10,4) NULL,
        CONSTRAINT UC_Stock_SKU UNIQUE (SKU)
        --CONSTRAINT FK_Stock_Product FOREIGN KEY (product_id) REFERENCES Products(ID)
)
