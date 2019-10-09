<%@ Page Title="Add Medicine | NHS Management System" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Medicine.aspx.cs" Inherits="NHS_Web_App.Pages.Create.Medicine" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>

    <link href="../../Styles/dropdown-theme.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager runat="server" />
    <div class="nhs-center-header">
        <% if (IsEditing)
            { %>
        <h1>Edit Medication</h1>
        <% }
            else
            { %>
        <h1>Add Medication</h1>
        <% } %>
        <p>Enter medicine and provider details below...</p>
    </div>
    <div class="boxes">
        <div class="column size70">
            <div class="box">
                <div class="box-header">
                    <h2>Medicine details</h2>
                    <p>Enter medicine details below...</p>
                </div>
                <div class="input">
                    <fieldset class="half">
                        <asp:Label Text="Name:*" runat="server" />
                        <asp:TextBox runat="server" required="required" Text="Untitled" ID="txtMedicineName" />
                    </fieldset>
                    <fieldset class="half">
                        <asp:Label Text="Quantity:*" runat="server" />
                        <asp:TextBox runat="server" required="required" TextMode="Number" Text="100" ID="txtQuantity" />
                    </fieldset>
                </div>
                <div class="input">
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <fieldset class="half">
                                <asp:Label Text="Max dosage per day:*" runat="server" />
                                <asp:TextBox runat="server" Text="3" AutoPostBack="true" required="required" ID="txtMaxDosagePerDay" TextMode="Number" />
                            </fieldset>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <fieldset class="half">
                                <asp:Label Text="Max dosage per week:*" runat="server" />
                                <asp:TextBox runat="server" Text="2" AutoPostBack="true" required="required" ID="txtMaxDosagePerWeek" TextMode="Number" />
                            </fieldset>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="box">
                <div class="box-header">
                    <h2>Provider</h2>
                    <p>Pick or input provider details...</p>
                </div>
                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <fieldset>
                            <asp:Label Text="Provider:" runat="server" />
                            <asp:DropDownList AutoPostBack="true" ID="select_provider" runat="server" class="search_picker md-select2">
                            </asp:DropDownList>
                        </fieldset>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <fieldset>
                    <asp:Label Text="Click below to add new provider information:" runat="server" />
                    <asp:Button ID="btnAddProviderReturn" OnClick="btnAddProviderReturn_Click" Text="Add provider info then return..." CssClass="button long" runat="server" />
                </fieldset>
            </div>
        </div>
        <div class="column size30">
            <div class="button-box">
                <div class="box">
                    <asp:Label Text="If all details are correct, and you have the correct authorisation, click or press continue below..." runat="server" />
                </div>
                <asp:Button Text="Continue..." Enabled="<%# HasProviders() %>" CssClass="button long" runat="server" ID="btnContinue" OnClick="btnContinue_Click" />
            </div>
        </div>
    </div>

    <script>
        function pageLoad() {
            $(".search_picker").select2();
        }
    </script>
</asp:Content>
