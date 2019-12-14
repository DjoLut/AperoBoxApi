using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AperoBoxApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "apero");

            migrationBuilder.CreateTable(
                name: "Adresse",
                schema: "apero",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "numeric(18, 0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rue = table.Column<string>(unicode: false, maxLength: 250, nullable: false),
                    Numero = table.Column<decimal>(type: "numeric(18, 0)", nullable: false),
                    Localite = table.Column<string>(unicode: false, maxLength: 250, nullable: false),
                    CodePostal = table.Column<decimal>(type: "numeric(18, 0)", nullable: false),
                    Pays = table.Column<string>(unicode: false, maxLength: 250, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresse", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Box",
                schema: "apero",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "numeric(18, 0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    PrixUnitaireHTVA = table.Column<decimal>(type: "numeric(18, 2)", nullable: false),
                    TVA = table.Column<decimal>(type: "numeric(18, 3)", nullable: false),
                    Promotion = table.Column<decimal>(type: "numeric(18, 2)", nullable: true),
                    Description = table.Column<string>(unicode: false, maxLength: 350, nullable: false),
                    Photo = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    Affichable = table.Column<byte>(nullable: false),
                    DateCreation = table.Column<DateTime>(type: "date", nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Box", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Produit",
                schema: "apero",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "numeric(18, 0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrixUnitaireHTVA = table.Column<decimal>(type: "numeric(18, 2)", nullable: false),
                    TVA = table.Column<decimal>(type: "numeric(6, 6)", nullable: false),
                    Nom = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    DatePeremption = table.Column<DateTime>(type: "date", nullable: true),
                    Alcool = table.Column<byte>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produit", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "apero",
                columns: table => new
                {
                    Nom = table.Column<string>(unicode: false, maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Nom);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateur",
                schema: "apero",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "numeric(18, 0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    Prenom = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    DateNaissance = table.Column<DateTime>(type: "date", nullable: false),
                    Mail = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    Telephone = table.Column<decimal>(type: "numeric(18, 0)", nullable: true),
                    GSM = table.Column<decimal>(type: "numeric(18, 0)", nullable: false),
                    Username = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    MotDePasse = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    Adresse = table.Column<decimal>(type: "numeric(18, 0)", nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateur", x => x.ID);
                    table.ForeignKey(
                        name: "Fk_Utilisateur_Adresse",
                        column: x => x.Adresse,
                        principalSchema: "apero",
                        principalTable: "Adresse",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LigneProduit",
                schema: "apero",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "numeric(18, 0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantite = table.Column<decimal>(type: "numeric(3, 0)", nullable: false),
                    Box = table.Column<decimal>(type: "numeric(18, 0)", nullable: false),
                    Produit = table.Column<decimal>(type: "numeric(18, 0)", nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LigneProduit", x => x.ID);
                    table.ForeignKey(
                        name: "Fk_LigneProduit_Box",
                        column: x => x.Box,
                        principalSchema: "apero",
                        principalTable: "Box",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Fk_LigneProduit_Produit",
                        column: x => x.Produit,
                        principalSchema: "apero",
                        principalTable: "Produit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Commande",
                schema: "apero",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "numeric(18, 0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreation = table.Column<DateTime>(type: "date", nullable: false),
                    Promotion = table.Column<decimal>(type: "numeric(18, 2)", nullable: false),
                    Utilisateur = table.Column<decimal>(type: "numeric(18, 0)", nullable: false),
                    Adresse = table.Column<decimal>(type: "numeric(18, 0)", nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commande", x => x.ID);
                    table.ForeignKey(
                        name: "Fk_Commande_Adresse",
                        column: x => x.Adresse,
                        principalSchema: "apero",
                        principalTable: "Adresse",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Fk_Commande_Utilisateur",
                        column: x => x.Utilisateur,
                        principalSchema: "apero",
                        principalTable: "Utilisateur",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Commentaire",
                schema: "apero",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "numeric(18, 0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Texte = table.Column<string>(unicode: false, maxLength: 2000, nullable: false),
                    DateCreation = table.Column<DateTime>(type: "date", nullable: false),
                    Utilisateur = table.Column<decimal>(type: "numeric(18, 0)", nullable: false),
                    Box = table.Column<decimal>(type: "numeric(18, 0)", nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commentaire", x => x.ID);
                    table.ForeignKey(
                        name: "Fk_Commentaire_Box",
                        column: x => x.Box,
                        principalSchema: "apero",
                        principalTable: "Box",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Fk_Commentaire_Utilisateur",
                        column: x => x.Utilisateur,
                        principalSchema: "apero",
                        principalTable: "Utilisateur",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UtilisateurRole",
                schema: "apero",
                columns: table => new
                {
                    IdRole = table.Column<string>(unicode: false, maxLength: 450, nullable: false),
                    IdUtilisateur = table.Column<decimal>(type: "numeric(18, 0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtilisateurRole", x => new { x.IdRole, x.IdUtilisateur });
                    table.ForeignKey(
                        name: "FK_UtilisateurRole_Role_IdRole",
                        column: x => x.IdRole,
                        principalSchema: "apero",
                        principalTable: "Role",
                        principalColumn: "Nom",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UtilisateurRole_Utilisateur_IdUtilisateur",
                        column: x => x.IdUtilisateur,
                        principalSchema: "apero",
                        principalTable: "Utilisateur",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LigneCommande",
                schema: "apero",
                columns: table => new
                {
                    ID = table.Column<decimal>(type: "numeric(18, 0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantite = table.Column<decimal>(type: "numeric(3, 0)", nullable: false),
                    Commande = table.Column<decimal>(type: "numeric(18, 0)", nullable: false),
                    Box = table.Column<decimal>(type: "numeric(18, 0)", nullable: true),
                    Produit = table.Column<decimal>(type: "numeric(18, 0)", nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LigneCommande", x => x.ID);
                    table.ForeignKey(
                        name: "Fk_LigneCommande_Box",
                        column: x => x.Box,
                        principalSchema: "apero",
                        principalTable: "Box",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Fk_LigneCommande_Commande",
                        column: x => x.Commande,
                        principalSchema: "apero",
                        principalTable: "Commande",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Fk_LigneCommande_Produit",
                        column: x => x.Produit,
                        principalSchema: "apero",
                        principalTable: "Produit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commande_Adresse",
                schema: "apero",
                table: "Commande",
                column: "Adresse");

            migrationBuilder.CreateIndex(
                name: "IX_Commande_Utilisateur",
                schema: "apero",
                table: "Commande",
                column: "Utilisateur");

            migrationBuilder.CreateIndex(
                name: "IX_Commentaire_Box",
                schema: "apero",
                table: "Commentaire",
                column: "Box");

            migrationBuilder.CreateIndex(
                name: "IX_Commentaire_Utilisateur",
                schema: "apero",
                table: "Commentaire",
                column: "Utilisateur");

            migrationBuilder.CreateIndex(
                name: "IX_LigneCommande_Box",
                schema: "apero",
                table: "LigneCommande",
                column: "Box");

            migrationBuilder.CreateIndex(
                name: "IX_LigneCommande_Commande",
                schema: "apero",
                table: "LigneCommande",
                column: "Commande");

            migrationBuilder.CreateIndex(
                name: "IX_LigneCommande_Produit",
                schema: "apero",
                table: "LigneCommande",
                column: "Produit");

            migrationBuilder.CreateIndex(
                name: "IX_LigneProduit_Box",
                schema: "apero",
                table: "LigneProduit",
                column: "Box");

            migrationBuilder.CreateIndex(
                name: "IX_LigneProduit_Produit",
                schema: "apero",
                table: "LigneProduit",
                column: "Produit");

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateur_Adresse",
                schema: "apero",
                table: "Utilisateur",
                column: "Adresse");

            migrationBuilder.CreateIndex(
                name: "UNIQUE_Utilisateur_Username",
                schema: "apero",
                table: "Utilisateur",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UtilisateurRole_IdUtilisateur",
                schema: "apero",
                table: "UtilisateurRole",
                column: "IdUtilisateur");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commentaire",
                schema: "apero");

            migrationBuilder.DropTable(
                name: "LigneCommande",
                schema: "apero");

            migrationBuilder.DropTable(
                name: "LigneProduit",
                schema: "apero");

            migrationBuilder.DropTable(
                name: "UtilisateurRole",
                schema: "apero");

            migrationBuilder.DropTable(
                name: "Commande",
                schema: "apero");

            migrationBuilder.DropTable(
                name: "Box",
                schema: "apero");

            migrationBuilder.DropTable(
                name: "Produit",
                schema: "apero");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "apero");

            migrationBuilder.DropTable(
                name: "Utilisateur",
                schema: "apero");

            migrationBuilder.DropTable(
                name: "Adresse",
                schema: "apero");
        }
    }
}
