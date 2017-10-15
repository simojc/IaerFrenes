using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace FreneValue.Models
{
    public class souche
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Évaluation")]
        public int id_eval { get; set; }
        [Display(Name = "DHS")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal dhs { get; set; }
        [Display(Name = "Accès nutriment")]
        public string acces_nutrmt { get; set; }
        [Display(Name = "Aération au sol")]
        public string aeration_sol { get; set; }
        [Display(Name = "Surf. déploy racine")]
        public string surf_deplmt_racin { get; set; }
        [Display(Name = "Racine Hors sol")]
        public bool racine_hs { get; set; }
        [Display(Name = "Blessure racine")]
        public string blesre_racine { get; set; }
        [Display(Name = "Cavité hors sol")]
        public bool cavite_hrs_sol { get; set; }
        [Display(Name = "Exigce Essouchmt")]
        public string exig_essouchmt { get; set; }
        [Display(Name = "Type Essouchmt")]
        public string typ_essouchmt { get; set; }
        [Display(Name = "Profondeur. Essouchmt")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal profdeur_essouchmt { get; set; }
        [Display(Name = "Rayon rognage")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal ray_rognage { get; set; }
        [Display(Name = "Défaut")]
        public string defaut { get; set; }
        [Display(Name = "Infrastructure")]
        public string infrstr { get; set; }
        [Display(Name = "Spécificité")]
        public string specificite { get; set; }
        [Display(Name = "Hauteur souche")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal haut_souche { get; set; }
        [Display(Name = "Exigence abattage")]
        public string exig_abat { get; set; }
        [Display(Name = "Espace de subtitution")]
        public bool espace_subs { get; set; }
        [Display(Name = "Fosse implantation")]
        public bool fosse_plant { get; set; }
        [Display(Name = "Commentaire")]

        public string util { get; set; }
        public DateTime dt_cretn { get; set; }
        public DateTime dt_modf { get; set; }


        [ForeignKey("id_eval")]
        public virtual eval_abr Evaluation { get; set; }
    }
}