using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region provjera parametara i postavljanje naslova stranice

        PlaceHolderPrikazNaslov.Controls.Clear();

        if (Request.QueryString["kljuc"] == null)
        {
            Literal naslov = new Literal();
            naslov.Text = "<h1>Prikaz svih obveza</h1><hr>";
            PlaceHolderPrikazNaslov.Controls.Add(naslov);
        }
        else
        {
            Literal prazanRed = new Literal();
            prazanRed.Text = "<br />";
            PlaceHolderPrikazNaslov.Controls.Add(prazanRed);
            Label label = new Label();
            label.Font.Size = FontUnit.XLarge;
            label.Attributes.Add("style", "color:Maroon; font-weight:bold;");
            label.Text = "Rezultati pretrage za traženi pojam: &quot;" + Request.QueryString["kljuc"] + "&quot;";
            PlaceHolderPrikazNaslov.Controls.Add(label);
            
            Literal prijelom = new Literal();
            prijelom.Text = "<br /><br /><hr>";
            PlaceHolderPrikazNaslov.Controls.Add(prijelom);
            
        }
        #endregion

        if (Request.QueryString["kljuc"] == null)
        {
            TablePrikaz.Rows.Clear();

            #region upit u bazu za sve obveze
            OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["konekcijaNaBazu"].ConnectionString);
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("SELECT * FROM ["
                    + User.Identity.Name
                    + "] ORDER BY datum, pocetak" 
                    + ";", connection);
            
                OleDbDataReader dr;
                dr = command.ExecuteReader();

                if (!dr.HasRows)
                {
                    LabelPrikazUpozorenje.Text = "U rokovniku se ne nalazi niti jedna obveza!";
                    LabelPrikazUpozorenje.Font.Size = FontUnit.XLarge;
                    LabelPrikazUpozorenje.Attributes.Add("style", "color:Maroon; font-weight:bold;");
                    LabelPrikazUpozorenje.Visible = true;
                }
                else
                {
                    popuniTablicu(dr);
                }
            }
            catch (OleDbException ex)
            {
                LabelPrikazUpozorenje.Visible = true;
                LabelPrikazUpozorenje.Text = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            #endregion
        }
        else
        {
            string kljuc = Request.QueryString["kljuc"];
            kljuc = "%" + kljuc + "%";

            TablePrikaz.Rows.Clear();

            #region upit u bazu za sve obveze s trazenim pojmom
            OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["konekcijaNaBazu"].ConnectionString);
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("SELECT * FROM ["
                    + User.Identity.Name
                    + "] WHERE naslov LIKE @kljuc OR sadrzaj LIKE @kljuc" 
                    + " ORDER BY datum, pocetak"
                    + ";", connection);
                command.Parameters.AddWithValue("@kljuc", kljuc);

                OleDbDataReader dr;
                dr = command.ExecuteReader();

                if (!dr.HasRows)
                {
                    PlaceHolderPrikazNaslov.Controls.Clear();
                    
                    Literal prazanRed = new Literal();
                    prazanRed.Text = "<br /> <br />";
                    PlaceHolderPrikazNaslov.Controls.Add(prazanRed);

                    LabelPrikazUpozorenje.Text = "U rokovniku se ne nalazi niti jedna obveza koja sadrži traženi pojam: &quot;" + Request.QueryString["kljuc"] + "&quot;";
                    LabelPrikazUpozorenje.Font.Size = FontUnit.XLarge;
                    LabelPrikazUpozorenje.Attributes.Add("style", "color:Maroon; font-weight:bold;");
                    LabelPrikazUpozorenje.Visible = true;
                }
                else
                {
                    popuniTablicu(dr);
                }
            }
            catch (OleDbException ex)
            {
                LabelPrikazUpozorenje.Visible = true;
                LabelPrikazUpozorenje.Text = ex.Message;
            }
            finally
            {
                connection.Close();
            }
            #endregion
        }

    }

    protected void buttonUredi_Click(object sender, EventArgs e)
    {
        string[] buttonName = ((Button)sender).ID.ToString().Split('_');
        string id = buttonName[buttonName.Length - 1];

        if(Request.QueryString["kljuc"] == null)
            Response.Redirect("~/Nova.aspx?id=" + id + "&povratak=Prikaz.aspx");
        else if (Request.QueryString["kljuc"] != null)
            Response.Redirect("~/Nova.aspx?id=" + id + "&povratak=Prikaz.aspx?kljuc=" + Request.QueryString["kljuc"]);
    }

    protected void buttonBrisi_Click(object sender, EventArgs e)
    {
        string[] buttonName = ((Button)sender).ID.ToString().Split('_');
        string id = buttonName[buttonName.Length - 1];

        OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["konekcijaNaBazu"].ConnectionString);
        try
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand("DELETE FROM ["
                + User.Identity.Name
                + "] WHERE id=@id"
                + ";", connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
        catch (OleDbException ex)
        {
            LabelPrikazUpozorenje.Visible = true;
            LabelPrikazUpozorenje.Text = ex.Message;
        }
        finally
        {
            connection.Close();
            Page_Load(sender, e);
        }
    }

    protected void popuniTablicu(OleDbDataReader dr)
    {
        LabelPrikazUpozorenje.Text = "";
        LabelPrikazUpozorenje.Visible = false;

        #region Popunjavanje zaglavlja tablice
        TableHeaderRow thr = new TableHeaderRow();
        thr.Height = 100;
                
        TableHeaderCell thc1 = new TableHeaderCell();
        Label labelNaslov1 = new Label();
        labelNaslov1.Text = "Obveza";
        labelNaslov1.Font.Size = FontUnit.XLarge;
        labelNaslov1.Attributes.Add("style", "color:Maroon; font-weight:bold;");
        thc1.Controls.Add(labelNaslov1);
        thc1.Width = 400;
                
        TableHeaderCell thc2 = new TableHeaderCell();
        Label labelNaslov2 = new Label();
        labelNaslov2.Text = "Opcije";
        labelNaslov2.Font.Size = FontUnit.XLarge;
        labelNaslov2.Attributes.Add("style", "color:Maroon; font-weight:bold;");
        thc2.Controls.Add(labelNaslov2);
        thc2.Width = 200;
                
        thr.Cells.Add(thc1);
        thr.Cells.Add(thc2);
                
        TablePrikaz.Rows.Add(thr);
        #endregion

        #region Popunjavanje zapisa zablice
        while (dr.Read())
        {
            TableRow tr = new TableRow();
            TableCell tc1 = new TableCell();
            TableCell tc2 = new TableCell();

            Label labelZaglavlje = new Label();
            Label labelSadrzaj = new Label();

            Literal prijelom1 = new Literal();
            Literal prijelom2 = new Literal();

            Button buttonBrisi = new Button();
            Button buttonUredi = new Button();

            DateTime pocetak = dr.GetDateTime(2);
            DateTime trajanje = dr.GetDateTime(3);
            DateTime kraj = pocetak;

            kraj = kraj.AddHours(trajanje.Hour);
            kraj = kraj.AddMinutes(trajanje.Minute);

            labelZaglavlje.Text = dr.GetDateTime(1).ToShortDateString()
                                + "   " + pocetak.ToShortTimeString()
                                + " - " + kraj.ToShortTimeString()
                                + "           " + dr.GetString(4);
            labelZaglavlje.Font.Size = FontUnit.Large;
            labelZaglavlje.Attributes.Add("style", "color:Maroon; font-weight:bold;");
            labelSadrzaj.Text = dr.GetString(5);

            prijelom1.Text = "<hr />";
            prijelom2.Text = "<br /><br />";

            tc1.Controls.Add(labelZaglavlje);
            tc1.Controls.Add(prijelom1);
            tc1.Controls.Add(labelSadrzaj);

            tc1.Attributes.Add("style", "text-align: left;");

            int id = dr.GetInt32(0);

            buttonUredi.ID = "buttonUredi_" + id.ToString();
            buttonBrisi.ID = "buttonBrisi_" + id.ToString();

            buttonUredi.Width = 100;
            buttonBrisi.Width = 100;

            buttonUredi.Click += new EventHandler(buttonUredi_Click);
            buttonBrisi.Click += new EventHandler(buttonBrisi_Click);

            buttonUredi.Text = "Uredi obvezu";
            buttonBrisi.Text = "Obriši obvezu";

            tc2.Controls.Add(buttonUredi);
            tc2.Controls.Add(prijelom2);
            tc2.Controls.Add(buttonBrisi);

            tc2.Attributes.Add("style", "text-align: center;");

            tr.Cells.Add(tc1);
            tr.Cells.Add(tc2);
            TablePrikaz.Rows.Add(tr);            

            TableRow trSpacer = new TableRow();
            trSpacer.Height = 35;
            TablePrikaz.Rows.Add(trSpacer);
        }
            #endregion
    }
}