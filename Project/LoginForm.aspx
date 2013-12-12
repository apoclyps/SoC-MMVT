<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/TwoColView.Master" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="MMVT.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainMenu" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LeftColumn" runat="server">
    <h1>MVT Online services</h1>
    <p>
        An introduction to the online services and some of the benifits they might provide for the customer.
    </p>
    <p>
        <ul>
            <li>Make bookings online</li>
            <li>Keep a record of your service history</li>
            <li>...</li>
        </ul>
    </p>
    <p>
         Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi placerat diam at neque suscipit sed vehicula massa euismod. In sed mi erat. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Quisque eu turpis a ipsum lobortis iaculis id nec nunc. Pellentesque accumsan felis sit amet libero varius sit amet viverra quam pulvinar. Aliquam laoreet, orci gravida pulvinar lobortis, lorem dui iaculis dui, vitae facilisis justo velit et turpis. Pellentesque diam velit, commodo in dapibus vitae, dictum at turpis. Nullam quis mauris sit amet odio ultricies egestas at eu lacus. Quisque porta massa nec sapien tempor gravida. Nullam consequat luctus ipsum sit amet ullamcorper. Proin laoreet tincidunt tortor sed commodo.
    </p>
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="RightColumn" runat="server">
    <div class="grouping_box">
        <h1>Already have an account?</h1>
        <label for = "username">Username</label><br /><asp:TextBox ID="username" runat="server"></asp:TextBox>
        <label for="password">Password</label><br /><asp:TextBox ID="password" runat="server"></asp:TextBox>
        <p class="right">
            <asp:Button ID="login" runat="server" Text="Login" CssClass="button" OnClick="loginClick" />
        </p>
    </div>

    <p class="center">
        Create an account to access our online features...<br />

        <asp:Button ID="signup" runat="server" text="Sign up" CssClass="button" OnClick="signupClick" />
    </p>

</asp:Content>
