using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ProjetFinalGuichet.Classes
{
    public class Utilisateur
    {
        public string CodeClient { get; set; }
        public string NIP { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Telephone { get; set; }
        public string Courriel { get; set; }
        public string Type { get; set; }
        public bool EstBloque { get; set; }
        public List<Compte> comptes { get; set; }
        public List<Facture> factures { get; set; }

        public string nomComplet
        {
            get { return Prenom + " " + Nom; }
        }

        private SqlDataAdapter da;

        private DataSet dsGuichet = new DataSet();

        public void fillDataAdapter(SqlConnection conn)
        {
            da = new SqlDataAdapter("spSelectComptes", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.FillSchema(dsGuichet, SchemaType.Mapped, "Compte");
            da.Fill(dsGuichet, "Compte");

            da = new SqlDataAdapter("spSelectFactures", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.FillSchema(dsGuichet, SchemaType.Mapped, "Facture");
            da.Fill(dsGuichet, "Facture");

            da = new SqlDataAdapter("spSelectOperations", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.FillSchema(dsGuichet, SchemaType.Mapped, "Operation");
            da.Fill(dsGuichet, "Operation");
        }

        public List<Compte> getComptes()
        {
            List<Compte> compteList = new List<Compte>();

            foreach (DataRow dr in dsGuichet.Tables["Compte"].Rows)
            {
                Compte compte = new Compte();

                compte.Nom = dr["Nom"].ToString();
                compte.CodeClient = dr["CodeClient"].ToString();
                compte.Type = (int)dr["Type"];
                compte.Solde = (decimal)dr["Solde"];
                compte.operations = getOperations((int)dr["Type"]);

                compteList.Add(compte);
            }
            return compteList;
        }

        public List<Compte> getComptesById(string userId)
        {
            List<Compte> compteList = new List<Compte>();

            foreach (DataRow dr in dsGuichet.Tables["Compte"].Rows)
            {
                if (dr["CodeClient"].ToString() == userId)
                {
                    Compte compte = new Compte();

                    compte.Nom = dr["Nom"].ToString();
                    compte.CodeClient = dr["CodeClient"].ToString();
                    compte.Type = (int)dr["Type"];
                    compte.Solde = (decimal)dr["Solde"];
                    compte.operations = getOperations((int)dr["Type"]);

                    compteList.Add(compte);
                }
            }
            return compteList;
        }

        private List<Operation> getOperations(int typeCompte)
        {
            List<Operation> operations = new List<Operation>();

            foreach (DataRow dr in dsGuichet.Tables["Operation"].Rows)
            {
                if ((int)dr["TypeCompte"] == typeCompte)
                {
                    Operation operation = new Operation();

                    operation.Id = (int)dr["Id"];
                    operation.typeCompte = (int)dr["TypeCompte"];
                    operation.typeTransaction = (int)dr["TypeTransaction"];

                    operations.Add(operation);
                }
            }

            return operations;
        }

        public List<Facture> getFactures(string userId)
        {
            List<Facture> factures = new List<Facture>();

            foreach (DataRow dr in dsGuichet.Tables["Facture"].Rows)
            {
                if (dr["CodeClient"].ToString() == userId)
                {
                    Facture facture = new Facture();

                    facture.NumeroCompte = dr["NumeroCompte"].ToString();
                    facture.CodeClient = dr["CodeClient"].ToString();
                    facture.Nom = dr["Nom"].ToString();
                    facture.Fournisseur = dr["Fournisseur"].ToString();

                    factures.Add(facture);
                }
            }

            return factures;
        }

        public Compte getCompte(string nom)
        {
            Compte compte = new Compte();

            foreach (Compte comp in this.comptes)
            {
                if (comp.Nom == nom)
                {
                    compte = comp;
                }
            }

            return compte;
        }

        public Facture getFacture(string numero)
        {
            Facture facture = new Facture();

            foreach (Facture fact in this.factures)
            {
                if (fact.NumeroCompte == numero)
                {
                    facture = fact;
                }
            }

            return facture;
        }

        public bool hasCredit()
        {
            foreach (Compte comp in this.comptes)
            {
                if (comp.Type == 4)
                {
                    return true;
                }
            }
            return false;
        }
        public Compte getCompteCredit()
        {
            Compte compte = new Compte();

            foreach (Compte comp in this.comptes)
            {
                if (comp.Type == 4)
                {
                    compte = comp;
                }
            }

            return compte;
        }
    }
}