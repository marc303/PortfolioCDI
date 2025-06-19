USE MultiLocations7336
GO

INSERT INTO Couleurs (Nom) VALUES ('Bleu foncé')
INSERT INTO Couleurs (Nom) VALUES ('Rouge vin')
INSERT INTO Couleurs (Nom) VALUES ('Jaune citron')
INSERT INTO Couleurs (Nom) VALUES ('Vert lime')
INSERT INTO Couleurs (Nom) VALUES ('Gris argenté')

INSERT INTO Types (Nom) VALUES ('Coupé 2 portes')
INSERT INTO Types (Nom) VALUES ('Sedan 4 portes')
INSERT INTO Types (Nom) VALUES ('Camion')
INSERT INTO Types (Nom) VALUES ('VUS')
INSERT INTO Types (Nom) VALUES ('Van')

INSERT INTO Modeles	(Nom) VALUES ('SC-430')
INSERT INTO Modeles	(Nom) VALUES ('Pirate')
INSERT INTO Modeles	(Nom) VALUES ('Rainier')
INSERT INTO Modeles	(Nom) VALUES ('ROCK')
INSERT INTO Modeles	(Nom) VALUES ('Speedy')

INSERT INTO Clients (Prenom, Nom, Adresse, Ville, Province, CodePostal, Telephone)
VALUES('Claudine', 'Latreille', '1000 chemins des Pruches', 'St-Clinclin', 'QC', 'X1X 1X1', '(450) 123-4567')

INSERT INTO Clients (Prenom, Nom, Adresse, Ville, Province, CodePostal, Telephone) 
VALUES ('Armand', 'Guindon', '728 rue Vorien', 'St Benit', 'QC', 'G1X 1H1', '(450) 476-8276')

INSERT INTO Clients (Prenom, Nom, Adresse, Ville, Province, CodePostal, Telephone) 
VALUES ('Pierre', 'Monfils', '248 Garneau', 'Tenaga', 'QC', 'J6C 8B2', '(819) 337-8219')

INSERT INTO Clients (Prenom, Nom, Adresse, Ville, Province, CodePostal, Telephone) 
VALUES ('Alfred', 'Léon', '1269 chemin des Anges', 'St-Clinclin', 'QC', 'X1X 1X1', '(450) 887-1690')


INSERT INTO Vehicules (VehiculeNIV, ModeleID, TypeID, CouleurID, Annee, ValeurCourante, Kilometrage, Automatique, Climatise, AntiDemarreur, Nouveau, EnLocation)
VALUES('3W9T1-2Q10D-12D0P-2E1R2', 1, 1, 1, 2003, 90000, 0, 1, 1, 1, 1, 1)

INSERT INTO Vehicules (VehiculeNIV, ModeleID, TypeID, CouleurID, Annee, ValeurCourante, Kilometrage, Automatique, Climatise, AntiDemarreur, Nouveau, EnLocation)
VALUES('7D901-9W120-Z0029-021P2', 2, 1, 2, 2003, 45000, 100000, 0, 1, 1, 0, 1)

INSERT INTO Vehicules (VehiculeNIV, ModeleID, TypeID, CouleurID, Annee, ValeurCourante, Kilometrage, Automatique, Climatise, AntiDemarreur, Nouveau, EnLocation)
VALUES('Z1221-X129A-KO212-9021J', 3, 2, 3, 2003, 70000, 0, 1, 1, 0, 1, 1)

INSERT INTO Vehicules (VehiculeNIV, ModeleID, TypeID, CouleurID, Annee, ValeurCourante, Kilometrage, Automatique, Climatise, AntiDemarreur, Nouveau, EnLocation)
VALUES('M21L1-3129S-V1292-LI2X1', 4, 3, 5, 2003, 85000, 0, 0, 1, 1, 1, 1)

INSERT INTO Vehicules (VehiculeNIV, ModeleID, TypeID, CouleurID, Annee, ValeurCourante, Kilometrage, Automatique, Climatise, AntiDemarreur, Nouveau, EnLocation)
VALUES('K219M-K129P-V12BP-210G4', 5, 1, 5, 2003, 60000, 0, 1, 1, 1, NULL, 0)

--INSERT INTO Vehicules (VehiculeNIV, ModeleID, TypeID, CouleurID, Annee, Valeur, Automatique, Climatise, AntiDemarreur, Nouveau)
--VALUES('M21L1-3129S-V1292-LI2X1', 4, 3, 5, 2003, 45000, 0, 1, 1, 0)


INSERT INTO TermesLocation (NbAnnees, KilometragePermis, TauxSurprime)
VALUES(3, 120000, 0.25)

INSERT INTO TermesLocation (NbAnnees, KilometragePermis, TauxSurprime)
VALUES(1, 85000, 0.20)

INSERT INTO TermesLocation (NbAnnees, KilometragePermis, TauxSurprime)
VALUES(2, 150000, 0.20)

INSERT INTO TermesLocation (NbAnnees, KilometragePermis, TauxSurprime)
VALUES(4, 130000, 0.15)

INSERT INTO TermesLocation (NbAnnees, KilometragePermis, TauxSurprime)
VALUES(1, 150000, 0.35)


INSERT INTO Locations(DateDebut, DateFin, DatePremierPaiement, MontantPaiementMensuel, NbPaiementMensuel, VehiculeNIV, ValeurVehicule, KilometrageDebut, KilometrageFin, Nouveau, ClientID, TermeLocationID)
VALUES(CAST('2004-01-15' AS date), NULL, CAST('2004-02-15' AS date), 650, 36, '3W9T1-2Q10D-12D0P-2E1R2', 90000, 0, NULL, 1, 1, 1)

INSERT INTO Locations(DateDebut, DateFin, DatePremierPaiement, MontantPaiementMensuel, NbPaiementMensuel, VehiculeNIV, ValeurVehicule, KilometrageDebut, KilometrageFin, Nouveau, ClientID, TermeLocationID)
VALUES(CAST('2004-03-16' AS date), NULL, CAST('2004-04-16' AS date), 350, 12, '7D901-9W120-Z0029-021P2', 45000, 100000, NULL, 0, 2, 2)

INSERT INTO Locations(DateDebut, DateFin, DatePremierPaiement, MontantPaiementMensuel, NbPaiementMensuel, VehiculeNIV, ValeurVehicule, KilometrageDebut, KilometrageFin, Nouveau, ClientID, TermeLocationID)
VALUES(CAST('2004-04-15' AS date), NULL, CAST('2004-05-15' AS date), 600, 24, 'Z1221-X129A-KO212-9021J', 70000, 0, NULL, 1, 3, 3)

INSERT INTO Locations(DateDebut, DateFin, DatePremierPaiement, MontantPaiementMensuel, NbPaiementMensuel, VehiculeNIV, ValeurVehicule, KilometrageDebut, KilometrageFin, Nouveau, ClientID, TermeLocationID)
VALUES(CAST('2002-02-20' AS date), CAST('2006-02-20' AS date), CAST('2002-03-20' AS date), 450, 48, 'M21L1-3129S-V1292-LI2X1', 85000, 0, 127000, 1, 4, 4)

INSERT INTO Locations(DateDebut, DateFin, DatePremierPaiement, MontantPaiementMensuel, NbPaiementMensuel, VehiculeNIV, ValeurVehicule, KilometrageDebut, KilometrageFin, Nouveau, ClientID, TermeLocationID)
VALUES(CAST('2005-05-15' AS date), NULL, CAST('2005-06-15' AS date), 300, 12, 'M21L1-3129S-V1292-LI2X1', 45000, 127000, NULL, 0, 1, 5)


INSERT INTO Paiements(DatePäiement, Montant, LocationID)
VALUES(CAST('2004-02-15' AS date), 650, 1)

INSERT INTO Paiements(DatePäiement, Montant, LocationID)
VALUES(CAST('2004-04-16' AS date), 350, 2)

INSERT INTO Paiements(DatePäiement, Montant, LocationID)
VALUES(CAST('2004-05-15' AS date), 600, 3)

INSERT INTO Paiements(DatePäiement, Montant, LocationID)
VALUES(CAST('2002-03-20' AS date), 450, 4)

INSERT INTO Paiements(DatePäiement, Montant, LocationID)
VALUES(CAST('2002-04-20' AS date), 450, 4)

INSERT INTO Paiements(DatePäiement, Montant, LocationID)
VALUES(CAST('2005-06-15' AS date), 300, 5)