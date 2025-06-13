using ProjetFinalGuichet.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjetFinalGuichet.Views
{
    public partial class Comptes : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlDataAdapter da;
        DataSet dsGuichet = new DataSet();
        DataRow dr;

        public Utilisateur user;
        public Compte compte = new Compte();

        protected void Page_Load(object sender, EventArgs e)
        {
            getSessions();
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

            user.fillDataAdapter(conn);

            user.comptes = user.getComptesById(user.CodeClient);
            user.factures = user.getFactures(user.CodeClient);
            
            compte.fillDataAdapter(conn, "");

            repeatCompte.DataSource = user.comptes;
            repeatCompte.DataBind();

        }

        protected void btnCompte_Command(object sender, CommandEventArgs e)
        {
            string value = e.CommandName;
            compte = user.getCompte(value);
            compte.fillDataAdapter(conn, "");
            createSessions();
            Response.Redirect("Operations.aspx");
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("Login.aspx");
        }

        private void getSessions()
        {
            user = Session["Utilisateur"] as Utilisateur;
        }

        private void createSessions()
        {
            Session["Compte"] = compte;
            Session["TypeCompte"] = compte.getTypeCompte(compte.Type);

        }

    }
}