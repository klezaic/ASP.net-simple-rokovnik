<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Nova.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:PlaceHolder ID="PlaceHolderNovaNaslov" runat="server"></asp:PlaceHolder>
<div style="position:relative; left:-100px; width:400px; height:195px; text-align:left;">
    <asp:Label ID="LabelNovaDatum" runat="server" Width="135" Text="Datum: "></asp:Label>
    <asp:TextBox ID="TextBoxNovaDatum" runat="server" Width="200"></asp:TextBox><br />
    <asp:Label ID="LabelNovaPocetak" runat="server" Width="135" Text="Pocetak (h:mm): "></asp:Label>
    <asp:TextBox ID="TextBoxNovaPocetak" runat="server" Width="200"></asp:TextBox><br />
    <asp:Label ID="LabelNovaTrajanje" runat="server" Width="135" Text="Trajanje (h:mm): "></asp:Label>
    <asp:TextBox ID="TextBoxNovaTrajanje" runat="server" Width="200"></asp:TextBox><br />
    <asp:Label ID="LabelNovaNaslov" runat="server" Width="135" Text="Naslov: "></asp:Label>
    <asp:TextBox ID="TextBoxNovaNaslov" runat="server" Width="200"></asp:TextBox><br />
    <asp:Label ID="LabelNovaSadržaj" runat="server" Width="135" Text="Sadržaj: "></asp:Label><br />
    <asp:TextBox ID="TextBoxNovaSadržaj" runat="server" Width="335" Height="75" TextMode="MultiLine"></asp:TextBox><br />
    <div style="position:relative; left:-30px; width:335px; text-align:right; ">
        <asp:Label ID="LabelNoviUpozorenje" runat="server" Text="" Visible="false"></asp:Label>
        <br /><asp:Button ID="ButtonNovaOdbaci" runat="server" Width="150" Text="Odbaci promjene" OnClick="ButtonNovaOdbaci_Click"/> &nbsp
        <asp:Button ID="ButtonNovaSpremi" runat="server" Width="150" Text="Spremi promjene" OnClick="ButtonNovaSpremi_Click" />
    </div>
</div >
<div style="position:relative; left:200px; top:-195px; width:200px; height:180px; ">
    <asp:Calendar ID="CalendarNova" runat="server" OnSelectionChanged="CalendarNova_Click"></asp:Calendar>
</div>
<asp:Label ID="LabelNoviKontrola" runat="server" Text="true" Visible="false"></asp:Label>
</asp:Content>

