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
    public partial class RetirerFacture : System.Web.UI.Page
    {
        SqlConnection conn;

        Utilisateur user;

        Facture factureSupp;

        protected void Page_Load(object sender, EventArgs e)
        {
            getSessions();
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

            if (!Page.IsPostBack)
            {
                fillFacturesDropDown();
            }
        }

        protected void btnConfirmer_Click(object sender, EventArgs e)
        {
            dvMessageErreur.Visible = false;
            dvMessageSucces.Visible = false;

            deleteFacture();

            Reset();

            afficherMessageSucces("La facture " + factureSupp.NumeroCompte + " a été supprimé");
        }

        protected void btnAnnuler_Click(object sender, EventArgs e)
        {
            Response.Redirect("Operations.aspx");
        }

        protected void dropdownFactures_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropdownFactures.SelectedIndex != 0)
            {
                dropdownFactures.Items[0].Enabled = false;
                btnConfirmer.Enabled = true;

                string numero_compte = dropdownFactures.SelectedValue;

                factureSupp = user.getFacture(numero_compte);

                Session["FactureSupp"] = factureSupp;
            }
        }

        private void getSessions()
        {
            if (Session["FactureSupp"] is null)
                Session["FactureSupp"] = new Facture();
            else
                factureSupp = Session["FactureSupp"] as Facture;

            user = Session["Utilisateur"] as Utilisateur;
        }
        private void fillFacturesDropDown()
        {
            dropdownFactures.Items.Add(new ListItem("-- Sélection d'une facture à payer --", "0"));
            foreach (Facture fact in user.factures)
            {
                dropdownFactures.Items.Add(new ListItem(fact.Fournisseur + " - " + fact.Nom, fact.NumeroCompte));
            }
        }
        private void afficherMessageSucces(string message)
        {
            dvMessageSucces.Visible = true;
            lblMessageSucces.Text = message;
        }

        private void afficherMessageErreur(string message)
        {
            dvMessageErreur.Visible = true;
            lblMessageErreur.Text = message;
        }

        private void Reset()
        {
            user.factures.Remove(factureSupp);
            Session["Utilisateur"] = user;

            dropdownFactures.Items.Clear();
            fillFacturesDropDown();
            btnConfirmer.Enabled = false;
        }

        private void deleteFacture()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spDeleteFacture", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("NumeroCompte", factureSupp.NumeroCompte);

                cmd.ExecuteNonQuery();
           
            }
            catch (Exception ex)
            {
                afficherMessageErreur(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}