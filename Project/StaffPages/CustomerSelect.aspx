<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/TwoColView.Master" AutoEventWireup="true" CodeBehind="CustomerSelect.aspx.cs" Inherits="MMVT.CustomerSelect" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainMenu" runat="server">
<ul>
    <li><a href="ScheduleForm.aspx">Schedule</a></li>
    <li><a href="Requests.aspx">Requests</a></li>
    <li><a href="CustomerList.aspx">Customers</a></li>
    <li><a href="../LoginForm.aspx">Logout</a></li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LeftColumn" runat="server">

<asp:Panel ID="CustomersPanel" runat="server">

</asp:Panel>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="RightColumn" runat="server">

<div class="grouping_box">
    <h1>Search customers</h1>
    <div class="half_width_field_container_left">
        <label for="search_firstName">First name</label><br /><asp:TextBox ID="search_firstName" runat="server"></asp:TextBox>
    </div>
    <div class="half_width_field_container_right">
        <label for="search_surname">Surname</label><br /><asp:TextBox ID="search_surname" runat="server"></asp:TextBox>
    </div>
    <label for="search_vechicleReg">Vechicle registration number</label><br /><asp:TextBox ID="search_vehicleReg" runat="server"></asp:TextBox><br />
    <label for="search_emailAddress">Email address</label><br /><asp:TextBox ID="search_emailAddress" runat="server"></asp:TextBox>
    <p class="right"><asp:Button ID="search" runat="server" OnClick="search_click" CssClass="button" Text="Search" /></p>
</div>

<p />

</asp:Content>
