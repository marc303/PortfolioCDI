using BiblioPortCartier.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioPortCartier.Models
{
    [PrimaryKey(nameof(Id))]
    public class Address
    {
        public Address()
        {

        }

        public Address(int doorNumber, string? apartmentNumber, string streetName, string cityName, string province, string postalCode)
        {
            DoorNumber = doorNumber;
            ApartmentNumber = apartmentNumber;
            StreetName = streetName;
            CityName = cityName;
            Province = province;
            PostalCode = postalCode;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int DoorNumber { get; set; }
        public string? ApartmentNumber { get; set; }

        public string StreetName { get; set; }

        public string CityName { get; set; }
        public string Province { get; set; }
  
        public string PostalCode { get; set; }
        public string FullAddress { 
            
            get {

                    if (string.IsNullOrEmpty(ApartmentNumber))
                    {
                        return DoorNumber + " " + StreetName + ", " + CityName + 
                                ", " + Province + " " + PostalCode;
                    }
                    else 
                    {
                        return ApartmentNumber + "-" + DoorNumber + " " + StreetName + ", " + CityName +
                                ", " + Province + " " + PostalCode; ;
                    }
        
                }
        
        }

        public string Country { get { return "Canada"; } }

        public virtual Member? Member { get; set; }


        public static Address CreateAddress(CreateMemberViewModel model, string province)
        {
            Address address = new Address();

            address.DoorNumber = model.DoorNumber;
            if (model.ApartmentNumber.IsNullOrEmpty())
                address.ApartmentNumber = null;
            else
                address.ApartmentNumber = model.ApartmentNumber;
            address.StreetName = model.StreetName;
            address.CityName = model.CityName;
            address.Province = province;
            address.PostalCode = model.PostalCode;

            return address;
        }
    }
}
