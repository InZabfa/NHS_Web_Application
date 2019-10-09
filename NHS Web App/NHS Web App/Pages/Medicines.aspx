<%@ Page Title="Medicines | NHS Management System" EnableViewState="false" ViewStateMode="Disabled" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Medicines.aspx.cs" Inherits="NHS_Web_App.Medicines" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ViewStateMode="Disabled" ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager runat="server" />
    <div class="nhs-center-header">
        <h1>Medicines and more</h1>
        <p style="text-align: center;">Modify and view medicines and more...</p>
    </div>

    <div class="boxes">
        <div class="column size70">
            <div class="box">
                <div class="box-header">
                    <h2>Medication</h2>
                    <p>Summary of medications...</p>
                </div>
                <div id="medicines_list" data-empty-text="No medicines exist..." runat="server">
                </div>
            </div>
            <div class="box">
                <div class="box-header">
                    <h2>Conditions</h2>
                    <p>Summary of conditions...</p>
                </div>
                <div id="conditions_list" data-empty-text="No conditions exist..." runat="server"></div>
            </div>
            <div class="box">
                <div class="box-header">
                    <h2>Providers</h2>
                    <p>Summary of providers...</p>
                </div>
                <div id="providers_list" data-empty-text="No providers exist..." runat="server"></div>
            </div>
        </div>
        <div class="column size30">
            <div class="box">
                <div class="box-header">
                    <h2>Manage</h2>
                    <hr />
                </div>
                <a class="button long <%= HasPermission(NHS_Web_App.BasePage.Permissions.MODIFY_CONDITION) ? "" :"disabled" %>" href="<%= HasPermission(NHS_Web_App.BasePage.Permissions.MODIFY_CONDITION) ? DataLayer.Helper.PageAddress(DataLayer.Helper.Pages.CREATE_CONDITION) : "#" %>">Add condition...</a>
                <a class="button long <%= HasPermission(NHS_Web_App.BasePage.Permissions.MODIFY_MEDICINE) ? "" : "disabled" %>" href="<%= HasPermission(NHS_Web_App.BasePage.Permissions.MODIFY_MEDICINE) ? DataLayer.Helper.PageAddress(DataLayer.Helper.Pages.CREATE_MEDICINE) : "#" %>">Add medicine...</a>
                <a class="button long <%= HasPermission(NHS_Web_App.BasePage.Permissions.CREATE_PROVIDER_INFO) ? "" : "disabled" %>" href="<%= HasPermission(NHS_Web_App.BasePage.Permissions.CREATE_PROVIDER_INFO) ? DataLayer.Helper.PageAddress(DataLayer.Helper.Pages.CREATE_PROVIDER_INFO) : "#" %>">Add provider info...</a>
            </div>
        </div>
    </div>
</asp:Content>
