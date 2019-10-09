<%@ Page Title="Account Disabled | NHS Management System" Language="C#" MasterPageFile="~/MainMasterWithoutNavigation.Master" AutoEventWireup="true" CodeBehind="Disabled.aspx.cs" Inherits="NHS_Web_App.Pages.Disabled" %>

<%@ Import Namespace="DataLayer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="nhs-center-header">
        <img src="../img/nhs-logo.svg" alt="NHS Logo" style="margin-bottom: 20px; -moz-user-select: none; -ms-user-select: none; -webkit-user-select: none; user-select: none;" draggable="false" />
        <h1>Account Disabled</h1>
        <p>Your account has been disabled and you can no longer log into the NHS Management System.</p>

        <% if (LoggedInUser.Practice_Info != null)
            { %>
        <p>If you believe this is an error, contact support at: </p>
        <a href="mailto:<%= LoggedInUser.Practice_Info.Email ?? string.Empty %>"><%= LoggedInUser.Practice_Info.Email ?? "n/a" %></a>
        <% }
            else
            { %>
        <p>If you believe this is an error, contact support.</p>
        <% } %>
        <p>Alternatively, you can try and login again:</p>
        <a class="button" href="<%= Helper.PageAddress(Helper.Pages.LOGOUT) %>">Try again...</a>
    </div>
</asp:Content>
