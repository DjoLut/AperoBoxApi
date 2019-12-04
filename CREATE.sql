/****** Object:  Table [apero].[Produit]    Script Date: 23-11-19 14:27:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [apero].[Produit](
	[ID] [numeric](18, 0) NOT NULL IDENTITY(1,1),
	[PrixUnitaireHTVA] [numeric](18, 2) NOT NULL,
	[TVA] [numeric](6, 6) NOT NULL,
	[Nom] [varchar](50) NOT NULL,
	[DatePeremption] [date] NULL,
	[Alcool] [tinyint] NOT NULL,
	RowVersion timestamp
 CONSTRAINT [PK_Produit] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [apero].[Produit]  WITH CHECK ADD  CONSTRAINT [CK_Produit_PrixUnitaireHTVA_Positif] CHECK  (([PrixUnitaireHTVA]>=(0)))
GO

ALTER TABLE [apero].[Produit] CHECK CONSTRAINT [CK_Produit_PrixUnitaireHTVA_Positif]
GO

ALTER TABLE [apero].[Produit]  WITH CHECK ADD  CONSTRAINT [CK_Produit_TVA_MAX50%] CHECK  (([TVA]<=(0.5)))
GO

ALTER TABLE [apero].[Produit] CHECK CONSTRAINT [CK_Produit_TVA_MAX50%]
GO

/****** Object:  Table [apero].[Adresse]    Script Date: 23-11-19 19:42:47 ******/

CREATE TABLE [apero].[Adresse](
	[ID] [numeric](18, 0) NOT NULL IDENTITY(1,1),
	[Rue] [varchar](250) NOT NULL,
	[Numero] [varchar](50) NOT NULL,
	[Localite] [varchar](250) NOT NULL,
	[CodePostal] [numeric](18, 0) NOT NULL,
	[Pays] [varchar](250) NOT NULL,
	RowVersion timestamp
 CONSTRAINT [PK_Adresse] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [apero].[Adresse] WITH CHECK ADD CONSTRAINT [CK_Adresse_CodePostal] CHECK (([CodePostal]>=(0)))
GO

ALTER TABLE [apero].[Adresse] CHECK CONSTRAINT [CK_Adresse_CodePostal]
GO

/****** Object:  Table [dbo].[Box]    Script Date: 23-11-19 19:39:07 ******/

CREATE TABLE [apero].[Box](
	[ID] [numeric](18, 0) NOT NULL IDENTITY(1,1),
	[Nom] [varchar](50) NOT NULL,
	[PrixUnitaireHTVA] [numeric](18, 2) NOT NULL,
	[TVA] [numeric](18, 3) NOT NULL,
	[Promotion] [numeric](18, 2) NULL,
	[Description] [varchar](350) NOT NULL,
	[Photo] [varchar](150) NOT NULL,
	[Affichable] [tinyint] NOT NULL,
	[DateCreation] [date] NOT NULL,
	RowVersion timestamp
 CONSTRAINT [PK_Box] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [apero].[Box]  WITH CHECK ADD  CONSTRAINT [CK_Box_PrixUnitaireHTVA_Positif] CHECK  (([PrixUnitaireHTVA]>=(0)))
GO

ALTER TABLE [apero].[Box] CHECK CONSTRAINT [CK_Box_PrixUnitaireHTVA_Positif]
GO

ALTER TABLE [apero].[Box]  WITH CHECK ADD  CONSTRAINT [CK_Box_Promotion_Max100%] CHECK  (([Promotion]<(1)))
GO

ALTER TABLE [apero].[Box] CHECK CONSTRAINT [CK_Box_Promotion_Max100%]
GO

/****** Object:  Table [apero].[Utilisateur]    Script Date: 23-11-19 19:39:07 ******/

CREATE TABLE [apero].[Utilisateur](
	[ID] [numeric](18, 0) NOT NULL IDENTITY(1,1),
	[Nom] [varchar](255) NOT NULL,
	[Prenom] [varchar](255) NOT NULL,
    [DateNaissance] [date] NOT NULL,
    [Mail][varchar](255) NOT NULL,
    [Telephone] [numeric](18,0),
    [GSM] [numeric](18,0) NOT NULL,
    [Username] [varchar](255) NOT NULL,
    [Authorities] [varchar](500) NOT NULL,
    [MotDePasse] [varchar](255) NOT NULL,
    [Adresse] [numeric](18, 0) NOT NULL,
	RowVersion timestamp
 CONSTRAINT [PK_Utilisateur] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [apero].[Utilisateur] ADD CONSTRAINT [Fk_Utilisateur_Adresse] FOREIGN KEY ([Adresse]) REFERENCES [apero].[Adresse] ([ID])
GO

ALTER TABLE [apero].[Utilisateur] ADD CONSTRAINT [UNIQUE_Utilisateur_Username] UNIQUE ([Username])
GO

/****** Object:  Table [apero].[Commentaire]    Script Date: 23-11-19 19:39:07 ******/

CREATE TABLE [apero].[Commentaire](
	[ID] [numeric](18, 0) NOT NULL IDENTITY(1,1),
	[Texte] [varchar](2000) NOT NULL,
    [DateCreation] [date] NOT NULL,
    [Utilisateur] [numeric](18, 0) NOT NULL,
    [Box] [numeric](18, 0) NOT NULL,
	RowVersion timestamp
 CONSTRAINT [PK_Commentaire] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [apero].[Commentaire] ADD CONSTRAINT [Fk_Commentaire_Box] FOREIGN KEY ([Box]) REFERENCES [apero].[Box] ([ID])
GO

ALTER TABLE [apero].[Commentaire] ADD CONSTRAINT [Fk_Commentaire_Utilisateur] FOREIGN KEY ([Utilisateur]) REFERENCES [apero].[Utilisateur] ([ID])
GO


/****** Object:  Table [apero].[Commande]    Script Date: 23-11-19 19:39:07 ******/

CREATE TABLE [apero].[Commande](
	[ID] [numeric](18, 0) NOT NULL IDENTITY(1,1),
    [DateCreation] [date] NOT NULL,
    [Promotion] [numeric](18,2) NOT NULL,
    [Utilisateur] [numeric](18, 0) NOT NULL,
    [Adresse] [numeric](18, 0) NOT NULL,
	RowVersion timestamp
 CONSTRAINT [PK_Commande] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [apero].[Commande] ADD CONSTRAINT [Fk_Commande_Adresse] FOREIGN KEY ([Adresse]) REFERENCES [apero].[Adresse] ([ID])
GO

ALTER TABLE [apero].[Commande] ADD CONSTRAINT [Fk_Commande_Utilisateur] FOREIGN KEY ([Utilisateur]) REFERENCES [apero].[Utilisateur] ([ID])
GO

/****** Object:  Table [apero].[LigneCommande]    Script Date: 23-11-19 19:39:07 ******/

CREATE TABLE [apero].[LigneCommande](
	[ID] [numeric](18, 0) NOT NULL IDENTITY(1,1),
    [Quantite] [numeric](3,0) NOT NULL,
    [Commande] [numeric](18, 0) NOT NULL,
    [Box] [numeric](18, 0),
    [Produit] [numeric] (18,0),
	RowVersion timestamp
 CONSTRAINT [PK_LigneCommande] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [apero].[LigneCommande] ADD CONSTRAINT [Fk_LigneCommande_Box] FOREIGN KEY ([Box]) REFERENCES [apero].[Box] ([ID])
GO

ALTER TABLE [apero].[LigneCommande] ADD CONSTRAINT [Fk_LigneCommande_Commande] FOREIGN KEY ([Commande]) REFERENCES [apero].[Commande] ([ID])
GO

ALTER TABLE [apero].[LigneCommande] ADD CONSTRAINT [Fk_LigneCommande_Produit] FOREIGN KEY ([Produit]) REFERENCES [apero].[Produit] ([ID])
GO

/****** Object:  Table [apero].[LigneProduit]    Script Date: 23-11-19 19:39:07 ******/

CREATE TABLE [apero].[LigneProduit](
	[ID] [numeric](18, 0) NOT NULL IDENTITY(1,1),
    [Quantite] [numeric](3,0) NOT NULL,
    [Box] [numeric](18, 0) NOT NULL,
    [Produit] [numeric] (18,0) NOT NULL,
	RowVersion timestamp
 CONSTRAINT [PK_LigneProduit] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [apero].[LigneProduit] ADD CONSTRAINT [Fk_LigneProduit_Box] FOREIGN KEY ([Box]) REFERENCES [apero].[Box] ([ID])
GO

ALTER TABLE [apero].[LigneProduit] ADD CONSTRAINT [Fk_LigneProduit_Produit] FOREIGN KEY ([Produit]) REFERENCES [apero].[Produit] ([ID])
GO

/****** Object:  Table [apero].[Role]    Script Date: 30-11-19 19:39:07 ******/
/*
CREATE TABLE [apero].[Role](
	[Nom] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Nom] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
*/
/****** Object:  Table [apero].[UtilisateurRole]    Script Date: 30-11-19 19:39:07 ******/
/*
CREATE TABLE [apero].[UtilisateurRole](
	[IdRole] [nvarchar](450) NOT NULL,
	[IdUtilisateur] [int] NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[IdRole] ASC,
	[IdUtilisateur] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [apero].[UtilisateurRole] WITH CHECK ADD  CONSTRAINT [FK_UserRole_Role_IdRole] FOREIGN KEY([IdRole])
REFERENCES [apero].[Role] ([Nom])
ON DELETE CASCADE
GO

ALTER TABLE [apero].[UtilisateurRole] CHECK CONSTRAINT [FK_UserRole_Role_IdRole]
GO

ALTER TABLE [apero].[UtilisateurRole] WITH CHECK ADD  CONSTRAINT [FK_UserRole_User_IdUtilisateur] FOREIGN KEY([IdUtilisateur])
REFERENCES [apero].[Utilisateur] ([ID])
ON DELETE CASCADE
GO

ALTER TABLE [apero].[UtilisateurRole] CHECK CONSTRAINT [FK_UserRole_User_IdUtilisateur]
GO
*/



