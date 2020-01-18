namespace Inspinia_MVC5.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class GanttContext : DbContext
    {
        public GanttContext()
            : base("name=GanttContext")
        {
        }

        public virtual DbSet<ACHATS_PREVISIONNELS> ACHATS_PREVISIONNELS { get; set; }
        public virtual DbSet<AffaireCommerciales> AffaireCommerciales { get; set; }
        public virtual DbSet<Categorie> Categorie { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Commande> Commande { get; set; }
        public virtual DbSet<CoordonneesSoc> CoordonneesSoc { get; set; }
        public virtual DbSet<Decaissement> Decaissement { get; set; }
        public virtual DbSet<Devis> Devis { get; set; }
        public virtual DbSet<Devise> Devise { get; set; }
        public virtual DbSet<Direction> Direction { get; set; }
        public virtual DbSet<Documents> Documents { get; set; }
        public virtual DbSet<Emails> Emails { get; set; }
        public virtual DbSet<Encaissement> Encaissement { get; set; }
        public virtual DbSet<Facturation> Facturation { get; set; }
        public virtual DbSet<FeuilleEstimations> FeuilleEstimations { get; set; }
        public virtual DbSet<Fournisseur> Fournisseur { get; set; }
        public virtual DbSet<Historique_Prix_Achat> Historique_Prix_Achat { get; set; }
        public virtual DbSet<Inscription> Inscription { get; set; }
        public virtual DbSet<Ligne_devis> Ligne_devis { get; set; }
        public virtual DbSet<Links> Links { get; set; }
        public virtual DbSet<Marque> Marque { get; set; }
        public virtual DbSet<Modules> Modules { get; set; }
        public virtual DbSet<Parametrages> Parametrages { get; set; }
        public virtual DbSet<PersonnelProjets> PersonnelProjets { get; set; }
        public virtual DbSet<Personnels> Personnels { get; set; }
        public virtual DbSet<Prix_Achat> Prix_Achat { get; set; }
        public virtual DbSet<Projets> Projets { get; set; }
        public virtual DbSet<ProjetTechniques> ProjetTechniques { get; set; }
        public virtual DbSet<Proprietaire> Proprietaire { get; set; }
        public virtual DbSet<Receivers> Receivers { get; set; }
        public virtual DbSet<Ressources> Ressources { get; set; }
        public virtual DbSet<Societes> Societes { get; set; }
        public virtual DbSet<Sous_Categorie> Sous_Categorie { get; set; }
        public virtual DbSet<Stock> Stock { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Taches> Taches { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<Tiers> Tiers { get; set; }
        public virtual DbSet<TVA> TVA { get; set; }
        public virtual DbSet<Unite> Unite { get; set; }
        public virtual DbSet<ACTIVITE> ACTIVITE { get; set; }
        public virtual DbSet<BUDGETS> BUDGETS { get; set; }
        public virtual DbSet<Categorie_Sous_Categorie> Categorie_Sous_Categorie { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ACHATS_PREVISIONNELS>()
                .Property(e => e.MONTANT_HT)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Categorie>()
                .HasOptional(e => e.Categorie11)
                .WithRequired(e => e.Categorie2);

            modelBuilder.Entity<Categorie>()
                .HasMany(e => e.Sous_Categorie)
                .WithOptional(e => e.Categorie)
                .HasForeignKey(e => e.CentreID);

            modelBuilder.Entity<Categorie>()
                .HasMany(e => e.Prix_Achat)
                .WithOptional(e => e.Categorie1)
                .HasForeignKey(e => e.Categorie);

            modelBuilder.Entity<Categorie>()
                .HasMany(e => e.Societes)
                .WithMany(e => e.Categorie)
                .Map(m => m.ToTable("Societe_Centre_Couts").MapLeftKey("CentreID").MapRightKey("SociID"));

            modelBuilder.Entity<Commande>()
                .Property(e => e.Prix_Totale)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Commande>()
                .HasOptional(e => e.Facturation)
                .WithRequired(e => e.Commande);

            modelBuilder.Entity<Decaissement>()
                .HasMany(e => e.Projets1)
                .WithOptional(e => e.Decaissement1)
                .HasForeignKey(e => e.DecaissID);

            modelBuilder.Entity<Devis>()
                .Property(e => e.designation)
                .IsUnicode(false);

            modelBuilder.Entity<Devis>()
                .HasMany(e => e.Ligne_devis)
                .WithOptional(e => e.Devis)
                .HasForeignKey(e => e.id_devis);

            modelBuilder.Entity<Devise>()
                .HasMany(e => e.Prix_Achat)
                .WithOptional(e => e.Devise1)
                .HasForeignKey(e => e.Devise);

            modelBuilder.Entity<Fournisseur>()
                .Property(e => e.TEL)
                .IsFixedLength();

            modelBuilder.Entity<Fournisseur>()
                .Property(e => e.FAX)
                .IsFixedLength();

            modelBuilder.Entity<Fournisseur>()
                .Property(e => e.EMAIL)
                .IsFixedLength();

            modelBuilder.Entity<Fournisseur>()
                .Property(e => e.RC)
                .IsFixedLength();

            modelBuilder.Entity<Fournisseur>()
                .Property(e => e.MF)
                .IsFixedLength();

            modelBuilder.Entity<Fournisseur>()
                .HasMany(e => e.Prix_Achat)
                .WithOptional(e => e.Fournisseur1)
                .HasForeignKey(e => e.Fournisseur);

            modelBuilder.Entity<Marque>()
                .HasMany(e => e.Prix_Achat)
                .WithOptional(e => e.Marque1)
                .HasForeignKey(e => e.Marque);

            modelBuilder.Entity<Personnels>()
                .HasMany(e => e.PersonnelProjets)
                .WithOptional(e => e.Personnels)
                .HasForeignKey(e => e.Personnel_PersonnelId);

            modelBuilder.Entity<Prix_Achat>()
                .HasMany(e => e.Ligne_devis)
                .WithOptional(e => e.Prix_Achat)
                .HasForeignKey(e => e.id_produit);

            modelBuilder.Entity<Projets>()
                .HasMany(e => e.Decaissement)
                .WithOptional(e => e.Projets)
                .HasForeignKey(e => e.PrID);

            modelBuilder.Entity<ProjetTechniques>()
                .HasMany(e => e.PersonnelProjets)
                .WithOptional(e => e.ProjetTechniques)
                .HasForeignKey(e => e.ProjetTechnique_ProjetTechniqueId);

            modelBuilder.Entity<ProjetTechniques>()
                .HasMany(e => e.Tasks)
                .WithOptional(e => e.ProjetTechniques)
                .HasForeignKey(e => e.ProjetTechniquesID);

            modelBuilder.Entity<Proprietaire>()
                .Property(e => e.SocieteNom)
                .IsUnicode(false);

            modelBuilder.Entity<Proprietaire>()
                .Property(e => e.MatriculeFiscale)
                .IsUnicode(false);

            modelBuilder.Entity<Proprietaire>()
                .Property(e => e.RC)
                .IsUnicode(false);

            modelBuilder.Entity<Proprietaire>()
                .Property(e => e.Adresse)
                .IsUnicode(false);

            modelBuilder.Entity<Proprietaire>()
                .Property(e => e.Phone)
                .IsFixedLength();

            modelBuilder.Entity<Proprietaire>()
                .Property(e => e.SiteWeb)
                .IsUnicode(false);

            modelBuilder.Entity<Proprietaire>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Societes>()
                .HasMany(e => e.Devis)
                .WithOptional(e => e.Societes)
                .HasForeignKey(e => e.id_destinataire);

            modelBuilder.Entity<Societes>()
                .HasMany(e => e.Proprietaire)
                .WithOptional(e => e.Societes)
                .HasForeignKey(e => e.socID);

            modelBuilder.Entity<Societes>()
                .HasMany(e => e.Tiers)
                .WithRequired(e => e.Societes)
                .HasForeignKey(e => e.SociID);

            modelBuilder.Entity<Societes>()
                .HasMany(e => e.Tiers1)
                .WithMany(e => e.Societes1)
                .Map(m => m.ToTable("Societe_Tiers").MapLeftKey("SociID").MapRightKey("TiersID"));

            modelBuilder.Entity<Sous_Categorie>()
                .Property(e => e.Libelle)
                .IsFixedLength();

            modelBuilder.Entity<Sous_Categorie>()
                .HasMany(e => e.Categorie1)
                .WithOptional(e => e.Sous_Categorie1)
                .HasForeignKey(e => e.CatID);

            modelBuilder.Entity<Sous_Categorie>()
                .HasMany(e => e.Prix_Achat)
                .WithOptional(e => e.Sous_Categorie1)
                .HasForeignKey(e => e.Sous_Categorie);

            modelBuilder.Entity<Tasks>()
                .HasMany(e => e.Documents)
                .WithRequired(e => e.Tasks)
                .HasForeignKey(e => e.TacheId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tasks>()
                .HasMany(e => e.Links)
                .WithRequired(e => e.Tasks)
                .HasForeignKey(e => e.SourceTaskId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tasks>()
                .HasMany(e => e.Links1)
                .WithRequired(e => e.Tasks1)
                .HasForeignKey(e => e.TargetTaskId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tasks>()
                .HasMany(e => e.Ressources)
                .WithMany(e => e.Tasks)
                .Map(m => m.ToTable("RessourceTaches").MapLeftKey("Tache_TacheId").MapRightKey("Ressource_RessourceId"));

            modelBuilder.Entity<Tiers>()
                .HasMany(e => e.Devis)
                .WithOptional(e => e.Tiers)
                .HasForeignKey(e => e.id_tiers);

            modelBuilder.Entity<Unite>()
                .HasMany(e => e.Prix_Achat)
                .WithOptional(e => e.Unite1)
                .HasForeignKey(e => e.Unite);

            modelBuilder.Entity<BUDGETS>()
                .Property(e => e.MONTANT_HT)
                .HasPrecision(18, 3);
        }
    }
}
