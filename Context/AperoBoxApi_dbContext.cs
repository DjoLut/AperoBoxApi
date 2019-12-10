using System;
using Microsoft.EntityFrameworkCore;
using AperoBoxApi.Models;

namespace AperoBoxApi.Context
{
    public partial class AperoBoxApi_dbContext : DbContext
    {
        public AperoBoxApi_dbContext()
        {
        }

        public AperoBoxApi_dbContext(DbContextOptions<AperoBoxApi_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Adresse> Adresse { get; set; }
        public virtual DbSet<Box> Box { get; set; }
        public virtual DbSet<Commande> Commande { get; set; }
        public virtual DbSet<Commentaire> Commentaire { get; set; }
        public virtual DbSet<LigneCommande> LigneCommande { get; set; }
        public virtual DbSet<LigneProduit> LigneProduit { get; set; }
        public virtual DbSet<Produit> Produit { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Utilisateur> Utilisateur { get; set; }
        public virtual DbSet<UtilisateurRole> UtilisateurRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:aperoboxapidbserver.database.windows.net,1433;Initial Catalog=AperoBoxApi_db;Persist Security Info=False;User ID=etu32766;Password=aperoBOX123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }*/
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adresse>(entity =>
            {
                entity.ToTable("Adresse", "apero");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CodePostal).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Localite)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Numero).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Pays)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.Rue)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Box>(entity =>
            {
                entity.ToTable("Box", "apero");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.DateCreation).HasColumnType("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Photo)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PrixUnitaireHtva)
                    .HasColumnName("PrixUnitaireHTVA")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Promotion).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.Tva)
                    .HasColumnName("TVA")
                    .HasColumnType("numeric(18, 3)");
            });

            modelBuilder.Entity<Commande>(entity =>
            {
                entity.ToTable("Commande", "apero");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Adresse).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.DateCreation).HasColumnType("date");

                entity.Property(e => e.Promotion).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.Utilisateur).HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.AdresseNavigation)
                    .WithMany(p => p.Commande)
                    .HasForeignKey(d => d.Adresse)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Commande_Adresse");

                entity.HasOne(d => d.UtilisateurNavigation)
                    .WithMany(p => p.Commande)
                    .HasForeignKey(d => d.Utilisateur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Commande_Utilisateur");
            });

            modelBuilder.Entity<Commentaire>(entity =>
            {
                entity.ToTable("Commentaire", "apero");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Box).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.DateCreation).HasColumnType("date");

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.Texte)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Utilisateur).HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.BoxNavigation)
                    .WithMany(p => p.Commentaire)
                    .HasForeignKey(d => d.Box)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Commentaire_Box");

                entity.HasOne(d => d.UtilisateurNavigation)
                    .WithMany(p => p.Commentaire)
                    .HasForeignKey(d => d.Utilisateur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Commentaire_Utilisateur");
            });

            modelBuilder.Entity<LigneCommande>(entity =>
            {
                entity.ToTable("LigneCommande", "apero");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Box).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Commande).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Produit).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Quantite).HasColumnType("numeric(3, 0)");

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion();

                entity.HasOne(d => d.BoxNavigation)
                    .WithMany(p => p.LigneCommande)
                    .HasForeignKey(d => d.Box)
                    .HasConstraintName("Fk_LigneCommande_Box");

                entity.HasOne(d => d.CommandeNavigation)
                    .WithMany(p => p.LigneCommande)
                    .HasForeignKey(d => d.Commande)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_LigneCommande_Commande");

                entity.HasOne(d => d.ProduitNavigation)
                    .WithMany(p => p.LigneCommande)
                    .HasForeignKey(d => d.Produit)
                    .HasConstraintName("Fk_LigneCommande_Produit");
            });

            modelBuilder.Entity<LigneProduit>(entity =>
            {
                entity.ToTable("LigneProduit", "apero");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Box).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Produit).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Quantite).HasColumnType("numeric(3, 0)");

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion();

                entity.HasOne(d => d.BoxNavigation)
                    .WithMany(p => p.LigneProduit)
                    .HasForeignKey(d => d.Box)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_LigneProduit_Box");

                entity.HasOne(d => d.ProduitNavigation)
                    .WithMany(p => p.LigneProduit)
                    .HasForeignKey(d => d.Produit)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_LigneProduit_Produit");
            });

            modelBuilder.Entity<Produit>(entity =>
            {
                entity.ToTable("Produit", "apero");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.DatePeremption).HasColumnType("date");

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrixUnitaireHtva)
                    .HasColumnName("PrixUnitaireHTVA")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.Tva)
                    .HasColumnName("TVA")
                    .HasColumnType("numeric(6, 6)");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Nom);

                entity.ToTable("Role", "apero");

                entity.Property(e => e.Nom)
                    .HasMaxLength(450)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Utilisateur>(entity =>
            {
                entity.ToTable("Utilisateur", "apero");

                entity.HasIndex(e => e.Username)
                    .HasName("UNIQUE_Utilisateur_Username")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Adresse).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.DateNaissance).HasColumnType("date");

                entity.Property(e => e.Gsm)
                    .HasColumnName("GSM")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MotDePasse)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Prenom)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RowVersion)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.Telephone).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.AdresseNavigation)
                    .WithMany(p => p.Utilisateur)
                    .HasForeignKey(d => d.Adresse)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Utilisateur_Adresse");
            });

            modelBuilder.Entity<UtilisateurRole>(entity =>
            {
                entity.HasKey(e => new { e.IdRole, e.IdUtilisateur });

                entity.ToTable("UtilisateurRole", "apero");

                entity.Property(e => e.IdRole)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.IdUtilisateur).HasColumnType("numeric(18, 0)");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.UtilisateurRole)
                    .HasForeignKey(d => d.IdRole);

                entity.HasOne(d => d.IdUtilisateurNavigation)
                    .WithMany(p => p.UtilisateurRole)
                    .HasForeignKey(d => d.IdUtilisateur);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
