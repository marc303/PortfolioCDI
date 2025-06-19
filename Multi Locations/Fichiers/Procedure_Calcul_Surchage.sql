USE MultiLocations7336
GO
CREATE PROCEDURE CalculSurchage @kilometrage int, @locationID int
AS
BEGIN
	DECLARE @termeID int, @surchage smallmoney, @kilometrage_permis int
	SET @termeID = (SELECT TermeLocationID FROM Locations WHERE LocationID = @locationID)
	SET @surchage = 0
	SET @kilometrage_permis = (SELECT KilometragePermis FROM TermesLocation WHERE TermeLocationID = @termeID)

	IF( @kilometrage > @kilometrage_permis)
	BEGIN
		SET @surchage = ( SELECT (@kilometrage - @kilometrage_permis) * TauxSurprime
						  FROM TermesLocation
						  WHERE TermeLocationID = @termeID)
	END
	SELECT @surchage AS SurchageLocation
END