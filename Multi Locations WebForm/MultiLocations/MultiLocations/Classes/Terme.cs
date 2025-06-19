using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiLocations.Classes
{
    public class Terme
    {
        public int Id { get; set; }

        public int NbAnnees { get; set; }
        public int KiloPermis { get; set; }

        public decimal TauxSurprime { get; set; }
    }
}