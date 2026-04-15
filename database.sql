-- ============================================================
-- Script de création de la base de données BibliothequeApp
-- Exécuter dans MySQL Workbench, phpMyAdmin, ou en CLI :
--   mysql -u root -p < database.sql
-- ============================================================

CREATE DATABASE IF NOT EXISTS LibraryDB
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE LibraryDB;

CREATE TABLE IF NOT EXISTS Books (
    Id          INT             NOT NULL AUTO_INCREMENT,
    Titre       VARCHAR(255)    NOT NULL,
    Auteur      VARCHAR(255)    NOT NULL,
    ISBN        VARCHAR(20)     NOT NULL UNIQUE,
    AnneePubli  INT             NOT NULL,
    Genre       VARCHAR(100)    NOT NULL,
    Rayon       VARCHAR(100)    NOT NULL,
    Etagere     VARCHAR(50)     NOT NULL,
    Disponible  TINYINT(1)      NOT NULL DEFAULT 1,
    Couverture  VARCHAR(255)    NULL,
    Emprunteur  VARCHAR(255)    NULL,
    DateEmprunt DATE            NULL,
    PRIMARY KEY (Id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Données de démonstration
INSERT INTO Books (Titre, Auteur, ISBN, AnneePubli, Genre, Rayon, Etagere, Disponible, Couverture, Emprunteur, DateEmprunt) VALUES
('Le Seigneur des Anneaux', 'J.R.R. Tolkien',  '978-2266155731', 1954, 'Fantasy',    'Littérature Étrangère', 'A3', 1, NULL, NULL, NULL),
('1984',                    'George Orwell',    '978-2070368228', 1949, 'Dystopie',   'Littérature Étrangère', 'B1', 1, NULL, NULL, NULL),
('L''Étranger',             'Albert Camus',     '978-2070360024', 1942, 'Roman',      'Littérature Française', 'C2', 0, NULL, 'Jean Dupont', '2023-10-15'),
('Dune',                    'Frank Herbert',    '978-2266320740', 1965, 'Sci-Fi',     'Littérature Étrangère', 'A5', 1, NULL, NULL, NULL),
('Les Misérables',          'Victor Hugo',      '978-2253004226', 1862, 'Roman',      'Littérature Française', 'C1', 1, NULL, NULL, NULL);
