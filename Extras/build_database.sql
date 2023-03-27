-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Mar 15, 2023 at 05:08 AM
-- Server version: 8.0.32
-- PHP Version: 8.1.6

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `salerno_server`
--

-- --------------------------------------------------------

--
-- Table structure for table `accounts`
--

CREATE TABLE `accounts` (
  `AccountId` bigint NOT NULL,
  `EmployeeId` bigint NOT NULL,
  `Password` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RefreshToken` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `addons`
--

CREATE TABLE `addons` (
  `AddonId` bigint NOT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Price` decimal(65,30) NOT NULL,
  `ModifierId` bigint NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `addons`
--

INSERT INTO `addons` (`AddonId`, `Name`, `Price`, `ModifierId`) VALUES
(13, 'Mozzarella Cheese', '1.000000000000000000000000000000', 18),
(14, 'Cheddar Cheese', '1.000000000000000000000000000000', 18),
(15, 'Hot Giardinera', '0.750000000000000000000000000000', 18),
(16, 'Sweet Peppers', '0.750000000000000000000000000000', 18),
(18, 'New Option', '0.000000000000000000000000000000', 18),
(19, 'New Option', '0.000000000000000000000000000000', 18),
(20, 'Add Ketchup', '0.000000000000000000000000000000', 19),
(21, 'Add Chili', '0.500000000000000000000000000000', 19),
(22, 'Add Cheese', '0.500000000000000000000000000000', 19),
(23, 'Add Grilled Onion', '0.750000000000000000000000000000', 19),
(28, 'Add Ketchup', '0.000000000000000000000000000000', 23),
(29, 'Add Chili', '0.500000000000000000000000000000', 23),
(30, 'Add Cheese', '0.500000000000000000000000000000', 23),
(31, 'Add Grilled Onion', '0.750000000000000000000000000000', 23),
(32, 'Add Ketchup', '0.000000000000000000000000000000', 24),
(33, 'Add Chili', '0.500000000000000000000000000000', 24),
(34, 'Add Cheese', '0.500000000000000000000000000000', 24),
(35, 'Add Grilled Onion', '0.750000000000000000000000000000', 24),
(36, 'Add Ketchup', '0.000000000000000000000000000000', 25),
(39, 'Add Jalapenos', '0.500000000000000000000000000000', 27),
(42, 'Add Ketchup', '0.000000000000000000000000000000', 29),
(43, 'Add Mustard', '0.000000000000000000000000000000', 29),
(44, 'Add Sport Pepper', '0.000000000000000000000000000000', 29),
(45, 'Add Cup of Cheese', '1.000000000000000000000000000000', 30),
(46, 'Add Cup of Chili', '1.000000000000000000000000000000', 30),
(52, 'Add Ketchup', '0.000000000000000000000000000000', 33),
(53, 'Add Chili', '0.500000000000000000000000000000', 33),
(54, 'Add Cheese', '0.500000000000000000000000000000', 33),
(55, 'Add Grilled Onion', '0.750000000000000000000000000000', 33);

-- --------------------------------------------------------

--
-- Table structure for table `customeraccounts`
--

CREATE TABLE `customeraccounts` (
  `CustomerAccountId` bigint NOT NULL,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Password` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `FirstName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LastName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RefreshToken` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `customeraccounts`
--

INSERT INTO `customeraccounts` (`CustomerAccountId`, `Email`, `Password`, `FirstName`, `LastName`, `PhoneNumber`, `RefreshToken`) VALUES
(1, 'anthony@gmail.com', '123456', 'anthony', 'salerno', '1234567890', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJFbWFpbCI6ImFudGhvbnlAZ21haWwuY29tIiwianRpIjoiZjYzNGIwY2MtNWVkYi00NjFlLWFmOWItYWI1ZGQzNTZjOTEzIiwiZXhwIjoxNjc3NjkxMTQ2LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo3MDc0IiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NzA3NCJ9.2tKOeZ18ZnoWZg-NexZajIhE-iTpsDxcBq0jM05MqO0');

-- --------------------------------------------------------

--
-- Table structure for table `employees`
--

CREATE TABLE `employees` (
  `EmployeeId` bigint NOT NULL,
  `FirstName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LastName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `BackOfficeAccess` tinyint(1) NOT NULL,
  `RegisterCode` int NOT NULL,
  `EmployeeRole` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `employees`
--

INSERT INTO `employees` (`EmployeeId`, `FirstName`, `LastName`, `PhoneNumber`, `Email`, `BackOfficeAccess`, `RegisterCode`, `EmployeeRole`) VALUES
(1, 'Anthony', 'Salerno Jr', '6303622052', 'anthony@gmail', 1, 6553, 'Manager'),
(2, 'Frankie', 'Salerno', '6305545627', 'frankie@gmail.com', 1, 1103, 'Manager');

-- --------------------------------------------------------

--
-- Table structure for table `groupoptions`
--

CREATE TABLE `groupoptions` (
  `GroupOptionId` bigint NOT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Price` decimal(65,30) NOT NULL,
  `GroupId` bigint NOT NULL,
  `IsDefault` tinyint(1) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `groupoptions`
--

INSERT INTO `groupoptions` (`GroupOptionId`, `Name`, `Price`, `GroupId`, `IsDefault`) VALUES
(23, 'Dry', '0.000000000000000000000000000000', 33, 0),
(24, 'Wet', '0.000000000000000000000000000000', 33, 0),
(25, 'Soaked', '0.000000000000000000000000000000', 33, 0),
(28, 'New Option', '0.000000000000000000000000000000', 33, 0),
(29, 'New Option', '0.000000000000000000000000000000', 35, 0),
(30, 'Barbeque', '0.000000000000000000000000000000', 36, 0),
(31, 'Honey Mustard', '0.000000000000000000000000000000', 36, 0),
(32, 'Ranch', '0.000000000000000000000000000000', 36, 0),
(33, 'Buffalo', '0.000000000000000000000000000000', 36, 0),
(34, 'Ketchup', '0.000000000000000000000000000000', 36, 0),
(40, 'Regular', '0.000000000000000000000000000000', 38, 1),
(41, 'Large', '0.650000000000000000000000000000', 38, 0),
(42, 'Hot Dog Bun', '0.000000000000000000000000000000', 39, 1),
(43, 'French Roll', '0.000000000000000000000000000000', 39, 0);

-- --------------------------------------------------------

--
-- Table structure for table `groups`
--

CREATE TABLE `groups` (
  `GroupId` bigint NOT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ModifierId` bigint NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `groups`
--

INSERT INTO `groups` (`GroupId`, `Name`, `ModifierId`, `Description`) VALUES
(33, 'Gravy', 18, ''),
(35, 'New Group', 18, ''),
(36, 'Sauce', 20, ''),
(38, 'Size', 37, ''),
(39, 'Breading', 25, '');

-- --------------------------------------------------------

--
-- Table structure for table `items`
--

CREATE TABLE `items` (
  `ItemId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Department` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Category` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UPC` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `SKU` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Price` decimal(65,30) NOT NULL,
  `Discountable` tinyint(1) NOT NULL,
  `Taxable` tinyint(1) NOT NULL,
  `TrackingInventory` tinyint(1) NOT NULL,
  `Cost` decimal(65,30) NOT NULL,
  `AssignedCost` decimal(65,30) NOT NULL,
  `Quantity` int NOT NULL,
  `ReorderTrigger` int NOT NULL,
  `RecommendedOrder` int NOT NULL,
  `LastSoldDate` datetime(6) NOT NULL,
  `Supplier` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LiabilityItem` tinyint(1) NOT NULL,
  `LiabilityRedemptionTender` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TaxGroupOrRate` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `items`
--

INSERT INTO `items` (`ItemId`, `Name`, `Department`, `Category`, `UPC`, `SKU`, `Price`, `Discountable`, `Taxable`, `TrackingInventory`, `Cost`, `AssignedCost`, `Quantity`, `ReorderTrigger`, `RecommendedOrder`, `LastSoldDate`, `Supplier`, `LiabilityItem`, `LiabilityRedemptionTender`, `TaxGroupOrRate`) VALUES
('03475B0D-9D5C-4D30-8E5A-84CE8A6E350C', 'Brisk Lemon Iced Tea', 'Brisk Lemon Iced Tea', 'general', '', '400000000565', '1.000000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '0.360000000000000000000000000000', 0, 0, 0, '2022-03-04 00:00:00.000000', 'not tracked', 0, '', '0'),
('066E9A13-F8AD-49F4-B4D9-5FD3FE673024', 'Cup of Chili Sauce', 'Cup of Chili Sauce', 'general', '', '400000000367', '1.000000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '0.150000000000000000000000000000', 0, 0, 0, '2022-03-05 14:58:32.000000', 'not tracked', 0, '', '0'),
('0F806B56-D361-4DF3-83C2-455D73CFE41E', 'Diet Coke Can', 'Diet Coke 12 oz', 'general', '', '400000000084', '1.000000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '0.320000000000000000000000000000', 0, 0, 0, '2021-06-05 10:51:17.000000', 'Sams Club', 0, '', '0'),
('14764368-C6EB-4413-B9A6-547AA298AECF', 'Green River Ice Cream Float', 'Green River Floats', 'general', '', '400000000527', '3.930000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '1.750000000000000000000000000000', 0, 0, 0, '2020-05-06 15:11:20.000000', 'not tracked', 0, '', '0'),
('18E69BDE-10D4-4AA6-B198-F542B2F249EE', 'Chicken Strips - 3 Piece', 'Chicken Strips 3', 'general', '', '400000000244', '5.630000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '1.500000000000000000000000000000', 0, 0, 0, '2022-03-06 13:37:14.000000', 'not tracked', 0, '', '0'),
('1DD95CD4-27DB-4F46-9EF2-5D289CAC41D1', 'Cup of Cheese', 'Cup of Cheese', 'general', '', '400000000350', '1.000000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '0.150000000000000000000000000000', 0, 0, 0, '2022-03-06 17:16:19.000000', 'not tracked', 0, '', '0'),
('1E4C8EFB-3F5C-4DA4-B401-DA23964E8C8D', 'Chicago Pizza Puff - Beef', 'Pizza Puff Beef Sausage', 'general', '', '400000000169', '3.990000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '0.900000000000000000000000000000', 0, 0, 0, '2022-03-06 13:59:05.000000', 'not tracked', 0, '', '0'),
('34B4F231-7CCA-4C51-B7DE-D045DF77A9DE', 'Chicago Polish', 'Chicago Polish', 'general', '', '400000000145', '5.150000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '1.600000000000000000000000000000', 0, 0, 0, '2022-03-06 13:11:38.000000', 'Panos', 0, '', '0'),
('39EE5BCC-1ED7-47B9-8B22-3D3434779EAD', 'Double Dog', 'Double Dog', 'general', '', '400000000190', '5.150000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '1.660000000000000000000000000000', 0, 0, 0, '2022-03-06 12:39:03.000000', 'not tracked', 0, '', '0'),
('3A70993F-8C84-4B3A-A166-40728ED280B7', 'Fresh Cut Fries - Regular', 'Fresh Cut Fries- One Size', 'general', '', '400000000176', '1.990000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '0.500000000000000000000000000000', 0, 0, 0, '2022-03-06 17:09:28.000000', 'Panos', 0, '', '0'),
('3FEAFE25-195B-4411-A43C-8C628C742D20', 'Maxwell Street Polish', 'Maxwell', 'general', '', '400000000152', '5.740000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '1.500000000000000000000000000000', 0, 0, 0, '2022-03-06 17:09:28.000000', 'Panos', 0, '', '0'),
('45B55EC6-7874-4D37-8A4A-76ED0357CD1F', 'Nachos with Cheese', 'Nachos with Cheese', 'general', '', '400000000466', '4.500000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '1.000000000000000000000000000000', 0, 0, 0, '2022-03-05 13:42:40.000000', 'not tracked', 0, '', '0'),
('4750F70F-6E01-46B5-A8CF-F1B0218AFAFD', 'Gatorade', 'Gatorade', 'general', '', '400000000305', '2.990000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '0.750000000000000000000000000000', 0, 0, 0, '2022-03-05 17:09:44.000000', 'not tracked', 0, '', '0'),
('65289FE9-340C-4AE0-B77E-6E0332662711', 'Italian Beef Sandwich', 'Italian Beef', 'general', '', '400000000121', '7.490000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '2.500000000000000000000000000000', 0, 0, 0, '2022-03-06 17:01:08.000000', 'Panos', 0, '', '0'),
('6FC2528B-9DAA-4DDA-8484-82C24C43C2A1', 'Fanta Orange Can', 'Fanta Orange 12 oz Can', 'general', '', '400000000329', '1.000000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '0.320000000000000000000000000000', 0, 0, 0, '2021-02-11 14:05:12.000000', 'not tracked', 0, '', '0'),
('78D9EF77-DBA5-4CB9-A30C-3E59FF490619', 'Chili Cheese Dog', 'Chili Cheese Dog', 'general', '', '400000000237', '4.550000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '1.500000000000000000000000000000', 0, 0, 0, '2022-03-06 17:16:19.000000', 'not tracked', 0, '', '0'),
('7CD3E335-C0F8-4FAB-B673-0D972AB9121A', 'Tom Tom Tamale', 'Tom Tom Tamale', 'general', '', '400000000336', '2.990000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '1.000000000000000000000000000000', 0, 0, 0, '2022-03-05 17:09:44.000000', 'not tracked', 0, '', '0'),
('8CF0B27B-6E84-4D7E-AA6A-3894801C6518', 'Root Beer Float', 'Root Beer Float', 'Root Beer Float', '', '400000000589', '3.930000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '1.000000000000000000000000000000', 0, 0, 0, '2020-09-17 13:16:15.000000', 'not tracked', 0, '', '0'),
('8D229A78-F885-48D6-B24F-0F6D188DEBEB', 'Chicago Pizza Puff - Pepperoni', 'Chicago Pizza Puff - Pepperoni', 'general', '', '400000000299', '3.990000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '0.900000000000000000000000000000', 0, 0, 0, '2022-03-06 17:01:08.000000', 'not tracked', 0, '', '0'),
('96A48FE7-18A4-4748-BBF4-A7CAFBBCB090', 'Minute Maid Lemonade', 'Minute Maid Lemonade', 'general', '', '400000000572', '1.000000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '0.360000000000000000000000000000', 0, 0, 0, '2022-03-04 00:00:00.000000', 'not tracked', 0, '', '0'),
('A9495557-FE3C-40E1-904E-B2BB2C6A4B79', 'Barq’s Root Beer  Can', 'Barqs Root Beer 12 oz Cans', 'general', '', '400000000312', '1.000000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '0.320000000000000000000000000000', 0, 0, 0, '2021-05-18 13:26:40.000000', 'not tracked', 0, '', '0'),
('B53E690A-F833-4F99-B57A-E6C871001F82', 'Sprite Can', 'Sprite 12 oz Can', 'general', '', '400000000091', '1.000000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '0.320000000000000000000000000000', 0, 0, 0, '2021-04-13 14:07:01.000000', 'Sams Club', 0, '', '0'),
('C2222AC2-7A84-455D-891A-81C3710B21D4', 'Chicken Strips - 5 Piece', 'Chicken Strips 5', 'general', '', '400000000282', '8.390000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '2.000000000000000000000000000000', 0, 0, 0, '2022-03-06 12:54:13.000000', 'not tracked', 0, '', '0'),
('CA82FDCA-E651-4D04-924C-2F38618C1B91', 'Cheese Dog', 'Cheese Dog', 'general', '', '400000000220', '4.310000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '1.250000000000000000000000000000', 0, 0, 0, '2022-03-06 17:16:19.000000', 'not tracked', 0, '', '0'),
('CBF01EBF-0611-4401-8BD4-B75D2ED0B508', 'Chili Dog', 'Chili Dog', 'general', '', '400000000213', '4.310000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '1.250000000000000000000000000000', 0, 0, 0, '2022-03-04 19:04:35.000000', 'not tracked', 0, '', '0'),
('D00DFA48-BD13-4371-BF5A-82C446F9E0E4', 'Dasani Water', 'Dasani Water', 'general', '', '400000000251', '1.790000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '0.400000000000000000000000000000', 0, 0, 0, '2022-03-05 13:25:54.000000', 'not tracked', 0, '', '0'),
('DD3D06DE-DD5F-4C0A-925B-87BE863DB494', 'Chicago Style Hot Dog', 'Hot Dog 8 to 1', 'general', '', '400000000046', '3.590000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '1.050000000000000000000000000000', 0, 0, 0, '2022-03-06 17:16:19.000000', 'not tracked', 0, '', '0'),
('DDD3DFB6-7A5A-47CD-88F7-C979387C0736', 'Corn Dog', 'Corn Dog', 'general', '', '400000000206', '4.190000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '1.000000000000000000000000000000', 0, 0, 0, '2022-03-06 13:05:11.000000', 'not tracked', 0, '', '0'),
('E1D42779-05A9-4251-B94E-378C11191A59', '2 DOG DEAL- 2 Dogs Fries & 12oz Can -', 'Hotdog Special Free', 'general', '', '400000000343', '9.240000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '2.670000000000000000000000000000', 0, 0, 0, '2022-03-06 14:33:22.000000', 'not tracked', 0, '', '0'),
('E56ED17D-F2DA-4304-9F0A-4E76F203041B', 'Monster', 'Monster', 'general', '', '400000000268', '3.990000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '1.420000000000000000000000000000', 0, 0, 0, '2022-02-25 18:26:40.000000', 'not tracked', 0, '', '0'),
('EDA86594-C546-4CB9-A6C8-ED568DDE78E7', '12 oz Can', 'Coke 12 oz Can', 'general', '', '400000000077', '1.250000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '0.320000000000000000000000000000', 0, 0, 0, '2022-03-06 17:09:28.000000', 'Sams Club', 0, '', '0'),
('F4B7B414-0B6E-45E1-B48D-D98AB3857F69', 'Green River Bottle', 'Green River Bottle', 'general', '', '400000000558', '3.990000000000000000000000000000', 0, 0, 0, '0.000000000000000000000000000000', '1.500000000000000000000000000000', 0, 0, 0, '2021-12-07 11:56:03.000000', 'not tracked', 0, '', '0');

-- --------------------------------------------------------

--
-- Table structure for table `modifiers`
--

CREATE TABLE `modifiers` (
  `ModifierId` bigint NOT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ItemId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `modifiers`
--

INSERT INTO `modifiers` (`ModifierId`, `Name`, `Description`, `ItemId`) VALUES
(18, 'Italian Beef Modifier', 'Modifiers for Italian Beef', '65289FE9-340C-4AE0-B77E-6E0332662711'),
(19, 'Chicago Style Hot Dog', '', 'DD3D06DE-DD5F-4C0A-925B-87BE863DB494'),
(20, '', '', '18E69BDE-10D4-4AA6-B198-F542B2F249EE'),
(23, '', '', '34B4F231-7CCA-4C51-B7DE-D045DF77A9DE'),
(24, '', '', '39EE5BCC-1ED7-47B9-8B22-3D3434779EAD'),
(25, 'Maxwell Street Polish', 'hello', '3FEAFE25-195B-4411-A43C-8C628C742D20'),
(27, '', '', '45B55EC6-7874-4D37-8A4A-76ED0357CD1F'),
(29, '', '', '78D9EF77-DBA5-4CB9-A30C-3E59FF490619'),
(30, 'Tom Tom Tamale', 'hello', '7CD3E335-C0F8-4FAB-B673-0D972AB9121A'),
(33, 'Chicken Strips - 5 Piece', '', 'C2222AC2-7A84-455D-891A-81C3710B21D4'),
(37, 'Fresh Cut Fries - Regular', 'hello', '3A70993F-8C84-4B3A-A166-40728ED280B7');

-- --------------------------------------------------------

--
-- Table structure for table `nooptions`
--

CREATE TABLE `nooptions` (
  `NoOptionId` bigint NOT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DiscountPrice` decimal(65,30) NOT NULL,
  `ModifierId` bigint NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `nooptions`
--

INSERT INTO `nooptions` (`NoOptionId`, `Name`, `DiscountPrice`, `ModifierId`) VALUES
(2, 'New Option', '0.000000000000000000000000000000', 18),
(3, 'New Option', '0.000000000000000000000000000000', 18),
(4, 'No Mustard', '0.000000000000000000000000000000', 19),
(5, 'No Relish', '0.000000000000000000000000000000', 19),
(6, 'No Onion', '0.000000000000000000000000000000', 19),
(7, 'No Pickle', '0.000000000000000000000000000000', 19),
(8, 'No Celery Salt', '0.000000000000000000000000000000', 19),
(9, 'No Sport Pepper', '0.000000000000000000000000000000', 19),
(16, 'No Mustard', '0.000000000000000000000000000000', 23),
(17, 'No Relish', '0.000000000000000000000000000000', 23),
(18, 'No Onion', '0.000000000000000000000000000000', 23),
(19, 'No Pickle', '0.000000000000000000000000000000', 23),
(20, 'No Celery Salt', '0.000000000000000000000000000000', 23),
(21, 'No Sport Pepper', '0.000000000000000000000000000000', 23),
(22, 'No Mustard', '0.000000000000000000000000000000', 24),
(23, 'No Relish', '0.000000000000000000000000000000', 24),
(24, 'No Onion', '0.000000000000000000000000000000', 24),
(25, 'No Pickle', '0.000000000000000000000000000000', 24),
(26, 'No Celery Salt', '0.000000000000000000000000000000', 24),
(27, 'No Sport Pepper', '0.000000000000000000000000000000', 24),
(28, 'No Mustard', '0.000000000000000000000000000000', 25),
(29, 'No Grilled Onion', '0.000000000000000000000000000000', 25),
(30, 'No Sport Pepper', '0.000000000000000000000000000000', 25),
(33, 'No Onions', '0.000000000000000000000000000000', 29),
(40, 'No Mustard', '0.000000000000000000000000000000', 33),
(41, 'No Relish', '0.000000000000000000000000000000', 33),
(42, 'No Onion', '0.000000000000000000000000000000', 33),
(43, 'No Pickle', '0.000000000000000000000000000000', 33),
(44, 'No Celery Salt', '0.000000000000000000000000000000', 33),
(45, 'No Sport Pepper', '0.000000000000000000000000000000', 33);

-- --------------------------------------------------------

--
-- Table structure for table `orderitemaddons`
--

CREATE TABLE `orderitemaddons` (
  `OrderItemId` bigint NOT NULL,
  `AddonId` bigint NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `orderitemaddons`
--

INSERT INTO `orderitemaddons` (`OrderItemId`, `AddonId`) VALUES
(14, 13),
(11, 14),
(14, 15);

-- --------------------------------------------------------

--
-- Table structure for table `orderitemgroups`
--

CREATE TABLE `orderitemgroups` (
  `OrderItemId` bigint NOT NULL,
  `GroupId` bigint NOT NULL,
  `GroupOptionId` bigint NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `orderitemnooptions`
--

CREATE TABLE `orderitemnooptions` (
  `OrderItemId` bigint NOT NULL,
  `NoOptionId` bigint NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- --------------------------------------------------------

--
-- Table structure for table `orderitems`
--

CREATE TABLE `orderitems` (
  `OrderItemId` bigint NOT NULL,
  `OrderId` bigint NOT NULL,
  `ItemId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `orderitems`
--

INSERT INTO `orderitems` (`OrderItemId`, `OrderId`, `ItemId`) VALUES
(1, 1, '6FC2528B-9DAA-4DDA-8484-82C24C43C2A1'),
(2, 1, '65289FE9-340C-4AE0-B77E-6E0332662711'),
(3, 2, '6FC2528B-9DAA-4DDA-8484-82C24C43C2A1'),
(4, 2, '65289FE9-340C-4AE0-B77E-6E0332662711'),
(5, 3, '6FC2528B-9DAA-4DDA-8484-82C24C43C2A1'),
(6, 3, '65289FE9-340C-4AE0-B77E-6E0332662711'),
(7, 4, '65289FE9-340C-4AE0-B77E-6E0332662711'),
(8, 4, '7CD3E335-C0F8-4FAB-B673-0D972AB9121A'),
(9, 4, 'CA82FDCA-E651-4D04-924C-2F38618C1B91'),
(10, 5, '6FC2528B-9DAA-4DDA-8484-82C24C43C2A1'),
(11, 6, '65289FE9-340C-4AE0-B77E-6E0332662711'),
(12, 6, '7CD3E335-C0F8-4FAB-B673-0D972AB9121A'),
(13, 7, '6FC2528B-9DAA-4DDA-8484-82C24C43C2A1'),
(14, 7, '65289FE9-340C-4AE0-B77E-6E0332662711');

-- --------------------------------------------------------

--
-- Table structure for table `orders`
--

CREATE TABLE `orders` (
  `OrderId` bigint NOT NULL,
  `CustomerAccountId` bigint NOT NULL,
  `Subtotal` decimal(65,30) NOT NULL,
  `Tax` decimal(65,30) NOT NULL,
  `Net` decimal(65,30) NOT NULL,
  `Status` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `OrderDate` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `orders`
--

INSERT INTO `orders` (`OrderId`, `CustomerAccountId`, `Subtotal`, `Tax`, `Net`, `Status`, `OrderDate`) VALUES
(1, 1, '0.000000000000000000000000000000', '0.000000000000000000000000000000', '0.000000000000000000000000000000', 'Pending', '2023-03-05 17:14:24.027253'),
(2, 1, '0.000000000000000000000000000000', '0.000000000000000000000000000000', '0.000000000000000000000000000000', 'Pending', '2023-03-05 17:16:10.273504'),
(3, 1, '8.490000000000000000000000000000', '0.700425000000000100000000000000', '9.190425000000001000000000000000', 'Pending', '2023-03-05 17:19:02.944711'),
(4, 1, '14.790000000000000000000000000000', '1.220000000000000000000000000000', '16.010000000000000000000000000000', 'Pending', '2023-03-05 17:22:51.965856'),
(5, 1, '1.000000000000000000000000000000', '0.080000000000000000000000000000', '1.080000000000000000000000000000', 'Pending', '2023-03-05 17:32:24.680320'),
(6, 1, '10.480000000000000000000000000000', '0.860000000000000000000000000000', '11.340000000000000000000000000000', 'Pending', '2023-03-07 14:29:40.587780'),
(7, 1, '8.490000000000000000000000000000', '0.700000000000000000000000000000', '9.190000000000000000000000000000', 'Pending', '2023-03-07 14:30:48.210880');

-- --------------------------------------------------------

--
-- Table structure for table `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20230303210639_initial-create', '7.0.3'),
('20230308200220_new-modifier-item-relation', '7.0.3');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `accounts`
--
ALTER TABLE `accounts`
  ADD PRIMARY KEY (`AccountId`),
  ADD KEY `IX_Accounts_EmployeeId` (`EmployeeId`);

--
-- Indexes for table `addons`
--
ALTER TABLE `addons`
  ADD PRIMARY KEY (`AddonId`),
  ADD KEY `IX_Addons_ModifierId` (`ModifierId`);

--
-- Indexes for table `customeraccounts`
--
ALTER TABLE `customeraccounts`
  ADD PRIMARY KEY (`CustomerAccountId`);

--
-- Indexes for table `employees`
--
ALTER TABLE `employees`
  ADD PRIMARY KEY (`EmployeeId`);

--
-- Indexes for table `groupoptions`
--
ALTER TABLE `groupoptions`
  ADD PRIMARY KEY (`GroupOptionId`),
  ADD KEY `IX_GroupOptions_GroupId` (`GroupId`);

--
-- Indexes for table `groups`
--
ALTER TABLE `groups`
  ADD PRIMARY KEY (`GroupId`),
  ADD KEY `IX_Groups_ModifierId` (`ModifierId`);

--
-- Indexes for table `items`
--
ALTER TABLE `items`
  ADD PRIMARY KEY (`ItemId`);

--
-- Indexes for table `modifiers`
--
ALTER TABLE `modifiers`
  ADD PRIMARY KEY (`ModifierId`),
  ADD KEY `IX_Modifiers_ItemId` (`ItemId`);

--
-- Indexes for table `nooptions`
--
ALTER TABLE `nooptions`
  ADD PRIMARY KEY (`NoOptionId`),
  ADD KEY `IX_NoOptions_ModifierId` (`ModifierId`);

--
-- Indexes for table `orderitemaddons`
--
ALTER TABLE `orderitemaddons`
  ADD PRIMARY KEY (`OrderItemId`,`AddonId`),
  ADD KEY `IX_OrderItemAddons_AddonId` (`AddonId`);

--
-- Indexes for table `orderitemgroups`
--
ALTER TABLE `orderitemgroups`
  ADD PRIMARY KEY (`OrderItemId`,`GroupId`,`GroupOptionId`),
  ADD KEY `IX_OrderItemGroups_GroupId` (`GroupId`),
  ADD KEY `IX_OrderItemGroups_GroupOptionId` (`GroupOptionId`);

--
-- Indexes for table `orderitemnooptions`
--
ALTER TABLE `orderitemnooptions`
  ADD PRIMARY KEY (`OrderItemId`,`NoOptionId`),
  ADD KEY `IX_OrderItemNoOptions_NoOptionId` (`NoOptionId`);

--
-- Indexes for table `orderitems`
--
ALTER TABLE `orderitems`
  ADD PRIMARY KEY (`OrderItemId`),
  ADD KEY `IX_OrderItems_ItemId` (`ItemId`),
  ADD KEY `IX_OrderItems_OrderId` (`OrderId`);

--
-- Indexes for table `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`OrderId`),
  ADD KEY `IX_Orders_CustomerAccountId` (`CustomerAccountId`);

--
-- Indexes for table `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `accounts`
--
ALTER TABLE `accounts`
  MODIFY `AccountId` bigint NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `addons`
--
ALTER TABLE `addons`
  MODIFY `AddonId` bigint NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=58;

--
-- AUTO_INCREMENT for table `customeraccounts`
--
ALTER TABLE `customeraccounts`
  MODIFY `CustomerAccountId` bigint NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `employees`
--
ALTER TABLE `employees`
  MODIFY `EmployeeId` bigint NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `groupoptions`
--
ALTER TABLE `groupoptions`
  MODIFY `GroupOptionId` bigint NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=46;

--
-- AUTO_INCREMENT for table `groups`
--
ALTER TABLE `groups`
  MODIFY `GroupId` bigint NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=41;

--
-- AUTO_INCREMENT for table `modifiers`
--
ALTER TABLE `modifiers`
  MODIFY `ModifierId` bigint NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=39;

--
-- AUTO_INCREMENT for table `nooptions`
--
ALTER TABLE `nooptions`
  MODIFY `NoOptionId` bigint NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=47;

--
-- AUTO_INCREMENT for table `orderitems`
--
ALTER TABLE `orderitems`
  MODIFY `OrderItemId` bigint NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT for table `orders`
--
ALTER TABLE `orders`
  MODIFY `OrderId` bigint NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `accounts`
--
ALTER TABLE `accounts`
  ADD CONSTRAINT `FK_Accounts_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `employees` (`EmployeeId`) ON DELETE CASCADE;

--
-- Constraints for table `addons`
--
ALTER TABLE `addons`
  ADD CONSTRAINT `FK_Addons_Modifiers_ModifierId` FOREIGN KEY (`ModifierId`) REFERENCES `modifiers` (`ModifierId`) ON DELETE CASCADE;

--
-- Constraints for table `groupoptions`
--
ALTER TABLE `groupoptions`
  ADD CONSTRAINT `FK_GroupOptions_Groups_GroupId` FOREIGN KEY (`GroupId`) REFERENCES `groups` (`GroupId`) ON DELETE CASCADE;

--
-- Constraints for table `groups`
--
ALTER TABLE `groups`
  ADD CONSTRAINT `FK_Groups_Modifiers_ModifierId` FOREIGN KEY (`ModifierId`) REFERENCES `modifiers` (`ModifierId`) ON DELETE CASCADE;

--
-- Constraints for table `modifiers`
--
ALTER TABLE `modifiers`
  ADD CONSTRAINT `FK_Modifiers_Items_ItemId` FOREIGN KEY (`ItemId`) REFERENCES `items` (`ItemId`) ON DELETE CASCADE;

--
-- Constraints for table `nooptions`
--
ALTER TABLE `nooptions`
  ADD CONSTRAINT `FK_NoOptions_Modifiers_ModifierId` FOREIGN KEY (`ModifierId`) REFERENCES `modifiers` (`ModifierId`) ON DELETE CASCADE;

--
-- Constraints for table `orderitemaddons`
--
ALTER TABLE `orderitemaddons`
  ADD CONSTRAINT `FK_OrderItemAddons_Addons_AddonId` FOREIGN KEY (`AddonId`) REFERENCES `addons` (`AddonId`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_OrderItemAddons_OrderItems_OrderItemId` FOREIGN KEY (`OrderItemId`) REFERENCES `orderitems` (`OrderItemId`) ON DELETE CASCADE;

--
-- Constraints for table `orderitemgroups`
--
ALTER TABLE `orderitemgroups`
  ADD CONSTRAINT `FK_OrderItemGroups_GroupOptions_GroupOptionId` FOREIGN KEY (`GroupOptionId`) REFERENCES `groupoptions` (`GroupOptionId`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_OrderItemGroups_Groups_GroupId` FOREIGN KEY (`GroupId`) REFERENCES `groups` (`GroupId`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_OrderItemGroups_OrderItems_OrderItemId` FOREIGN KEY (`OrderItemId`) REFERENCES `orderitems` (`OrderItemId`) ON DELETE CASCADE;

--
-- Constraints for table `orderitemnooptions`
--
ALTER TABLE `orderitemnooptions`
  ADD CONSTRAINT `FK_OrderItemNoOptions_NoOptions_NoOptionId` FOREIGN KEY (`NoOptionId`) REFERENCES `nooptions` (`NoOptionId`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_OrderItemNoOptions_OrderItems_OrderItemId` FOREIGN KEY (`OrderItemId`) REFERENCES `orderitems` (`OrderItemId`) ON DELETE CASCADE;

--
-- Constraints for table `orderitems`
--
ALTER TABLE `orderitems`
  ADD CONSTRAINT `FK_OrderItems_Items_ItemId` FOREIGN KEY (`ItemId`) REFERENCES `items` (`ItemId`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_OrderItems_Orders_OrderId` FOREIGN KEY (`OrderId`) REFERENCES `orders` (`OrderId`) ON DELETE CASCADE;

--
-- Constraints for table `orders`
--
ALTER TABLE `orders`
  ADD CONSTRAINT `FK_Orders_CustomerAccounts_CustomerAccountId` FOREIGN KEY (`CustomerAccountId`) REFERENCES `customeraccounts` (`CustomerAccountId`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
