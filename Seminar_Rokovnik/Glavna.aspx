<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Glavna.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Pocetna Stranica</h1><br /><br />
    <div style="position:relative; left:-125px; top:50px; width:225px;">
        <asp:Label ID="LabelGlavnaUpute" runat="server" Text="Odaberite željeni datum na kalendaru kako bi vidjeli obveze zabilježene za taj dan."></asp:Label>
    </div>
    <div style="position:relative; left:125px; top:-75px; width:200px;">
        <asp:Calendar ID="CalendarGlavna" runat="server" OnSelectionChanged="CalendarGlavna_Click"></asp:Calendar>
    </div>
    <hr />
    <div style="width:600px;">
        <asp:Table ID="TableGlavna" runat="server">
        </asp:Table>
    </div>
    <asp:Label ID="LabelGlavnaKontrola" runat="server" Text="true" Visible="false"></asp:Label>
     <asp:Label ID="LabelGlavnaUpozorenje" runat="server" Text="true" Visible="false"></asp:Label>
</asp:Content>

