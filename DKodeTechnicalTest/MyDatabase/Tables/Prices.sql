CREATE TABLE [dbo].[Prices]
(
        Id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
        warehouse_id VARCHAR(16) NOT NULL,
        SKU VARCHAR(16) NOT NULL,
        net_product_price DECIMAL(15,2) NOT NULL,
        net_product_price_after_discount DECIMAL(15,2) NOT NULL,
        VAT_rate INT NULL,
        net_product_price_after_discount_logistic_unit DECIMAL(15,2) NULL,
        CONSTRAINT UC_Prices_SKU UNIQUE (SKU)
        --CONSTRAINT FK_Prices_Product FOREIGN KEY (SKU) REFERENCES Products(SKU)
)
