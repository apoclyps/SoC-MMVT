<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/TwoColView.Master" AutoEventWireup="true" CodeBehind="CustomerDetails.aspx.cs" Inherits="MMVT.StaffPages.WebForm1" %>
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
    <asp:Panel ID="vehicleDetails" runat="server" CssClass="grouping_box" Visible="false">
            <div class="half_width_field_container_left">
                <div class="full_width_field_container">
                    <label for="vehicleEditRegistration">Vehicle registration number</label><br /><asp:TextBox ID="vehicleEditRegistration" runat="server"></asp:TextBox><br />
                </div>
                <div class="full_width_field_container">
                    <label for="vehicleEditManufacturer">Manufacturer</label><br /><asp:TextBox ID="vehicleEditManufacturer" runat="server"></asp:TextBox>
                </div>

            </div>


            <div class="half_width_field_container_right">
             <div class="half_width_field_container_left">
                    <label for="vehicleEditMiles">Millage</label><br /><asp:TextBox ID="vehicleEditMiles" runat="server"></asp:TextBox>
                </div>

                <div class="half_width_field_container_right">
                    <label for="vehicleEditYear">Year</label><asp:TextBox ID="vehicleEditYear" runat="server"></asp:TextBox>
                </div>
                <div class="full_width_field_container">
                    <label for="vehicleEditModel">Model</label><br /><asp:DropDownList ID="vehicleEditModel" runat="server"></asp:DropDownList>
                </div>

                <div class="half_width_field_container_left">
                   <p />
                </div>

                <div class="half_width_field_container_right">
                    <asp:Button ID="saveVehicleBtn" runat="server" text="Save" CssClass="button" OnClick="saveVehicle_click" />
                </div>
            </div>
    </asp:Panel>
    
    <p />
    <asp:Panel ID="vehicleHistory" runat="server" Visible="false">
    
    </asp:Panel>
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="RightColumn" runat="server">
    <div class="grouping_box">
        <asp:Panel ID="customerDetails" runat="server">
        
        </asp:Panel>

        <asp:Panel ID="editCustomerDetails" runat="server" Visible="false">
            <div class="half_width_field_container_left">
                <label for="edit_firstName">First name</label><br /><asp:TextBox ID="edit_firstName" runat="server"></asp:TextBox>
            </div>
            <div class="half_width_field_container_right">
                <label for="edit_surname">Surname</label><br /><asp:TextBox ID="edit_surname" runat="server"></asp:TextBox>
            </div>

            <div class="full_width_field_container">
                <label for="edit_addressL1">Address</label><br /><asp:TextBox ID="edit_addressL1" runat="server"></asp:TextBox><br />
                <asp:TextBox ID="edit_addressL2" runat="server"></asp:TextBox><br />
            </div>

            <div class="half_width_field_container_left">
                <label for="edit_town">Town</label><br /><asp:TextBox ID="edit_town" runat="server"></asp:TextBox>
            </div>
            <div class="half_width_field_container_right">
                <label for="edit_postcode">Postcode</label><br /><asp:TextBox ID="edit_postcode" runat="server"></asp:TextBox>
            </div>

            <div class="full_width_field_container">
                <label for="edit_homePhone">Home phone number</label><br /><asp:TextBox ID="edit_homePhone" runat="server"></asp:TextBox><br />
                <label for="edit_mobilePhone">Mobile phone number</label><br /><asp:TextBox ID="edit_mobilePhone" runat="server"></asp:TextBox><br />
                <label for="edit_email">Email address</label><br /><asp:TextBox ID="edit_email" runat="server"></asp:TextBox><br />

                <p class="right"><asp:Button ID="edit_customer_save" runat="server" Text="Save" CssClass="button" OnClick="saveCustomerClick" /></p>
            </div>

        </asp:Panel>
    </div>
    
    <p />

    <div class="grouping_box">
        <asp:Panel ID="customerVehicles" runat="server">


        </asp:Panel>
    </div>
</asp:Content>
