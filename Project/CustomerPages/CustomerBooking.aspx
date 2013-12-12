<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThreeColView.Master" AutoEventWireup="true" CodeBehind="CustomerBooking.aspx.cs" Inherits="MMVT.CustomerPages.CustomerBooking" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainMenu" runat="server">
<ul>
    <li><a href="MyVehicles.aspx">My vehicles and personal details</a></li>
    <li><a href="CustomerBooking.aspx">Make booking</a></li>
    <li><a href="../LoginForm.aspx">Logout</a></li>
</ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LeftColumn" runat="server">
<h1>Select a vehicle</h1>
<asp:Panel ID="customerVehicles" runat="server" CssClass="grouping_box">


</asp:Panel>

</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="CentreColumn" runat="server">
<h1>Select a date</h1>
<asp:Calendar ID="SelectCalender" runat="server" EnableViewState="true" Width="100%"></asp:Calendar>

</asp:Content>


<asp:Content ID="Content5" ContentPlaceHolderID="RightColumn" runat="server">

<asp:Label ID="header" runat="server"></asp:Label>
<div class="grouping_box">
            <div class="full_width_field_container">

            <label>Prefered time:</label><br />
            <label for="time_hours">HH:</label>
            <asp:DropDownList ID="time_hours" runat="server">
                <asp:ListItem Value="08">08</asp:ListItem>
                <asp:ListItem Value="09">09</asp:ListItem>
                <asp:ListItem Value="10">10</asp:ListItem>
                <asp:ListItem Value="11">11</asp:ListItem>
                <asp:ListItem Value="12">12</asp:ListItem>
                <asp:ListItem Value="13">13</asp:ListItem>
                <asp:ListItem Value="14">14</asp:ListItem>
                <asp:ListItem Value="15">15</asp:ListItem>
                <asp:ListItem Value="16">16</asp:ListItem>
                <asp:ListItem Value="17">17</asp:ListItem>
                <asp:ListItem Value="18">18</asp:ListItem>
            </asp:DropDownList>
            <label for="time_hours">MM:</label>
            <asp:DropDownList ID="time_minutes" runat="server">
                <asp:ListItem Value="00">00</asp:ListItem>
                <asp:ListItem Value="15">15</asp:ListItem>
                <asp:ListItem Value="30">30</asp:ListItem>
                <asp:ListItem Value="45">45</asp:ListItem>
            </asp:DropDownList>
            <p />
                <label for="add_jobDescription">Description of problem/work to be carried out:</label><br />
                <asp:TextBox ID="add_jobDescription" runat="server" TextMode="MultiLine" Rows="10"></asp:TextBox><br />
                
            </div>
</div>

<p class="right">
    <asp:Button ID="Button1" runat="server" CssClass="button" onclick="Submit_Click" text="Make booking request!" />
</p>
</asp:Content>
