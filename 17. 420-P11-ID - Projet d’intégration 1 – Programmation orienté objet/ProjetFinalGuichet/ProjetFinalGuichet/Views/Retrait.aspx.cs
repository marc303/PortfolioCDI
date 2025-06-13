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
    public partial class Retrait : System.Web.UI.Page
    {
        SqlConnection conn;

        Utilisateur user;
        public Compte compte;
        public TypeCompte typeCompte;
        Guichet guichet;
        TypeTransaction typeTransacation;

        decimal soldeApres = 0;
        decimal margeCredit = 0;
        
        bool creditExist = false;
        Compte compteCredit;

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

                guichet = new Guichet();
                guichet.fillDataAdapter(conn);
                guichet = guichet.createGuichet(conn);
                Session["Guichet"] = guichet;
            }
        } 

        protected void btnAnnuler_Click(object sender, EventArgs e)
        {
            Response.Redirect("Operations.aspx");
        }

        protected void btnConfirmer_Click(object sender, EventArgs e)
        {
            dvMessageErreur.Visible = false;
            dvMessageSucces.Visible = false;

            if (guichet.Solde == 0)
            {
                afficherMessageErreur("Le guichet automatique est vide.");
            }
            else if(guichet.Solde - Decimal.Parse(txtMontant.Text) < 0)
            {
                afficherMessageErreur("Le guichet automatique a un solde insuffisant.");
            }

            else if(compte.Solde - Decimal.Parse(txtMontant.Text) < 0)
            {
                creditExist = user.hasCredit();
                if (creditExist)
                {
                    compteCredit = user.getCompteCredit();

                    decimal montantRetrait = Decimal.Parse(txtMontant.Text);
                    margeCredit = montantRetrait - compte.Solde;

                    insertTransaction(creditExist);
                    updateCompte(creditExist);
                    updateGuichet(montantRetrait);
                    afficherMessageSucces("Le retrait de " + montantRetrait + "$ a bien été effectué de votre compte " + typeCompte.Nom + ". Le surplus de " + margeCredit + "$ a été ajouté à votre marge de crédit.");
                }
                else
                {
                    afficherMessageErreur("Le solde de votre compte est insuffant. La transaction a été refusée parce que vous n'avez pas de compte marge de crédit.");
                }
            }
            else
            {
                decimal montantRetrait = Decimal.Parse(txtMontant.Text);

                soldeApres = compte.Solde - montantRetrait;

                insertTransaction(creditExist);
                updateCompte(creditExist);
                updateGuichet(montantRetrait);
                afficherMessageSucces("Le retrait de " + montantRetrait + "$ a bien été effectué de votre compte " + typeCompte.Nom + ".");
            }
        }

        private void getSessions()
        {
            guichet = Session["Guichet"] as Guichet;
            user = Session["Utilisateur"] as Utilisateur;
            compte = Session["Compte"] as Compte;
            typeCompte = Session["TypeCompte"] as TypeCompte;
            typeTransacation = Session["TypeTransaction"] as TypeTransaction;
        }

        //private bool isCreditExist()
        //{
        //    foreach (Compte compte in user.comptes)
        //    {
        //        if (compte.Type == 4)
        //        {
        //            compteCredit = compte;
        //            creditExist = true;
        //        }
        //    }
        //    return creditExist;
        //}

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

        private void insertTransaction(bool addCredit)
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
                string desc = "Retrait du compte " + typeCompte.Nom + " de " + txtMontant.Text + "$";
                cmd.Parameters.AddWithValue("Description", desc);
                var date = DateTime.Now;
                cmd.Parameters.AddWithValue("DateTransaction", date);

                cmd.ExecuteNonQuery();

                if (addCredit)
                {
                    SqlCommand cmd2 = new SqlCommand("spInsertTransaction", conn);
                    cmd2.CommandType = CommandType.StoredProcedure;

                    cmd2.Parameters.AddWithValue("CompteDe", compte.Nom);
                    cmd2.Parameters.AddWithValue("CompteVers", compteCredit.Nom);
                    cmd2.Parameters.AddWithValue("Type", typeTransacation.Id);
                    cmd2.Parameters.AddWithValue("NumeroFacture", null);
                    cmd2.Parameters.AddWithValue("Montant", margeCredit);
                    string desc2 = "Ajout compte marge de crédit de " + margeCredit + "$";
                    cmd2.Parameters.AddWithValue("Description", desc2);
                    var date2 = DateTime.Now;
                    cmd2.Parameters.AddWithValue("DateTransaction", date2);

                    cmd2.ExecuteNonQuery();
                }
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

        private void updateCompte(bool upCredit)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spUpdateCompte", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("Nom", compte.Nom);
                cmd.Parameters.AddWithValue("Solde", soldeApres);

                cmd.ExecuteNonQuery();

                if (upCredit)
                {
                    decimal creditSolde = compteCredit.Solde + margeCredit;

                    SqlCommand cmd2 = new SqlCommand("spUpdateCompte", conn);
                    cmd2.CommandType = CommandType.StoredProcedure;

                    cmd2.Parameters.AddWithValue("Nom", compteCredit.Nom);
                    cmd2.Parameters.AddWithValue("Solde", creditSolde);

                    cmd2.ExecuteNonQuery();
                }
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

        private void updateGuichet(decimal montantRetrait)
        {
            decimal nouveauSolde = guichet.Solde - montantRetrait;

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spUpdateGuichet", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("Nom", guichet.Nom);
                cmd.Parameters.AddWithValue("Solde", nouveauSolde);
                cmd.Parameters.AddWithValue("EstFerme", guichet.EstFerme);

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