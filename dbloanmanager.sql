-- MySQL dump 10.13  Distrib 8.0.27, for macos11 (arm64)
--
-- Host: localhost    Database: dbloanmanager
-- ------------------------------------------------------
-- Server version	8.0.27

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Customer`
--

DROP TABLE IF EXISTS `Customer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Customer` (
  `CustomerId` int NOT NULL AUTO_INCREMENT,
  `Names` varchar(45) NOT NULL,
  `Lastnames` varchar(45) NOT NULL,
  `DNI` varchar(16) DEFAULT NULL,
  `Phone` varchar(15) NOT NULL,
  `Address` varchar(500) DEFAULT NULL,
  `Latitude` float DEFAULT NULL,
  `Longitude` float DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  `ModifiedOn` datetime DEFAULT NULL,
  PRIMARY KEY (`CustomerId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Customer`
--

LOCK TABLES `Customer` WRITE;
/*!40000 ALTER TABLE `Customer` DISABLE KEYS */;
INSERT INTO `Customer` VALUES (2,'Pepito','Perez','001-000000-0000U','22336789','Del arbolito 1 C. al sur.',0,0,'2021-11-11 03:33:02','2021-11-11 03:33:02'),(3,'Juan','Perez','001-000000-0000V','22449988','Km 18 carretera nueva a Leon',0,0,'2021-11-14 02:54:52','2021-11-14 02:54:52'),(5,'Tatiana','Cabezas','001-000011-0011S','75020259','Camino a los Vanegas 3 arboles de jocote al cerro',0,0,'2021-11-25 21:50:30','2021-11-25 21:50:30');
/*!40000 ALTER TABLE `Customer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Loan`
--

DROP TABLE IF EXISTS `Loan`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Loan` (
  `LoanId` int NOT NULL AUTO_INCREMENT,
  `Amount` decimal(9,2) NOT NULL,
  `InterestRate` decimal(9,2) NOT NULL,
  `LoanDate` datetime NOT NULL,
  `Frequency` enum('Once','Weekly','BiWeekly','Monthly') DEFAULT NULL,
  `Terms` int DEFAULT '0',
  `LoanType` enum('Regular','NoCalendar') NOT NULL,
  `Status` enum('PastDue','PaidInFull','WriteOff','UpToDay','PayOff') NOT NULL,
  `Currency` enum('Dolar','Euro','Cordobas') NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `ModifiedOn` datetime NOT NULL,
  `CustomerId` int NOT NULL,
  `ScheduleMethod` enum('Straight Line','Declining balance') DEFAULT NULL,
  `Balance` decimal(9,2) DEFAULT NULL,
  PRIMARY KEY (`LoanId`),
  KEY `fk_Loan_Customer_idx` (`CustomerId`),
  CONSTRAINT `fk_Loan_Customer` FOREIGN KEY (`CustomerId`) REFERENCES `Customer` (`CustomerId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Loan`
--

LOCK TABLES `Loan` WRITE;
/*!40000 ALTER TABLE `Loan` DISABLE KEYS */;
/*!40000 ALTER TABLE `Loan` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Payment`
--

DROP TABLE IF EXISTS `Payment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Payment` (
  `PaymentId` int NOT NULL AUTO_INCREMENT,
  `Type` enum('Book','Regular','PayOff','WriteOff','Principal Only') NOT NULL,
  `Date` datetime NOT NULL,
  `AmountReceived` decimal(9,2) NOT NULL,
  `CollectedPrincipal` decimal(9,2) NOT NULL,
  `CollectedInterest` decimal(9,2) NOT NULL,
  `BeginningPrincipal` decimal(9,2) NOT NULL,
  `EndingPrincipal` decimal(9,2) NOT NULL,
  `BeginningInterest` decimal(9,2) NOT NULL,
  `EndingInterest` decimal(9,2) NOT NULL,
  `CreatedOn` datetime NOT NULL,
  `ModifiedOn` datetime DEFAULT NULL,
  `LoanId` int NOT NULL,
  PRIMARY KEY (`PaymentId`),
  KEY `fk_Payment_Loan1_idx` (`LoanId`),
  CONSTRAINT `fk_Payment_Loan1` FOREIGN KEY (`LoanId`) REFERENCES `Loan` (`LoanId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Payment`
--

LOCK TABLES `Payment` WRITE;
/*!40000 ALTER TABLE `Payment` DISABLE KEYS */;
/*!40000 ALTER TABLE `Payment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PaymentSchedule`
--

DROP TABLE IF EXISTS `PaymentSchedule`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `PaymentSchedule` (
  `PaymentScheduleId` int NOT NULL AUTO_INCREMENT,
  `Type` enum('Regular') NOT NULL,
  `Status` enum('Pending','Paid','Deleted') NOT NULL,
  `Amount` decimal(9,2) NOT NULL,
  `Principal` decimal(9,2) NOT NULL,
  `Interest` decimal(9,2) NOT NULL,
  `AmountPaid` decimal(9,2) DEFAULT NULL,
  `PrincipalPaid` decimal(9,2) DEFAULT NULL,
  `InterestPaid` decimal(9,2) DEFAULT NULL,
  `Date` datetime NOT NULL,
  `DatePaid` datetime DEFAULT NULL,
  `Createdon` datetime NOT NULL,
  `ModifiedOn` datetime DEFAULT NULL,
  `LoanId` int NOT NULL,
  `PaymentId` int DEFAULT NULL,
  PRIMARY KEY (`PaymentScheduleId`),
  KEY `fk_PaymentSchedule_Loan1_idx` (`LoanId`),
  KEY `fk_PaymentSchedule_Payment1_idx` (`PaymentId`),
  CONSTRAINT `fk_PaymentSchedule_Loan1` FOREIGN KEY (`LoanId`) REFERENCES `Loan` (`LoanId`),
  CONSTRAINT `fk_PaymentSchedule_Payment1` FOREIGN KEY (`PaymentId`) REFERENCES `Payment` (`PaymentId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PaymentSchedule`
--

LOCK TABLES `PaymentSchedule` WRITE;
/*!40000 ALTER TABLE `PaymentSchedule` DISABLE KEYS */;
/*!40000 ALTER TABLE `PaymentSchedule` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `User`
--

DROP TABLE IF EXISTS `User`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `User` (
  `UserId` int NOT NULL AUTO_INCREMENT,
  `UserName` varchar(45) DEFAULT NULL,
  `Password` varchar(45) DEFAULT NULL,
  `Role` varchar(20) NOT NULL,
  `Email` varchar(50) NOT NULL,
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `User`
--

LOCK TABLES `User` WRITE;
/*!40000 ALTER TABLE `User` DISABLE KEYS */;
INSERT INTO `User` VALUES (1,'Pepito','Passw0rd!','Admin','pepito.perez@gmail.com'),(2,'Ana','D3fault!','Default','ana.conda@gmail.com');
/*!40000 ALTER TABLE `User` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Warranty`
--

DROP TABLE IF EXISTS `Warranty`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Warranty` (
  `WarrantyId` int NOT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `Description` varchar(200) DEFAULT NULL,
  `Location` varchar(200) DEFAULT NULL,
  `Documents` varchar(250) DEFAULT NULL,
  `ExecutionTerms` int DEFAULT NULL,
  `Frequency` enum('Weekly','BiWeekly','Monthly') DEFAULT NULL,
  `PenaltyRate` float DEFAULT NULL,
  `DOFD` datetime DEFAULT NULL,
  `ExecutionDate` datetime DEFAULT NULL,
  `WarrantyStatus` enum('guaranteed','Executed','Execution Process') DEFAULT NULL,
  `CreatedOn` datetime DEFAULT NULL,
  `ModifiedOn` datetime DEFAULT NULL,
  `CreatedBy` varchar(45) DEFAULT NULL,
  `LoanId` int NOT NULL,
  PRIMARY KEY (`WarrantyId`),
  KEY `fk_Warranty_Loan1_idx` (`LoanId`),
  CONSTRAINT `fk_Warranty_Loan1` FOREIGN KEY (`LoanId`) REFERENCES `Loan` (`LoanId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Warranty`
--

LOCK TABLES `Warranty` WRITE;
/*!40000 ALTER TABLE `Warranty` DISABLE KEYS */;
/*!40000 ALTER TABLE `Warranty` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-12-17 23:47:11
