using System;
using System.ComponentModel.DataAnnotations;

namespace FreneValue.Models
{
    public class loclsn
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "N° Civique")]
        public string num_civc { get; set; }
        [Display(Name = "Voie")]
        public string voie { get; set; }
        [Display(Name = "Lot")]
        public string lot { get; set; }
        [Display(Name = "Rue")]
        public string nom_rue { get; set; }
        [Display(Name = "Tronçon rue")]
        public string tronc_rue { get; set; }
        [Display(Name = "Cours d'eau")]
        public string nom_cours_eau { get; set; }
        [Display(Name = "Section Cours d'eau")]
        public string sectn_cours_eau { get; set; }
        [Display(Name = "Emplacement")]
        public string emplcmt { get; set; }
        [Display(Name = "Code Postal")]
        public string code_post { get; set; }
        [Display(Name = "Ville")]
        public string ville { get; set; }
        [Display(Name = "Province")]
        public string prov { get; set; }
        [Display(Name = "Pays")]
        public string pays { get; set; }
        [Display(Name = "Superficie")]
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public decimal? suprfc { get; set; }
        [Display(Name = "Latitude")]
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public decimal? lattd { get; set; }
        [Display(Name = "Longitude")]
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public decimal? longtd { get; set; }
        public string util { get; set; }
        public DateTime dt_cretn { get; set; }
        public DateTime dt_modf { get; set; }

        [Display(Name = "Adresse Complète")]
        public string AdresseComplete
        {
            get
            {
                return num_civc + ", " + nom_rue + ", " + ville;
            }
        }

    }
}