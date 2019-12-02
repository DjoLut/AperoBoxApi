INSERT INTO [apero].[Adresse]([Rue], [Numero], [Localite], [CodePostal], [Pays])
	VALUES('Senonchamps','107', 'Bastogne', 6600, 'Belgique');
INSERT INTO [apero].[Adresse]([Rue], [Numero], [Localite], [CodePostal], [Pays])
	VALUES('Rue de l''ermitage', '18', 'Aubange', 6782, 'Belgique');
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

INSERT INTO [apero].[Utilisateur]([Nom], [Prenom], [DateNaissance], [Mail], [GSM], [Username], [Authorities], [MotDePasse], [Adresse])
	VALUES('Philipin', 'Pierre', PARSE('02/03/1995' AS datetime2 USING 'fr-FR'), 'pierre.philipin@hotmail.com', 0488654916, 'philipinPierre', 'ADMIN', '123', 1)
INSERT INTO [apero].[Utilisateur]([Nom], [Prenom], [DateNaissance], [Mail], [GSM], [Username], [Authorities], [MotDePasse], [Adresse])
	VALUES('Lutgen', 'Jordan', PARSE('15/08/1995' AS datetime2 USING 'fr-FR'), 'jordan.lutgen@hotmail.com', 0475872133, 'jordanLutgen', 'ADMIN', '123', 2)
GO

select * from [apero].[Utilisateur];

INSERT INTO [apero].[Produit]([PrixUnitaireHtva], [Tva], [Nom], [DatePeremption], [Alcool]) 
	VALUES(3.5, 0.21, 'Cuvées des trolls', NULL, 1);
INSERT INTO [apero].[Produit]([PrixUnitaireHtva], [Tva], [Nom], [DatePeremption], [Alcool]) 
	VALUES(4, 0.21, 'Fromage en Cube', NULL, 0);
INSERT INTO [apero].[Produit]([PrixUnitaireHtva], [Tva], [Nom], [DatePeremption], [Alcool]) 
	VALUES(5.5, 0.21, 'Saucisse en tranches', NULL, 0);
INSERT INTO [apero].[Produit]([PrixUnitaireHtva], [Tva], [Nom], [DatePeremption], [Alcool]) 
	VALUES(3, 0.21, 'Chips au sel', NULL, 0);
GO

select * from [apero].[Produit];

INSERT INTO [apero].[Box]([Nom], [PrixUnitaireHtva], [Tva], [Promotion], [Description], [Photo], [Affichable], [DateCreation])
	VALUES('Box Apero Simple', 7.99, 0.21, NULL, 'Box bières belges et saucisse', 'img.jpeg', 1, PARSE('30/11/2019' AS datetime2 USING 'fr-FR'));
INSERT INTO [apero].[Box]([Nom], [PrixUnitaireHtva], [Tva], [Promotion], [Description], [Photo], [Affichable], [DateCreation])
	VALUES('Box Apero Gourmand', 15.99, 0.21, NULL, 'Box gourmande sans alcool', 'img2.jpeg', 1, PARSE('16/09/2019' AS datetime2 USING 'fr-FR'));
GO

select * from [apero].[Box];

INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(2, 1, 1);
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(1, 1, 3);
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(3, 2, 2);
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(2, 2, 3);
INSERT INTO [apero].[LigneProduit]([Quantite], [Box], [Produit])
	VALUES(2, 2, 4);
GO

select * from [apero].[LigneProduit];

INSERT INTO [apero].[Commentaire]([Texte], [DateCreation], [Utilisateur], [Box])
	VALUES('Super bon !', PARSE('02/12/2019' AS datetime2 USING 'fr-FR'), 2, 1);
INSERT INTO [apero].[Commentaire]([Texte], [DateCreation], [Utilisateur], [Box])
	VALUES('Bonne bières belges :)', PARSE('02/12/2019' AS datetime2 USING 'fr-FR'), 1, 1);
INSERT INTO [apero].[Commentaire]([Texte], [DateCreation], [Utilisateur], [Box])
	VALUES('Sans plus', PARSE('02/12/2019' AS datetime2 USING 'fr-FR'), 1, 1);
INSERT INTO [apero].[Commentaire]([Texte], [DateCreation], [Utilisateur], [Box])
	VALUES('Pas terrible', PARSE('02/12/2019' AS datetime2 USING 'fr-FR'), 2, 1);
INSERT INTO [apero].[Commentaire]([Texte], [DateCreation], [Utilisateur], [Box])
	VALUES('J''adore !', PARSE('02/12/2019' AS datetime2 USING 'fr-FR'), 2, 2);
GO

select * from [apero].[Commentaire];
/*
INSERT INTO [apero].[Role]([Nom]) 
	VALUES('user');
INSERT INTO [apero].[Role]([Nom]) 
	VALUES('admin');

select * from [apero].[Role];

INSERT INTO [apero].[UtilisateurRole](IdRole, IdUtilisateur) 
	VALUES('admin', 1);
INSERT INTO [apero].[UtilisateurRole](IdRole, IdUtilisateur) 
	VALUES('user', 1);
INSERT INTO [apero].[UtilisateurRole](IdRole, IdUtilisateur) 
	VALUES('admin', 2);
INSERT INTO [apero].[UtilisateurRole](IdRole, IdUtilisateur) 
	VALUES('user', 2);

select * from [apero].[UtilisateurRole];
*/




