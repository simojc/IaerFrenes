using System.ComponentModel.DataAnnotations;
namespace FreneValue.Models
{
    public class mun_cod_post
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Code postal")]
        public string cod_post { get; set; }
        [Display(Name = "Code municipalité")]
        public string cod_mun { get; set; }
        [Display(Name = "Nom municipalité")]
        public string nom_mun { get; set; }
        [Display(Name = "Code région")]
        public string cod_reg { get; set; }
        [Display(Name = "Nom région")]
        public string nom_reg { get; set; }
    }
}

