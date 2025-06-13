using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BiblioPortCartier.ViewModels
{
    public class CreateMemberViewModel : CreateUserViewModel
    {
        //Address object
        [Required(ErrorMessage = "Numéro de porte ou d'immeuble requis.")]
        [Range(0, 10000)]
        [DefaultValue(0)]
        [Display(Name = "Code Civique")]
        public int DoorNumber { get; set; }

        [Display(Name = "Numéro d'appartement")]
        public string? ApartmentNumber { get; set; }

        [Required(ErrorMessage = "Nom de rue requis.")]
        [Display(Name = "Rue")]
        public string StreetName { get; set; }

        [Required(ErrorMessage = "Nom de ville requis.")]
        [Display(Name = "Ville")]
        public string CityName { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "Vous devez sélectionner une province.")]
        public string Province { get; set; }

        [Required(ErrorMessage = "Code postal requis.")]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Code Postal")]
        public string PostalCode { get; set; }

        //Member related property

        [Required(ErrorMessage = "Date de naissance requise.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Date de naissance")]
        public DateTime BirthdayDate { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy H:mm}")]
        [Display(Name = "Date d'inscription")]
        public DateTime RegisterDate { get; set; }

    }
}
