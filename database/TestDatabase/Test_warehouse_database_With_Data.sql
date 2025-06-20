-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               11.1.3-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win64
-- HeidiSQL Version:             12.10.0.7000
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dumping database structure for testwarehouse
DROP DATABASE IF EXISTS `testwarehouse`;
CREATE DATABASE IF NOT EXISTS `testwarehouse` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;
USE `testwarehouse`;

-- Dumping structure for table testwarehouse.deliveries
DROP TABLE IF EXISTS `deliveries`;
CREATE TABLE IF NOT EXISTS `deliveries` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Store_ID` int(11) NOT NULL,
  `Warehouse_ID` int(11) DEFAULT NULL,
  `Status` enum('Deliverd','On_the_way','Being_prepared') DEFAULT 'Being_prepared',
  PRIMARY KEY (`ID`) USING BTREE,
  KEY `Store` (`Store_ID`),
  KEY `Delivery_Warehouse` (`Warehouse_ID`),
  CONSTRAINT `Delivery_Warehouse` FOREIGN KEY (`Warehouse_ID`) REFERENCES `warehouses` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Store` FOREIGN KEY (`Store_ID`) REFERENCES `stores` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table testwarehouse.deliveries: ~0 rows (approximately)
DELETE FROM `deliveries`;

-- Dumping structure for table testwarehouse.delivery_products
DROP TABLE IF EXISTS `delivery_products`;
CREATE TABLE IF NOT EXISTS `delivery_products` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Delivery_ID` int(11) DEFAULT NULL,
  `Product_ID` int(11) DEFAULT NULL,
  `Amount` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `Product` (`Product_ID`),
  KEY `Delivery` (`Delivery_ID`),
  CONSTRAINT `Delivery` FOREIGN KEY (`Delivery_ID`) REFERENCES `deliveries` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Product` FOREIGN KEY (`Product_ID`) REFERENCES `products` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table testwarehouse.delivery_products: ~0 rows (approximately)
DELETE FROM `delivery_products`;

-- Dumping structure for table testwarehouse.products
DROP TABLE IF EXISTS `products`;
CREATE TABLE IF NOT EXISTS `products` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Product_code` varchar(255) DEFAULT NULL,
  `Product_type_ID` int(11) NOT NULL,
  `Shelve_ID` int(11) NOT NULL,
  `Amount` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  KEY `Product_Type` (`Product_type_ID`) USING BTREE,
  KEY `Shelve` (`Shelve_ID`),
  CONSTRAINT `Product_Type` FOREIGN KEY (`Product_type_ID`) REFERENCES `product_type` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Shelve` FOREIGN KEY (`Shelve_ID`) REFERENCES `shelves` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table testwarehouse.products: ~4 rows (approximately)
DELETE FROM `products`;
INSERT INTO `products` (`ID`, `Product_code`, `Product_type_ID`, `Shelve_ID`, `Amount`) VALUES
	(7, 'gdsr24', 4, 13, 12),
	(8, '12dsa3', 4, 14, 12313),
	(9, '12BAst', 4, 15, 1),
	(10, 'bfsa3r', 4, 15, 145);

-- Dumping structure for table testwarehouse.product_type
DROP TABLE IF EXISTS `product_type`;
CREATE TABLE IF NOT EXISTS `product_type` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Description` text DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table testwarehouse.product_type: ~1 rows (approximately)
DELETE FROM `product_type`;
INSERT INTO `product_type` (`ID`, `Name`, `Description`) VALUES
	(4, 'beans', 'test');

-- Dumping structure for table testwarehouse.roles
DROP TABLE IF EXISTS `roles`;
CREATE TABLE IF NOT EXISTS `roles` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table testwarehouse.roles: ~2 rows (approximately)
DELETE FROM `roles`;
INSERT INTO `roles` (`ID`, `Name`) VALUES
	(1, 'worker'),
	(2, 'admin');

-- Dumping structure for table testwarehouse.shelves
DROP TABLE IF EXISTS `shelves`;
CREATE TABLE IF NOT EXISTS `shelves` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) DEFAULT NULL,
  `Warehouse_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  KEY `warehouse` (`Warehouse_ID`) USING BTREE,
  CONSTRAINT `Warehouse` FOREIGN KEY (`Warehouse_ID`) REFERENCES `warehouses` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table testwarehouse.shelves: ~4 rows (approximately)
DELETE FROM `shelves`;
INSERT INTO `shelves` (`ID`, `Name`, `Warehouse_ID`) VALUES
	(13, 'Shelve', 1),
	(14, 'Shelve', 1),
	(15, 'MasterShelve', 2),
	(16, 'Shelver', 2);

-- Dumping structure for table testwarehouse.stores
DROP TABLE IF EXISTS `stores`;
CREATE TABLE IF NOT EXISTS `stores` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) DEFAULT NULL,
  `Street` varchar(255) DEFAULT NULL,
  `Postcode` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table testwarehouse.stores: ~0 rows (approximately)
DELETE FROM `stores`;

-- Dumping structure for table testwarehouse.users
DROP TABLE IF EXISTS `users`;
CREATE TABLE IF NOT EXISTS `users` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Passcode` varchar(255) NOT NULL,
  `Role_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `Role` (`Role_ID`) USING BTREE,
  CONSTRAINT `Role` FOREIGN KEY (`Role_ID`) REFERENCES `roles` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table testwarehouse.users: ~1 rows (approximately)
DELETE FROM `users`;
INSERT INTO `users` (`ID`, `Name`, `Passcode`, `Role_ID`) VALUES
	(5, 'Steve', '9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08', 2);

-- Dumping structure for table testwarehouse.user_warehouses
DROP TABLE IF EXISTS `user_warehouses`;
CREATE TABLE IF NOT EXISTS `user_warehouses` (
  `User_ID` int(11) DEFAULT NULL,
  `Warehouse_ID` int(11) DEFAULT NULL,
  KEY `Users` (`User_ID`),
  KEY `Warehouse_ID` (`Warehouse_ID`),
  CONSTRAINT `Users` FOREIGN KEY (`User_ID`) REFERENCES `users` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Warehouse_ID` FOREIGN KEY (`Warehouse_ID`) REFERENCES `warehouses` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table testwarehouse.user_warehouses: ~0 rows (approximately)
DELETE FROM `user_warehouses`;

-- Dumping structure for table testwarehouse.warehouses
DROP TABLE IF EXISTS `warehouses`;
CREATE TABLE IF NOT EXISTS `warehouses` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) DEFAULT NULL,
  `Postcode` varchar(255) DEFAULT NULL,
  `Street` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table testwarehouse.warehouses: ~2 rows (approximately)
DELETE FROM `warehouses`;
INSERT INTO `warehouses` (`ID`, `Name`, `Postcode`, `Street`) VALUES
	(1, 'Warehouse', '1245BS', 'Street 04'),
	(2, 'WarehouseShelves', '1254BS', 'ShelveStreet');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
