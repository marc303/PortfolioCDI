using System.ComponentModel.DataAnnotations;

namespace BiblioPortCartier.ViewModels
{
    public class LogInViewModel
    {
        [Required(ErrorMessage = "Adresse courriel requise.")]
        [EmailAddress]
        [Display(Name = "Adresse courriel")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mot de passe requis.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [Display(Name = "Se souvenir de moi?")]
        public bool RememberMe { get; set; }
    }
}
