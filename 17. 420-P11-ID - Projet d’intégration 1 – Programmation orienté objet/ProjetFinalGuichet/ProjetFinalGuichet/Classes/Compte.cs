using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using ProjetFinalGuichet.Views;

namespace ProjetFinalGuichet.Classes
{
    public class Compte
    {
        public string Nom { get; set; }
        public string CodeClient { get; set; }
        public int Type { get; set; }
        public decimal Solde { get; set; }
        public List<Operation> operations { get; set; }
        public List<Transactions> transactions { get; set; }
        public TypeCompte typeCompte { get; set; }


        private SqlDataAdapter da;

        private DataSet dsGuichet = new DataSet();

        private DataRow dr;

        public void fillDataAdapter(SqlConnection conn, string nom)
        {
            da = new SqlDataAdapter("spSelectTypeCompte", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.FillSchema(dsGuichet, SchemaType.Mapped, "TypeCompte");
            da.Fill(dsGuichet, "TypeCompte");

            da = new SqlDataAdapter("spSelectTransactions", conn);
            da.SelectCommand.Parameters.AddWithValue("Compte", nom);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.FillSchema(dsGuichet, SchemaType.Mapped, "Transactions");
            da.Fill(dsGuichet, "Transactions");
        }

        public TypeCompte getTypeCompte(int typeId)
        {
            TypeCompte typeCompte = new TypeCompte();

            dr = dsGuichet.Tables["TypeCompte"].Rows.Find(typeId);

            typeCompte.Id = (int)dr["Id"];
            typeCompte.Nom = dr["Nom"].ToString();
            typeCompte.Abbreviation = dr["Abbreviation"].ToString();

            return typeCompte;
        }

        public List<TypeCompte> getListTypeCompte()
        {
            List<TypeCompte> listTypeCompte = new List<TypeCompte>();

            foreach (DataRow _dr in dsGuichet.Tables["TypeCompte"].Rows)
            {
                TypeCompte typeCompte = new TypeCompte();

                typeCompte.Id= (int)_dr["Id"];
                typeCompte.Nom = _dr["Nom"].ToString();
                typeCompte.Abbreviation = _dr["Abbreviation"].ToString();

                listTypeCompte.Add(typeCompte);
            }

            return listTypeCompte;
        }

        public List<Transactions> getListTransactions(SqlConnection conn)
        {
            List<Transactions> transactions = new List<Transactions>();

            foreach (DataRow _dr in dsGuichet.Tables["Transactions"].Rows)
            {
                Transactions trans = new Transactions();

                trans.Id = (int)_dr["Id"];
                trans.CompteDe = _dr["CompteDe"].ToString();

                if (string.IsNullOrEmpty(_dr["CompteVers"].ToString()))
                    trans.CompteVers = "Aucun";
                else
                    trans.CompteVers = _dr["CompteVers"].ToString();

                trans.Type = (int)_dr["Type"];

                if (string.IsNullOrEmpty(_dr["NumeroFacture"].ToString()))
                    trans.NumeroFacture = "Aucun";
                else
                    trans.NumeroFacture = _dr["NumeroFacture"].ToString();

                trans.Montant = (decimal)_dr["Montant"];
                trans.Description = _dr["Description"].ToString();
                trans.DateTransaction = (DateTime)_dr["DateTransaction"];
                trans.fillDataAdapter(conn);
                trans.typeTransaction = trans.getTypeTransaction((int)_dr["Type"]);

                transactions.Add(trans);
            }

            return transactions;
        }
    }
}