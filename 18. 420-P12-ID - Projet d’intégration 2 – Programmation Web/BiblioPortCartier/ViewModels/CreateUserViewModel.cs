using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BiblioPortCartier.ViewModels
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "Code utilisateur requis.")]
        [Range(100000, 1000000)]
        [DefaultValue(100000)]
        [Display(Name = "Code Utilisateur")]
        public int UserCode { get; set; }

        [Required(ErrorMessage = "Prénom requis.")]
        [Display(Name = "Prénom")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nom requis.")]
        [Display(Name = "Nom")]
        public string LastName { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "Vous devez sélectionner un sexe.")]
        [Display(Name = "Sexe")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Numéro de téléphone requis.")]
        [Phone]
        [Display(Name = "Numéro de téléphone")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Adresse courriel requise.")]
        [EmailAddress]
        [Display(Name = "Adresse Courriel")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mot de passe requis.")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "Le {0} doit être au minimum {2} et au maximum {1} caractères de long.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "Le mot de passe ne correspond pas.")]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmation du mot de passe requise.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmation mot de passe")]
        public string ConfirmPassword { get; set; }

    }
}
