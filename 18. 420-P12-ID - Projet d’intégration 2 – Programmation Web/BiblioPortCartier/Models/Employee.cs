using System.ComponentModel.DataAnnotations;

namespace BiblioPortCartier.Models
{
    public class Employee : User
    {   
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime HiredDate { get; set; }
    }
}
