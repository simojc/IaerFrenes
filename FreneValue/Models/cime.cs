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
        [RegularExpression(@"^\d+(\.\d)?$", ErrorMessage = "Only one decimal point value allowed")]
        [Range(0.1, 100)]
        [Display(Name = "Densité branche (%)")]
        public decimal dens_brach { get; set; }
        [RegularExpression(@"^\d+(\.\d)?$", ErrorMessage = "Only one decimal point value allowed")]
        [Range(0.1, 100)]
        [Display(Name = "densité feuille(%)")]
        public decimal dens_feuille { get; set; }
        [RegularExpression(@"^\d+(\.\d)?$", ErrorMessage = "Only one decimal point value allowed")]
        [Range(0.1, 100)]
        [Display(Name = "densité rameaux(%)")]
        public decimal dens_rameaux { get; set; }
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