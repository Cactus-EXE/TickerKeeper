<%@ Page Title="Edit Customer" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="EditCustomer.aspx.cs" Inherits="FinalApplication.EditCustomer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Edit Customer</h2>
    <div>
        <label for="txtFirstName">First Name:</label>
        <asp:TextBox ID="txtFirstName" runat="server" />
    </div>
    <div>
        <label for="txtLastName">Last Name:</label>
        <asp:TextBox ID="txtLastName" runat="server" />
    </div>
    <div>
        <label for="txtPhoneNumber">Phone Number:</label>
        <asp:TextBox ID="txtPhoneNumber" runat="server" />
    </div>
    <div>
        <label for="txtEmail">Email:</label>
        <asp:TextBox ID="txtEmail" runat="server" />
    </div>
    <div>
        <label for="txtAddress">Address:</label>
        <asp:TextBox ID="txtAddress" runat="server" />
    </div>
    <div>
        <label for="txtCity">City:</label>
        <asp:TextBox ID="txtCity" runat="server" />
    </div>
    <div>
        <label for="txtState">State:</label>
        <asp:TextBox ID="txtState" runat="server" />
    </div>
    <div>
        <label for="txtPostalCode">Postal Code:</label>
        <asp:TextBox ID="txtPostalCode" runat="server" />
    </div>
    <div>
        <label for="txtCountry">Country:</label>
        <asp:TextBox ID="txtCountry" runat="server" />
    </div>
    <div>
        <label for="txtCustomerNotes">Notes:</label>
        <asp:TextBox ID="txtCustomerNotes" runat="server" TextMode="MultiLine" Rows="4" />
    </div>
    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" />
</asp:Content>
