USE PerfumeStore;
GO

INSERT INTO Brands (Name) VALUES ('Chanel'), ('Dior'), ('Versace');

INSERT INTO Categories (Name) VALUES ('Квіткові'), ('Деревні'), ('Цитрусові');

INSERT INTO Perfumes (Name, BrandID, CategoryID, Price, Volume, StockQuantity)
VALUES 
('Sauvage', 2, 2, 3500.00, 100, 10),
('No. 5', 1, 1, 4200.00, 50, 5),
('Eros', 3, 3, 2800.00, 100, 15);
GO

SELECT * FROM Perfumes;