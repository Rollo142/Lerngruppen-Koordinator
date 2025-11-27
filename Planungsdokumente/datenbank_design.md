# Datenbank-Design
# Warum haben Sie diese Tabellen und Spalten gewählt?
In der Datenbank gibt es 3 Tabellen:

1. BenutzerTabelle:
Wie in anforderungen_final.txt beschrieben brauchen wir Benutzer die auf die Termine zugreifen, einschreiben oder erstellen können.

2. LergruppenTabelle:
Damit die Benutzer sich einschreiben können brauchen wir natürlich auch eine Tabelle inder die Benutzer aufgelistet werden und sich mit ihren Interessen einschreiben können.

3. TermineTabelle:
Die TermineTabelle ist eine Tabelle die aus Lerngruppe und Benutzer besteht. Sie verwendet attribute beider Tabellen und hat dazu noch ein neues Attribut: Datum und Uhrzeit. Die Tabelle brauchen wir damit Benutzer wissen wann sie wo Lernen

# Warum ist die Beziehung zwischen den Tabellen vom Typ 1:n oder n:m?
Die Beziehungun zwischen den 3 Tabellen lässt sich folgend erklären:
## Benutzer /  Termine Beziehung:
### "Mehrere Benutzer haben Mehrere Termine" , dies stellt sicher dass alle Benutzer in der Database sich einem Termin (Treffpunkt zum lernen) anschließen können.
### Die Termine müssen (damit eine Lerngruppe stattfinden kann) mehr als einen Benutzer haben.
## Termine / Lerngruppe:
### "Termine haben eine Lerngruppe aber Lerngruppen haben mehrere Termine" , Termine sind spezialisiert auf genau ein Thema deshalb haben sie auch nur eine Lerngruppe, jedoch können Benutzer öfter in Lerngruppen zusammen lernen und mehrere Termine realisieren.

# Welche Datentypen haben Sie gewählt und warum?
## Benutzer:
### ID = INT ( Die ID zählt durch die Tabelle und wird damit als Zahl dargestellt)
### NAME = VARCHAR (Namen sind aus Buchstaben)
### ADRESSE = VARCHAR (Adressen sind Buchstaben)
### PLZ = NVARCHAR ( Kombination von Zahlen und Buchstaben)
### STUDIENGANG = VARCHAR (Studiengang wird in Buchstaben dargestellt)
### FACHSEMESTER = INT (Fachsemester werden in Zahlen angegeben)

## Termine
### ID = INT (Die ID zählt durch die Tabelle und wird damit als Zahl dargestellt)
### ADRESSE = VARCHAR (Adressen sind aus Buchstaben)
### FACH = VARCHAR (wird in Buchstaben dargestellt)
### Unterrichtsmaterial = VARBINARY (Material der Benutzer zum lernen)
### RAUM = Dezimal (Wird als Zahl angegeben zb "Raum 615.2")
### DATUM UND UHRZEIT = DATETIME (Bester Datentyp für Zeitangabe)

## Lerngruppe
### ID = INT ( Die ID zählt durch die Tabelle und wird damit als Zahl dargestellt)
### FACH = VARCHAR (wird in Buchstaben dargestellt)
### RAUM = Dezimal (Wird als Zahl angegeben zb "Raum 615.2")
### ADRESSE = VARCHAR (Adressen sind aus Buchstaben)
### PLZ = INT ( PLZ sind Zahlen)
### Unterrichtsmaterial = VARBINARY (Material der Benutzer zum lernen)
### DATUM UND UHRZEIT = DATETIME (Bester Datentyp für Zeitangabe)
