<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="~/css/mojStil.css" rel="stylesheet" type="text/css" media="screen" runat="server" />
</head>

<body>
    <form id="form1" runat="server">
    <div style="width: 400px;">
        <h1 style="width: 250px"> Prijava korisnika </h1>
        <br /><br />        
        <asp:Label ID="LabelLoginKorisnik" runat="server" Width="150" Text="Korisničko ime:"></asp:Label>
        <asp:TextBox ID="TextBoxLoginKorisnik" runat="server" Width="200"></asp:TextBox>
        <br /><br />        
        <asp:Label ID="LabelLoginLozinka" runat="server" Width="150" Text="Loznika:"></asp:Label>
        <asp:TextBox ID="TextBoxLoginLozinka" runat="server" Width="200" TextMode="Password" ></asp:TextBox>
        <br /><br />          
        <asp:Label ID="LabelRegistracijaUpozorenje" runat="server" ForeColor="Red" Text=""></asp:Label>
        <br /><br />
        <asp:Button ID="ButtonPrijava" runat="server" Width="150" Text="Prijava" OnClick="ButtonPrijava_Click" />
        <br /><br /><br />
        <asp:Label ID="LabelLoginNapraviteKorisnika" runat="server" Text="Nemate korsnički račun? <br> Napravite novi pritiskom "></asp:Label>
        <asp:HyperLink ID="HyperLinkLoginRegistracija" runat="server" NavigateUrl="~/Register.aspx">ovdje.</asp:HyperLink>     
    </div>
    </form>
</body>
</html>
