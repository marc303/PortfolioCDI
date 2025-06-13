USE MultiLocations7336
GO

CREATE VIEW LocationsEtPaiements AS
SELECT c.Prenom + ' ' + c.Nom AS NomComplet,
	   c.Telephone,
	   v.VehiculeNIV,
	   (SELECT Nom FROM Modeles 
		WHERE v.ModeleID = Modeles.ModeleID) AS Modèle,
	   (SELECT Nom FROM Couleurs 
		WHERE v.CouleurID = Couleurs.CouleurID) AS Couleur,
		l.DateDebut,
		l.DatePremierPaiement,
		l.MontantPaiementMensuel,
		l.NbPaiementMensuel,
		p.DatePäiement,
		p.Montant
FROM Paiements p, Clients c, Locations l, Vehicules v
WHERE c.ClientID = l.ClientID AND l.LocationID = p.LocationID AND l.VehiculeNIV = v.VehiculeNIV