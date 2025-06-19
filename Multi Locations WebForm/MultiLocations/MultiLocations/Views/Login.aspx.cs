using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MultiLocations
{
   

    public partial class Login : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet dsLocation = new DataSet();
        DataRow dr;
        User user;

        bool isValid = false;
        int userId;

        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

            da = new SqlDataAdapter("spSelectUsers", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.FillSchema(dsLocation, SchemaType.Mapped, "Employes");
            da.Fill(dsLocation, "Employes");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
           
            try
            {
                conn.Open();

                cmd = new SqlCommand("spAuthentification", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                SqlParameter exist = new SqlParameter("@exist", SqlDbType.Bit);
                exist.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(exist);

                SqlParameter id = new SqlParameter("@id", SqlDbType.Int);
                id.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(id);

                cmd.ExecuteNonQuery();
                userId = (int)id.Value;
                isValid = (bool)exist.Value;
            }
            catch (Exception ex)
            {
                    dvMessage.Visible = true;
                    lblMessage.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            if (isValid)
            {
                createSession();
                Response.Redirect("FormLocation.aspx");
            }
            else
            {
                dvMessage.Visible = true;
                lblMessage.Text = "L'utilisateur est inexistant ou les entrées saisies sont erronées";
            }
        }

        private void createSession()
        {
            Session["User"] = createUser(userId);
        }

        private User createUser(int userId)
        {
            user = new User();

            dr = dsLocation.Tables["Employes"].Rows.Find(userId);

            user.Id = (int)dr["CodeEmploye"];
            user.Prenom = dr["Prenom"].ToString();
            user.Nom = dr["Nom"].ToString();

            return user;
        }
    }
}