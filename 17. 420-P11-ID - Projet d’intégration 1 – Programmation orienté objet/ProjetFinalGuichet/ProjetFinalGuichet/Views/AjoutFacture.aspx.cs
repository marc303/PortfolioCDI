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
    public partial class AjoutFacture : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlDataAdapter da;
        DataSet dsGuichet = new DataSet();
        DataRowCollection data_rows;

        Utilisateur user;
        string numero_compte;

        protected void Page_Load(object sender, EventArgs e)
        {
            getSessions();
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

            da = new SqlDataAdapter("spSelectFactures", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.FillSchema(dsGuichet, SchemaType.Mapped, "Facture");
            da.Fill(dsGuichet, "Facture");

            if (Page.IsPostBack)
            {
                user.fillDataAdapter(conn);
                user.factures = user.getFactures(user.CodeClient);
                Session["Utilisateur"] = user;
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

            numero_compte = getNomNoSpace(txtNom.Text) + "-" + user.CodeClient;

            if (!factureExist(numero_compte))
            {
                insertFacture();
                afficherMessageSucces("La facture " + txtNom.Text + " a bien été ajouté");
            }
            else
            {
                afficherMessageErreur("La facture existe déjà.");
            }
        }

        private void getSessions()
        {
            user = Session["Utilisateur"] as Utilisateur;
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

        private string getNomNoSpace(string nom)
        {
            return nom.Replace(" ", "");
        }

        private bool factureExist(string nom)
        {
            data_rows = dsGuichet.Tables["Facture"].Rows;

            foreach (DataRow dr in data_rows)
            {
                if (dr["NumeroCompte"].ToString() == nom)
                {
                    return true;
                }
            }

            return false;
        }

        private void insertFacture()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spInsertFacture", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("NumeroCompte", numero_compte);
                cmd.Parameters.AddWithValue("CodeClient", user.CodeClient);
                cmd.Parameters.AddWithValue("Nom", txtNom.Text);
                if (string.IsNullOrEmpty(txtFournisseur.Text))
                    cmd.Parameters.AddWithValue("Fournisseur", null);
                else
                    cmd.Parameters.AddWithValue("Fournisseur", txtFournisseur.Text);

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