<%@ Page Title="Patients | NHS Management System" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Patients.aspx.cs" Inherits="NHS_Web_App.Patients" %>

<%@ Import Namespace="DataLayer" %>
<%@ Import Namespace="NHS_Web_App" %>
<%@ Register Src="~/Controls/PatientListItem.ascx" TagPrefix="uc1" TagName="PatientListItem" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>
    <link href="../../Styles/dropdown-theme.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager runat="server" />
    <script type="text/javascript">
        Sys.Application.add_init(function () {
            var list = $get("tags_searchbox");
            $addHandler(list, "keyup", ClientSelectedIndexChanged);
            $addHandler(list, "mouseup", ClientSelectedIndexChanged);
        });
        function ClientSelectedIndexChanged(e) {
            setTimeout("__doPostBack('tags_searchbox','')", 0);
        }
    </script>
    <div class="nhs-center-header">
        <h1>People</h1>
        <p style="text-align: center;">Search, view, add patients and create users...</p>
    </div>

    <div class="boxes">
        <div class="column size70">
            <div class="box">
                <div class="box-header">
                    <h2>Patients</h2>
                    <p>View patients below...</p>
                </div>
                <asp:UpdatePanel ID="panel_patients" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <fieldset>
                            <asp:ListBox ID="tags_searchbox" ClientIDMode="Static" SelectionMode="Multiple" CssClass="tags_search md-select2" runat="server"></asp:ListBox>
                        </fieldset>
                        <div id="patients_container">
                            <% var array = (from u1 in (GetRole() == ROLES.RECEPTIONIST || GetRole() == ROLES.ADMIN ? DB.PatientsGetAll() : DB.PatientsGet(DB.StaffGet(LoggedInUser))) where DB.HasConditions(u1.User.Id, tag_values.Value) select u1); %>
                            <% if (array.Count() > 0)
                                {
                                    foreach (BusinessObject.Patient patient in array)
                                    { %>
                            <div class="list_item">
                                <a href="/Pages/View/Profile.aspx?id=<%= patient.UserID %>">
                                    <div>
                                        <div>
                                            <img src="/img/default-picture.png" alt="Profile Picture" />
                                        </div>
                                        <div class="content">
                                            <p><%= String.Format("{0} {1}", patient.User.Forename, patient.User.Surname) %></p>
                                        </div>
                                        <div>
                                            <i class="fas fa-search"></i>
                                        </div>
                                    </div>
                                </a>
                            </div>
                            <% }
                                }
                                else
                                { %>
                            <br />
                            <p>You don't have any patients.</p>
                            <% } %>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="column size30">
            <div class="box">
                <div class="box-header">
                    <h2>Manage</h2>
                    <p>Manage your patients...</p>
                </div>
                <% if (HasPermission(NHS_Web_App.BasePage.Permissions.CREATE_PATIENT))
                    {%>
                <a href="<%= Helper.PageAddress(Helper.Pages.CREATE_PAITENT) %>" class="button long">Appoint patient...</a>
                <% } %>
                <% if (HasPermission(NHS_Web_App.BasePage.Permissions.ADD_STAFF))
                    { %>
                <a href="<%= Helper.PageAddress(Helper.Pages.CREATE_USER) %>" class="button long">Create User...</a>
                <%} %>
            </div>
        </div>
    </div>
    <asp:HiddenField runat="server" ID="tag_values" ClientIDMode="Static" />
    <script>
        function pageLoad() {
            $(".tags_search").select2({
                placeholder: "Filter patients...",
                allowClear: true
            });

            $('#tags_searchbox').on('change.select2', function (e) {
                var val = $("#tags_searchbox").select2("val");
                $("#tag_values").val(val);
                __doPostBack("<%= panel_patients.ClientID %>", null);
            });
        };
    </script>
</asp:Content>
