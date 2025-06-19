USE MultiLocations7336
GO

SELECT c.Prenom, c.Nom, c.Telephone, COUNT(*) 'QuantiteLoue'
FROM Clients c INNER JOIN Locations l ON
c.ClientID = l.ClientID
GROUP BY c.Telephone, c.Nom, c.Prenom