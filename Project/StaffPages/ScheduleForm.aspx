<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/TwoColView.Master" AutoEventWireup="true" CodeBehind="ScheduleForm.aspx.cs" Inherits="MMVT.ScheduleForm" %>
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
        
        <h1>Bookings</h1>
        <p class="right"><asp:Button ID="showAddBooking" runat="server" OnClick="showAddBooking_click" Text="Add new booking" CssClass="button" /></p>

        <asp:Panel ID="todays_bookings" runat="server">
        </asp:Panel>

    </asp:Content>

    <asp:Content ID="Content4" ContentPlaceHolderID="RightColumn" runat="server">
        <div class="grouping_box_calender">
            <h1><asp:label ID="calenderHeader" runat="server"></asp:label></h1>
           
                <asp:Calendar ID="SelectCalender" runat="server" EnableViewState="true" 
                OnSelectionChanged="updateSelectedDate" SelectedDate="2012-03-18" 
                Width="100%"></asp:Calendar>
        </div>
        <p />
        <asp:Panel ID="JobDetailsPanel" runat="server" CssClass="grouping_box" Visible="false">
            
        </asp:Panel>

        <asp:Panel ID="AddJobPanel" runat="server" CssClass="grouping_box" Visible="false">
            <div class="half_width_field_container_left"><label for="add_jobTime">Time</label><br />
            <asp:TextBox ID="add_jobTime" runat="server"></asp:TextBox></div>
            <div class="half_width_field_container_right"><label for="add_jobPriority">Priority</label><br />
            <asp:DropDownList ID="add_jobPriority" runat="server">
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="2">2</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
                <asp:ListItem Value="5">5</asp:ListItem>
                </asp:DropDownList></div>
            <div class="full_width_field_container">
                <label for="add_jobDescription">Description</label><br />
                <asp:TextBox ID="add_jobDescription" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox><br />
                <asp:Button ID="add_jobSubmit" runat="server" CssClass="button" OnClick="add_jobSubmitClick" Text="Next..." />
            </div>
        </asp:Panel>

        <p />

        
    </asp:Content>


