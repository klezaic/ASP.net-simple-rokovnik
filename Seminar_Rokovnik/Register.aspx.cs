using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LabelRegistracijaUpozorenje.Text = "";
    }

    protected void ButtonRegistracija_Click(object sender, EventArgs e)
    {
        OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["konekcijaNaBazu"].ConnectionString);
        bool uspjeh = false;
        
        try
        {
            connection.Open();
            OleDbCommand command1 = new OleDbCommand("SELECT * FROM users WHERE username = @ime;", connection);
            command1.Parameters.AddWithValue("@ime", TextBoxRegisterKorisnik.Text);
            object obj1 = command1.ExecuteScalar();

            if (obj1 != null)
            {
                LabelRegistracijaUpozorenje.Text = "Već postoji korisnik s imenom: " + TextBoxRegisterKorisnik.Text;
                TextBoxRegisterKorisnik.Text = "";
                TextBoxRegisterLozinka.Text = "";
                uspjeh = false;
            }
            else
            {
                OleDbCommand command2 = new OleDbCommand("INSERT INTO users ([username],[password]) values (@ime,@pass);", connection);
                command2.Parameters.AddWithValue("@ime", TextBoxRegisterKorisnik.Text);
                command2.Parameters.AddWithValue("@pass", TextBoxRegisterLozinka.Text);
                object obj2 = command2.ExecuteScalar();

                OleDbCommand command3 = new OleDbCommand("CREATE TABLE [" + TextBoxRegisterKorisnik.Text
                    + "] ([id] AUTOINCREMENT, [datum] DATE, [pocetak] DATE, [trajanje] DATE, [naslov] VARCHAR(50), [sadrzaj] VARCHAR(250) );", connection);
                command3.ExecuteNonQuery();
                uspjeh = true;
            }
        }
        catch (Exception ex)
        {
            LabelRegistracijaUpozorenje.Text = ex.Message;
        }
        finally
        {
            connection.Close();
            if(uspjeh)
                Response.Redirect("~/Login.aspx");
        }
    }
}