<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="PatientCondition.aspx.cs" Inherits="NHS_Web_App.Pages.Create.PatientCondition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>

    <link href="../../Styles/dropdown-theme.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="topActionButtons" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="quickActions" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager runat="server" />
    <div class="nhs-center-header">
        <h1>Add Condition</h1>
        <p>Pick the condition to associate with this patient below...</p>
    </div>
    <div class="boxes">
        <div class="column size70">
            <div class="box">
                <div class="box-header">
                    <h2>Patient</h2>
                    <p>Pick the patient below...</p>
                </div>
                <div>
                    <fieldset>
                        <asp:Label Text="Patient:" runat="server" />
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList AutoPostBack="true" ID="select_patients" runat="server" class="search_picker md-select2">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </div>
            </div>
            <div class="box">
                <div class="box-header">
                    <h2>Condition</h2>
                    <p>Pick or input condition details...</p>
                </div>
                <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <fieldset>
                            <asp:Label Text="Condition:" runat="server" />
                            <asp:DropDownList AutoPostBack="false" ID="select_conditions" runat="server" class="search_picker md-select2">
                            </asp:DropDownList>
                        </fieldset>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="select_patients" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="box">
                <div class="box-header">
                    <h2>Additional details</h2>
                    <p>Enter any additional details...</p>
                </div>
                <fieldset>
                    <asp:Label Text="Additional comments:" runat="server" />
                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txtAdditionalComments" MaxLength="700" />
                </fieldset>
            </div>
        </div>
        <div class="column size30">
            <div class="button-box">
                <div class="box">
                    <asp:Label Text="If all details are correct, and you have the correct authorisation, click or press the button below..." runat="server" />
                </div>
                <asp:Button Text="Continue..." CssClass="button long" runat="server" ID="btnContinue" OnClick="BtnContinue_Click" />
            </div>
        </div>
    </div>

    <script>
        function pageLoad() {
            $(".search_picker").select2();
        }
    </script>
</asp:Content>
