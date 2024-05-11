<%@ Page Title="Customer Details" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CustomerDetails.aspx.cs" Inherits="FinalApplication.CustomerDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- If you need to add something specific to the <head> (like custom CSS or scripts), do it here -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Customer Details</h2>
    <div>Customer ID: <asp:Label ID="lblCustomerId" runat="server" Text=""></asp:Label></div>
    <div>First Name: <asp:Label ID="lblFirstName" runat="server" Text=""></asp:Label></div>
    <div>Last Name: <asp:Label ID="lblLastName" runat="server" Text=""></asp:Label></div>
    <div>Phone Number: <asp:Label ID="lblPhoneNumber" runat="server" Text=""></asp:Label></div>
    <div>Email: <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label></div>
    <div>Address: <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label></div>
    <div>City: <asp:Label ID="lblCity" runat="server" Text=""></asp:Label></div>
    <div>State: <asp:Label ID="lblState" runat="server" Text=""></asp:Label></div>
    <div>Postal Code: <asp:Label ID="lblPostalCode" runat="server" Text=""></asp:Label></div>
    <div>Country: <asp:Label ID="lblCountry" runat="server" Text=""></asp:Label></div>
    <div>Customer Creation Date: <asp:Label ID="lblCustomerCreation" runat="server" Text=""></asp:Label></div>
    <div>Customer Notes: <asp:Label ID="lblCustomerNotes" runat="server" Text=""></asp:Label></div>
    <br />
    <asp:Button ID="btnEditCustomer" runat="server" Text="Edit" OnClick="btnEditCustomer_Click" />
    <asp:Button ID="btnDeleteCustomer" runat="server" Text="Delete" OnClick="btnDeleteCustomer_Click" />
    <asp:Button ID="btnCreateTicket" runat="server" Text="Create Ticket" OnClick="btnCreateTicket_Click" />
    <br /><br />

    <!-- GridView for displaying customer repairs -->
    <h3>Customer Repairs</h3>
    <asp:GridView ID="gridViewRepairs" runat="server" AutoGenerateColumns="false" EmptyDataText="No repairs found for this customer.">
        <Columns>
            <asp:BoundField DataField="RepairId" HeaderText="Repair ID" />
            <asp:BoundField DataField="TypeName" HeaderText="Device Type" />
            <asp:BoundField DataField="MakeName" HeaderText="Make" />
            <asp:BoundField DataField="ModelName" HeaderText="Model" />
            <asp:BoundField DataField="ProblemDescription" HeaderText="Problem Description" />
        </Columns>
    </asp:GridView>
</asp:Content>
