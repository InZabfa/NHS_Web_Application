<%@ Page Title="Staff | NHS Management System" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Staff.aspx.cs" Inherits="NHS_Web_App.Staff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="nhs-center-header">
        <h1>Staff</h1>
        <p style="text-align: center;">View and manage staff below...</p>
    </div>
    <div class="boxes">
        <div class="column size70">
            <div class="box">
                <div class="box-header">
                    <h2>Colleagues</h2>
                </div>
                <div id="colleague_list" runat="server"></div>
            </div>
        </div>
        <div class="column size30">
            <div class="box">
                <div class="box-header">
                    <h2>Manage</h2>
                    <p>Manage Staff in this Practice...</p>
                </div>
                <fieldset>
                    <%-- Add this later : OnClick="btn_AddStaff_Clicked" --%>
                    <a href="<%= DataLayer.Helper.PageAddress(DataLayer.Helper.Pages.CREATE_STAFF) %>" class="button long">Create Staff...</a>
                </fieldset>
            </div>
        </div>
    </div>
</asp:Content>
