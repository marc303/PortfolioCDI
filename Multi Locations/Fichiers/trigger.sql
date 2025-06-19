USE MultiLocations7336
GO
CREATE TRIGGER VerificationAjout_Location
   ON Locations 
   AFTER INSERT
AS 
BEGIN
	IF(SELECT ValeurVehicule FROM inserted) <
		(SELECT ValeurCourante FROM Vehicules
		INNER JOIN inserted ON
		Vehicules.VehiculeNIV = inserted.VehiculeNIV)
	BEGIN
		UPDATE Vehicules
		SET ValeurCourante = inserted.ValeurVehicule, 
			Kilometrage = inserted.KilometrageDebut,
			Nouveau = inserted.Nouveau
		PRINT 'Mise à jour de la table Véhicules effectuées avec succès.'
	END
END
GO
