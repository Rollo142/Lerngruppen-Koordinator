use master;
IF exists (select * from sys.databases where name = 'LerngruppenKoordinatorDB')
BEGIN 
	DROP DATABASE LerngruppenKoordinatorDB;
END

CREATE DATABASE LerngruppenKoordinatorDB;
USE LerngruppenKoordinatorDB;

IF OBJECT_ID('Terminkalender', 'U') IS NOT NULL DROP TABLE Terminkalender;
IF OBJECT_ID('LERNGRUPPE', 'U') IS NOT NULL DROP TABLE LERNGRUPPE;
IF OBJECT_ID('BENUTZER', 'U') IS NOT NULL DROP TABLE BENUTZER;

CREATE TABLE BENUTZER (
	ID INT PRIMARY KEY IDENTITY(1,1),
	NAME VARCHAR(100) NOT NULL,
	ADRESSE VARCHAR(255),
	PLZ VARCHAR(255),
	STUDIENGANG VARCHAR(100),
	FACHSEMESTER INT
);


CREATE TABLE LERNGRUPPE ( 
	ID INT PRIMARY KEY IDENTITY(1,1),
	NAME VARCHAR(100) NOT NULL,
	FACH VARCHAR(255),
	ADRESSE VARCHAR(250),
	PLZ VARCHAR(10)
);

CREATE TABLE Terminkalender (
	BenutzerID INT,
	LERNGRUPPENID INT,
	TERMIN DATETIME DEFAULT GETDATE(),
	PRIMARY KEY (BENUTZERID, LERNGRUPPENID),
	FOREIGN KEY (BENUTZERID) REFERENCES BENUTZER(ID),
	FOREIGN KEY (LERNGRUPPENID) REFERENCES LERNGRUPPE(ID)
	);

INSERT INTO BENUTZER (NAME, ADRESSE, PLZ, STUDIENGANG, FACHSEMESTER)
VALUES 
('Anna Schmidt', 'Hauptstraﬂe 12', '66111', 'Informatik', 3),
('Jonas Becker', 'Bergstraﬂe 5', '66115', 'Maschinenbau', 2),
('Laura Klein', 'Marktplatz 8', '66119', 'Psychologie', 4),
('Max M¸ller', 'Schulstraﬂe 7', '66113', 'BWL', 1),
('Sophie Meier', 'Gartenweg 3', '66121', 'Medizin', 2);

INSERT INTO LERNGRUPPE (NAME, FACH, ADRESSE, PLZ)
VALUES 
('Algo-Team', 'Algorithmen und Datenstrukturen', 'Uni Campus Geb‰ude E1', '66123'),
('Maschbau-Gruppe', 'Technische Mechanik', 'HTW Saar Geb‰ude B', '66117'),
('PsychoLab', 'Kognitive Psychologie', 'Uni Campus Geb‰ude C3', '66125'),
('BWL-Workshop', 'Betriebswirtschaftliche ‹bungen', 'Uni Campus Geb‰ude F2', '66122'),
('Medizin-Gruppe', 'Anatomie und Physiologie', 'Uni Campus Geb‰ude G1', '66124');

INSERT INTO Terminkalender (BenutzerID, LERNGRUPPENID, TERMIN)
VALUES 
(1, 1, '2025-11-02 10:00'),
(2, 2, '2025-11-03 14:30'),
(3, 3, '2025-11-04 09:00'),
(4, 4, '2025-11-05 11:00'),
(5, 5, '2025-11-06 13:30');



SELECT 
    b.ID AS BenutzerID,
    b.NAME AS Benutzer,
    b.STUDIENGANG,
    l.ID AS LerngruppenID,
    l.NAME AS Lerngruppe,
    l.FACH,
    t.TERMIN
FROM Terminkalender t
JOIN BENUTZER b ON t.BenutzerID = b.ID
JOIN LERNGRUPPE l ON t.LERNGRUPPENID = l.ID
ORDER BY t.TERMIN;