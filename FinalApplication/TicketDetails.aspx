<%@ Page
Title="Ticket Details" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="TicketDetails.aspx.cs" Inherits="FinalApplication.TicketDetails" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
<!-- Add page-specific head content here, like custom CSS or JavaScript -->
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<h2>Ticket Details</h2>
<div>Ticket #<asp:Label ID="lblRepairId" runat="server" Text=""></asp:Label></div>
<div>First Name: <asp:Label ID="lblFirstName" runat="server" Text=""></asp:Label></div>
<div>Last Name: <asp:Label ID="lblLastName" runat="server" Text=""></asp:Label></div>
<div>Make: <asp:Label ID="lblMake" runat="server" Text=""></asp:Label></div>
<div>Model: <asp:Label ID="lblModel" runat="server" Text=""></asp:Label></div>
<div>Problem Description: <asp:Label ID="lblProblemDescription" runat="server" Text=""></asp:Label></div>
<!-- Other details -->
</asp:Content>