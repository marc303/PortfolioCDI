using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Ajax.Utilities;

namespace MultiLocations.Classes
{
    public class Filler
    {
        public List<Location> locations;
        public List<Modele> modeles;
        public List<Vehicule> vehicules;
        public List<Terme> termes;
        public List<Client> clients;

        public DataSet dsLocation { get; set; }
        private User user { get; set; }

        public Filler()
        {

        }
        public Filler(DataSet ds, User _user)
        {
            this.dsLocation = ds;
            this.user = _user;
        }

        public void fillAll()
        {
            fillModelesList();
            fillTermesList();
            fillCientsList();
            fillVehiculesList();
            fillLocationsList();
        }

        private void fillModelesList()
        {
            this.modeles = new List<Modele>();

            foreach (DataRow dr in this.dsLocation.Tables["Modeles"].Rows)
            {
                Modele mod = new Modele();

                mod.Id = (int)dr["ModeleID"];
                mod.Nom = dr["Nom"].ToString();

                this.modeles.Add(mod);
            }
        }

        public void fillTermesList()
        {
            this.termes = new List<Terme>();

            foreach (DataRow dr in this.dsLocation.Tables["TermesLocation"].Rows)
            {
                Terme terme = new Terme();

                terme.Id = (int)dr["TermeLocationID"];
                terme.NbAnnees = (int)dr["NbAnnees"];
                terme.KiloPermis = (int)dr["KilometragePermis"];
                terme.TauxSurprime = (decimal)dr["TauxSurprime"];
                
                this.termes.Add(terme);
            }
        }

        private void fillCientsList()
        {
            this.clients = new List<Client>();

            foreach (DataRow dr in this.dsLocation.Tables["Clients"].Rows)
            {
                Client client = new Client();

                client.Id = (int)dr["ClientID"];
                client.Nom = dr["Nom"].ToString();
                client.Prenom = dr["Prenom"].ToString();

                this.clients.Add(client);
            }
        }

        private void fillVehiculesList()
        {
            this.vehicules = new List<Vehicule>();

            foreach (DataRow dr in this.dsLocation.Tables["Vehicules"].Rows)
            {
                Vehicule vehicule = new Vehicule();

                vehicule.NIV = dr["VehiculeNIV"].ToString();
                vehicule.Annee = (int)dr["Annee"];
                vehicule.Modele = modeles.Find(x => x.Id == (int)dr["ModeleID"]);

                if (vehicule.Modele is null)
                    throw new Exception("Modèle inexistant");

                this.vehicules.Add(vehicule);
            }

        }
        public void fillLocationsList()
        {
            this.locations = new List<Location>();

            foreach (DataRow dr in this.dsLocation.Tables["Locations"].Rows)
            {

                if ((int)dr["CodeEmploye"] != this.user.Id)
                    continue;

                Location loc = new Location();

                loc.Id = (int)dr["LocationID"];
                loc.CodeEmploye = user.Id;
                loc.NIV = dr["VehiculeNIV"].ToString();
                loc.ValeurVehicule = (decimal)dr["ValeurVehicule"];
                loc.KiloDebut = (int)dr["KilometrageDebut"];
                loc.Neuf = (bool)dr["Nouveau"];
                loc.ClientID = (int)dr["ClientID"];
                loc.DateDebut = (DateTime)dr["DateDebut"];
                loc.DatePremierPaiment = (DateTime)dr["DatePremierPaiement"];
                loc.MontantMensuel = (decimal)dr["MontantPaiementMensuel"];
                loc.NbPaiement = (int)dr["NbPaiementMensuel"];

                if (dr["DateFin"] != DBNull.Value)
                    loc.DateFin = (DateTime)dr["DateFin"];

                if (dr["KilometrageFin"] != DBNull.Value)
                    loc.KiloFin = (int)dr["KilometrageFin"];



                loc.Vehicule = vehicules.Find(x => x.NIV == dr["VehiculeNIV"].ToString());

                if (loc.Vehicule is null)
                    throw new Exception("Véhicule non trouvable");

                loc.Terme = termes.Find(x => x.Id == (int)dr["TermeLocationID"]);

                if (loc.Terme is null)
                    throw new Exception("Termes de location inexistants");


                this.locations.Add(loc);

            }
        }
    }
}