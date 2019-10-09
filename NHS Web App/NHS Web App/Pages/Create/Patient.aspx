<%@ Page Title="Create patient | NHS Management System" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Patient.aspx.cs" Inherits="NHS_Web_App.Pages.Create.Patient" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>
    <link href="../../Styles/dropdown-theme.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager runat="server" />
    <div class="nhs-center-header">
        <h1>Appoint patient...</h1>
        <p>This shows all users without allocated doctor, use this to allocate patients to the doctor...</p>
    </div>
    <div id="messageContainer" runat="server"></div>
    <div class="boxes">
        <div class="column size70">
            <div class="box">
                <div class="box-header">
                    <h2>Patient</h2>
                    <p>Which patient would you like to associate...</p>
                </div>
                <div>
                    <fieldset>
                        <asp:Label Text="Patient:" runat="server" />
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList AutoPostBack="true" ID="select_patients" OnSelectedIndexChanged="select_patients_SelectedIndexChanged" runat="server" class="search_picker md-select2">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </div>
            </div>
            <div class="box">
                <div class="box-header">
                    <h2>Associated doctor</h2>
                    <p>Choose the doctor to associate this patient with...</p>
                </div>
                <div>
                    <fieldset class="">
                        <asp:Label Text="Doctor:" runat="server" />
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList AutoPostBack="true" ID="select_doctors" OnSelectedIndexChanged="select_doctors_SelectedIndexChanged" runat="server" class="search_picker md-select2">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </div>
            </div>
        </div>
        <div class="column size30">
            <div class="box">
                <div class="box-header">
                    <h2>Notifications</h2>
                    <p>Patient notification settings...</p>
                </div>
                <asp:CheckBox Text="Allow email notifications" ID="chckEmailNotifications" Checked="true" runat="server" />
            </div>
            <div class="button-box">
                <div class="box">
                    <p>Please check and ensure that correct doctor and patient are selected. If you believe that everything is correct and you are privileged to do so, continue.</p>
                    <hr />
                    <%--<asp:CheckBox AutoPostBack="true" Text="I confirm all details entered are correct." ID="chkConfirmDetails" runat="server" CssClass="checkbox" />--%>
                    <%--<asp:CheckBox AutoPostBack="true" Text="I confirm I am privileged to manage and run this system." ID="chkConfirmAccess" runat="server" CssClass="checkbox" />--%>
                </div>
                <asp:UpdatePanel runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:Button Enabled="true" PostBackUrl="~/Pages/Create/Patient.aspx" CssClass="button long" Text="Continue..." runat="server" ID="btnContinue" OnClick="btnContinue_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <script>
        function pageLoad() {
            $(".search_picker").select2();
        };
    </script>
</asp:Content>
