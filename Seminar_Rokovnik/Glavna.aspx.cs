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
        if (LabelGlavnaKontrola.Text == "true")
        {
            CalendarGlavna.SelectedDate = DateTime.Today;
            CalendarGlavna_Click(sender, e);
            LabelGlavnaKontrola.Text = "false";
            return;
        }
        CalendarGlavna_Click(sender, e);
    }

    protected void CalendarGlavna_Click(object sender, EventArgs e)
    {
        TableGlavna.Rows.Clear();

        OleDbConnection connection = new OleDbConnection(ConfigurationManager.ConnectionStrings["konekcijaNaBazu"].ConnectionString);
        try
        {
            connection.Open();
            DateTime datum = CalendarGlavna.SelectedDate;
            OleDbCommand command = new OleDbCommand("SELECT * FROM ["
                + User.Identity.Name
                + "] WHERE datum=@datum"
                + " ORDER BY datum, pocetak" 
                + ";", connection);
            command.Parameters.AddWithValue("@datum",datum);
            
            OleDbDataReader dr;
            dr = command.ExecuteReader();

            if (!dr.HasRows)
            {
                LabelGlavnaUpozorenje.Text = "Za zadani datum nema obveza!";
                LabelGlavnaUpozorenje.Font.Size = FontUnit.XLarge;
                LabelGlavnaUpozorenje.Attributes.Add("style", "color:Maroon; font-weight:bold;");
                LabelGlavnaUpozorenje.Visible = true;
            }
            else
            {
                LabelGlavnaUpozorenje.Text = "";
                LabelGlavnaUpozorenje.Visible = false;

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
                
                TableGlavna.Rows.Add(thr);
                #endregion

                while (dr.Read())
                {
                    #region Popunjavanje redaka tablice
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
                    TableGlavna.Rows.Add(tr);

                    #endregion

                    TableRow trSpacer = new TableRow();
                    trSpacer.Height = 35;
                    TableGlavna.Rows.Add(trSpacer);
                }
            }
        }
        catch (OleDbException ex)
        {
            LabelGlavnaUpozorenje.Visible = true;
            LabelGlavnaUpozorenje.Text = ex.Message;
        }
        finally
        {
            connection.Close();
        }
    }

    protected void buttonUredi_Click(object sender, EventArgs e)
    {
        string[] buttonName = ((Button)sender).ID.ToString().Split('_');
        string id = buttonName[buttonName.Length - 1];

        Response.Redirect("~/Nova.aspx?id=" + id);
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
            LabelGlavnaUpozorenje.Visible = true;
            LabelGlavnaUpozorenje.Text = ex.Message;
        }
        finally
        {
            connection.Close();
            CalendarGlavna_Click(sender, e);
        }
    }      
}