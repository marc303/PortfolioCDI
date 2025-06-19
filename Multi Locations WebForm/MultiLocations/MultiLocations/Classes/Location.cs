using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiLocations.Classes
{
    public class Location
    {
        public int Id { get; set; }

        public int CodeEmploye { get; set; }

        public string NIV { get; set; }

        public decimal ValeurVehicule { get; set; }

        public int KiloDebut { get; set; }

        public int? KiloFin { get; set; }

        public bool Neuf { get; set; }

        public Vehicule Vehicule { get; set; }

        public Terme Terme { get; set; }

        public int ClientID { get; set; }

        public DateTime DateDebut { get; set; }

        public DateTime? DateFin { get; set; }

        public DateTime DatePremierPaiment { get; set; }

        public decimal MontantMensuel { get; set; }

        public int NbPaiement { get; set; }

        public string nomVehicule { get => this.Vehicule?.nomVehicule; }
    }
}