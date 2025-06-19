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
    public partial class Transfert : System.Web.UI.Page
    {
        SqlConnection conn;
      
        Utilisateur user;
        public Compte compte;
        public TypeCompte typeCompte;
        TypeTransaction typeTransacation;

        Compte compteDest;
        decimal soldeAvant = 0;
        decimal soldeApres = 0;
        List<Compte> compteList = new List<Compte>();

        protected void Page_Load(object sender, EventArgs e)
        {
            getSessions();
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

            compte.fillDataAdapter(conn, "");

            if (Page.IsPostBack)
            {
                user.fillDataAdapter(conn);
                user.comptes = user.getComptesById(user.CodeClient);
                compte = user.getCompte(compte.Nom);
                Session["Compte"] = compte;
            }
            else
            {
                fillComptesDropDown();
            }
        }

        protected void btnConfirmer_Click(object sender, EventArgs e)
        {
            dvMessageErreur.Visible = false;
            dvMessageSucces.Visible = false;

            compteList.Add(compte);
            compteList.Add(compteDest);
            decimal montantTransfert = Decimal.Parse(txtMontant.Text);

            compte.fillDataAdapter(conn, "");

            if (montantTransfert > 0)
            {
                insertTransaction();

                foreach (Compte comp in compteList)
                {
                    switch (comp.Type)
                    {
                        case 2:
                        case 3:
                            soldeAvant = comp.Solde;
                            soldeApres = soldeAvant + montantTransfert;
                            updateCompte(comp);
                            break;
                        case 1:
                        case 4:
                            soldeAvant = comp.Solde;
                            soldeApres = soldeAvant - montantTransfert;
                            updateCompte(comp);
                            break;
                        default:
                            break;
                    }

                }

                string message;

                if (compteDest.Type == 4)
                    message = "Le transfert du compte " + compte.getTypeCompte(compte.Type).Nom + " vers le compte " + compte.getTypeCompte(compteDest.Type).Nom + " s'est bien effectué. Le montant " + montantTransfert + "$ a été soustrait du compte " + compte.getTypeCompte(compteDest.Type).Nom;
                else
                    message = "Le transfert du compte " + compte.getTypeCompte(compte.Type).Nom + " vers le compte " + compte.getTypeCompte(compteDest.Type).Nom + " s'est bien effectué. Le montant " + montantTransfert + "$ a été ajouté au compte " + compte.getTypeCompte(compteDest.Type).Nom;

                afficherMessageSucces(message);   
            }

            else
            {
                afficherMessageErreur("La transfert a été refusé. Veuillez saisir un montant plus grand que 0$");
            }
        }

        protected void btnAnnuler_Click(object sender, EventArgs e)
        {
            Response.Redirect("Operations.aspx");
        }
        protected void dropdownComptes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropdownComptes.SelectedIndex != 0)
            {
                dropdownComptes.Items[0].Enabled = false;
                btnConfirmer.Enabled = true;

                string compteId = dropdownComptes.SelectedValue;

                compteDest = user.getCompte(compteId);

                Session["CompteDest"] = compteDest;
            }
        }

        private void getSessions()
        {
            if (Session["CompteDest"] is null)
                Session["CompteDest"] = new Compte();
            else
                compteDest = Session["CompteDest"] as Compte;

            user = Session["Utilisateur"] as Utilisateur;
            compte = Session["Compte"] as Compte;
            typeCompte = Session["TypeCompte"] as TypeCompte;
            typeTransacation = Session["TypeTransaction"] as TypeTransaction;
        }

        private void fillComptesDropDown()
        {
            dropdownComptes.Items.Add(new ListItem("-- Sélection du compte destinataire --", "0"));
            foreach (Compte comp in user.comptes)
            {
                if (!(comp.Nom == compte.Nom))
                {
                    dropdownComptes.Items.Add(new ListItem("Compte " + compte.getTypeCompte(comp.Type).Nom + " - " + comp.Nom, comp.Nom));
                }
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
                cmd.Parameters.AddWithValue("CompteVers", compteDest.Nom);
                cmd.Parameters.AddWithValue("Type", typeTransacation.Id);
                cmd.Parameters.AddWithValue("NumeroFacture", null);
                if (compteDest.Type == 4)
                {
                    cmd.Parameters.AddWithValue("Montant", (decimal.Parse(txtMontant.Text) * -1));
                }
                else
                {
                    cmd.Parameters.AddWithValue("Montant", decimal.Parse(txtMontant.Text));

                }
                string desc = "Transfet du compte " + compte.getTypeCompte(compte.Type).Nom + " au compte " + compte.getTypeCompte(compteDest.Type).Nom + " de " + txtMontant.Text + "$";
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

        private void updateCompte(Compte comp)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spUpdateCompte", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("Nom", comp.Nom);
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