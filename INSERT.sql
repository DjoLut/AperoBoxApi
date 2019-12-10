INSERT INTO [apero].[Adresse]([Rue], [Numero], [Localite], [CodePostal], [Pays])
	VALUES('Senonchamps',107, 'Bastogne', 6600, 'Belgique');
INSERT INTO [apero].[Adresse]([Rue], [Numero], [Localite], [CodePostal], [Pays])
	VALUES('Rue de l''ermitage', 18, 'Aubange', 6782, 'Belgique');
INSERT INTO [apero].[Adresse]([Rue], [Numero], [Localite], [CodePostal], [Pays])
	VALUES('Rue de la Rulles', 409, 'Saint-nicolas',4420,'Belgique')
INSERT INTO [apero].[Adresse]([Rue], [Numero], [Localite], [CodePostal], [Pays])
	VALUES('Taille Maréchal', 485, 'Gallaix', 7906, 'Belgique')
INSERT INTO [apero].[Adresse]([Rue], [Numero], [Localite], [CodePostal], [Pays])
	VALUES('Rue du Thisnes', 82, 'Reninge', 8647, 'Belgique')
INSERT INTO [apero].[Adresse]([Rue], [Numero], [Localite], [CodePostal], [Pays])
	VALUES('Heirstraat', 443, 'Mannekensvere', 8433, 'Belgique')
INSERT INTO [apero].[Adresse]([Rue], [Numero], [Localite], [CodePostal], [Pays])
	VALUES('Lange Elzenstraat', 3, 'Bredene', 8450, 'Belgique')
INSERT INTO [apero].[Adresse]([Rue], [Numero], [Localite], [CodePostal], [Pays])
	VALUES('Dijkstraat', 193, 'Eigenbilzen', 3740, 'Belgique')
GO

select * from [apero].[Adresse];

INSERT INTO [apero].[Utilisateur]([Nom], [Prenom], [DateNaissance], [Mail], [GSM], [Username], [MotDePasse], [Adresse])
	VALUES('Philipin', 'Pierre', PARSE('02/03/1995' AS datetime2 USING 'fr-FR'), 'pierre.philipin@hotmail.com', 0488654916, 'philipinPierre', '123', 1)
INSERT INTO [apero].[Utilisateur]([Nom], [Prenom], [DateNaissance], [Mail], [GSM], [Username], [MotDePasse], [Adresse])
	VALUES('Lutgen', 'Jordan', PARSE('15/08/1995' AS datetime2 USING 'fr-FR'), 'jordan.lutgen@hotmail.com', 0475872133, 'jordanLutgen', '123', 2)
GO

select * from [apero].[Utilisateur];

INSERT INTO [apero].[Produit]([PrixUnitaireHtva], [Tva], [Nom], [DatePeremption], [Alcool]) 
	VALUES(3.84, 0.21, 'Chips de pommes', NULL, 1);
INSERT INTO [apero].[Produit]([PrixUnitaireHtva], [Tva], [Nom], [DatePeremption], [Alcool]) 
	VALUES(2.83, 0.21, 'Pistache grillées et salées', NULL, 0);
INSERT INTO [apero].[Produit]([PrixUnitaireHtva], [Tva], [Nom], [DatePeremption], [Alcool]) 
	VALUES(4.55, 0.21, 'Olives vertes', NULL, 0);
INSERT INTO [apero].[Produit]([PrixUnitaireHtva], [Tva], [Nom], [DatePeremption], [Alcool]) 
	VALUES(5.19, 0.21, 'Rilettes de truite', NULL, 0);
INSERT INTO [apero].[Produit]([PrixUnitaireHtva], [Tva], [Nom], [DatePeremption], [Alcool]) 
	VALUES(1.43, 0.21, 'Grissini', NULL, 0);
INSERT INTO [apero].[Produit]([PrixUnitaireHtva], [Tva], [Nom], [DatePeremption], [Alcool]) 
	VALUES(3.83, 0.21, 'Saucisson au vin', NULL, 0);
INSERT INTO [apero].[Produit]([PrixUnitaireHtva], [Tva], [Nom], [DatePeremption], [Alcool]) 
	VALUES(5.17, 0.21, 'Bière Elfique', NULL, 0);
GO

select * from [apero].[Produit];

INSERT INTO [apero].[Box]([Nom], [PrixUnitaireHtva], [Tva], [Promotion], [Description], [Photo], [Affichable], [DateCreation])
	VALUES('Box Apero Simple 2', 26.83, 0.21, NULL, 'Box bières et saucisson pour 2 personnes', 'box_biscuit_biere.jpg', 1, PARSE('30/11/2019' AS datetime2 USING 'fr-FR'));
INSERT INTO [apero].[Box]([Nom], [PrixUnitaireHtva], [Tva], [Promotion], [Description], [Photo], [Affichable], [DateCreation])
	VALUES('Box Apero Simple 4', 53.66, 0.21, NULL, 'Box bières et saucisson pour 4 personnes', 'box_biscuit_biere.jpg', 1, PARSE('30/11/2019' AS datetime2 USING 'fr-FR'));
INSERT INTO [apero].[Box]([Nom], [PrixUnitaireHtva], [Tva], [Promotion], [Description], [Photo], [Affichable], [DateCreation])
	VALUES('Box Apero Simple 6', 80.50, 0.21, NULL, 'Box bières et saucisson pour 6 personnes', 'box_biscuit_biere.jpg', 1, PARSE('30/11/2019' AS datetime2 USING 'fr-FR'));
INSERT INTO [apero].[Box]([Nom], [PrixUnitaireHtva], [Tva], [Promotion], [Description], [Photo], [Affichable], [DateCreation])
	VALUES('Box Apero Simple 8', 107.33, 0.21, NULL, 'Box bières et saucisson pour 8 personnes', 'box_biscuit_biere.jpg', 1, PARSE('30/11/2019' AS datetime2 USING 'fr-FR'));
GO

select * from [apero].[Box];

/* Pour la box 2 personnes */
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(1, 1, 1);
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(1, 1, 2);
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(1, 1, 3);
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(1, 1, 4);
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(1, 1, 5);
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(1, 1, 6);
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(1, 1, 7);

/* Pour la box 4 personnes */
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(2, 2, 1);
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(2, 2, 2);
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(2, 2, 3);
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(2, 2, 4);
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(2, 2, 5);
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(2, 2, 6);
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(2, 2, 7);

/* Pour la box 6 personnes */
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(3, 3, 1);
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(3, 3, 2);
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(3, 3, 3);
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(3, 3, 4);
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(3, 3, 5);
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(3, 3, 6);
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(3, 3, 7);

/* Pour la box 8 personnes */
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(4, 4, 1);
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(4, 4, 2);
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(4, 4, 3);
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(4, 4, 4);
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(4, 4, 5);
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(4, 4, 6);
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(4, 4, 7);
GO

select * from [apero].[LigneProduit];

INSERT INTO [apero].[Commentaire]([Texte], [DateCreation], [Utilisateur], [Box])
	VALUES('Super bon !', PARSE('02/12/2019' AS datetime2 USING 'fr-FR'), 1, 1);
INSERT INTO [apero].[Commentaire]([Texte], [DateCreation], [Utilisateur], [Box])
	VALUES('Bonne bières :)', PARSE('02/12/2019' AS datetime2 USING 'fr-FR'), 1, 2);
INSERT INTO [apero].[Commentaire]([Texte], [DateCreation], [Utilisateur], [Box])
	VALUES('Sans plus', PARSE('02/12/2019' AS datetime2 USING 'fr-FR'), 1, 3);
INSERT INTO [apero].[Commentaire]([Texte], [DateCreation], [Utilisateur], [Box])
	VALUES('Pas terrible', PARSE('02/12/2019' AS datetime2 USING 'fr-FR'), 2, 1);
INSERT INTO [apero].[Commentaire]([Texte], [DateCreation], [Utilisateur], [Box])
	VALUES('J''adore !', PARSE('02/12/2019' AS datetime2 USING 'fr-FR'), 2, 2);
INSERT INTO [apero].[Commentaire]([Texte], [DateCreation], [Utilisateur], [Box])
	VALUES('Excellent !', PARSE('02/12/2019' AS datetime2 USING 'fr-FR'), 2, 3);
GO

select * from [apero].[Commentaire];

INSERT INTO [apero].[Role]([Nom]) 
	VALUES('utilisateur');
INSERT INTO [apero].[Role]([Nom]) 
	VALUES('admin');
GO

select * from [apero].[Role];

INSERT INTO [apero].[UtilisateurRole](IdRole, IdUtilisateur) 
	VALUES('admin', 1);
INSERT INTO [apero].[UtilisateurRole](IdRole, IdUtilisateur) 
	VALUES('utilisateur', 1);
INSERT INTO [apero].[UtilisateurRole](IdRole, IdUtilisateur) 
	VALUES('admin', 2);
INSERT INTO [apero].[UtilisateurRole](IdRole, IdUtilisateur) 
	VALUES('utilisateur', 2);
GO

select * from [apero].[UtilisateurRole];

