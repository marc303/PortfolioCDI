USE MultiLocations7336
GO

SELECT v.VehiculeNIV, m.Nom AS Modèle, t.Nom AS Type, c.Nom AS Couleur
FROM Vehicules v
LEFT JOIN Modeles m
ON v.ModeleID = m.ModeleID
LEFT JOIN Types t
ON v.TypeID = t.TypeID
LEFT JOIN Couleurs c
ON v.CouleurID = c.CouleurID
WHERE v.EnLocation = 0