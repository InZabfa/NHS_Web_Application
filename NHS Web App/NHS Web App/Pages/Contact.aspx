<%@ Page Title="Contact | NHS Management System" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="NHS_Web_App.Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>

    <link href="../../Styles/dropdown-theme.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager runat="server" />
    <div class="nhs-center-header">
        <h1>Contact</h1>
        <p style="text-align: center;">Here you can contact other practices and transfer patients...</p>
    </div>

    <div id="patientTransferal" runat="server"></div>
    <div class="boxes">
        <div class="column size70">
            <div class="box">
                <div class="box-header">
                    <h2>Practices</h2>
                    <p>Select a practice to view details or actions associated with this practice...</p>
                </div>
                <div>
                    <fieldset>
                        <asp:Label Text="Practice:" runat="server" />
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList AutoPostBack="true" ID="selected_practice" runat="server" class="search_picker md-select2">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </div>
                <div>
                    <% if (GetSelectedPractice() != null)
                        { %>
                    <fieldset>
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <hr />
                                <fieldset>
                                    <p><strong>Email:</strong> <a href="mailto:<%= GetSelectedPractice().Email %>"><%= GetSelectedPractice().Email %></a></p>
                                </fieldset>
                                <fieldset>
                                    <p><strong>Phone:</strong> <a href="tel:<%= GetSelectedPractice().Phone_Number %>"><%= GetSelectedPractice().Phone_Number %></a></p>
                                </fieldset>
                                <fieldset>
                                    <p><strong>Address:</strong> <%= GetSelectedPractice().Address %></p>
                                </fieldset>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="selected_practice" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </fieldset>
                    <% } %>
                </div>
            </div>
            <% if (HasPermission(Permissions.TRANSFER_PATIENT))
                { %>
            <div class="box">
                <div class="box-header">
                    <h2>Patient</h2>
                    <p>Patient to modify practice...</p>
                </div>
                <div>
                    <fieldset>
                        <asp:Label Text="Patient:" runat="server" />
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList AutoPostBack="true" ID="selected_patient" runat="server" class="search_picker md-select2">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </div>
            </div>
            <% } %>
        </div>

        <div class="column size30">
            <div class="box">
                <div class="box-header">
                    <h2>Manage</h2>
                    <p>Actions associated with this practice...</p>
                </div>
                <asp:Button Text="Transfer patient to this practice..." Enabled="<%# HasPermission(Permissions.TRANSFER_PATIENT) %>" CssClass="button long" ID="btnTransfer" OnClick="btnTransfer_Click" runat="server" />
            </div>
            <div class="box">
                <div class="box-header">
                    <h2>Add practice</h2>
                    <p>If practice is not on the list add it here...</p>
                </div>
                <a href="<%= HasPermission(Permissions.MODIFY_GP_INFO) ? DataLayer.Helper.PageAddress(DataLayer.Helper.Pages.CREATE_PRACTICE) : "#" %>" class="button long <%= HasPermission(Permissions.MODIFY_GP_INFO) ? "" : "disabled" %>">Create a practice...</a>
            </div>
        </div>
    </div>

    <script>
        function pageLoad() {
            $(".search_picker").select2();
        }
    </script>
</asp:Content>
