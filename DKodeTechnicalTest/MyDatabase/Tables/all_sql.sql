IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Products')
BEGIN
    CREATE TABLE Products (
        Id INT PRIMARY KEY NOT NULL,
        SKU VARCHAR(16) NOT NULL,
        name VARCHAR(128) NULL,
        reference_number VARCHAR(128) NULL,
        EAN BIGINT NULL,
        can_be_returned BIT NULL,
        producer_name VARCHAR(40) NOT NULL,
        category VARCHAR(512) NULL,
        is_wire BIT NULL,
        shipping VARCHAR(32) NOT NULL,
        package_size VARCHAR(8) NULL,
        available BIT NOT NULL,
        logistic_height BIGINT NULL,
        logistic_width BIGINT NULL,
        logistic_length BIGINT NULL,
        logistic_weight DECIMAL(10,8) NULL,
        is_vendor BIT NOT NULL,
        available_in_parcel_locker BIT NOT NULL,
        default_image VARCHAR(512) NULL
    );
END;

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Stock')
BEGIN
    CREATE TABLE Stock (
        Id INT PRIMARY KEY NOT NULL,
        product_id BIGINT UNIQUE NOT NULL,
        sku VARCHAR(16) NOT NULL,
        unit VARCHAR(16) NULL,
        qty DECIMAL(10,8) NOT NULL,
        manufacturer_name VARCHAR(128) NULL,
        manufacturer_ref_num VARCHAR(64) NULL,
        shipping VARCHAR(32) NOT NULL,
        shipping_cost DECIMAL(10,8) NULL,
        --CONSTRAINT FK_Stock_Product FOREIGN KEY (product_id) REFERENCES Products(ID)
    );
END;

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Prices')
BEGIN
    CREATE TABLE Prices (
        Id INT PRIMARY KEY NOT NULL,
        warehouse_id VARCHAR(16) NOT NULL,
        SKU VARCHAR(16) NOT NULL,
        net_product_price DECIMAL(10,2) NOT NULL,
        net_product_price_after_discount DECIMAL(10,2) NOT NULL,
        VAT_rate INT NULL,
        net_product_price_after_discount_logistic_unit DECIMAL(10,2) NULL,
        --CONSTRAINT FK_Prices_Product FOREIGN KEY (SKU) REFERENCES Products(SKU)
    );
END;
