using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LabelGlavnaTrenutniKorisnik.Text = "Trenutno ste prijavljeni kao: " + HttpContext.Current.User.Identity.Name;
    }

    protected void ButtonGlavnaLogout_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("~/Login.aspx");
    }

    protected void ButtonGlavnaPocetnaStranica_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Glavna.aspx");
    }
    protected void ButtonGlavnaNovaObveza_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Nova.aspx");
    }

    protected void ButtonGlavnaPrikaziSve_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Prikaz.aspx");
    }

    protected void ButtonGlavnaTrazi_Click(object sender, EventArgs e)
    {
        if (TextBoxGlavnaTrazi.Text.Length > 0)
            Response.Redirect("~/Prikaz.aspx?kljuc=" + TextBoxGlavnaTrazi.Text);
        else
            Response.Redirect("~/Prikaz.aspx");
    }
}
