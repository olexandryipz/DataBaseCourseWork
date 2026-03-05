CREATE DATABASE PerfumeStore;
GO
USE PerfumeStore;
GO

CREATE TABLE Brands (
    BrandID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL
);

CREATE TABLE Perfumes (
    PerfumeID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(200) NOT NULL,
    BrandID INT FOREIGN KEY REFERENCES Brands(BrandID),
    CategoryID INT FOREIGN KEY REFERENCES Categories(CategoryID),
    Price DECIMAL(10, 2) NOT NULL,
    Volume INT, -- в мл
    StockQuantity INT DEFAULT 0
);
