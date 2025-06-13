USE MultiLocations7336
GO

CREATE PROCEDURE InsertionAudit @change_date smalldatetime, @mod_column nvarchar(25), @old_value nvarchar(30),
							 @new_value nvarchar(30), @locationID int
AS
BEGIN
	INSERT INTO [dbo].[Audits]
           ([DateChangement]
           ,[ChampModifie]
           ,[AncienneValeur]
           ,[NouvelleValeur]
           ,[LocationID])
     VALUES
           (@change_date, @mod_column, @old_value, @new_value, @locationID)
END