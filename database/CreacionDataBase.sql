-- Creación de la base de datos
CREATE DATABASE appInventarioDB;
GO

USE appInventarioDB;
GO

-- Creación de la tabla Productos
CREATE TABLE Productos (
    IdProducto INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255) NULL,
    Categoria NVARCHAR(50) NULL,
    ImagenUrl NVARCHAR(255) NULL,
    Precio DECIMAL(10,2) NOT NULL,
    Stock INT NOT NULL DEFAULT 0
);
GO

-- Creación de la tabla Transacciones
CREATE TABLE Transacciones (
    IdTransaccion INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATETIME NOT NULL,
    TipoTransaccion NVARCHAR(1) NOT NULL, -- 'C' para compra, 'V' para venta
    IdProducto INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    PrecioTotal DECIMAL(12,2) NOT NULL,
    Detalle NVARCHAR(255) NULL,
    CONSTRAINT FK_Transacciones_Productos FOREIGN KEY (IdProducto)
        REFERENCES Productos(IdProducto)
);
GO

-- creacion de datos
INSERT INTO Productos (Nombre, Descripcion, Categoria, ImagenUrl, Precio, Stock) VALUES
('Laptop Dell Inspiron', 'Laptop Dell con procesador i7, 16GB RAM', 'Electrónica', 'https://example.com/images/laptop-dell.jpg', 850.00, 15),
('Smartphone Samsung Galaxy S21', 'Smartphone Samsung con pantalla AMOLED', 'Electrónica', 'https://example.com/images/samsung-s21.jpg', 650.00, 30),
('Mouse Inalámbrico Logitech', 'Mouse inalámbrico ergonómico', 'Accesorios', 'https://example.com/images/logitech-mouse.jpg', 25.99, 100),
('Teclado Mecánico Corsair', 'Teclado mecánico RGB para gaming', 'Accesorios', 'https://example.com/images/corsair-teclado.jpg', 120.50, 50),
('Monitor LG 24"', 'Monitor LG Full HD de 24 pulgadas', 'Electrónica', 'https://example.com/images/lg-monitor.jpg', 180.75, 20);
GO


INSERT INTO Transacciones (Fecha, TipoTransaccion, IdProducto, Cantidad, PrecioUnitario, PrecioTotal, Detalle) VALUES
(GETDATE(), 'C', 1, 5, 840.00, 4200.00, 'Compra inicial de laptops'),
(GETDATE(), 'V', 2, 3, 650.00, 1950.00, 'Venta de smartphones Samsung'),
(GETDATE(), 'C', 3, 20, 24.50, 490.00, 'Compra de mouse inalámbricos'),
(GETDATE(), 'V', 4, 10, 120.00, 1200.00, 'Venta de teclados mecánicos'),
(GETDATE(), 'C', 5, 7, 175.00, 1225.00, 'Compra de monitores LG');
GO
