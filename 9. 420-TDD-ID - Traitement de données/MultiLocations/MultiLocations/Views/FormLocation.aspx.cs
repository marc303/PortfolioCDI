using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;  
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MultiLocations.Classes;
using System.Data.SqlTypes;
using Microsoft.Ajax.Utilities;
using System.Globalization;

namespace MultiLocations
{
    public partial class FormLocation : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlDataAdapter da;
        DataSet dsLocation;

        User user;
        Filler filler;
        int termeId;

        protected void Page_Load(object sender, EventArgs e)
        {
            createSessions();
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

            if (this.Page.IsPostBack)
                return;

            Reset();
        }
        private void createUser()
        {
            user = new User();
            user.Id = 2222;
            user.Prenom = "Daniel";
            user.Nom = "Desmarais";
        }
        private void createSessions()
        { 
            user = Session["User"] as User;
            //createUser();

            if (Session["dsLocation"] is null)
            {
                Session["dsLocation"] = new DataSet();
            }
            dsLocation = Session["dsLocation"] as DataSet;

            if (Session["filler"] is null)
            {
                Session["filler"] = new Filler(dsLocation, user);
            }
            filler = Session["filler"] as Filler;
            
        }
        private void fillDataAdapter()
        {
            da = new SqlDataAdapter("spSelectLocations", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.FillSchema(dsLocation, SchemaType.Mapped, "Locations");
            da.Fill(dsLocation, "Locations");

            da = new SqlDataAdapter("spSelectVehicules", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.FillSchema(dsLocation, SchemaType.Mapped, "Vehicules");
            da.Fill(dsLocation, "Vehicules");

            da = new SqlDataAdapter("spSelectModeles", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.FillSchema(dsLocation, SchemaType.Mapped, "Modeles");
            da.Fill(dsLocation, "Modeles");

            da = new SqlDataAdapter("spSelectTermes", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.FillSchema(dsLocation, SchemaType.Mapped, "TermesLocation");
            da.Fill(dsLocation, "TermesLocation");

            da = new SqlDataAdapter("spSelectClients", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.FillSchema(dsLocation, SchemaType.Mapped, "Clients");
            da.Fill(dsLocation, "Clients");
        }
        private void Reset()
        {
            fillDataAdapter();
            //filler.dsLocation = dsLocation;
            filler.fillAll();

            dropdownLocation.Items.Clear();
            dropdownNIV.Items.Clear();
            dropdownClient.Items.Clear();

            fillMainDropDown();
            fillNIVDropDown();
            fillClientDropDown();

            ViewState["nouveau"] = "false";

            clearForm();
        }

        private void clearForm()
        {
            dropdownLocation.SelectedIndex = 0;
            dropdownNIV.SelectedIndex = 0;
            dropdownClient.SelectedIndex = 0;

            dropdownLocation.Items[0].Attributes["disabled"] = "disabled";
            dropdownNIV.Items[0].Attributes["disabled"] = "disabled";
            dropdownClient.Items[0].Attributes["disabled"] = "disabled";
            
            txtValeur.Text = txtKiloDebut.Text = txtKiloFin.Text = iptStartDate.Value =
            iptEndDate.Value = ipt1stPayment.Value = txtPaiement.Text = txtNbPaiement.Text =
            txtSurprime.Text = txtKiloPermis.Text = txtNbAnnees.Text = string.Empty;

            chkNeuf.Checked = false;

            dropdownLocation.Enabled = true;
            txtKiloFin.Enabled = true;
            iptEndDate.Disabled = false;
            btnNouveau.Enabled = true;
            btnEnregistrer.Enabled = false; ;
        }
        private void fillMainDropDown()
        {
            dropdownLocation.Items.Add(new ListItem("-- Sélection d'un véhicule en location --", "0"));
            foreach (Location location in filler.locations)
            {
                dropdownLocation.Items.Add(new ListItem(location.Vehicule.nomVehicule, location.Id.ToString()));
            }
        }

        private void fillNIVDropDown()
        {
            dropdownNIV.Items.Add(new ListItem("-- Sélection du NIV --", "0"));
            foreach (Vehicule vehivule in filler.vehicules)
            {
                dropdownNIV.Items.Add(new ListItem(vehivule.NIV, vehivule.NIV));
            }
        }

        private void fillClientDropDown()
        {
            dropdownClient.Items.Add(new ListItem("-- Sélection d'un client --", "0"));
            foreach (Client client in filler.clients)
            {
                dropdownClient.Items.Add(new ListItem(client.nomComplet, client.Id.ToString()));

            }
        }
        protected void dropdownLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropdownLocation.SelectedIndex != 0)
            {
                btnEnregistrer.Enabled = true;

                int idLocation = int.Parse(dropdownLocation.SelectedValue);

                Location location = filler.locations.Find(x => x.Id == idLocation);

                if (location is null)
                  throw new Exception("Location non trouvée");

                dropdownNIV.SelectedIndex = dropdownNIV.Items.IndexOf(dropdownNIV.Items.FindByValue(location.NIV));
                txtValeur.Text = location.ValeurVehicule.ToString("0.00");
                txtKiloDebut.Text = location.KiloDebut.ToString();
                if (location.KiloFin.HasValue)
                    txtKiloFin.Text = location.KiloFin.ToString();
                else
                    txtKiloFin.Text = string.Empty;
                chkNeuf.Checked = location.Neuf;
                dropdownClient.SelectedIndex = dropdownClient.Items.IndexOf(dropdownClient.Items.FindByValue(location.ClientID.ToString()));

                iptStartDate.Value = location.DateDebut.ToString("yyyy-MM-dd");

                if (location.DateFin.HasValue)
                    iptEndDate.Value = location.DateFin.Value.ToString("yyyy-MM-dd");
                else
                    iptEndDate.Value = string.Empty;

                ipt1stPayment.Value = location.DatePremierPaiment.ToString("yyyy-MM-dd");

                txtPaiement.Text = location.MontantMensuel.ToString("0.00");
                txtNbPaiement.Text = location.NbPaiement.ToString();
                txtSurprime.Text = location.Terme.TauxSurprime.ToString("0.00");
                txtKiloPermis.Text = location.Terme.KiloPermis.ToString();
                txtNbAnnees.Text = location.Terme.NbAnnees.ToString();
            }
            
        }
        protected void btnNouveau_Click(object sender, EventArgs e)
        {
            Reset();

            dropdownLocation.Enabled = false;
            txtKiloFin.Enabled = false;
            iptEndDate.Disabled = true;
            btnNouveau.Enabled = false;
            btnEnregistrer.Enabled = true;

            ViewState["nouveau"] = "true";
        }
        protected void btnAnnuler_Click(object sender, EventArgs e)
        {
            Reset();
        }
        protected void btnEnregistrer_Click(object sender, EventArgs e)
        {
            if (ViewState["nouveau"].ToString() == "true")
            {
                insertTermeLocation();
                insertLocation();
            }
            else 
            { 
                updateTermeLocation();
                updateLocation();
            }
            Reset();
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("Login.aspx");
        }
        private void updateTermeLocation()
        {
            try
            {
                int idLocation = int.Parse(dropdownLocation.SelectedValue);
                Location location = filler.locations.Find(x => x.Id == idLocation);
                termeId = location.Terme.Id;

                conn.Open();

                SqlCommand cmd = new SqlCommand("spUpdateTermeLocation", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("TermeLocationID", termeId);
                cmd.Parameters.AddWithValue("@NbAnnees", int.Parse(txtNbAnnees.Text));
                cmd.Parameters.AddWithValue("@KilometragePermis", int.Parse(txtKiloPermis.Text));
                cmd.Parameters.AddWithValue("@TauxSurprime", decimal.Parse(txtSurprime.Text));

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        private void updateLocation()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spUpdateLocation", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("LocationID", int.Parse(dropdownLocation.SelectedValue));
                cmd.Parameters.AddWithValue("CodeEmploye", user.Id);
                var dateDebut = DateTime.ParseExact(iptStartDate.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                cmd.Parameters.AddWithValue("DateDebut", dateDebut);
                if (iptEndDate.Value.IsNullOrWhiteSpace())
                    cmd.Parameters.AddWithValue("DateFin", DBNull.Value);
                else
                {
                    var dateFin = DateTime.ParseExact(iptEndDate.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    cmd.Parameters.AddWithValue("DateFin", dateFin);
                }
                var datePaiement = DateTime.ParseExact(ipt1stPayment.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                cmd.Parameters.AddWithValue("DatePremierPaiement", datePaiement);
                cmd.Parameters.AddWithValue("MontantPaiementMensuel", decimal.Parse(txtPaiement.Text));
                cmd.Parameters.AddWithValue("NbPaiementMensuel", int.Parse(txtNbPaiement.Text));
                cmd.Parameters.AddWithValue("VehiculeNIV", dropdownNIV.SelectedItem.Value);
                cmd.Parameters.AddWithValue("ValeurVehicule", decimal.Parse(txtValeur.Text));
                cmd.Parameters.AddWithValue("KilometrageDebut", int.Parse(txtKiloDebut.Text));

                if (txtKiloFin.Text.IsNullOrWhiteSpace())
                    cmd.Parameters.AddWithValue("KilometrageFin", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("KilometrageFin", int.Parse(txtKiloFin.Text));
                cmd.Parameters.AddWithValue("Nouveau", chkNeuf.Checked);
                cmd.Parameters.AddWithValue("ClientID", int.Parse(dropdownClient.SelectedItem.Value));
                cmd.Parameters.AddWithValue("TermeLocationID", termeId);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        private void insertTermeLocation()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spInsertTermeLocation", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@NbAnnees", int.Parse(txtNbAnnees.Text));
                cmd.Parameters.AddWithValue("@KilometragePermis", int.Parse(txtKiloPermis.Text));
                cmd.Parameters.AddWithValue("@TauxSurprime", decimal.Parse(txtSurprime.Text));

                SqlParameter id = new SqlParameter("@id", SqlDbType.Int);
                id.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(id);

                cmd.ExecuteNonQuery();
                termeId = (int)id.Value;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        private void insertLocation()
        {
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("spInsertLocation", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("CodeEmploye", user.Id);
                var dateDebut = DateTime.ParseExact(iptStartDate.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                cmd.Parameters.AddWithValue("DateDebut", dateDebut);
                cmd.Parameters.AddWithValue("DateFin", DBNull.Value);
                var datePaiement = DateTime.ParseExact(ipt1stPayment.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                cmd.Parameters.AddWithValue("DatePremierPaiement", datePaiement);
                cmd.Parameters.AddWithValue("MontantPaiementMensuel", decimal.Parse(txtPaiement.Text));
                cmd.Parameters.AddWithValue("NbPaiementMensuel", int.Parse(txtNbPaiement.Text));
                cmd.Parameters.AddWithValue("VehiculeNIV", dropdownNIV.SelectedItem.Value);
                cmd.Parameters.AddWithValue("ValeurVehicule", decimal.Parse(txtValeur.Text));
                cmd.Parameters.AddWithValue("KilometrageDebut", int.Parse(txtKiloDebut.Text));
                cmd.Parameters.AddWithValue("KilometrageFin", DBNull.Value);
                cmd.Parameters.AddWithValue("Nouveau", chkNeuf.Checked);
                cmd.Parameters.AddWithValue("ClientID", int.Parse(dropdownClient.SelectedItem.Value));
                cmd.Parameters.AddWithValue("TermeLocationID", termeId);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

    }
}