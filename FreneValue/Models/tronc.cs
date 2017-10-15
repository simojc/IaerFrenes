using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FreneValue.Models
{
    public class tronc
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "No arbre")]
        public int id_eval { get; set; }
        [Display(Name = "No tronc")]
        public int no_tronc { get; set; }
        [Display(Name = "Tronc parent")]
        public int? id_tronc_parnt { get; set; }
        [Display(Name = "DHP ")]
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public decimal dhp { get; set; }
        [Display(Name = "Diam. Moyen")]
        public string diam_moy { get; set; }
        [Display(Name = "Haut. Moyen")]
        public string haut_moy { get; set; }
        [Display(Name = "Morphologie ")]
        public string morphlg { get; set; }
        [Display(Name = "Racordement ")]
        public string racdmt { get; set; }
        [Display(Name = "Qualité Valo ")]
        public string qual { get; set; }
        [Display(Name = "Cavité ")]
        public string cavt { get; set; }
        [Display(Name = "Fente/fissure ")]
        public string fent_fissre { get; set; }
        [Display(Name = "Blessure ")]
        public string blesr { get; set; }
        [Display(Name = "Comtamination ")]
        public string contaminatn { get; set; }
        [Display(Name = "Symptôme visuel ")]
        public string sympt_visuel { get; set; }
        [Display(Name = "A une cime? ")]
        public bool possede_cime { get; set; }
        [Display(Name = "Est BM? ")]       
        public bool est_branch_maitr { get; set; }
        [Display(Name = "Long. Moyen")]
        public string long_moy { get; set; }
        [Display(Name = "Catégorie BM")]
        public string catgr_branch_maitr { get; set; }
        [Display(Name = "Nombre de BM ")]
        public int nb_branch_maitr { get; set; }
        [Display(Name = "Commentaire ")]
        public string util { get; set; }
        public DateTime dt_cretn { get; set; }
        public DateTime dt_modf { get; set; }
        [ForeignKey("id_eval")]
        public virtual eval_abr Evaluation { get; set; }
    }
}