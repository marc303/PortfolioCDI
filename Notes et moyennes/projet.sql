-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               8.0.31 - MySQL Community Server - GPL
-- Server OS:                    Win64
-- HeidiSQL Version:             12.5.0.6677
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dumping database structure for mmarchand
CREATE DATABASE IF NOT EXISTS `mmarchand` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `mmarchand`;

-- Dumping structure for table mmarchand.cours
CREATE TABLE IF NOT EXISTS `cours` (
  `id` char(3) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL DEFAULT '',
  `description` varchar(128) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `duree` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table mmarchand.cours: ~20 rows (approximately)
DELETE FROM `cours`;
INSERT INTO `cours` (`id`, `description`, `duree`) VALUES
	('ARP', 'APPROCHE STRUCTURÉE À LA RÉSOLUTION DE PROBLÈMES', 60),
	('AWB', 'ANIMATION WEB', 60),
	('BD1', 'BASE DE DONNÉES 1', 60),
	('BD2', 'BASE DE DONNÉES 2', 60),
	('DCS', 'DÉVELOPPEMENT WEB CÔTÉ SERVEUR', 60),
	('DGP', 'DÉVELOPPEMENT ET GESTION DE PROJETS', 60),
	('DM1', 'DÉVELOPPEMENT APPLICATIONS MOBILES 1', 60),
	('DM2', 'DÉVELOPPEMENT APPLICATIONS MOBILES 2', 60),
	('DW1', 'DÉVELOPPEMENT WEB 1', 60),
	('DW2', 'DÉVELOPPEMENT WEB 2', 60),
	('INF', 'INFONUAGIQUE', 60),
	('NTE', 'NOUVELLES TECHNOLOGIES', 60),
	('PFE', 'PROJET DE FIN D’ÉTUDES – INTÉGRATION', 270),
	('PI1', 'PROJET D’INTÉGRATION 1 – PROGRAMMATION ORIENTÉ OBJET', 60),
	('PI2', 'PROJET D’INTÉGRATION 2 – PROGRAMMATION WEB', 60),
	('PO1', 'PROGRAMMATION ORIENTÉE OBJET 1', 60),
	('PO2', 'PROGRAMMATION ORIENTÉE OBJET 2', 60),
	('PPA', 'PROFESSION DE PROGRAMMEUR-ANALYSTE', 60),
	('PWB', 'PROGRAMMATION WEB', 60),
	('TDD', 'TRAITEMENT DE DONNÉES', 60);

-- Dumping structure for table mmarchand.evaluations
CREATE TABLE IF NOT EXISTS `evaluations` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `codeCours` char(3) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `ponderation` int DEFAULT NULL,
  `Evaluation` varchar(128) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `codeCours` (`codeCours`),
  CONSTRAINT `FK_evaluations_cours` FOREIGN KEY (`codeCours`) REFERENCES `cours` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=61 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table mmarchand.evaluations: ~54 rows (approximately)
DELETE FROM `evaluations`;
INSERT INTO `evaluations` (`id`, `codeCours`, `ponderation`, `Evaluation`) VALUES
	(1, 'PPA', 30, 'Examen intra'),
	(2, 'PPA', 30, 'Examen final'),
	(3, 'PPA', 40, 'Projet 1'),
	(4, 'DW1', 30, 'Examen intra'),
	(5, 'DW1', 30, 'Examen final'),
	(6, 'DW1', 40, 'Projet 1'),
	(7, 'ARP', 15, 'Examen intra'),
	(8, 'ARP', 15, 'Examen final'),
	(9, 'ARP', 30, 'TP et Professionnalisme'),
	(10, 'ARP', 15, 'Projet 1'),
	(11, 'ARP', 15, 'Projet 2'),
	(12, 'ARP', 10, 'Projet 3'),
	(13, 'DW2', 30, 'Examen intra'),
	(14, 'DW2', 30, 'Examen final'),
	(15, 'DW2', 40, 'Projet 1'),
	(16, 'AWB', 30, 'Examen intra'),
	(17, 'AWB', 30, 'Examen final'),
	(18, 'AWB', 40, 'Projet 1'),
	(19, 'PO1', 30, 'Examen intra'),
	(20, 'PO1', 30, 'Examen final'),
	(21, 'PO1', 40, 'Projet 1'),
	(22, 'PO2', 30, 'Examen intra'),
	(23, 'PO2', 30, 'Examen final'),
	(24, 'PO2', 40, 'Projet 1'),
	(25, 'BD1', 30, 'Examen intra'),
	(26, 'BD1', 30, 'Examen final'),
	(27, 'BD1', 40, 'Projet 1'),
	(28, 'BD2', 30, 'Examen intra'),
	(29, 'BD2', 30, 'Examen final'),
	(30, 'BD2', 40, 'Projet 1'),
	(31, 'TDD', 30, 'Examen intra'),
	(32, 'TDD', 30, 'Examen final'),
	(33, 'TDD', 40, 'Projet 1'),
	(34, 'DCS', 30, 'Examen intra'),
	(35, 'DCS', 30, 'Examen final'),
	(36, 'DCS', 40, 'Projet 1'),
	(37, 'PWB', 30, 'Examen intra'),
	(38, 'PWB', 30, 'Examen final'),
	(39, 'PWB', 40, 'Projet 1'),
	(40, 'DM1', 30, 'Examen intra'),
	(41, 'DM1', 30, 'Examen final'),
	(42, 'DM1', 40, 'Projet 1'),
	(43, 'DM2', 30, 'Examen intra'),
	(44, 'DM2', 30, 'Examen final'),
	(45, 'DM2', 40, 'Projet 1'),
	(46, 'INF', 30, 'Examen intra'),
	(47, 'INF', 30, 'Examen final'),
	(48, 'INF', 40, 'Projet 1'),
	(49, 'NTE', 30, 'Examen intra'),
	(50, 'NTE', 30, 'Examen final'),
	(51, 'NTE', 40, 'Projet 1'),
	(52, 'PI1', 100, 'Projet 1'),
	(53, 'PI2', 100, 'Projet 1'),
	(54, 'PFE', 100, 'Projet 1');

-- Dumping structure for table mmarchand.resultats
CREATE TABLE IF NOT EXISTS `resultats` (
  `id` int unsigned NOT NULL,
  `note` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `id` (`id`),
  CONSTRAINT `FK_resultats_evaluations` FOREIGN KEY (`id`) REFERENCES `evaluations` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table mmarchand.resultats: ~17 rows (approximately)
DELETE FROM `resultats`;
INSERT INTO `resultats` (`id`, `note`) VALUES
	(2, 85),
	(3, 16),
	(4, 53),
	(5, 14),
	(6, 1),
	(7, 30),
	(8, 81),
	(9, 2),
	(10, 33),
	(11, 55),
	(12, 69),
	(13, 75),
	(14, 60),
	(15, 29),
	(16, 32),
	(17, 32),
	(18, 52),
	(19, 58);

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
