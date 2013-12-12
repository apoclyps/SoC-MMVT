<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/TwoColView.Master" AutoEventWireup="true" CodeBehind="Requests.aspx.cs" Inherits="MMVT.StaffPages.Requests" %>
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
        
        <h1>Booking requests</h1>

        <asp:Panel ID="todays_bookings" runat="server">
        </asp:Panel>

    </asp:Content>

    <asp:Content ID="Content4" ContentPlaceHolderID="RightColumn" runat="server">


        <p />

   </asp:Content> 