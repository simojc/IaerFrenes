using System;
using System.ComponentModel.DataAnnotations;

namespace FreneValue.Models
{
    public class val_dom
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Code Domaine")]
        [Required]
        public string code_dom { get; set; }
        [Display(Name = "Code Valeur")]
        [Required]
        public string code_val { get; set; }
        [Display(Name = "Valeur")]
        [Required]
        public string val { get; set; }
        [Display(Name = "Description")]
        public string descrip { get; set; }
        [Display(Name = "Actif?")]
        [Required]
        public bool actif { get; set; }
        public string util { get; set; }
        public DateTime dt_cretn { get; set; }
        public DateTime dt_modf { get; set; }
    }

}