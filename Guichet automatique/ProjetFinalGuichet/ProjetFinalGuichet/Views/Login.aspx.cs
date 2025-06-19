using ProjetFinalGuichet.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Antlr.Runtime.Misc;

namespace ProjetFinalGuichet.Views
{
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        Guichet guichet = new Guichet();

        bool isCodeClientValid = false;
        bool isNIPValid = false;
        bool est_bloque = false;
        string userId;
        string typeUtilisateur;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["tentatives"] == null)
            {
                Session["tentatives"] = 0;
            }

            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

            guichet.fillDataAdapter(conn);
            guichet = guichet.createGuichet(conn);
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            dvMessageCodeClient.Visible = false;
            dvMessageNIP.Visible = false;
            dvTentative.Visible = false;

            try
            {
                conn.Open();

                cmd = new SqlCommand("spAuthentification", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@codeclient", txtCodeClient.Text);
                cmd.Parameters.AddWithValue("@nip", txtNIP.Text);

                SqlParameter codeclient_valid = new SqlParameter("@codeclient_valid", SqlDbType.Bit);
                codeclient_valid.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(codeclient_valid);

                SqlParameter nip_valid = new SqlParameter("@nip_valid", SqlDbType.Bit);
                nip_valid.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(nip_valid);

                SqlParameter id = new SqlParameter("@id", SqlDbType.NVarChar, 15);
                id.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(id);

                SqlParameter type = new SqlParameter("@type", SqlDbType.NVarChar, 20);
                type.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(type);

                SqlParameter estBloque = new SqlParameter("@estBloque", SqlDbType.Bit);
                estBloque.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(estBloque);

                cmd.ExecuteNonQuery();

                userId = id.Value.ToString();
                typeUtilisateur = type.Value.ToString();
                isCodeClientValid = (bool)codeclient_valid.Value;
                isNIPValid = (bool)nip_valid.Value;
                est_bloque = (bool)estBloque.Value;
            }
            catch (Exception ex)
            {
                dvTentative.Visible = true;
                lblTentative.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            if (isCodeClientValid && isNIPValid)
            {
                if (guichet.EstFerme && typeUtilisateur == "Client")
                {
                    dvTentative.Visible = true;
                    lblTentative.Text = "Le guichet est fermé. Veuillez réessayer plus tard";
                }

                else if (est_bloque && typeUtilisateur == "Client")
                {
                    dvTentative.Visible = true;
                    lblTentative.Text = "Votre compte est bloqué. Veuillez contacter votre banque pour le débloquer";
                }

                else if (!est_bloque && typeUtilisateur == "Client")
                {
                    createSessions();
                    Response.Redirect("Comptes.aspx");
                }

                else if (typeUtilisateur == "Administrateur")
                {
                    createSessions();
                    Response.Redirect("Admin.aspx");
                }

            }
            else
            {
                if (!isCodeClientValid)
                {
                    dvMessageCodeClient.Visible = true;
                    lblMessageCodeClient.Text = "Le code client est introuvable. Il a mal été saisi ou il n'existe pas.";
                }

                if (!isNIPValid)
                {
                    dvMessageNIP.Visible = true;
                    lblMessageNIP.Text = "Le NIP ne correspond pas au code client ou il a été mal saisi";
                }
                if (isCodeClientValid && !isNIPValid)
                {
                    Session["tentatives"] = (int)Session["tentatives"] + 1;

                    if ((int)Session["tentatives"] == 3 && !est_bloque)
                    {
                        bloqueUser();
                        dvTentative.Visible = true;
                        lblTentative.Text = "Suite à votre troisième tentative de connexion, votre compte a été bloqué. Veuillez contacter votre banque pour le débloquer";
                        Session["tentatives"] = 0;
                    }
                    else if (est_bloque)
                    {
                        dvTentative.Visible = true;
                        lblTentative.Text = "Le compte correspondant au code client " + userId + " est bloqué. Veuillez contacter votre banque pour le débloquer";
                        Session["tentatives"] = 0;
                    }
                }
            }

        }

        private void createSessions()
        {
            Session["Guichet"] = guichet;
            Session["Utilisateur"] = guichet.getUser(userId);
        }

        private void bloqueUser()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spUpdateUtilisateurBloque", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("CodeClient", txtCodeClient.Text);
                cmd.Parameters.AddWithValue("EstBloque", true);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                dvTentative.Visible = true;
                lblTentative.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}