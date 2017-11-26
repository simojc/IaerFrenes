using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace FreneValue.Models
{
    public class arbre
    {
        [Key]
        public int? id { get; set; }
        [Display(Name = "N° arbre")]
        public string num_arbre { get; set; }
        [Display(Name = "Propriétaire ")]
        public int id_profil { get; set; }
        [Display(Name = "Localisation")]
        public int id_local { get; set; }
        [Display(Name = "Type emplacement")]
        public string typ_emplcmt { get; set; }
        [Display(Name = "Emplacement")]
        public string emplcmt { get; set; }
        [Display(Name = "Orientation")]
        public string orientatn { get; set; }
        [Display(Name = "Essence")]
        public int ess_id { get; set; }
        [Display(Name = "Latitude")]
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public decimal? lattd { get; set; }
        [Display(Name = "Longitude")]
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public decimal? longtd { get; set; }
        [Display(Name = "Planté le")]
        //[DataType(DataType.Date)]
        public Nullable<System.DateTime> dt_plant { get; set; }
        [Display(Name = "DHP")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal dhp_tot { get; set; }
        [Display(Name = "Nb_troncs")]
        public int nb_tronc { get; set; }
        [Display(Name = "Type lieu")]
        public string type_lieu { get; set; }
        [Display(Name = "Type arbre")]
        public string typ_abr { get; set; }
        [Display(Name = "Type proprio")]
        public string typ_prop { get; set; }
        [Display(Name = "Nom topologique")]
        public string nom_topo { get; set; }
        public string util { get; set; }
        public DateTime dt_cretn { get; set; }
        public DateTime dt_modf { get; set; }

        [Display(Name = "Image")]
        public int? image_id { get; set; }      

        [ForeignKey("id_profil")]
        public virtual prof_util proprio { get; set; }

        [ForeignKey("id_local")]
        public virtual loclsn adresse { get; set; }

        [ForeignKey("ess_id")]
        public virtual essence essence { get; set; }


        //[ForeignKey("image_id")]
        //public virtual Image image { get; set; }

        public ICollection<eval_abr> Evals { get; set; }
        
    }

}