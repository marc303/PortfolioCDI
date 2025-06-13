using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjetFinalGuichet.Classes;
using System.Configuration;

namespace ProjetFinalGuichet.Views
{
    public partial class Solde : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlDataAdapter da;
        DataSet dsGuichet = new DataSet();
        DataRow dr;

        public Compte compte;
        public TypeCompte typeCompte;
        public Transactions transaction = new Transactions(); 

        List<Transactions> transactions;

        protected void Page_Load(object sender, EventArgs e)
        {
            getSessions();
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

            compte.fillDataAdapter(conn, compte.Nom);

            transaction.fillDataAdapter(conn);

            transactions = compte.getListTransactions(conn);

            repeatTransaction.DataSource = transactions;
            repeatTransaction.DataBind();
        }

        protected void btnRetour_Click(object sender, EventArgs e)
        {
            Response.Redirect("Operations.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("Login.aspx");
        }

        private void getSessions()
        {
            compte = Session["Compte"] as Compte;
            typeCompte = Session["TypeCompte"] as TypeCompte;
        }
    }
}