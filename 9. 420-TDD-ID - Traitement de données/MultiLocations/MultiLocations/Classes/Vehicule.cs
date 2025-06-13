using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiLocations.Classes
{
    public class Vehicule
    {
        public string NIV { get; set; }
        public int Annee { get; set; }

        public Modele Modele { get; set; }

        public string nomVehicule
        {
            get { return Annee.ToString() + ", " + Modele.Nom; }
        }
    }
}