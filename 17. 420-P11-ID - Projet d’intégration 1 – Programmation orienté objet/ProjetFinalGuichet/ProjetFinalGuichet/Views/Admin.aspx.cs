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
    public partial class Admin : System.Web.UI.Page
    {
        SqlConnection conn;
        public Guichet guichet;
        Utilisateur client;
        List<Utilisateur> clients;
        List<TypeCompte> list_typeCompte;
        List<Compte> comptes;
        List<Transactions> transactions;

        Compte compte = new Compte();
        Compte compteCredit;

        public string state = "collapse";
        public string state2 = "collapse";
        public string state3 = "collapse";
        public string state4 = "collapse";
        
        int cptCompte = 1;
        int typecompteId;
        decimal soldeApres = 0;
        decimal margeCredit = 0;
        bool creditExist = false;

        const decimal taux_interet_epargne = 0.01M;
        const decimal taux_interet_credit = 0.05M;

        protected void Page_Load(object sender, EventArgs e)
        {
            getSessions();
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

            clients = guichet.getClients();
            compte.fillDataAdapter(conn, string.Empty);
            list_typeCompte = compte.getListTypeCompte();

            if (!Page.IsPostBack)
            {
                txtMontantGuichet.Attributes["max"] = (20000 - guichet.Solde).ToString();
                chkFermerGuichet.Checked = guichet.EstFerme;
                fillClientsDropDown();
                fillCompteDropDown();
                fillComptesHypoDropDown();
            }
        }

        protected void btnCreerClient_Click(object sender, EventArgs e)
        {
            Page.Validate("creerClient");

            dvMessageSucces.Visible = false;
            dvMessageErreur.Visible = false;

            if (!clientExist())
            {
                insertClient();

                compte = new Compte();
                compte.fillDataAdapter(conn, string.Empty);
                compte.typeCompte = compte.getTypeCompte(typecompteId);

                string nom = compte.typeCompte.Abbreviation + "-" + txtCodeClient.Text + "-" + cptCompte;
                compte.Nom = nom;
                compte.CodeClient = txtCodeClient.Text;
                compte.Type = typecompteId;
                compte.Solde = 0;

                insertCompte(compte);

                Reset();
                ResetCreerClient();

                afficherMessageSucces("Le client a bien été ajouté.");
            }
            else
                afficherMessageErreur("Le client existe déjà.");


        }
        protected void btnCreerCompte_Click(object sender, EventArgs e)
        {
            Page.Validate("creerCompte");

            dvMessageSucces.Visible = false;
            dvMessageErreur.Visible = false;

            compte = new Compte();
            compte.fillDataAdapter(conn, string.Empty);
            compte.typeCompte = compte.getTypeCompte(typecompteId);

            string nom = compte.typeCompte.Abbreviation + "-" + client.CodeClient + "-" + cptCompte;

            while (compteExist(nom))
            {
                cptCompte++;
                nom = compte.typeCompte.Abbreviation + "-" + client.CodeClient + "-" + cptCompte;
            }
            compte.Nom = nom;
            compte.CodeClient = client.CodeClient;
            compte.Type = typecompteId;
            switch (typecompteId)
            {
                case 1:
                case 2:
                case 3:
                    compte.Solde = Decimal.Parse(txtSolde.Text);
                    break;
                case 4:
                    compte.Solde = 0;
                    break;
            }

            insertCompte(compte);

            Reset();
            ResetCreerCompte();

            afficherMessageSucces("Le compte a bien été ajouté.");

        }
        protected void btnBlock_Click(object sender, EventArgs e)
        {
            Page.Validate("blockClient");

            dvMessageSucces.Visible = false;
            dvMessageErreur.Visible = false;

            bloqueUser();

            Reset();
            ResetBloquerClient();

            if (client.EstBloque)
            {
                afficherMessageSucces("Le client a bien été débloqué.");
            }
            else
            {
                afficherMessageSucces("Le client a bien été bloqué.");
            }
        }
        protected void btnModGuichet_Click(object sender, EventArgs e)
        {
            Page.Validate("modGuichet");

            dvMessageErreur.Visible = false;
            dvMessageSucces.Visible = false;

            decimal montant = Decimal.Parse(txtMontantGuichet.Text);

            updateGuichet(montant);

            Reset();

            afficherMessageSucces("Le guichet a bien été mis à jour.");
        }
        protected void btnPrelevement_Click(object sender, EventArgs e)
        {
            Page.Validate("prevHypo");

            dvMessageErreur.Visible = false;
            dvMessageSucces.Visible = false;

            decimal montant = Decimal.Parse(txtMontantPrelevement.Text);


            if (compte.Solde - montant < 0)
            {
                creditExist = client.hasCredit();
                if (creditExist)
                {
                    compteCredit = client.getCompteCredit();

                    margeCredit = montant - compte.Solde;

                    insertTransaction(creditExist);
                    updateCompte(creditExist);
                    afficherMessageSucces("Le prélèvement de " + montant + "$ a bien été effectué au compte " + compte.Nom + ". Le surplus de " + margeCredit + "$ a été ajouté à votre marge de crédit.");
                }
                else
                {
                    afficherMessageErreur("Le solde de votre compte est insuffant. La transaction a été refusée parce que vous n'avez pas de compte marge de crédit.");
                }
            }
            else
            {
                soldeApres = compte.Solde - montant;
                insertTransaction(creditExist);
                updateCompte(creditExist);
                afficherMessageSucces("Le prélèvement de " + montant + "$ a bien été effectué au compte " + compte.Nom + ".");
            }

            Reset();
            ResetPrevelementHypo();
        }
        protected void btnPayerInteret_Click(object sender, EventArgs e)
        {
            Page.Validate("payerInteret");

            dvMessageErreur.Visible = false;
            dvMessageSucces.Visible = false;

            Utilisateur user = new Utilisateur();
            comptes = getListComptes(user);

            List<Compte> list_comptesEpargne = getListComptesByType(comptes, 2);

            foreach (Compte comp in list_comptesEpargne)
            {
                soldeApres = comp.Solde + (comp.Solde * taux_interet_epargne);
                compte = comp;

                updateCompte(creditExist);
            }

            Reset();

            afficherMessageSucces("Le paiement d'intérêt aux comptes épargnes a bien été effectué");
        }

        protected void btnAugmenterCredit_Click(object sender, EventArgs e)
        {
            Page.Validate("augmenterCredit");

            dvMessageErreur.Visible = false;
            dvMessageSucces.Visible = false;

            Utilisateur user = new Utilisateur();
            comptes = getListComptes(user);

            List<Compte> list_comptesCredit = getListComptesByType(comptes, 4);

            foreach (Compte comp in list_comptesCredit)
            {
                soldeApres = comp.Solde + (comp.Solde * taux_interet_credit);
                compte = comp;

                updateCompte(creditExist);
            }

            Reset();

            afficherMessageSucces("L'ajout de frais aux comptes marge de crédit a bien été effectué");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("Login.aspx");
        }

        protected void dropdownClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropdownClients.SelectedIndex != 0)
            {
                dropdownClients.Items[0].Enabled = false;
                dropdownTypeCompte.Enabled = true;
                dropdownTypeCompte.Items.Clear();

                string userId = dropdownClients.SelectedValue;

                client = guichet.getUser(userId);

                Session["Client"] = client;

                state = "expand";

                fillTypeCompteDropDown();
            }
        }

        protected void dropdownTypeCompte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropdownTypeCompte.SelectedIndex != 0)
            {
                int typeCompte = int.Parse(dropdownTypeCompte.SelectedValue);

                dropdownTypeCompte.Items[0].Enabled = false;
                switch (typeCompte)
                {
                    case 1:
                    case 2:
                    case 3:
                        txtSolde.Enabled = true;
                        break;
                    default:
                        txtSolde.Enabled = false;
                        break;
                }

                btnCreerCompte.Enabled = true;

                Session["TypeCompte"] = typeCompte;

                state = "expand";
            }
        }
        protected void dropdownComptes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropdownComptes.SelectedIndex != 0)
            {
                string compteId = dropdownComptes.SelectedValue;

                dropdownComptes.Items[0].Enabled = false;

                compte = new Compte();
                compte.fillDataAdapter(conn, compteId);
                transactions = compte.getListTransactions(conn);

                state2 = "expand";

                repeatTransaction.Visible = true;
                repeatTransaction.DataSource = transactions;
                repeatTransaction.DataBind();
            }
        }
        protected void dropdownClients2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropdownClients2.SelectedIndex != 0)
            {
                string userId = dropdownClients2.SelectedValue;
                client = guichet.getUser(userId);
                Session["Client"] = client;

                dropdownClients2.Items[0].Enabled = false;
                btnBlock.Enabled = true;

                if (client.EstBloque)
                {
                    btnBlock.Text = "Débloquer";
                }
                else
                {
                    btnBlock.Text = "Bloquer";
                }

                state3 = "expand";
            }
        }
        protected void dropdownComptesHypo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropdownComptesHypo.SelectedIndex != 0)
            {
                dropdownComptesHypo.Items[0].Enabled = false;
                txtMontantPrelevement.Enabled = true;
                btnPrelevement.Enabled = true;

                string compteId = dropdownComptesHypo.SelectedValue;

                client = new Utilisateur();
                client.fillDataAdapter(conn);
                client.comptes = client.getComptes();

                Compte comp = client.getCompte(compteId);
                client = guichet.getUser(comp.CodeClient);
               
                state4 = "expand";

                Session["Client"] = client;
                Session["ComptePre"] = comp;
            }
        }

        private void getSessions()
        {
            if (Session["Client"] is null)
                Session["Client"] = new Utilisateur();
            else
                client = Session["Client"] as Utilisateur;

            if (Session["TypeCompte"] is null)
                Session["TypeCompte"] = 1;

            typecompteId = (int)Session["TypeCompte"];

            if (Session["ComptePre"] is null)
                Session["ComptePre"] = new Compte();
            else
                compte = Session["ComptePre"] as Compte;

            //user = Session["Utilisateur"] as Utilisateur;
            guichet = Session["Guichet"] as Guichet;
        }

        private List<Compte> getListComptes(Utilisateur user)
        {
            user.fillDataAdapter(conn);

            return user.getComptes();
        }
        private List<Compte> getListComptesByType(List<Compte> comptes, int typeCompte)
        {
            List<Compte> list_comptes = new List<Compte>();

            foreach (Compte comp in comptes)
            {
                if (comp.Type == typeCompte)
                {
                    list_comptes.Add(comp);
                }
            }

            return list_comptes;
        }
        private void fillClientsDropDown()
        {
            dropdownClients.Items.Add(new ListItem("-- Sélection du client --", "0"));
            dropdownClients2.Items.Add(new ListItem("-- Sélection du client --", "0"));
            foreach (Utilisateur client in clients)
            {
                dropdownClients.Items.Add(new ListItem(client.CodeClient + " - " + client.nomComplet, client.CodeClient));
                dropdownClients2.Items.Add(new ListItem(client.CodeClient + " - " + client.nomComplet, client.CodeClient));
            }
        }

        private void fillTypeCompteDropDown()
        {
            if (client.hasCredit())
                list_typeCompte.RemoveAt(3);

            dropdownTypeCompte.Items.Add(new ListItem("-- Sélection du type de compte --", "0"));
            foreach (TypeCompte type_compte in list_typeCompte)
            {
                dropdownTypeCompte.Items.Add(new ListItem(type_compte.Nom, type_compte.Id.ToString()));
            }
        }

        private void fillCompteDropDown()
        {
            Utilisateur user = new Utilisateur();

            comptes = getListComptes(user);

            dropdownComptes.Items.Add(new ListItem("-- Sélection du compte --", "0"));
            foreach (Compte comp in comptes)
            {
                user = guichet.getUser(comp.CodeClient);
                dropdownComptes.Items.Add(new ListItem(user.nomComplet + " - " + comp.Nom, comp.Nom));
            }
        }

        private void fillComptesHypoDropDown()
        {
            Utilisateur user = new Utilisateur();
            comptes = getListComptes(user);

            List<Compte> list_comptesHypo = getListComptesByType(comptes, 3);

            dropdownComptesHypo.Items.Add(new ListItem("-- Sélection du compte hypothécaire --", "0"));
            foreach (Compte comp in list_comptesHypo)
            {
                user = guichet.getUser(comp.CodeClient);
                dropdownComptesHypo.Items.Add(new ListItem(user.nomComplet + " - " + comp.Nom, comp.Nom));
            }
        }

        private bool clientExist()
        {
            foreach (Utilisateur client in clients)
            {
                if (client.CodeClient == txtCodeClient.Text)
                {
                    return true;
                }
            }
            return false;
        }

        private bool compteExist(string name)
        {
            foreach (Compte compte in client.comptes)
            {
                if (compte.Nom == name)
                {
                    return true;
                }
            }

            return false;
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
            guichet = new Guichet();
            guichet.fillDataAdapter(conn);
            guichet = guichet.createGuichet(conn);
            clients = guichet.getClients();
            txtMontantGuichet.Text = "0";
            txtMontantGuichet.Attributes["max"] = (20000 - guichet.Solde).ToString();
            chkFermerGuichet.Checked = guichet.EstFerme;
            Session["Guichet"] = guichet;
        }

        private void ResetCreerClient()
        {
            txtPrenom.Text = txtNom.Text = txtEmail.Text = txtTelephone.Text = txtCodeClient.Text = txtNIP.Text = string.Empty;
            chkBloque.Checked = false;

            dropdownClients.Items.Clear();
            dropdownClients2.Items.Clear();
            fillClientsDropDown();

            dropdownComptes.Items.Clear();
            fillCompteDropDown();
        }

        private void ResetCreerCompte()
        {
            dropdownTypeCompte.Items.Clear();
            dropdownTypeCompte.Enabled = false;
            txtSolde.Text = "0.00";
            txtSolde.Enabled = false;
            btnCreerCompte.Enabled = false;

            dropdownClients.Items.Clear();
            dropdownClients2.Items.Clear();
            fillClientsDropDown();

            dropdownComptes.Items.Clear();
            fillCompteDropDown();

            dropdownComptesHypo.Items.Clear();
            fillComptesHypoDropDown();
        }
        private void ResetBloquerClient()
        {
            dropdownClients.Items.Clear();
            dropdownClients2.Items.Clear();
            btnBlock.Enabled = false;
            btnBlock.Text = "Bloquer";
            fillClientsDropDown();
        }
        private void ResetPrevelementHypo()
        {
            btnPrelevement.Enabled = false;
            txtMontantPrelevement.Enabled = false;
            txtMontantPrelevement.Text = "0.00";
            dropdownComptesHypo.Items.Clear();
            fillComptesHypoDropDown();
        }
        private void insertClient()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spInsertClient", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("CodeClient", txtCodeClient.Text);
                cmd.Parameters.AddWithValue("NIP", txtNIP.Text);
                cmd.Parameters.AddWithValue("Prenom", txtPrenom.Text);
                cmd.Parameters.AddWithValue("Nom", txtNom.Text);
                cmd.Parameters.AddWithValue("Telephone", txtTelephone.Text);
                cmd.Parameters.AddWithValue("Courriel", txtEmail.Text);
                cmd.Parameters.AddWithValue("TypeUtilisateur", "Client");
                cmd.Parameters.AddWithValue("EstBloque", chkBloque.Checked);

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

        private void insertCompte(Compte compte)
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spInsertCompte", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("Nom", compte.Nom);
                cmd.Parameters.AddWithValue("CodeClient", compte.CodeClient);
                cmd.Parameters.AddWithValue("Type", compte.Type);
                cmd.Parameters.AddWithValue("Solde", compte.Solde);

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

        private void bloqueUser()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spUpdateUtilisateurBloque", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("CodeClient", client.CodeClient);
                cmd.Parameters.AddWithValue("EstBloque", !client.EstBloque);

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

        private void updateGuichet(decimal montantAjoute)
        {
            decimal nouveauSolde = guichet.Solde + montantAjoute;

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spUpdateGuichet", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("Nom", guichet.Nom);
                cmd.Parameters.AddWithValue("Solde", nouveauSolde);
                cmd.Parameters.AddWithValue("EstFerme", chkFermerGuichet.Checked);

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

        private void insertTransaction(bool addCredit)
        {

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spInsertTransaction", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("CompteDe", compte.Nom);
                cmd.Parameters.AddWithValue("CompteVers", null);
                cmd.Parameters.AddWithValue("Type", 3);
                cmd.Parameters.AddWithValue("NumeroFacture", null);
                cmd.Parameters.AddWithValue("Montant", Decimal.Parse(txtMontantPrelevement.Text));
                string desc = "Prélèvement du compte " + compte.Nom + " de " + txtMontantPrelevement.Text + "$ par la banque.";
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
                    cmd2.Parameters.AddWithValue("Type", 3);
                    cmd2.Parameters.AddWithValue("NumeroFacture", null);
                    cmd2.Parameters.AddWithValue("Montant", margeCredit);
                    string desc2 = "Ajout compte marge de crédit de " + margeCredit + "$ après prélèvement de la banque.";
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

       
    }


}