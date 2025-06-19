using BiblioPortCartier.Models;
using System.ComponentModel.DataAnnotations;

namespace BiblioPortCartier.ViewModels
{
    public class BiblioViewModel
    {
        public IEnumerable<Reservation>? ReservationsMember { get; set; }

        public IEnumerable<Loan>? LoansMember { get; set; }
        public IEnumerable<Member>? Membres { get; set; }

        public IList<Document>? Documents { get; set; }

        public IList<Loan>? LateDocs { get; set; }
        public Member? Member { get; set; }

        public Document? Document { get; set; }

        [Display(Name = "Nom du membre : ")]
        public string? MemberName { get; set; }

        public string? SortOrder { get; set; }

        [Display(Name = "Rechercher par : ")]
        public string? SelectedValue { get; set; }

        public string? SearchString { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy H:mm}")]
        [Display(Name = "Date d'emprunt : ")]
        public DateTime BorrowedDate { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy H:mm}")]
        [Display(Name = "Date de retour prévue : ")]
        public DateTime ScheduledReturnDate { get; set; }
        
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy H:mm}")]
        [Display(Name = "Date de réservation : ")]
        public DateTime ReservationDate { get; set; }
    }
}
