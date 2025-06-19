using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioPortCartier.Models
{
    [PrimaryKey(nameof(Id))]
    public class Loan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy H:mm}")]
        public DateTime BorrowedDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ScheduledReturnDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? ExactReturnDate { get; set; }

        public string MemberId { get; set; }
        public virtual Member Member { get; set; }
        public string DocumentCode { get; set; }
        public virtual Document Document { get; set; }


    }
}
