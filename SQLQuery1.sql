USE PerfumeStore;
GO

INSERT INTO Categories (Name) 
VALUES (N'Чоловічі'), (N'Жіночі'), (N'Унісекс');
GO

INSERT INTO Brands (Name) 
VALUES (N'Chanel'), (N'Dior'), (N'Versace');
GO

INSERT INTO Perfumes (Name, BrandId, CategoryId, Price, Volume)
VALUES 
(N'Sauvage', 2, 1, 4500.00, 100),
(N'Chanel No. 5', 1, 2, 5200.00, 50),
(N'Eros', 3, 1, 3800.00, 100);
GO

SELECT p.Name, b.Name AS Brand, c.Name AS Category, p.Price 
FROM Perfumes p
JOIN Brands b ON p.BrandId = b.BrandId
JOIN Categories c ON p.CategoryId = c.CategoryId;