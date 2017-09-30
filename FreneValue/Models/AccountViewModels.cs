using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using FreneValue.Models;

namespace FreneValue.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Mémoriser ce navigateur ?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        //[Required]
        //[Display(Name = "Courrier électronique")]
        //[EmailAddress]
        //public string Email { get; set; }

        [Required]
        [Display(Name = "Nom Utilisateur")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [Display(Name = "Mémoriser le mot de passe ?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]       
        [Display(Name = "Rôle utilisateur")]
        public string UserRoles { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Code utilisateur")]
        public string UserName { get; set; }

        //[Required]
       //[DataType(DataType.Password)]
        //[Display(Name = "Mot de passe")]
        //public string Password { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe ")]
        [Compare("Password", ErrorMessage = "Le mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }
        
        // Add the new address properties:
        [Display(Name = "Adresse")]
        public string Address { get; set; }
        [Display(Name = "Ville")]
        public string City { get; set; }
        [Display(Name = "Province")]
        public string State { get; set; }

        // Use a sensible display name for views:
        [Display(Name = "Code postal ")]
        public string PostalCode { get; set; }
    }

    //public class RegisterViewModel
    //{
    //    [Required]
    //    [EmailAddress]
    //    [Display(Name = "Email")]
    //    public string Email { get; set; }

    //    [Required]
    //    [StringLength(100, ErrorMessage =
    //        "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "Password")]
    //    public string Password { get; set; }

    //    [DataType(DataType.Password)]
    //    [Display(Name = "Confirm password")]
    //    [Compare("Password", ErrorMessage =
    //        "The password and confirmation password do not match.")]
    //    public string ConfirmPassword { get; set; }

    //    // Add the new address properties:
    //    public string Address { get; set; }
    //    public string City { get; set; }
    //    public string State { get; set; }

    //    // Use a sensible display name for views:
    //    [Display(Name = "Code Postal ")]
    //    public string PostalCode { get; set; }
    //}

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe")]
        [Compare("Password", ErrorMessage = "Le nouveau mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
