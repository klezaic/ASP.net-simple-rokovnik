<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Prikaz.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:PlaceHolder ID="PlaceHolderPrikazNaslov" runat="server"></asp:PlaceHolder>
    <div style="width:600px;">
        <asp:Table ID="TablePrikaz" runat="server">
        </asp:Table>
    </div>
    <asp:Label ID="LabelPrikazUpozorenje" runat="server" Width="400" Text=""></asp:Label>
</asp:Content>

