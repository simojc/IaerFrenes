
using System.Data.Entity;

namespace FreneValue.Models
{

    public class arbredb : DbContext
    {
        // Votre contexte a été configuré pour utiliser une chaîne de connexion « arbredb » du fichier 
        // de configuration de votre application (App.config ou Web.config). Par défaut, cette chaîne de connexion cible 
        // la base de données « FreneValue.Models.arbredb » sur votre instance LocalDb. 
        // 
        // Pour cibler une autre base de données et/ou un autre fournisseur de base de données, modifiez 
        // la chaîne de connexion « arbredb » dans le fichier de configuration de l'application.
        public arbredb()
            : base("name=arbredb")
        {
        }

        // Ajoutez un DbSet pour chaque type d'entité à inclure dans votre modèle. Pour plus d'informations 
        // sur la configuration et l'utilisation du modèle Code First, consultez http://go.microsoft.com/fwlink/?LinkId=390109.

        

        public DbSet<val_dom> valeurs { get; set; }
        public DbSet<code_dom> domaines { get; set; }
        public DbSet<arbre> arbres { get; set; }
        public DbSet<tronc> troncs { get; set; }
        public DbSet<cime> cimes { get; set; }
        public DbSet<souche> souches { get; set; }
        public DbSet<prof_util> prof_utils { get; set; }
        public DbSet<loclsn> localisations { get; set; }
        public DbSet<org> organisations { get; set; }
        public DbSet<eval_abr> evaluations { get; set; }

        public DbSet<Image> images { get; set; }

        public DbSet<arbre_image> arbre_image { get; set; }

        public DbSet<ess_image> ess_image { get; set; }

        public DbSet<ErrorLog> Errors { get; set; }

        public DbSet<mun_cod_post> codepostal { get; set; }

        public DbSet<essence> essence { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<code_dom>().ToTable("code_dom");
            modelBuilder.Entity<val_dom>().ToTable("val_dom");
            modelBuilder.Entity<arbre>().ToTable("arbre");
            modelBuilder.Entity<tronc>().ToTable("tronc");
            modelBuilder.Entity<souche>().ToTable("souche");
            modelBuilder.Entity<cime>().ToTable("cime");
            modelBuilder.Entity<org>().ToTable("org");
            modelBuilder.Entity<loclsn>().ToTable("loclsn");
            modelBuilder.Entity<prof_util>().ToTable("prof_util");
            modelBuilder.Entity<ErrorLog>().ToTable("errorlog");
            modelBuilder.Entity<Image>().ToTable("image");
            modelBuilder.Entity<essence>().ToTable("ess");

        }

    }

}


