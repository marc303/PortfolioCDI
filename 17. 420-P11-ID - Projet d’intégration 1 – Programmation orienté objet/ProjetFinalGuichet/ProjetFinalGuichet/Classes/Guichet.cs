using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjetFinalGuichet.Classes
{
    public class Guichet
    {
        public string Nom { get; set; }
        public decimal Solde { get; set; }
        public bool EstFerme { get; set; }
        private List<Utilisateur> utilisateurs { get; set; }
        //public List<Utilisateur> clients { get; set; }

        private SqlDataAdapter da;

        private DataSet dsGuichet = new DataSet();

        private DataRow dr;

        public void fillDataAdapter(SqlConnection conn)
        {
            da = new SqlDataAdapter("spSelectGuichet", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.FillSchema(dsGuichet, SchemaType.Mapped, "Guichet");
            da.Fill(dsGuichet, "Guichet");

            da = new SqlDataAdapter("spSelectUtilisateurs", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.FillSchema(dsGuichet, SchemaType.Mapped, "Utilisateur");
            da.Fill(dsGuichet, "Utilisateur");
        }

        private List<Utilisateur> getUsers(SqlConnection conn)
        {
            List<Utilisateur> usersList = new List<Utilisateur>();

            foreach (DataRow dr in dsGuichet.Tables["Utilisateur"].Rows)
            {
                Utilisateur user = new Utilisateur();

                user.fillDataAdapter(conn);

                string codeClient = dr["CodeClient"].ToString();

                user.CodeClient = codeClient;
                user.Prenom = dr["Prenom"].ToString();
                user.Nom = dr["Nom"].ToString();
                user.Telephone = dr["Telephone"].ToString();
                user.Courriel = dr["Courriel"].ToString();
                user.Type = dr["TypeUtilisateur"].ToString();
                user.EstBloque = (bool)dr["EstBloque"];
                user.comptes = user.getComptesById(codeClient);
                user.factures = user.getFactures(codeClient);

                usersList.Add(user);
            }

            return usersList;
        }

        public Utilisateur getUser(string userId)
        {
            Utilisateur utilisateur = new Utilisateur();

            foreach (Utilisateur user in this.utilisateurs)
            {
                if (user.CodeClient == userId)
                {
                    utilisateur = user;
                }
            }

            return utilisateur;
        }

        public List<Utilisateur> getClients()
        {
            List<Utilisateur> clients = new List<Utilisateur>();

            foreach (Utilisateur user in this.utilisateurs)
            {
                if (user.Type == "Client")
                {
                    clients.Add(user);
                }
            }

            return clients;
        }

        public Guichet createGuichet(SqlConnection conn)
        {
            Guichet guichet = new Guichet();

            dr = dsGuichet.Tables["Guichet"].Rows[0];

            guichet.Nom = dr["Nom"].ToString();
            guichet.Solde = (decimal)dr["Solde"];
            guichet.EstFerme = (bool)dr["EstFerme"];
            guichet.utilisateurs = getUsers(conn);

            return guichet;
        }
    }
}