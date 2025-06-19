using ProjetFinalGuichet.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace ProjetFinalGuichet.Views
{
    public partial class Paiement : System.Web.UI.Page
    {
        SqlConnection conn;
    
        Utilisateur user;
        Compte compte;
        TypeTransaction typeTransacation;

        Facture factureDest;
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
            else
            {
                fillFacturesDropDown();
            }
        }


        protected void btnConfirmer_Click(object sender, EventArgs e)
        {
            dvMessageErreur.Visible = false;
            dvMessageSucces.Visible = false;

            decimal montantPaiement = Decimal.Parse(txtMontant.Text);

            if (montantPaiement > 0)
            {
                soldeAvant = compte.Solde;
                soldeApres = soldeAvant - montantPaiement;

                insertTransaction();
                updateCompte();

                afficherMessageSucces("Le paiement de " + txtMontant.Text + "$ a bien été effectué pour la facture " + factureDest.Nom);
            }
            else
            {
                afficherMessageErreur("La paiement a été refusé. Veuillez saisir un montant plus grand que 0$");
            }

        }

        protected void btnAnnuler_Click(object sender, EventArgs e)
        {
            Response.Redirect("Operations.aspx");
        }
        protected void dropdownFactures_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(dropdownFactures.SelectedIndex != 0)
            {
                dropdownFactures.Items[0].Enabled = false;
                btnConfirmer.Enabled = true;

                string numeroFacture = dropdownFactures.SelectedValue;

                factureDest = user.getFacture(numeroFacture);

                Session["FactureDest"] = factureDest;
            }
        }

        private void getSessions()
        {
            if (Session["FactureDest"] is null)
                Session["FactureDest"] = new Facture();
            else
                factureDest = Session["FactureDest"] as Facture;

            user = Session["Utilisateur"] as Utilisateur;
            compte = Session["Compte"] as Compte;
            typeTransacation = Session["TypeTransaction"] as TypeTransaction;
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

        private void insertTransaction()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spInsertTransaction", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("CompteDe", compte.Nom);
                cmd.Parameters.AddWithValue("CompteVers", factureDest.Fournisseur);
                cmd.Parameters.AddWithValue("Type", typeTransacation.Id);
                cmd.Parameters.AddWithValue("NumeroFacture", factureDest.NumeroCompte);
                cmd.Parameters.AddWithValue("Montant", decimal.Parse(txtMontant.Text));
                string desc = "Paiement de la facture " + factureDest.Nom + " pour le fournisseur " + factureDest.Fournisseur + " d'un montant de " + txtMontant.Text + "$";
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