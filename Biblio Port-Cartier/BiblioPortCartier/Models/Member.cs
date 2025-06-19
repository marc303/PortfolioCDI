using System.ComponentModel.DataAnnotations;

namespace BiblioPortCartier.Models
{
    public class Member : User
    {
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime BirthdayDate { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy H:mm}")]
        public DateTime RegisterDate { get; set; }

        public virtual IEnumerable<Loan>? Loans { get; set; }

        public virtual IEnumerable<Reservation>? Reservations { get; set; }
    }
}
