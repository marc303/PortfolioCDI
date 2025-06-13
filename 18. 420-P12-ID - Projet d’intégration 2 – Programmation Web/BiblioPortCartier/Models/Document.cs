using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioPortCartier.Models
{
    [PrimaryKey(nameof(DocumentCode))]
    public class Document
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string DocumentCode { get; set; }
        public string Title { get; set; }
        public string MakerName { get; set; } //author, producer, etc.
        public int PublishYear { get; set; }

        public string Category { get; set; }

        public string Rating { get; set; }
        public string Genre { get; set; }
        public string? ISBN { get; set; }
        [DefaultValue(false)]
        public bool IsBorrowed { get; set; }
        [DefaultValue(false)]
        public bool IsReserved { get; set; }

        public virtual Loan? Loan { get; set; }

        public virtual Reservation? Reservation { get; set; }

        public int FormatYear(DateTime date)
        {
            return date.Year;
        }

    }
}
