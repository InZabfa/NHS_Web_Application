<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="NHS_Web_App.Pages.Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager runat="server" />
    <div class="nhs-center-header">
        <h1>Search results for '<%= (Request.QueryString["q"] ?? "").ToString() %>'...</h1>
    </div>
    <div class="boxes">
        <div class="column size70">
            <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <% if (chkAppointments.Checked && HasPermission(Permissions.VIEW_APPOINTMENTS))
                        { %>
                    <div class="box">
                        <div class="box-header">
                            <h2>Appointments</h2>
                        </div>
                        <div id="appointments" runat="server">
                            <asp:Label Text="No appointments found..." runat="server" />
                        </div>
                    </div>
                    <% } %>
                    <% if (chkPatients.Checked && HasPermission(Permissions.VIEW_PATIENTS))
                        { %>
                    <div class="box">
                        <div class="box-header">
                            <h2>Patients</h2>
                        </div>
                        <div id="patients" runat="server">
                            <asp:Label Text="No patients found..." runat="server" />
                        </div>
                    </div>
                    <% } %>
                    <% if (chkMedications.Checked && HasPermission(Permissions.VIEW_MEDICINES))
                        { %>
                    <div class="box">
                        <div class="box-header">
                            <h2>Medicines</h2>
                        </div>
                        <div id="medicines" runat="server">
                            <asp:Label Text="No medicines found..." runat="server" />
                        </div>
                    </div>
                    <% } %>
                    <% if (chkConditions.Checked && HasPermission(Permissions.VIEW_MEDICINES))
                        { %>
                    <div class="box">
                        <div class="box-header">
                            <h2>Conditions</h2>
                        </div>
                        <div id="conditions" runat="server">
                            <asp:Label Text="No conditions found..." runat="server" />
                        </div>
                    </div>
                    <% } %>
                    <% if (chkStaff.Checked)
                        { %>
                    <div class="box">
                        <div class="box-header">
                            <h2>Staff</h2>
                        </div>
                        <div id="staff" runat="server">
                            <asp:Label Text="No staff found..." runat="server" />
                        </div>
                    </div>
                    <% } %>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="chkPatients" />
                    <asp:PostBackTrigger ControlID="chkConditions" />
                    <asp:PostBackTrigger ControlID="chkStaff" />
                    <asp:PostBackTrigger ControlID="chkMedications" />
                    <asp:PostBackTrigger ControlID="chkAppointments" />
                </Triggers>
            </asp:UpdatePanel>
        </div>

        <div class="column size30">
            <div class="box">
                <div class="box-header">
                    <h2>Search filters</h2>
                    <p>Refine your search...</p>
                </div>

                <asp:UpdatePanel UpdateMode="Always" runat="server">
                    <ContentTemplate>
                        <fieldset>
                            <asp:CheckBox CssClass="checkbox" Enabled="<%#  HasPermission(Permissions.VIEW_PATIENTS) %>" AutoPostBack="true" Checked="true" Text="Patients" ID="chkPatients" runat="server" />
                            <asp:CheckBox CssClass="checkbox" Enabled="<%#  HasPermission(Permissions.MODIFY_MEDICINE) %>" AutoPostBack="true" Checked="true" Text="Conditions" ID="chkConditions" runat="server" />
                            <asp:CheckBox CssClass="checkbox" Enabled="<%#  HasPermission(Permissions.VIEW_STAFF) %>" AutoPostBack="true" Checked="true" Text="Staff" ID="chkStaff" runat="server" />
                            <asp:CheckBox CssClass="checkbox" Enabled="<%#  HasPermission(Permissions.VIEW_MEDICINES) %>" AutoPostBack="true" Checked="true" Text="Medications" ID="chkMedications" runat="server" />
                            <asp:CheckBox CssClass="checkbox" Enabled="<%#  HasPermission(Permissions.VIEW_APPOINTMENTS) %>" AutoPostBack="true" Checked="true" Text="Appointments" ID="chkAppointments" runat="server" />
                        </fieldset>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <style>
        .checkbox {
            margin-top: 0;
        }
    </style>
</asp:Content>
