using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OleDb;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        #region Provjera parametra i unos podataka postojece obveze
        if (Request.QueryString["id"] == null)
        {
            Literal naslov = new Literal();
            naslov.Text = "<h1>Dodavanje nove obveze</h1><br /><br />";
            PlaceHolderNovaNaslov.Controls.Add(naslov);
        }
        else
        {
            Literal naslov = new Literal();
            naslov.Text = "<h1>Uređivanje obveze</h1><br /><br />";
            PlaceHolderNovaNaslov.Controls.Add(naslov);
        }

        if (LabelNoviKontrola.Text != "true")
            return;

        int id = 0;
        if (Request.QueryString["id"] != null)
            if (!Int32.TryParse(Request.QueryString["id"], out id))
            {
                LabelNoviUpozorenje.Visible = true;
                LabelNoviUpozorenje.Text = "Dogodila se pogreška prilikom predaje parametara!";
                return;
            }

        OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["konekcijaNaBazu"].ConnectionString);
        try
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand("SELECT * FROM ["
                    + User.Identity.Name
                    + "] WHERE id=@id"
                    + ";", connection);
            command.Parameters.AddWithValue("@id",id);
            OleDbDataReader dr;
            dr = command.ExecuteReader();

            if (!dr.HasRows)
                return;

            dr.Read();

            TextBoxNovaDatum.Text = dr.GetDateTime(1).ToShortDateString();
            TextBoxNovaPocetak.Text = dr.GetDateTime(2).ToShortTimeString();
            TextBoxNovaTrajanje.Text = dr.GetDateTime(3).ToShortTimeString();
            TextBoxNovaNaslov.Text = dr.GetString(4);
            TextBoxNovaSadržaj.Text = dr.GetString(5);            
        }
        catch (OleDbException ex)
        {
            LabelNoviUpozorenje.Visible = true;
            LabelNoviUpozorenje.Text = ex.Message;
        }
        finally
        {
            connection.Close();
        }

        #endregion

        LabelNoviKontrola.Text = "false";

    }

    protected void CalendarNova_Click(object sender, EventArgs e)
    {
        TextBoxNovaDatum.Text = CalendarNova.SelectedDate.ToShortDateString();
    }

    protected void ButtonNovaOdbaci_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["povratak"] != null)
            Response.Redirect(Request.QueryString["povratak"]);
        else
            Response.Redirect("~/Glavna.aspx");
    }

    protected void ButtonNovaSpremi_Click(object sender, EventArgs e)
    {
        DateTime datum = DateTime.Now;
        DateTime pocetak = DateTime.Now;
        DateTime trajanje = DateTime.Now;
        string naslov = TextBoxNovaNaslov.Text;
        string sadrzaj = TextBoxNovaSadržaj.Text;

        #region Provjera unosa podataka
        LabelNoviUpozorenje.Visible = true;
        if (!DateTime.TryParse(TextBoxNovaDatum.Text, out datum))
            LabelNoviUpozorenje.Text = "Neispravno unesen datum!";
        else if (!DateTime.TryParse(TextBoxNovaPocetak.Text, out pocetak))
            LabelNoviUpozorenje.Text = "Neispravno uneseno vrijeme pocetaka!";
        else if (!DateTime.TryParse(TextBoxNovaTrajanje.Text, out trajanje))
            LabelNoviUpozorenje.Text = "Neispravno uneseno vrijeme trajanja!";
        else if (naslov.Length < 1)
            LabelNoviUpozorenje.Text = "Naslov se sastoji od premalo znakova!";
        else if (naslov.Length > 50)
            LabelNoviUpozorenje.Text = "Naslov se sastoji od previse znakova!";
        else if (sadrzaj.Length < 1)
            LabelNoviUpozorenje.Text = "Sadržaj se sastoji od premalo znakova!";
        else if (sadrzaj.Length > 250)
            LabelNoviUpozorenje.Text = "Sadržaj se sastoji od previse znakova!";
        else
            LabelNoviUpozorenje.Visible = false;
        if (LabelNoviUpozorenje.Visible == true)
            return;
        #endregion

        #region Unos nove obveze
        if (Request.QueryString["id"] == null)
        {
            OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["konekcijaNaBazu"].ConnectionString);
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("INSERT INTO ["
                    + User.Identity.Name
                    + "] ([datum],[pocetak],[trajanje],[naslov],[sadrzaj])"
                    + " VALUES (@datum, @pocetak, @trajanje, @naslov, @sadrzaj)"
                    + ";", connection);
                command.Parameters.AddWithValue("@datum", datum);
                command.Parameters.AddWithValue("@pocetak", pocetak);
                command.Parameters.AddWithValue("@trajanje", trajanje);
                command.Parameters.AddWithValue("@naslov", naslov);
                command.Parameters.AddWithValue("@sadrzaj", sadrzaj);

                command.ExecuteNonQuery();
            }
            catch (OleDbException ex)
            {
                LabelNoviUpozorenje.Visible = true;
                LabelNoviUpozorenje.Text = ex.Message;
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion

        #region Uredivanje postojece obveze
        else if (Request.QueryString["id"] != null)
        {
            int id = 0;
            if (!Int32.TryParse(Request.QueryString["id"], out id))
            {
                LabelNoviUpozorenje.Visible = true;
                LabelNoviUpozorenje.Text = "Dogodila se pogreška prilikom predaje parametara!";
                return;
            }

            OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["konekcijaNaBazu"].ConnectionString);
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("UPDATE ["
                    + User.Identity.Name
                    + "] SET datum=@datum, pocetak=@pocetak, trajanje=@trajanje, naslov=@naslov, sadrzaj=@sadrzaj"
                    + " WHERE id=@id"
                    + ";", connection);
                command.Parameters.AddWithValue("@datum", datum);
                command.Parameters.AddWithValue("@pocetak", pocetak);
                command.Parameters.AddWithValue("@trajanje", trajanje);
                command.Parameters.AddWithValue("@naslov", naslov);
                command.Parameters.AddWithValue("@sadrzaj", sadrzaj);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }
            catch (OleDbException ex)
            {
                LabelNoviUpozorenje.Visible = true;
                LabelNoviUpozorenje.Text = ex.Message;
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion

        if (Request.QueryString["povratak"] != null)
            Response.Redirect(Request.QueryString["povratak"]);
        else
            Response.Redirect("~/Glavna.aspx");

    }
}