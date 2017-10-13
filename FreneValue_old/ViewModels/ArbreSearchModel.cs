using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FreneValue.Models;

namespace FreneValue.ViewModels
{
    public class ArbreSearchModel
    {
            [Display(Name = "Numero Arbre")]
            public String num_arbre { get; set; }

            [Display(Name = "Proprio")]
            public Decimal? id_profil { get; set; }

            [Display(Name = "Adresse")]
            public Int32? id_local { get; set; }

            [Display(Name = "Essence")]
            public String ess { get; set; }

            public Int32 Page { get; set; }
            public Int32 PageSize { get; set; }
            public String Sort { get; set; }
            public String SortDir { get; set; }
            public Int32 TotalRecords { get; set; }
            public List<arbre> arbres { get; set; }

        public ArbreSearchModel()
        {
            Page = 1;
            PageSize = 5;
            Sort = "id";
            SortDir = "DESC";
        }
    }

}
