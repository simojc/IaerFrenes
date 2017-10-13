using System;
using System.ComponentModel.DataAnnotations;

namespace FreneValue.Models
{
    public class cime
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Tronc")]
        public int id_tronc { get; set; }
        [Display(Name = "Racordement")]
        public string racdmt { get; set; }
        [Display(Name = "Densité branche")]
        public string dens_brach { get; set; }
        [Display(Name = "densité feuille")]
        public string dens_feuille { get; set; }
        [Display(Name = "densité rameaux")]
        public string dens_rameaux { get; set; }
        [Display(Name = "Symptôme visuel")]
        public string sympt_visuel { get; set; }
        [Display(Name = "Défaut")]
        public string defaut { get; set; }
        [Display(Name = "Interférence")]
        public string intfrce { get; set; }
        [Display(Name = "Travaux")]
        public string travaux { get; set; }
        public string util { get; set; }
        public DateTime dt_cretn { get; set; }
        public DateTime dt_modf { get; set; }
    }
}