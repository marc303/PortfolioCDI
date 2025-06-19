USE MultiLocations7336
GO
CREATE PROCEDURE InsertionPaiement @locationID int, @montant smallmoney
AS
BEGIN
	IF(SELECT MontantPaiementMensuel FROM Locations WHERE LocationID = @locationID) <> @montant
	BEGIN
		PRINT 'Paiement annulé. Le montant saisi ne correspond pas à celui exigé dans le contrat de location.'
		SET @montant = 0
	END
	INSERT INTO Paiements
	VALUES(GETDATE(), @montant, @locationID)
END