<%@ Page Title="Phone Repair Request Form" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RepairCheckIn.aspx.cs" Inherits="FinalApplication.RepairCheckIn" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <!-- Page-specific head content like scripts or styles -->
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <h2>Phone Repair Request Form</h2>
    
    <!-- Add the hidden field within the UpdatePanel or main form -->
    <asp:HiddenField ID="HiddenFieldCustomerId" runat="server" />

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <label for="DropDownListType">Device Type:</label>
            <asp:DropDownList ID="DropDownListType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListType_SelectedIndexChanged">
                <asp:ListItem Text="--Select Type--" Value="" />
            </asp:DropDownList>
            <br />
            <label for="DropDownListMake">Phone Make:</label>
            <asp:DropDownList ID="DropDownListMake" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownListMake_SelectedIndexChanged" Enabled="false">
                <asp:ListItem Text="--Select Make--" Value="" />
            </asp:DropDownList>
            <br />
            <label for="DropDownListModel">Phone Model:</label>
            <asp:DropDownList ID="DropDownListModel" runat="server" Enabled="false">
                <asp:ListItem Text="--Select Model--" Value="" />
            </asp:DropDownList>
            <br />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="DropDownListType" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="DropDownListMake" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>

    <label>Functionalities to Check:</label>
    <div>
        <asp:CheckBox ID="chkCamera" runat="server" Text="LCD" />
        <asp:CheckBox ID="CheckBox1" runat="server" Text="Touch" />
        <asp:CheckBox ID="CheckBox2" runat="server" Text="Top Speaker" />
        <asp:CheckBox ID="CheckBox3" runat="server" Text="Bottom Speaker" />
        <asp:CheckBox ID="CheckBox4" runat="server" Text="Top Mic" />
        <asp:CheckBox ID="CheckBox5" runat="server" Text="Front Camera" />   
        <asp:CheckBox ID="CheckBox6" runat="server" Text="Back Camera" />  
        <asp:CheckBox ID="CheckBox7" runat="server" Text="Flashlight" />  
        <asp:CheckBox ID="CheckBox8" runat="server" Text="Face ID" />  
    </div>
    <br />
    <label for="description">Problem Description:</label>
    <asp:TextBox ID="description" TextMode="MultiLine" Rows="4" Columns="50" runat="server" />
    <br />
    <asp:Button ID="submit" runat="server" Text="Submit" OnClick="SubmitForm_Click" />
</asp:Content>
