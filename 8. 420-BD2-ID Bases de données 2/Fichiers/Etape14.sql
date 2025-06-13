USE MultiLocations7336
GO

CREATE TRIGGER AuditLocation_AfterUpdate
ON Locations
AFTER UPDATE
AS
BEGIN
	DECLARE @locationID int, @mod_date smalldatetime, @old_value nvarchar(30), @new_value nvarchar(30)
	SET @locationID = (SELECT LocationID FROM inserted)
	SET @mod_date = GETDATE()

	IF (UPDATE(DateDebut))
	BEGIN
		SET @old_value = (SELECT CAST(DateDebut AS nvarchar(30)) FROM deleted)
		SET @new_value = (SELECT CAST(DateDebut AS nvarchar(30)) FROM inserted)
		EXEC dbo.InsertionAudit @mod_date, 'DateDebut', @old_value, @new_value, @locationID
	END

	IF (UPDATE(DateFin))
	BEGIN
		SET @old_value = (SELECT CAST(DateFin AS nvarchar(30)) FROM deleted)
		SET @new_value = (SELECT CAST(DateFin AS nvarchar(30)) FROM inserted)
		EXEC dbo.InsertionAudit @mod_date, 'DateFin', @old_value, @new_value, @locationID
	END

	IF (UPDATE(DatePremierPaiement))
	BEGIN
		SET @old_value = (SELECT CAST(DatePremierPaiement AS nvarchar(30)) FROM deleted)
		SET @new_value = (SELECT CAST(DatePremierPaiement AS nvarchar(30)) FROM inserted)
		EXEC dbo.InsertionAudit @mod_date, 'DatePremierPaiement', @old_value, @new_value, @locationID
	END

	IF (UPDATE(MontantPaimentMensuel))
	BEGIN
		SET @old_value = (SELECT CAST(MontantPaiementMensuel AS nvarchar(30)) FROM deleted)
		SET @new_value = (SELECT CAST(MontantPaiementMensuel AS nvarchar(30)) FROM inserted)
		EXEC dbo.InsertionAudit @mod_date, 'MontantPaimentMensuel', @old_value, @new_value, @locationID
	END

	IF (UPDATE(NbPaiementMensuel))
	BEGIN
		SET @old_value = (SELECT CAST(NbPaiementMensuel AS nvarchar(30)) FROM deleted)
		SET @new_value = (SELECT CAST(NbPaiementMensuel AS nvarchar(30)) FROM inserted)
		EXEC dbo.InsertionAudit @mod_date, 'NbPaiementMensuel', @old_value, @new_value, @locationID
	END

	IF (UPDATE(VehiculeNIV))
	BEGIN
		SET @old_value = (SELECT CAST(VehiculeNIV AS nvarchar(30)) FROM deleted)
		SET @new_value = (SELECT CAST(VehiculeNIV AS nvarchar(30)) FROM inserted)
		EXEC dbo.InsertionAudit @mod_date, 'VehiculeNIV', @old_value, @new_value, @locationID
	END

	IF (UPDATE(ValeurVehicule))
	BEGIN
		SET @old_value = (SELECT CAST(ValeurVehicule AS nvarchar(30)) FROM deleted)
		SET @new_value = (SELECT CAST(ValeurVehicule AS nvarchar(30)) FROM inserted)
		EXEC dbo.InsertionAudit @mod_date, 'ValeurVehicule', @old_value, @new_value, @locationID
	END

	IF (UPDATE(KilometrageDebut))
	BEGIN
		SET @old_value = (SELECT CAST(KilometrageDebut AS nvarchar(30)) FROM deleted)
		SET @new_value = (SELECT CAST(KilometrageDebut AS nvarchar(30)) FROM inserted)
		EXEC dbo.InsertionAudit @mod_date, 'KilometrageDebut', @old_value, @new_value, @locationID
	END

	IF (UPDATE(KilometrageFin))
	BEGIN
		SET @old_value = (SELECT CAST(KilometrageFin AS nvarchar(30)) FROM deleted)
		SET @new_value = (SELECT CAST(KilometrageFin AS nvarchar(30)) FROM inserted)
		EXEC dbo.InsertionAudit @mod_date, 'KilometrageFin', @old_value, @new_value, @locationID
	END

	IF (UPDATE(Nouveau))
	BEGIN
		IF(SELECT Nouveau FROM deleted) = 1 
			SET @old_value = 'True'
		ELSE
			SET @old_value = 'False'
		
		IF(SELECT Nouveau FROM inserted) = 1 
			SET @new_value = 'True'
		ELSE
			SET @new_value = 'False'

		EXEC dbo.InsertionAudit @mod_date, 'Nouveau', @old_value, @new_value, @locationID
	END

	IF (UPDATE(ClientID))
	BEGIN
		SET @old_value = (SELECT CAST(ClientID AS nvarchar(30)) FROM deleted)
		SET @new_value = (SELECT CAST(ClientID AS nvarchar(30)) FROM inserted)
		EXEC dbo.InsertionAudit @mod_date, 'ClientID', @old_value, @new_value, @locationID
	END

	IF (UPDATE(TermeLocationID))
	BEGIN
		SET @old_value = (SELECT CAST(TermeLocationID AS nvarchar(30)) FROM deleted)
		SET @new_value = (SELECT CAST(TermeLocationID AS nvarchar(30)) FROM inserted)
		EXEC dbo.InsertionAudit @mod_date, 'TermeLocationID', @old_value, @new_value, @locationID
	END
END