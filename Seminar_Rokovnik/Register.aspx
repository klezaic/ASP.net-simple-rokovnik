<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link id="Link1" href="~/css/mojStil.css" rel="stylesheet" type="text/css" media="screen" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 400px;">
        <h1 style="width: 325px;"> Registracija korisnika </h1>
        <br /><br />
        <asp:Label ID="LabelRegisterKorisnik" runat="server" Width="150" Text="Korisničko ime:"></asp:Label>
        <asp:TextBox ID="TextBoxRegisterKorisnik" runat="server" Width="200"></asp:TextBox>
        <br /><br />
        <asp:Label ID="LabelRegisterLozinka" runat="server" Width="150" Text="Loznika:"></asp:Label>
        <asp:TextBox ID="TextBoxRegisterLozinka" runat="server" Width="200" TextMode="Password"></asp:TextBox>
        <br /><br />
        <asp:Label ID="LabelRegistracijaUpozorenje" runat="server" ForeColor="Red" Text=""></asp:Label>
        <br /><br />
        <asp:Button ID="ButtonRegistracija" runat="server" Width="150" Text="Registracija" OnClick="ButtonRegistracija_Click" />
        <br /><br />
        <asp:Label ID="LabelPovratakNaLogin" runat="server" Text="Povratak na "></asp:Label>
        <asp:HyperLink ID="HyperLinkNaLogin" runat="server" NavigateUrl="~/Login.aspx">prijavu korisnika</asp:HyperLink>
    </div>
    </form>
</body>
</html>
