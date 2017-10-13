using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace FreneValue.Models
{
    public class prof_util
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Utilisateur")]
        public string id_util { get; set; }
        [Display(Name = "Type utilisateur")]
        public string typ_util { get; set; }
        [Display(Name = "Type propriétaire")]
        public string typ_propr { get; set; }
        [Display(Name = "Nom")]
        public string nom { get; set; }
        [Display(Name = "Prénom")]
        public string pren { get; set; }
        [Display(Name = "Organisation")]
        public int? org { get; set; }
        [Display(Name = "Sous-organisation")]
        public int? ss_org { get; set; }
        [Display(Name = "Tél.")]
        public string telph { get; set; }
        [Display(Name = "Poste")]
        public string extnsn { get; set; }
        [Display(Name = "Tel. Cel.")]
        public string tel_cell { get; set; }
        [Display(Name = "Adresse")]
        public int? id_local { get; set; }
        public string util { get; set; }
        public DateTime dt_cretn { get; set; }
        public DateTime dt_modf { get; set; }

        [Display(Name = "Nom et Prénom")]
        public string NomPrem
        {
            get
            {
                return nom + ", " + pren;
            }
        }
    }
}