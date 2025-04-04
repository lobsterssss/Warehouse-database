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
CREATE DATABASE IF NOT EXISTS `warehouse` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;
USE `warehouse`;

-- Dumping structure for table warehouse.deliveries
CREATE TABLE IF NOT EXISTS `deliveries` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Truck_ID` int(11) NOT NULL,
  `Store_ID` int(11) NOT NULL,
  `Amount` int(11) DEFAULT NULL,
  `Status` enum('Deliverd','On_the_way','Being_prepared') DEFAULT 'Being_prepared',
  PRIMARY KEY (`ID`) USING BTREE,
  KEY `Truck` (`Truck_ID`) USING BTREE,
  KEY `Store` (`Store_ID`),
  CONSTRAINT `Store` FOREIGN KEY (`Store_ID`) REFERENCES `stores` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Truck` FOREIGN KEY (`Truck_ID`) REFERENCES `trucks` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table warehouse.deliveries: ~0 rows (approximately)

-- Dumping structure for table warehouse.delivery_products
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table warehouse.delivery_products: ~0 rows (approximately)

-- Dumping structure for table warehouse.logs
CREATE TABLE IF NOT EXISTS `logs` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Log` text DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table warehouse.logs: ~0 rows (approximately)

-- Dumping structure for table warehouse.products
CREATE TABLE IF NOT EXISTS `products` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Product_code` varchar(255) DEFAULT NULL,
  `Product_info_ID` int(11) NOT NULL,
  `Shelve_ID` int(11) NOT NULL,
  `Amount` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  KEY `Shelve` (`Shelve_ID`),
  KEY `Product_Type` (`Product_info_ID`),
  CONSTRAINT `Product_Type` FOREIGN KEY (`Product_info_ID`) REFERENCES `product_type` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Shelve` FOREIGN KEY (`Shelve_ID`) REFERENCES `shelves` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table warehouse.products: ~0 rows (approximately)

-- Dumping structure for table warehouse.product_type
CREATE TABLE IF NOT EXISTS `product_type` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Description` text DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table warehouse.product_type: ~0 rows (approximately)

-- Dumping structure for table warehouse.reports
CREATE TABLE IF NOT EXISTS `reports` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Report_Type_ID` int(11) NOT NULL,
  `User_ID` int(11) DEFAULT NULL,
  `Product_ID` int(11) DEFAULT NULL,
  `Report` text DEFAULT NULL,
  `ReportedAt` datetime DEFAULT NULL,
  `HappendAt` datetime DEFAULT NULL,
  `Status` enum('In behandeling','Opgelost') DEFAULT 'In behandeling',
  PRIMARY KEY (`ID`),
  KEY `Report_type` (`Report_Type_ID`),
  KEY `Users_` (`User_ID`),
  KEY `Products_` (`Product_ID`),
  CONSTRAINT `Products_` FOREIGN KEY (`Product_ID`) REFERENCES `products` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Report_type` FOREIGN KEY (`Report_Type_ID`) REFERENCES `report_types` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Users_` FOREIGN KEY (`User_ID`) REFERENCES `users` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table warehouse.reports: ~0 rows (approximately)

-- Dumping structure for table warehouse.report_types
CREATE TABLE IF NOT EXISTS `report_types` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table warehouse.report_types: ~0 rows (approximately)

-- Dumping structure for table warehouse.roles
CREATE TABLE IF NOT EXISTS `roles` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table warehouse.roles: ~0 rows (approximately)

-- Dumping structure for table warehouse.shelves
CREATE TABLE IF NOT EXISTS `shelves` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Warehouse_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  KEY `warehouse` (`Warehouse_ID`) USING BTREE,
  CONSTRAINT `Warehouse` FOREIGN KEY (`Warehouse_ID`) REFERENCES `warehouses` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table warehouse.shelves: ~0 rows (approximately)

-- Dumping structure for table warehouse.stores
CREATE TABLE IF NOT EXISTS `stores` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) DEFAULT NULL,
  `Street` varchar(255) DEFAULT NULL,
  `Postcode` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table warehouse.stores: ~0 rows (approximately)

-- Dumping structure for table warehouse.trucks
CREATE TABLE IF NOT EXISTS `trucks` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `User_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  KEY `Driver` (`User_ID`),
  CONSTRAINT `Driver` FOREIGN KEY (`User_ID`) REFERENCES `users` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table warehouse.trucks: ~0 rows (approximately)

-- Dumping structure for table warehouse.users
CREATE TABLE IF NOT EXISTS `users` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Passcode` varchar(255) NOT NULL,
  `Role_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `Role` (`Role_ID`) USING BTREE,
  CONSTRAINT `Role` FOREIGN KEY (`Role_ID`) REFERENCES `roles` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table warehouse.users: ~0 rows (approximately)

-- Dumping structure for table warehouse.user_warehouses
CREATE TABLE IF NOT EXISTS `user_warehouses` (
  `User_ID` int(11) DEFAULT NULL,
  `Warehouse_ID` int(11) DEFAULT NULL,
  KEY `Users` (`User_ID`),
  KEY `Warehouse_ID` (`Warehouse_ID`),
  CONSTRAINT `Users` FOREIGN KEY (`User_ID`) REFERENCES `users` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `Warehouse_ID` FOREIGN KEY (`Warehouse_ID`) REFERENCES `warehouses` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table warehouse.user_warehouses: ~0 rows (approximately)

-- Dumping structure for table warehouse.warehouses
CREATE TABLE IF NOT EXISTS `warehouses` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) DEFAULT NULL,
  `Postcode` varchar(255) DEFAULT NULL,
  `Street` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table warehouse.warehouses: ~1 rows (approximately)
INSERT INTO `warehouses` (`ID`, `Name`, `Postcode`, `Street`) VALUES
	(1, NULL, NULL, NULL);

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
