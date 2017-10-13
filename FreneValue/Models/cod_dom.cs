using System;
using System.ComponentModel.DataAnnotations;

namespace FreneValue.Models
{   
    public class code_dom 
    {
        [Key]
        [Display(Name = "Code")]
        [Required]
        public string code { get; set; }
        [Display(Name = "Description")]
        [Required]
        public string descrip  { get; set; }
        public string util { get; set; }
        [Display(Name = "Actif?")]
        [Required]
        public bool actif { get; set; }
        public DateTime dt_cretn  { get; set; }
        public DateTime dt_modf  { get; set; }
    }
}