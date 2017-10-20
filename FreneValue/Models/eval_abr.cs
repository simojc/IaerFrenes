using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace FreneValue.Models
{
    public class eval_abr
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Arbre")]
        public int id_arbre { get; set; }
        [Display(Name = "Évaluateur")]
        public int id_evalteur { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "Date Éval.")]
        public DateTime dt_eval { get; set; }
        [Display(Name = "DHP Total")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal dhp_tot { get; set; }
        [Display(Name = "Classe hauteur")]
        public string clas_haut { get; set; }
        [Display(Name = "Interférence")]
        public string intfrce { get; set; }
        [Display(Name = "Raccordement")]
        public string racdmt { get; set; }
        [Display(Name = "Access. Manutention")]
        public string acesbt_manu { get; set; }
        [Display(Name = "Access. Machinerie")]
        public string acesbt_machn { get; set; }
        [Display(Name = "Access. Cammion")]
        public string acesbt_cam { get; set; }
        [Display(Name = "Contrainte transport")]
        public string contrnt_transp { get; set; }
        [Display(Name = "Obst. au sol")]
        public bool obstcl_sol { get; set; }
        [Display(Name = "Construction")]
        public string constrct { get; set; }
        [Display(Name = "Infastr. Urbaine")]
        public string infrstr_urbn { get; set; }
        [Display(Name = "Type Abbatage")]
        public string typ_abatg { get; set; }
        [Display(Name = "Nb. troncs")]
        public int nb_tronc { get; set; }
        [Display(Name = "Présence BM?")]
        public bool branch_maitr { get; set; }
        [Display(Name = "Actions")]
        public string action { get; set; }
        [Display(Name = "Conclusion")]
        public string concl { get; set; }
        public string util { get; set; }
        public DateTime dt_cretn { get; set; }
        public DateTime dt_modf { get; set; }

        [ForeignKey("id_arbre")]
        public virtual arbre Arbre { get; set; }
        [ForeignKey("id_evalteur")]
        public virtual prof_util Evaluateur { get; set; }
    }
}