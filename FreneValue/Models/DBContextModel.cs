using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

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

        public DbSet<ErrorLog> Errors { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<code_dom>()
                      .ToTable("code_dom");
            modelBuilder.Entity<val_dom>()
                  .ToTable("val_dom");
            modelBuilder.Entity<arbre>()
                  .ToTable("arbre");
            modelBuilder.Entity<tronc>()
            .ToTable("tronc");
            modelBuilder.Entity<souche>()
          .ToTable("souche");
            modelBuilder.Entity<cime>()
          .ToTable("cime");
            modelBuilder.Entity<org>()
         .ToTable("org");
            modelBuilder.Entity<loclsn>()
         .ToTable("loclsn");
            modelBuilder.Entity<prof_util>()
       .ToTable("prof_util");
            modelBuilder.Entity<ErrorLog>()
      .ToTable("errorlog");

            //// TABLE code_dom
            //modelBuilder.Entity<code_dom>().Property(r => r.code).HasColumnName("code");
            //modelBuilder.Entity<code_dom>().Property(r => r.descrip).HasColumnName("descrip");
            //modelBuilder.Entity<code_dom>().Property(r => r.dt_cretn).HasColumnName("dt_cretn");
            //modelBuilder.Entity<code_dom>().Property(r => r.dt_modf).HasColumnName("dt_modf");
            //modelBuilder.Entity<code_dom>().Property(r => r.util).HasColumnName("util");
            //modelBuilder.Entity<code_dom>().Property(r => r.actif).HasColumnName("actif");

            // TABLE val_dom
            //modelBuilder.Entity<val_dom>().Property(r => r.ID).HasColumnName("id");
            //modelBuilder.Entity<val_dom>().Property(r => r.code_dom).HasColumnName("code_dom");
            //modelBuilder.Entity<val_dom>().Property(r => r.code_val).HasColumnName("code_val");
            //modelBuilder.Entity<val_dom>().Property(r => r.val).HasColumnName("val");
            //modelBuilder.Entity<val_dom>().Property(r => r.descrip).HasColumnName("descrip");
            //modelBuilder.Entity<val_dom>().Property(r => r.dt_cretn).HasColumnName("dt_cretn");
            //modelBuilder.Entity<val_dom>().Property(r => r.dt_modf).HasColumnName("dt_modf");
            //modelBuilder.Entity<val_dom>().Property(r => r.util).HasColumnName("util");
            //modelBuilder.Entity<val_dom>().Property(r => r.actif).HasColumnName("actif");

            //     Fin Mapping colonne table TOTO pour base non oracle

        }

    }

    //public class code_dom
    //{
    //    [Key]
    //    [Display(Name = "Code")]
    //    [Required]
    //    public string code { get; set; }
    //    [Display(Name = "Description")]
    //    [Required]
    //    public string descrip { get; set; }
    //    public string util { get; set; }
    //    [Display(Name = "Actif?")]
    //    [Required]
    //    public bool actif { get; set; }
    //    public DateTime dt_cretn { get; set; }
    //    public DateTime dt_modf { get; set; }
    //}

    //public class val_dom
    //{
    //    [Key]
    //    public int ID { get; set; }
    //    [Display(Name = "Code Domaine")]
    //    [Required]
    //    public string code_dom { get; set; }
    //    [Display(Name = "Code Valeur")]
    //    [Required]
    //    public string code_val { get; set; }
    //    [Display(Name = "Valeur")]
    //    [Required]
    //    public string val { get; set; }
    //    [Display(Name = "Description")]
    //    public string descrip { get; set; }
    //    [Display(Name = "Actif?")]
    //    [Required]
    //    public bool actif { get; set; }
    //    public string util { get; set; }
    //    public DateTime dt_cretn { get; set; }
    //    public DateTime dt_modf { get; set; }
    //}

    //public class tronc
    //{
    //    [Key]
    //    public int id { get; set; }
    //    [Display(Name = "No arbre")]
    //    public int id_eval { get; set; }
    //    [Display(Name = "No tronc")]
    //    public int no_tronc { get; set; }
    //    [Display(Name = "Tronc parent")]
    //    public int? id_tronc_parnt { get; set; }
    //    [Display(Name = "DHP ")]
    //    [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
    //    public decimal dhp { get; set; }
    //    [Display(Name = "Diam. Moyen")]
    //    public string diam_moy { get; set; }
    //    [Display(Name = "Haut. Moyen")]
    //    public string haut_moy { get; set; } 
    //    [Display(Name = "Morphologie ")]
    //    public string morphlg { get; set; }
    //    [Display(Name = "Racordement ")]
    //    public string racdmt { get; set; }
    //    [Display(Name = "Qualité Valo ")]
    //    public string qual { get; set; }
    //    [Display(Name = "Cavité ")]
    //    public string cavt { get; set; }
    //    [Display(Name = "Fente/fissure ")]
    //    public string fent_fissre { get; set; }
    //    [Display(Name = "Blessure ")]
    //    public string blesr { get; set; }
    //    [Display(Name = "Comtamination ")]
    //    public string contaminatn { get; set; }
    //    [Display(Name = "Symptôme visuel ")]
    //    public string sympt_visuel { get; set; }
    //    [Display(Name = "A une cime? ")]
    //    public string possede_cime { get; set; }
    //    [Display(Name = "Est BM? ")]
    //    public string est_branch_maitr { get; set; }
    //    [Display(Name = "Long. Moyen")]
    //    public string long_moy { get; set; }
    //    [Display(Name = "Catégorie BM")]
    //    public string catgr_branch_maitr { get; set; }
    //    [Display(Name = "Nombre de BM? ")]
    //    public int nb_branch_maitr { get; set; }
    //    [Display(Name = "Commentaire? ")]      
    //    public string util { get; set; }
    //    public DateTime dt_cretn { get; set; }
    //    public DateTime dt_modf { get; set; }
    //    [ForeignKey("id_eval")]
    //    public virtual eval_abr Evaluation { get; set; }
    //}

    //public class cime
    //{
    //    [Key]
    //    public int id { get; set; }

    //    [Display(Name = "Tronc")]
    //    public int id_tronc { get; set; }
    //    [Display(Name = "Racordement")]
    //    public string racdmt { get; set; }
    //    [Display(Name = "Densité branche")]
    //    public string dens_brach { get; set; }
    //    [Display(Name = "densité feuille")]
    //    public string dens_feuille { get; set; }
    //    [Display(Name = "densité rameaux")]
    //    public string dens_rameaux { get; set; }
    //    [Display(Name = "Symptôme visuel")]
    //    public string sympt_visuel { get; set; }
    //    [Display(Name = "Défaut")]
    //    public string defaut { get; set; }
    //    [Display(Name = "Interférence")]
    //    public string intfrce { get; set; }
    //    [Display(Name = "Travaux")]
    //    public string travaux { get; set; }
    //    public string util { get; set; }
    //    public DateTime dt_cretn { get; set; }
    //    public DateTime dt_modf { get; set; }
    //}

    //public class souche
    //{
    //    [Key]
    //    public int id { get; set; }
    //    [Display(Name = "Évaluation")]
    //    public int id_eval { get; set; }
    //    [Display(Name = "DHS")]
    //    [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
    //    public decimal dhs { get; set; }
    //    [Display(Name = "Accès nutriment")]
    //    public string acces_nutrmt { get; set; }
    //    [Display(Name = "Aération au sol")]
    //    public string aeration_sol { get; set; }
    //    [Display(Name = "Surf. déploy racine")]
    //    public string surf_deplmt_racin { get; set; }
    //    [Display(Name = "Racine Hors sol")]
    //    public string racine_hs { get; set; }
    //    [Display(Name = "Blessure racine")]
    //    public string blesre_racine { get; set; }
    //    [Display(Name = "Cavité hors sol")]
    //    public string cavite_hrs_sol { get; set; }
    //    [Display(Name = "Exigce Essouchmt")]
    //    public string exig_essouchmt { get; set; }
    //    [Display(Name = "Type Essouchmt")]
    //    public string typ_essouchmt { get; set; }
    //    [Display(Name = "Profondeur. Essouchmt")]
    //    [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
    //    public decimal profdeur_essouchmt { get; set; }
    //    [Display(Name = "Rayon rognage")]
    //    [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
    //    public decimal ray_rognage { get; set; }
    //    [Display(Name = "Défaut")]
    //    public string defaut { get; set; }
    //    [Display(Name = "Infrastructure")]
    //    public string infrstr { get; set; }
    //    [Display(Name = "Spécificité")]
    //    public string specificite { get; set; }
    //    [Display(Name = "Hauteur souche")]
    //    [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
    //    public decimal haut_souche { get; set; }
    //    [Display(Name = "Exigence abattage")]
    //    public string exig_abat { get; set; }
    //    [Display(Name = "Espace de subtitution")]
    //    public string espace_subs { get; set; }
    //    [Display(Name = "Fosse implantation")]
    //    public string fosse_plant { get; set; }
    //    [Display(Name = "Commentaire")]
       
    //    public string util { get; set; }
    //    public DateTime dt_cretn { get; set; }
    //    public DateTime dt_modf { get; set; }

       
    //    [ForeignKey("id_eval")]
    //    public virtual eval_abr Evaluation { get; set; }
    //}


    //public class loclsn
    //{
    //    [Key]
    //    public int id { get; set; }
    //    [Display(Name = "N° Civique")]
    //    public string num_civc { get; set; }
    //    [Display(Name = "Voie")]
    //    public string voie { get; set; }
    //    [Display(Name = "Lot")]
    //    public string lot { get; set; }
    //    [Display(Name = "Rue")]
    //    public string nom_rue { get; set; }
    //    [Display(Name = "Tronçon rue")]
    //    public string tronc_rue { get; set; }
    //    [Display(Name = "Cours d'eau")]
    //    public string nom_cours_eau { get; set; }
    //    [Display(Name = "Section Cours d'eau")]
    //    public string sectn_cours_eau { get; set; }
    //    [Display(Name = "Emplacement")]
    //    public string emplcmt { get; set; }
    //    [Display(Name = "Code Postal")]
    //    public string code_post { get; set; }
    //    [Display(Name = "Ville")]
    //    public string ville { get; set; }
    //    [Display(Name = "Province")]
    //    public string prov { get; set; }
    //    [Display(Name = "Pays")]
    //    public string pays { get; set; }
    //    [Display(Name = "Superficie")]
    //    [DisplayFormat(DataFormatString = "{0:n0}")]
    //    public decimal? suprfc { get; set; }
    //    [Display(Name = "Latitude")]
    //    [DisplayFormat(DataFormatString = "{0:n0}")]
    //    public decimal? lattd { get; set; }
    //    [Display(Name = "Longitude")]
    //    [DisplayFormat(DataFormatString = "{0:n0}")]
    //    public decimal? longtd { get; set; }
    //    public string util { get; set; }
    //    public DateTime dt_cretn { get; set; }
    //    public DateTime dt_modf { get; set; }

    //    [Display(Name = "Adresse Complète")]
    //    public string AdresseComplete
    //    {
    //        get
    //        {
    //            return num_civc + ", " + nom_rue + ", " + ville ;
    //        }
    //    }
        
    //}


    //public class org
    //{
    //    [Key]
    //    public int id { get; set; }
    //    [Display(Name = "Niveau")]
    //    public int niv { get; set; }
    //    [Display(Name = "Nom")]
    //    public string nom { get; set; }
    //    [Display(Name = "sigle")]
    //    public string sigle { get; set; }
    //    [Display(Name = "Organisation mère")]
    //    public int? parnt { get; set; }
    //    [Display(Name = "Contact")]
    //    public string cntct { get; set; }
    //    [Display(Name = "Adresse")]
    //    public int adr { get; set; }
    //    public string util { get; set; }
    //    public DateTime dt_cretn { get; set; }
    //    public DateTime dt_modf { get; set; }
    //}

    //public class prof_util
    //{
    //    [Key]
    //    public int id { get; set; }
    //    [Display(Name = "Utilisateur")]
    //    public string id_util { get; set; }
    //    [Display(Name = "Type utilisateur")]
    //    public string typ_util { get; set; }
    //    [Display(Name = "Type propriétaire")]
    //    public string typ_propr { get; set; }
    //    [Display(Name = "Nom")]
    //    public string nom { get; set; }
    //    [Display(Name = "Prénom")]
    //    public string pren { get; set; }
    //    [Display(Name = "Organisation")]
    //    public int? org { get; set; }
    //    [Display(Name = "Sous-organisation")]
    //    public int? ss_org { get; set; }
    //    [Display(Name = "Tél.")]
    //    public string telph { get; set; }
    //    [Display(Name = "Poste")]
    //    public string extnsn { get; set; }
    //    [Display(Name = "Tel. Cel.")]
    //    public string tel_cell { get; set; }
    //    [Display(Name = "Adresse")]
    //    public int? id_local { get; set; }
    //    public string util { get; set; }
    //    public DateTime dt_cretn { get; set; }
    //    public DateTime dt_modf { get; set; }

    //    [Display(Name = "Nom et Prénom")]
    //    public string NomPrem
    //    {
    //        get
    //        {
    //            return nom + ", " + pren;
    //        }
    //    }
    //}


    //public class arbre
    //{
    //    [Key]
    //    public int id { get; set; }
    //    [Display(Name = "N° arbre")]
    //    public string num_arbre { get; set; }
    //    [Display(Name = "Propriétaire ")]
    //    public int id_profil { get; set; }
    //    [Display(Name = "Localisation")]
    //    public int id_local { get; set; }
    //    [Display(Name = "Type emplacement")]
    //    public string typ_emplcmt { get; set; }
    //    [Display(Name = "Emplacement")]
    //    public string emplcmt { get; set; }
    //    [Display(Name = "Orientation (pos. rel.)")]
    //    public string orientatn { get; set; }
    //    [Display(Name = "Essence")]
    //    public string ess { get; set; }
    //    [Display(Name = "Latitude")]
    //    [DisplayFormat(DataFormatString = "{0:n0}")]
    //    public decimal lattd { get; set; }
    //    [Display(Name = "Longitude")]
    //    [DisplayFormat(DataFormatString = "{0:n0}")]
    //    public decimal longtd { get; set; }
    //    [Display(Name = "Planté le")]       
    //    //[DataType(DataType.Date)]
    //    public Nullable<System.DateTime> dt_plant { get; set; }
    //    [Display(Name = "DHP")]
    //    [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
    //    public decimal dhp_tot { get; set; }
    //    [Display(Name = "Nb. troncs")]
    //    public int nb_tronc { get; set; }
    //    [Display(Name = "Type lieu")]
    //    public string type_lieu { get; set; }
    //    [Display(Name = "Type arbre")]
    //    public string typ_abr { get; set; }
    //    [Display(Name = "Type proprio")]
    //    public string typ_prop { get; set; }
    //    [Display(Name = "Nom topologique")]
    //    public string nom_topo { get; set; }
    //    public string util { get; set; }
    //    public DateTime dt_cretn { get; set; }
    //    public DateTime dt_modf { get; set; }

    //    [ForeignKey("id_profil")]
    //    public virtual prof_util proprio { get; set; }
    //    [ForeignKey("id_local")]
    //    public virtual loclsn adresse { get; set; }
    //}

    //public class eval_abr
    //{
    //    [Key]
    //    public int id { get; set; }
    //    [Display(Name = "Arbre")]
    //    public int id_arbre { get; set; }
    //    [Display(Name = "Évaluateur")]
    //    public int id_evalteur { get; set; }
    //    [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
    //    [Display(Name = "Date Éval.")]
    //    public DateTime dt_eval { get; set; }
    //    [Display(Name = "DHP Total")]
    //    [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
    //    public decimal dhp_tot { get; set; }
    //    [Display(Name = "Classe hauteur")]
    //    public string clas_haut { get; set; }
    //    [Display(Name = "Interférence")]
    //    public string intfrce { get; set; }
    //    [Display(Name = "Raccordement")]
    //    public string racdmt { get; set; }
    //    [Display(Name = "Accessbté Manutention")]
    //    public string acesbt_manu { get; set; }
    //    [Display(Name = "Accessbté Machinerie")]
    //    public string acesbt_machn { get; set; }
    //    [Display(Name = "Accessbté Cammion")]
    //    public string acesbt_cam { get; set; }
    //    [Display(Name = "Contrainte transport")]
    //    public string contrnt_transp { get; set; }
    //    [Display(Name = "Obstacle au sol")]
    //    public string obstcl_sol { get; set; }
    //    [Display(Name = "Construction")]
    //    public string constrct { get; set; }
    //    [Display(Name = "Infastr. Urbaine")]
    //    public string infrstr_urbn { get; set; }
    //    [Display(Name = "Type Abbatage")]
    //    public string typ_abatg { get; set; }
    //    [Display(Name = "Nb. troncs")]
    //    public int nb_tronc { get; set; }
    //    [Display(Name = "Présence BM?")]
    //    public bool branch_maitr { get; set; }
    //    [Display(Name = "Actions?")]
    //    public string action { get; set; }
    //    [Display(Name = "Conclusion?")]
    //    public string concl { get; set; }
    //    public string util { get; set; }
    //    public DateTime dt_cretn { get; set; }
    //    public DateTime dt_modf { get; set; }

    //    [ForeignKey("id_arbre")]
    //    public virtual arbre Arbre { get; set; }
    //    [ForeignKey("id_evalteur")]
    //    public virtual prof_util Evaluateur  { get; set; }
    //}


}


