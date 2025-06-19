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
    public partial class Operations : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlDataAdapter da;
        DataSet dsGuichet = new DataSet();
        DataRow dr;
        Guichet guichet;

        public Compte compte;
        public TypeCompte typeCompte;
        public Transactions transactions = new Transactions();
        Operation operation;

        protected void Page_Load(object sender, EventArgs e)
        {
            getSessions();
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

            transactions.fillDataAdapter(conn);

            repeatOperation.DataSource = compte.operations;
            repeatOperation.DataBind();
        }
        protected void btnOperation_Command(object sender, CommandEventArgs e)
        {
            int value = Int32.Parse(e.CommandName);
            operation = getOperation(value);
            createSessions();

            switch(value)
            {
                case 1:
                    Response.Redirect("Solde.aspx");
                    break;
                case 2:
                    Response.Redirect("Depot.aspx");
                    break;
                case 3:
                    Response.Redirect("Retrait.aspx");
                    break;
                case 4:
                    Response.Redirect("Transfert.aspx");
                    break;
                case 5:
                    Response.Redirect("Paiement.aspx");
                    break;
                case 6:
                    Response.Redirect("AjoutFacture.aspx");
                    break;
                case 7:
                    Response.Redirect("RetirerFacture.aspx");
                    break;
                default:
                    break;
            }
        }
        

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("Login.aspx");
        }

        protected void btnRetour_Click(object sender, EventArgs e)
        {
            Response.Redirect("Comptes.aspx");
        }
        private void getSessions()
        {
            compte = Session["Compte"] as Compte;
            typeCompte = Session["TypeCompte"] as TypeCompte;
        }

        private void createSessions()
        {
            Session["Guichet"] = guichet;
            Session["Operation"] = operation;
            Session["TypeTransaction"] = transactions.getTypeTransaction(operation.typeTransaction);
        }

        private Operation getOperation(int typeTrans)
        {
            Operation operation = new Operation();

            foreach (Operation op in compte.operations)
            {
                if (op.typeTransaction == typeTrans)
                {
                    operation = op;
                }
            }

            return operation;
        }

        public string getDescription(int typeTrans)
        {
            switch (typeTrans)
            {
                case 1:
                    return "Consulter le compte " + typeCompte.Nom + " pour son solde et sa liste de transactions";
                case 2:
                    return "Déposer de l'argent de votre portefeuille à votre compte " + typeCompte.Nom;
                case 3:
                    return "Retirer de l'argent de votre compte " + typeCompte.Nom + " vers votre portefeuille";
                case 4:
                    return "Transférer de l'argent de votre compte " + typeCompte.Nom + " vers un autre de vos comptes";
                case 5:
                    return "Payer une facture à l'aide de votre compte chèque";
                case 6:
                    return "Ajouter une facture à votre compte chèque";
                case 7:
                    return "Supprimer une facture à votre compte chèque";
                default:
                    return "";
            }
        }

        public string getButtonText(int typeTrans)
        {
            switch(typeTrans)
            {
                case 1:
                    return "Consulter";
                case 2:
                    return "Déposer";
                case 3:
                    return "Retirer";
                case 4:
                    return "Transférer";
                case 5:
                    return "Payer une facture";
                case 6:
                    return "Ajouter une facture";
                case 7:
                    return "Supprimer une facture";
                default:
                    return "";
            }
        }
    }
}