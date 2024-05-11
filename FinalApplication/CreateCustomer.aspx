<%@ Page Title="Create Customer" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CreateCustomer.aspx.cs" Inherits="FinalApplication.CreateCustomer" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <!-- Head content like scripts or styles specific to this page -->
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Create New Customer</h2>
    <div>
        <asp:Label ID="LabelFirstName" runat="server" Text="First Name:" AssociatedControlID="TextBoxFirstName"></asp:Label>
        <asp:TextBox ID="TextBoxFirstName" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="LabelLastName" runat="server" Text="Last Name:" AssociatedControlID="TextBoxLastName"></asp:Label>
        <asp:TextBox ID="TextBoxLastName" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="LabelPhoneNumber" runat="server" Text="Phone Number:" AssociatedControlID="TextBoxPhoneNumber"></asp:Label>
        <asp:TextBox ID="TextBoxPhoneNumber" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="LabelEmail" runat="server" Text="Email:" AssociatedControlID="TextBoxEmail"></asp:Label>
        <asp:TextBox ID="TextBoxEmail" runat="server" TextMode="Email"></asp:TextBox>
        <br />
        <asp:Label ID="LabelAddress" runat="server" Text="Address:" AssociatedControlID="TextBoxAddress"></asp:Label>
        <asp:TextBox ID="TextBoxAddress" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="LabelCity" runat="server" Text="City:" AssociatedControlID="TextBoxCity"></asp:Label>
        <asp:TextBox ID="TextBoxCity" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="LabelState" runat="server" Text="State:" AssociatedControlID="TextBoxState"></asp:Label>
        <asp:TextBox ID="TextBoxState" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="LabelPostalCode" runat="server" Text="Postal Code:" AssociatedControlID="TextBoxPostalCode"></asp:Label>
        <asp:TextBox ID="TextBoxPostalCode" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="LabelCountry" runat="server" Text="Country:" AssociatedControlID="TextBoxCountry"></asp:Label>
        <asp:TextBox ID="TextBoxCountry" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="LabelCustomerNotes" runat="server" Text="Customer Notes:" AssociatedControlID="TextBoxCustomerNotes"></asp:Label>
        <asp:TextBox ID="TextBoxCustomerNotes" runat="server" TextMode="MultiLine"></asp:TextBox>
        <br />
        <asp:Button ID="ButtonCreateCustomer" runat="server" Text="Submit" OnClick="ButtonCreateCustomer_Click" />
    </div>
</asp:Content>