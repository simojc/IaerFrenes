using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FreneValue.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Nom Role")]
        public string Name { get; set; }
    }

    //public class EditUserViewModel
    //{
    //    public string Id { get; set; }

    //    [Required(AllowEmptyStrings = false)]
    //    [Display(Name = "Email")]
    //    [EmailAddress]
    //    public string Email { get; set; }

    //    public IEnumerable<SelectListItem> RolesList { get; set; }
    //}

    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Code utilisateur")]
        public string UserName { get; set; }
        

        // Use a sensible display name for views:
        [Display(Name = "Code postal ")]
        public string PostalCode { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}