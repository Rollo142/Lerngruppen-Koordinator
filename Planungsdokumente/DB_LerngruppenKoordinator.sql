USE master;

IF EXISTS (SELECT * FROM sys.databases WHERE name = 'LerngruppenKoordinatorDB')
BEGIN
    DROP DATABASE LerngruppenKoordinatorDB;
END

CREATE DATABASE LerngruppenKoordinatorDB;
USE LerngruppenKoordinatorDB;

IF OBJECT_ID('TERMINE', 'U') IS NOT NULL DROP TABLE TERMINE;
IF OBJECT_ID('LERNGRUPPE', 'U') IS NOT NULL DROP TABLE LERNGRUPPE;
IF OBJECT_ID('BENUTZER', 'U') IS NOT NULL DROP TABLE BENUTZER;


CREATE TABLE BENUTZER (
    ID INT PRIMARY KEY IDENTITY(1,1),
    NAME VARCHAR(100) NOT NULL,
    ADRESSE VARCHAR(255),
    PLZ VARCHAR(20),
    STUDIENGANG VARCHAR(100),
    FACHSEMESTER INT
);


CREATE TABLE LERNGRUPPE (
    ID INT PRIMARY KEY IDENTITY(1,1),
    NAME VARCHAR(100) NOT NULL,
    FACH VARCHAR(255),
    RAUM DECIMAL(10,2),
    ADRESSE VARCHAR(250),
    PLZ VARCHAR(20),
    UNTERRICHTSMATERIAL VARBINARY(MAX)
);

CREATE TABLE TERMINE (
    ID INT PRIMARY KEY IDENTITY(1,1),
    BenutzerID INT NOT NULL,
    LerngruppenID INT NOT NULL,
    ADRESSE VARCHAR(255),
    FACH VARCHAR(100),
    Unterrichtsmaterial VARBINARY(MAX),
    RAUM DECIMAL(10,2),
    DATUM_UHRZEIT DATETIME,
    FOREIGN KEY (BenutzerID) REFERENCES BENUTZER(ID),
    FOREIGN KEY (LerngruppenID) REFERENCES LERNGRUPPE(ID)
);

INSERT INTO BENUTZER (NAME, ADRESSE, PLZ, STUDIENGANG, FACHSEMESTER)
VALUES
('Anna Schmidt', 'Hauptstraﬂe 12', '66111', 'Informatik', 3),
('Jonas Becker', 'Bergstraﬂe 5', '66115', 'Maschinenbau', 2),
('Laura Klein', 'Marktplatz 8', '66119', 'Psychologie', 4),
('Max M¸ller', 'Schulstraﬂe 7', '66113', 'BWL', 1),
('Sophie Meier', 'Gartenweg 3', '66121', 'Medizin', 2);

INSERT INTO LERNGRUPPE (NAME, FACH, ADRESSE, PLZ, RAUM, UNTERRICHTSMATERIAL)
VALUES
('Algo-Team', 'Algorithmen und Datenstrukturen', 'Uni Campus Geb‰ude E1', '66123', 101.1, NULL),
('Maschbau-Gruppe', 'Technische Mechanik', 'HTW Saar Geb‰ude B', '66117', 202.2, NULL),
('PsychoLab', 'Kognitive Psychologie', 'Uni Campus Geb‰ude C3', '66125', 303.3, NULL),
('BWL-Workshop', 'Betriebswirtschaftliche ‹bungen', 'Uni Campus Geb‰ude F2', '66122', 404.4, NULL),
('Medizin-Gruppe', 'Anatomie und Physiologie', 'Uni Campus Geb‰ude G1', '66124', 505.5, NULL);


INSERT INTO TERMINE (BenutzerID, LerngruppenID, ADRESSE, FACH, Unterrichtsmaterial, RAUM, DATUM_UHRZEIT)
VALUES
(1, 1, 'Adresse Beispiel 1', 'Mathematik', NULL, 615.2, '2025-11-02 10:00'),
(2, 2, 'Adresse Beispiel 2', 'Deutsch',     NULL, 402.0, '2025-11-03 14:30'),
(3, 3, 'Adresse Beispiel 3', 'Englisch',    NULL, 310.5, '2025-11-04 09:00'),
(4, 4, 'Adresse Beispiel 4', 'Physik',      NULL, 120.0, '2025-11-05 11:00'),
(5, 5, 'Adresse Beispiel 5', 'Chemie',      NULL, 215.3, '2025-11-06 13:30');


SELECT
    b.ID AS BenutzerID,
    b.NAME AS Benutzer,
    b.STUDIENGANG,
    l.ID AS LerngruppenID,
    l.NAME AS Lerngruppe,
    l.FACH,
    t.DATUM_UHRZEIT AS Termin
FROM TERMINE t
JOIN BENUTZER b ON t.BenutzerID = b.ID
JOIN LERNGRUPPE l ON t.LerngruppenID = l.ID
ORDER BY t.DATUM_UHRZEIT;
