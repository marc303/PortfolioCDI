using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetFinalGuichet.Classes
{
    public class Facture
    {
        public string NumeroCompte { get; set; }
        public string CodeClient { get; set; }
        public string Nom { get; set; }
        public string Fournisseur { get; set; }
    }
}