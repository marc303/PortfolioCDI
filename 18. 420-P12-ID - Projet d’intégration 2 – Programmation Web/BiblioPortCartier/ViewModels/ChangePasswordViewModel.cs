using System.ComponentModel.DataAnnotations;

namespace BiblioPortCartier.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Adresse courriel requise.")]
        [EmailAddress]
        [Display(Name = "Adresse Courriel")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Nouveau mot de passe requis.")]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "Le {0} doit être au minimum {2} et au maximum {1} caractères de long.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmNewPassword", ErrorMessage = "Le mot de passe ne correspond pas.")]
        [Display(Name = "Nouveau mot de passe")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirmation du nouveau mot de passe requise.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmation nouveau mot de passe")]
        public string ConfirmNewPassword { get; set; }
    }
}
