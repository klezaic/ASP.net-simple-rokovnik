using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated == true)
            Response.Redirect("~/Glavna.aspx");
    }

    protected void ButtonPrijava_Click(object sender, EventArgs e)
    {
        OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["konekcijaNaBazu"].ConnectionString);
        try
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand("SELECT * FROM users WHERE username = @ime AND password = @pass", connection);
            command.Parameters.AddWithValue("@ime", TextBoxLoginKorisnik.Text);
            command.Parameters.AddWithValue("@pass", TextBoxLoginLozinka.Text);
            object obj = command.ExecuteScalar();

            if (obj != null)
                FormsAuthentication.RedirectFromLoginPage(TextBoxLoginKorisnik.Text, true);
            else
            {
                TextBoxLoginKorisnik.Text = "";
                TextBoxLoginLozinka.Text = "";
                LabelRegistracijaUpozorenje.Text = "Prijava nije uspješna!";
            }
        }
        catch (Exception ex)
        {
            LabelRegistracijaUpozorenje.Text = ex.Message;
        }
        finally
        {
            connection.Close();
        }
    }    
}