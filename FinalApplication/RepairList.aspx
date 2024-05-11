<%@ Page Title="Repair Tickets" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RepairList.aspx.cs" Inherits="FinalApplication.RepairList" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <!-- Head-specific content such as additional CSS or JavaScript references can go here -->
      <style>
        /* Style for the GridView header */
        .gridview-header {
            background-color: #3A3A3A;
            color: #FFFFFF;
            font-weight: bold;
            padding: 8px;
        }

        /* Style for the GridView rows */
        .gridview-row, .gridview-alt-row {
            padding: 8px;
            border-bottom: 1px solid #CCCCCC;
        }

        .gridview-row {
            background-color: #E3E3E3;
            color: #333333;
        }

        .gridview-alt-row {
            background-color: #FFFFFF;
            color: #666666;
        }

        /* Style for the View Details link */
        .details-link {
            color: #007bff;
            text-decoration: underline;
            cursor: pointer;
        }

        .details-link:hover {
            color: #0056b3;
        }

        /* General page styling */
        .page-title {
            font-size: 24px;
            font-weight: bold;
            color: #2C3E50;
            margin-bottom: 20px;
        }

        .table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

        .table th, .table td {
            padding: 10px;
            text-align: left;
        }

        .table th {
            background-color: #3A3A3A;
            color: #FFFFFF;
        }
    </style>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Repair Tickets</h2>
    <asp:GridView ID="GridViewRepairs" runat="server" AutoGenerateColumns="False" 
                  HeaderStyle-BackColor="#3A3A3A" HeaderStyle-ForeColor="White" 
                  RowStyle-BackColor="#E3E3E3" RowStyle-ForeColor="#333333" 
                  AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#666666" 
                  GridLines="None" CssClass="table">
        <Columns>
            <asp:BoundField DataField="RepairId" HeaderText="Repair ID" />
            <asp:BoundField DataField="FirstName" HeaderText="First Name" />
            <asp:BoundField DataField="LastName" HeaderText="Last Name" />
            <asp:BoundField DataField="MakeId" HeaderText="Make" />
            <asp:BoundField DataField="ModelId" HeaderText="Model" />
            <asp:BoundField DataField="ProblemDescription" HeaderText="Problem Description" />
            <asp:TemplateField HeaderText="Details">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonDetails" runat="server" 
                                    CommandArgument='<%# Eval("RepairId") %>' 
                                    Text="View Details" OnClick="ViewDetails_Click"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
