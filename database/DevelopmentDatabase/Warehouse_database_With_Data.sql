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


-- Dumping database structure for warehouse
DROP DATABASE IF EXISTS `warehouse`;
CREATE DATABASE IF NOT EXISTS `warehouse` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;
USE `warehouse`;

-- Dumping structure for table warehouse.deliveries
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
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table warehouse.deliveries: ~8 rows (approximately)
DELETE FROM `deliveries`;
INSERT INTO `deliveries` (`ID`, `Store_ID`, `Warehouse_ID`, `Status`) VALUES
	(1, 1, 9, 'Being_prepared'),
	(2, 1, 9, 'Being_prepared'),
	(3, 1, 9, 'Being_prepared'),
	(4, 1, 9, 'Being_prepared'),
	(5, 1, 9, 'Being_prepared'),
	(6, 1, 10, 'Being_prepared'),
	(7, 1, 9, 'Being_prepared'),
	(8, 1, 10, 'Being_prepared');

-- Dumping structure for table warehouse.delivery_products
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
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table warehouse.delivery_products: ~4 rows (approximately)
DELETE FROM `delivery_products`;
INSERT INTO `delivery_products` (`ID`, `Delivery_ID`, `Product_ID`, `Amount`) VALUES
	(3, 5, 4, 14),
	(4, 6, 6, 10),
	(5, 7, 4, 12),
	(6, 8, 6, 12);

-- Dumping structure for table warehouse.products
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
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table warehouse.products: ~4 rows (approximately)
DELETE FROM `products`;
INSERT INTO `products` (`ID`, `Product_code`, `Product_type_ID`, `Shelve_ID`, `Amount`) VALUES
	(3, '1234dsa', 3, 10, 126),
	(4, 'aw321f', 2, 10, 122),
	(5, 'asd321', 3, 10, 1212),
	(6, '213asr4', 2, 11, 12);

-- Dumping structure for table warehouse.product_type
DROP TABLE IF EXISTS `product_type`;
CREATE TABLE IF NOT EXISTS `product_type` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Description` text DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table warehouse.product_type: ~2 rows (approximately)
DELETE FROM `product_type`;
INSERT INTO `product_type` (`ID`, `Name`, `Description`) VALUES
	(2, 'penuts', 'sweet and savory'),
	(3, 'juice', 'very liquid');

-- Dumping structure for table warehouse.roles
DROP TABLE IF EXISTS `roles`;
CREATE TABLE IF NOT EXISTS `roles` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table warehouse.roles: ~2 rows (approximately)
DELETE FROM `roles`;
INSERT INTO `roles` (`ID`, `Name`) VALUES
	(1, 'worker'),
	(2, 'admin');

-- Dumping structure for table warehouse.shelves
DROP TABLE IF EXISTS `shelves`;
CREATE TABLE IF NOT EXISTS `shelves` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) DEFAULT NULL,
  `Warehouse_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  KEY `warehouse` (`Warehouse_ID`) USING BTREE,
  CONSTRAINT `Warehouse` FOREIGN KEY (`Warehouse_ID`) REFERENCES `warehouses` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table warehouse.shelves: ~3 rows (approximately)
DELETE FROM `shelves`;
INSERT INTO `shelves` (`ID`, `Name`, `Warehouse_ID`) VALUES
	(10, 'waagh', 9),
	(11, 'bever', 10),
	(12, 'bevers', 9);

-- Dumping structure for table warehouse.stores
DROP TABLE IF EXISTS `stores`;
CREATE TABLE IF NOT EXISTS `stores` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) DEFAULT NULL,
  `Street` varchar(255) DEFAULT NULL,
  `Postcode` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table warehouse.stores: ~2 rows (approximately)
DELETE FROM `stores`;
INSERT INTO `stores` (`ID`, `Name`, `Street`, `Postcode`) VALUES
	(1, 'Jumbo', 'street', '1245Gs'),
	(2, 'Bumbo', 'stroot', '5432GS');

-- Dumping structure for table warehouse.users
DROP TABLE IF EXISTS `users`;
CREATE TABLE IF NOT EXISTS `users` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Passcode` varchar(255) NOT NULL,
  `Role_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `Role` (`Role_ID`) USING BTREE,
  CONSTRAINT `Role` FOREIGN KEY (`Role_ID`) REFERENCES `roles` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table warehouse.users: ~2 rows (approximately)
DELETE FROM `users`;
INSERT INTO `users` (`ID`, `Name`, `Passcode`, `Role_ID`) VALUES
	(3, 'Steve', '9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08', 2),
	(4, 'base', '60303ae22b998861bce3b28f33eec1be758a213c86c93c076dbe9f558c11c752', 1);

-- Dumping structure for table warehouse.user_warehouses
DROP TABLE IF EXISTS `user_warehouses`;
CREATE TABLE IF NOT EXISTS `user_warehouses` (
  `User_ID` int(11) DEFAULT NULL,
  `Warehouse_ID` int(11) DEFAULT NULL,
  KEY `Users` (`User_ID`),
  KEY `Warehouse_ID` (`Warehouse_ID`),
  CONSTRAINT `Users` FOREIGN KEY (`User_ID`) REFERENCES `users` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Warehouse_ID` FOREIGN KEY (`Warehouse_ID`) REFERENCES `warehouses` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table warehouse.user_warehouses: ~1 rows (approximately)
DELETE FROM `user_warehouses`;
INSERT INTO `user_warehouses` (`User_ID`, `Warehouse_ID`) VALUES
	(4, 9);

-- Dumping structure for table warehouse.warehouses
DROP TABLE IF EXISTS `warehouses`;
CREATE TABLE IF NOT EXISTS `warehouses` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) DEFAULT NULL,
  `Postcode` varchar(255) DEFAULT NULL,
  `Street` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table warehouse.warehouses: ~3 rows (approximately)
DELETE FROM `warehouses`;
INSERT INTO `warehouses` (`ID`, `Name`, `Postcode`, `Street`) VALUES
	(9, 'warehouse', '5316GV', 'straat'),
	(10, 'private', '1256GV', 'street'),
	(11, 'samador', 'samado', 'samado');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
