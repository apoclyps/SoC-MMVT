<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/TwoColView.Master" AutoEventWireup="true" CodeBehind="CustomerList.aspx.cs" Inherits="MMVT.CustomerList" %>
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
        <label for="search_firstName">First name</label><br /><asp:TextBox ID="search_firstName" runat="server" EnableViewState="true"></asp:TextBox>
    </div>
    <div class="half_width_field_container_right">
        <label for="search_surname">Surname</label><br /><asp:TextBox ID="search_surname" runat="server" EnableViewState="true"></asp:TextBox>
    </div>
    <label for="search_vechicleReg">Vechicle registration number</label><br /><asp:TextBox ID="search_vehicleReg" runat="server" EnableViewState="true"></asp:TextBox><br />
    <label for="search_emailAddress">Email address</label><br /><asp:TextBox ID="search_emailAddress" runat="server" EnableViewState="true"></asp:TextBox>
    <p class="right"><asp:Button ID="search" runat="server" OnClick="search_click" CssClass="button" Text="Search" /></p>
</div>

<p />

<div class="grouping_box">
    <h1>New customer</h1>
    <div class="half_width_field_container_left">
        <label for="add_firstName">First name</label><br /><asp:TextBox ID="add_firstName" runat="server"></asp:TextBox>
    </div>
    <div class="half_width_field_container_right">
        <label for="add_surname">Surname</label><br /><asp:TextBox ID="add_surname" runat="server"></asp:TextBox>
    </div>

    <div class="full_width_field_container">
        <label for="add_addressL1">Address</label><br /><asp:TextBox ID="add_addressL1" runat="server"></asp:TextBox><br />
        <asp:TextBox ID="add_addressL2" runat="server"></asp:TextBox><br />
    </div>

    <div class="half_width_field_container_left">
        <label for="add_town">Town</label><br /><asp:TextBox ID="add_town" runat="server"></asp:TextBox>
    </div>
    <div class="half_width_field_container_right">
        <label for="add_postcode">Postcode</label><br /><asp:TextBox ID="add_postcode" runat="server"></asp:TextBox>
    </div>

    <div class="full_width_field_container">
        <label for="add_homePhone">Home phone number</label><br /><asp:TextBox ID="add_homePhone" runat="server"></asp:TextBox><br />
        <label for="add_mobilePhone">Mobile phone number</label><br /><asp:TextBox ID="add_mobilePhone" runat="server"></asp:TextBox><br />
        <label for="add_email">Email address</label><br /><asp:TextBox ID="add_email" runat="server"></asp:TextBox><br />
    </div>

    <p class="right"><asp:Button ID="add" runat="server" OnClick="add_click" CssClass="button" Text="Add" /></p>
</div>

</asp:Content>
