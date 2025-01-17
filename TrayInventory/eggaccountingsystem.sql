-- Create the database
CREATE DATABASE IF NOT EXISTS eggaccountingsystem;

-- Use the database
USE eggaccountingsystem;

-- Create `admins` table
CREATE TABLE IF NOT EXISTS admins (
    AdminID INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(50) NOT NULL,
    Password VARCHAR(255) NOT NULL
);

-- Create `dailyrate` table
CREATE TABLE IF NOT EXISTS dailyrate (
    RateID INT AUTO_INCREMENT PRIMARY KEY,
    Date DATE NOT NULL,
    EggRate DECIMAL(10, 2) NOT NULL
);

-- Create `shops` table
CREATE TABLE IF NOT EXISTS shops (
    ShopID INT AUTO_INCREMENT PRIMARY KEY,
    ShopName VARCHAR(100) NOT NULL,
    PreviousPending DECIMAL(10, 2) NOT NULL
);

-- Create `transaction` table
CREATE TABLE IF NOT EXISTS transaction (
    TransactionID INT AUTO_INCREMENT PRIMARY KEY,
    Date DATE NOT NULL,
    ShopID INT NOT NULL,
    TrayCount INT NOT NULL,
    Cost DECIMAL(10, 2) NOT NULL,
    AmountReceived DECIMAL(10, 2) NOT NULL,
    PendingAmount DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (ShopID) REFERENCES shops(ShopID)
);

-- Create `trayinventory` table
CREATE TABLE IF NOT EXISTS trayinventory (
    TrayInventoryID INT AUTO_INCREMENT PRIMARY KEY,
    TraysReceived INT NOT NULL,
    TraysIssued INT NOT NULL,
    AdminID INT NOT NULL,
    FOREIGN KEY (AdminID) REFERENCES admins(AdminID)
);
