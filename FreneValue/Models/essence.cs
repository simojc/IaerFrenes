using System;
using System.ComponentModel.DataAnnotations;

namespace FreneValue.Models
{
    public class essence
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Nom Latin")]
        public string nom_lat { get; set; }
        [Display(Name = "Nom Français")]
        public string nom_fr { get; set; }
        [Display(Name = "Nom Anglais")]
        public string nom_en { get; set; }
        [Display(Name = "Famille")]
        public string famille { get; set; }
        [Display(Name = "Origine")]
        public string origin { get; set; }
        [Display(Name = "Description")]
        public string descrip { get; set; }
        [Display(Name = "Type arbre")]
        public string typ_abr { get; set; }
        [Display(Name = "Croissance")]
        public string croisnce { get; set; }
        [Display(Name = "Hauteur Max")]
        public int haut_max { get; set; }
        [Display(Name = "Diamètre Cime")]
        public int diam_cime { get; set; } 
        [Display(Name = "Type de lumière  ")]
        public string typ_lumiere { get; set; }
        [Display(Name = "Type de sol")]
        public string typ_sol { get; set; }
        [Display(Name = "Couleur automnale")]
        public string coulr_autom { get; set; }
        //dessn_feuil oid,
        //img_feuil oid,
        //dessn_abr oid,
        //img_abr oid,
        [Display(Name = "Valorisation matière ligneuse")]
        public string valo_mat_lignse { get; set; }
        [Display(Name = "Densité bois")]
        public decimal dens_bois { get; set; }
        [Display(Name = "Maladie")]
        public string maladie { get; set; }
        [Display(Name = "Insecte ravageur")]
        public string insecte_ravgeur { get; set; }
        [Display(Name = "Champignon ravageur")]
        public string champgn_ravgeur { get; set; }
        [Display(Name = "PH sol")]
        public decimal ph_sol { get; set; }
        [Display(Name = "Enracinement")]
        public string enracinemt { get; set; }
        public string util { get; set; }
        public DateTime dt_cretn { get; set; }
        public DateTime dt_modf { get; set; }
    }
}