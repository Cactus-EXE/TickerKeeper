<%@ Page Title="Customer List" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CustomerList.aspx.cs" Inherits="FinalApplication.CustomerList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Additional content for the head section if needed -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Customer List</h2>
    <asp:GridView ID="GridViewCustomers" runat="server" AutoGenerateColumns="False" 
                  CssClass="grid" GridLines="None" 
                  OnRowDataBound="GridViewCustomers_RowDataBound">
        <Columns>
            <asp:BoundField DataField="CustomerId" HeaderText="Customer ID" />
            <asp:BoundField DataField="FirstName" HeaderText="First Name" />
            <asp:BoundField DataField="LastName" HeaderText="Last Name" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="City" HeaderText="City" />
            <asp:TemplateField HeaderText="Details">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLinkDetails" runat="server" 
                        NavigateUrl='<%# "CustomerDetails.aspx?CustomerId=" + Eval("CustomerId") %>' 
                        Text="View Details"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>