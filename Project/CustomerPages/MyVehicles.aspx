<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThreeColView.Master" AutoEventWireup="true" CodeBehind="MyVehicles.aspx.cs" Inherits="MMVT.MyVehicles" %>
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

     <asp:Button ID="btnManageDetails" runat="server" OnClick="manageDetails_onClick" Text="Manage Personal Details" CssClass="button2" /><br />
     <asp:Button ID="btnManageVehicle" runat="server" OnClick="manageVehicles_onClick" Text="Manage Vehicles" CssClass="button2" /><br />
     <asp:Button ID="btnAddVehicle" runat="server" OnClick="addVehicle_onClick" Text="Add Vehicle" CssClass="button2_lighter" /><br />

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CentreColumn" runat="server">

<asp:Panel ID="CustomerPanel" runat="server">
    
    <div class ="grouping_box">
    
        <!-- Title -->
        <div class ="full_width_field_container">
            <h1>Personal Details</h1> <asp:Label ID="lblUpdated" runat="server" Text="Updated"></asp:Label>
        </div>
    
        <!-- Name -->
        <div class ="half_width_field_container_left">
                <asp:Label ID="lblFirstname" runat="server" Text="First Name"></asp:Label><br />
                 <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
        </div>

        <!-- Surname -->
        <div class ="half_width_field_container_right">
                <asp:Label ID="lblSurname" runat="server" Text="Surname"></asp:Label><br />
                <asp:TextBox ID="txtSurname" runat="server"></asp:TextBox>
        </div>
    
        <!-- Address -->
        <div class="full_width_field_container">
            <asp:Label ID="lblAddress" runat="server" Text="Address Line 1"></asp:Label><br />
            <asp:TextBox ID="txtAddressLine1" runat="server"></asp:TextBox><br />
            <asp:Label ID="lblAddress2" runat="server" Text="Address Line 2"></asp:Label><br />
            <asp:TextBox ID="txtAddressLine2" runat="server"></asp:TextBox><br />
        </div>

        <!-- Town -->
        <div class ="half_width_field_container_left">
                 <asp:Label ID="lblTown" runat="server" Text="Town"></asp:Label><br />
            <asp:TextBox ID="txtTown" runat="server"></asp:TextBox> 
        </div>

        <!-- Postcode -->
        <div class ="half_width_field_container_right">
                  <asp:Label ID="lblPostcode" runat="server" Text ="Postcode"></asp:Label><br />
                  <asp:TextBox ID="txtPostcode" runat="server"></asp:TextBox>
        </div>
    
        <!-- Phone -->
        <div class ="full_width_field_container">
                <asp:Label ID="lblPhone" runat="server" Text ="Phone Number"></asp:Label><br />
                <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
        </div>

        <!-- Mobile -->
        <div class ="full_width_field_container">
                <asp:Label ID="lblMobile" runat="server" Text ="Mobile Number"></asp:Label><br />
                <asp:TextBox ID="txtMobile" runat="server"></asp:TextBox>
        </div>

        <!-- Email -->
        <div class ="full_width_field_container">
             <asp:Label ID="lblEmailAddress" runat="server" Text ="Email Address"></asp:Label><br />
             <asp:TextBox ID="txtEmailAddress" runat="server"></asp:TextBox>
        </div>

        <!-- Save -->
        <asp:Button ID="btnSavePersonalDetails" runat="server" OnClick="UpdatePersonalDetails_onClick" Text="Save" CssClass="button" /><br />
    </div>
</asp:Panel>

<asp:Panel ID="editVehiclePanel" runat="server">

    <div class ="grouping_box">
    
        <!-- Title -->
        <div class ="full_width_field_container">
            <h1>Vehicle Details</h1>
        </div>
    
        <!-- Registration Number -->
        <div class ="full_width_field_container">
                <asp:Label ID="lblRegistrationNumber" runat="server" Text ="Registration Number"></asp:Label><br />
                <asp:TextBox ID="txtRegistartionNumber" runat="server"></asp:TextBox>
        </div>

        <!-- Type -->
        <div class ="full_width_field_container">
                <asp:Label ID="lblType" runat="server" Text ="Type"></asp:Label><br />
                <asp:TextBox ID="txtType" runat="server"></asp:TextBox>
        </div>

        <!-- Manufacturer -->
        <div class ="full_width_field_container">
                <asp:Label ID="lblManufacturer" runat="server" Text ="Manufacturer"></asp:Label><br />
                <asp:TextBox ID="txtManufacturer" runat="server"></asp:TextBox>
        </div>

        <!-- Model -->
        <div class ="full_width_field_container">
                <asp:Label ID="lblModel" runat="server" Text ="Model"></asp:Label><br />
                <asp:TextBox ID="txtModel" runat="server"></asp:TextBox>
        </div>


        <!-- Milage -->
        <div class ="half_width_field_container_left">
                <asp:Label ID="lblMilage" runat="server" Text="Milage"></asp:Label><br />
                 <asp:TextBox ID="txtMilage" runat="server"></asp:TextBox>
        </div>

        <!-- Year -->
        <div class ="half_width_field_container_right">
                <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label><br />
                <asp:TextBox ID="txtYear" runat="server"></asp:TextBox>
        </div>

         <!-- Modified -->
        <div class ="full_width_field_container">
                <asp:Label ID="lblModified" runat="server" Text ="Modified"></asp:Label><br />
            <asp:DropDownList ID="DDLModified" runat="server">
                     <asp:ListItem Text="No" Value="false"/>
                     <asp:ListItem Text="Yes" Value="true"/>
            </asp:DropDownList><br />
              
        </div>

        <!-- Save -->
        <asp:Button ID="btnSaveVehicle" runat="server" OnClick="SaveVehicle_onClick" Text="Save" CssClass="button" /><br />

    </div>
    </asp:Panel>

<!-- ADD VEHICLE -->
    <asp:Panel ID="AddVehiclePanel" runat="server">

    <div class ="grouping_box">
    
        <!-- Title -->
        <div class ="full_width_field_container">
            <h1>Vehicle Details</h1>
        </div>
    
        <!-- Registration Number -->
        <div class ="full_width_field_container">
                <asp:Label ID="lblAddRegistrationNumber" runat="server" Text ="Registration Number"></asp:Label><br />
                <asp:TextBox ID="txtAddRegistrationNumber" runat="server"></asp:TextBox>
        </div>

        <!-- Type 
        <div class ="full_width_field_container">
                <asp:Label ID="lblAddType" runat="server" Text ="Type"></asp:Label><br />
                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        </div>
        -->

        <!-- Manufacturer -->
        <div class ="full_width_field_container">
                <asp:Label ID="lblAddManufacturer" runat="server" Text ="Manufacturer"></asp:Label><br />
                <asp:TextBox ID="txtAddManufacturer" runat="server"></asp:TextBox>
        </div>

        <!-- Model -->
        <div class ="full_width_field_container">
                <asp:Label ID="lblAddModel" runat="server" Text ="Model"></asp:Label><br />
                <asp:DropDownList ID ="AddModel" runat="server"></asp:DropDownList>
        </div>

        <!-- Milage -->
        <div class ="half_width_field_container_left">
                <asp:Label ID="lblAddMilage" runat="server" Text="Milage"></asp:Label><br />
                 <asp:TextBox ID="txtAddMilage" runat="server"></asp:TextBox>
        </div>

        <!-- Year -->
        <div class ="half_width_field_container_right">
                <asp:Label ID="lblAddYear" runat="server" Text="Year"></asp:Label><br />
                <asp:TextBox ID="txtAddYear" runat="server"></asp:TextBox>
        </div>

         <!-- Modified -->
        <div class ="full_width_field_container">
                <asp:Label ID="lblAddModified" runat="server" Text ="Modified"></asp:Label><br />
            <asp:DropDownList ID="ddlAddModified" runat="server">
                     <asp:ListItem Text="No" Value="false"/>
                     <asp:ListItem Text="Yes" Value="true"/>
            </asp:DropDownList><br />
              
        </div>

        <!-- Add -->
        <asp:Button ID="btnAddNewVehicle" runat="server" OnClick="addNewVehicle_onClick" Text="Add" CssClass="button" /><br />

    </div>
    </asp:Panel>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="RightColumn" runat="server">

<!--<asp:Label ID="lblManageVehicles" runat="server" Text="Manage Vehicles" ></asp:Label><br />-->

<asp:Panel ID="vehicleListPanel" runat="server">

</asp:Panel>

</asp:Content>
