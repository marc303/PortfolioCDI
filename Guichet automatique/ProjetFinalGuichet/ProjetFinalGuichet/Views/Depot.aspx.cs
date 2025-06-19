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
    public partial class Depot : System.Web.UI.Page
    {
        SqlConnection conn;

        Utilisateur user;
        public Compte compte;
        public TypeCompte typeCompte;
        TypeTransaction typeTransacation;

        decimal soldeAvant = 0;
        decimal soldeApres = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            getSessions();
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

            if (Page.IsPostBack)
            {
                user.fillDataAdapter(conn);
                user.comptes = user.getComptesById(user.CodeClient);
                compte = user.getCompte(compte.Nom);
                Session["Compte"] = compte;
            }
        }

        protected void btnConfirmer_Click(object sender, EventArgs e)
        {
            dvMessageErreur.Visible = false;
            dvMessageSucces.Visible = false;

            decimal montantDepot = Decimal.Parse(txtMontant.Text);

            if (montantDepot > 0)
            {
                soldeAvant = compte.Solde;
                soldeApres = soldeAvant + montantDepot;

                insertTransaction();
                updateCompte();
                afficherMessageSucces("Le dépôt de " + montantDepot + "$ a bien été ajouté à votre compte " + typeCompte.Nom + ".");
            }

            else
            {
                afficherMessageErreur("Le dépôt a été refusé. Veuillez saisir un montant plus grand que 0$");
            }
        }

        protected void btnAnnuler_Click(object sender, EventArgs e)
        {
            Response.Redirect("Operations.aspx");
        }

        private void getSessions()
        {
            user = Session["Utilisateur"] as Utilisateur;
            compte = Session["Compte"] as Compte;
            typeCompte = Session["TypeCompte"] as TypeCompte;
            typeTransacation = Session["TypeTransaction"] as TypeTransaction;
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

        private void insertTransaction()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spInsertTransaction", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("CompteDe", compte.Nom);
                cmd.Parameters.AddWithValue("CompteVers", null);
                cmd.Parameters.AddWithValue("Type", typeTransacation.Id);
                cmd.Parameters.AddWithValue("NumeroFacture", null);
                cmd.Parameters.AddWithValue("Montant", decimal.Parse(txtMontant.Text));
                string desc = "Dépôt dans compte " + typeCompte.Nom + " de " + txtMontant.Text + "$";
                cmd.Parameters.AddWithValue("Description", desc);
                var date = DateTime.Now;
                cmd.Parameters.AddWithValue("DateTransaction", date);

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

        private void updateCompte()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spUpdateCompte", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("Nom", compte.Nom);
                cmd.Parameters.AddWithValue("Solde", soldeApres);

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