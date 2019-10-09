<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="NHS_Web_App.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="topActionButtons" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="quickActions" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="content" runat="server">
    <div class="nhs-center-header">
        <h1>Change your password</h1>
        <p>You can change your password below...</p>
    </div>
    <div class="boxes">
        <div class="column size70">
            <div class="box">
                <div class="box-header">
                    <h2>Confirm Password</h2>
                </div>
                <fieldset>
                    <asp:Label Text="Current Password:" runat="server" />
                    <asp:TextBox ID="txtCurrentPassword" TextMode="Password" runat="server" />
                </fieldset>
            </div>
            <div class="box">
                <div class="box-header">
                    <h2>New Password</h2>
                </div>
                <fieldset>
                    <asp:Label Text="New Password:" runat="server" />
                    <asp:TextBox ID="txtNewPassword" TextMode="Password" runat="server" />
                </fieldset>
                <fieldset>
                    <asp:Label Text="Confirm Password:" runat="server" />
                    <asp:TextBox ID="txtConfirmPassword" TextMode="Password" runat="server" />
                </fieldset>
            </div>
        </div>
        <div class="column size30">
            <div class="button-box">
                <div class="box">
                    <div class="box-header">
                        <h2>Confirm</h2>
                    </div>
                    <p>If your details are correct, please click continue...</p>
                </div>
                <asp:Button Text="Change Password..." CssClass="button long" ID="btnChangePassword" OnClick="btnChangePassword_Click" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
