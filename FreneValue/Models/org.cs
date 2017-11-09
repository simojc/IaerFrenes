using System;
using System.ComponentModel.DataAnnotations;

namespace FreneValue.Models
{
    public class org
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Niveau")]
        public int? niv { get; set; }
        [Display(Name = "Nom")]
        public string nom { get; set; }
        [Display(Name = "sigle")]
        public string sigle { get; set; }
        [Display(Name = "Organisation mère")]
        public int? parnt { get; set; }
        [Display(Name = "Contact")]
        public string cntct { get; set; }
        [Display(Name = "Adresse")]
        public int adr { get; set; }
        public string util { get; set; }
        public DateTime dt_cretn { get; set; }
        public DateTime dt_modf { get; set; }
    }

}