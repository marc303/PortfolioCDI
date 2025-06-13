using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioPortCartier.Models
{
    [PrimaryKey(nameof(Id))]
    public class User : IdentityUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override string Id 
        {
            get { return base.Id; }
            set { base.Id = value; } 
        
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName {

            get { return FirstName + " " + LastName; }
        }

        public string Gender { get; set; }
    }
}
