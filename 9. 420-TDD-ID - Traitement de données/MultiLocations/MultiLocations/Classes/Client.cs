using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiLocations.Classes
{
    public class Client
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }

        public string nomComplet
        {
            get { return Nom.ToUpper() + ", " + Prenom; }
        }
    }
}