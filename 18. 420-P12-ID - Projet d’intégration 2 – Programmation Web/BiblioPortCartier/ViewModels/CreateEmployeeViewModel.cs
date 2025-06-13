using System.ComponentModel.DataAnnotations;

namespace BiblioPortCartier.ViewModels
{
    public class CreateEmployeeViewModel : CreateUserViewModel
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Date d'embauche")]
        public DateTime HiredDate { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "Vous devez sélectionner un type d'employé.")]
        [Display(Name = "Type d'employé")]
        public string Type { get; set; }
    }
}
