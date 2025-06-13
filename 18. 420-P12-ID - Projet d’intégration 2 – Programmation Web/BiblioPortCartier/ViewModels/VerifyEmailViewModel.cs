using System.ComponentModel.DataAnnotations;

namespace BiblioPortCartier.ViewModels
{
    public class VerifyEmailViewModel
    {
        [Required(ErrorMessage = "Adresse courriel requise.")]
        [EmailAddress]
        [Display(Name = "Adresse Courriel")]
        public string Email { get; set; }
    }
}
