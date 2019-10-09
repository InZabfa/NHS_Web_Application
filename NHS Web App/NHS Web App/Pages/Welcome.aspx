<%@ Page Title="Welcome | NHS Management System" Language="C#" MasterPageFile="~/MainMasterWithoutNavigation.Master" AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="NHS_Web_App.Pages.Welcome" %>

<%@ Register Src="~/Controls/MessageControl.ascx" TagPrefix="uc1" TagName="MessageControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" />
    <div class="nhs-center-header">
        <img src="../img/nhs-logo.svg" alt="NHS Logo" style="margin-bottom: 1em;" />
        <h1>Welcome to the NHS Management System</h1>
        <p>Because there are no accounts created, an administrator account must be created to maintain and manage the system.</p>
        <p>To do so, please type your details below...</p>
    </div>
    <div id="messageContainer" runat="server"></div>
    <div class="boxes">
        <div class="column size70">
            <div class="box">
                <div class="box-header">
                    <h2>Personal details</h2>
                    <p>Information about this administrator...</p>
                </div>

                <div class="input">
                    <fieldset class="half">
                        <asp:Label Text="Forename:" runat="server" />
                        <asp:TextBox required="required" runat="server" ID="txtFName" />
                    </fieldset>
                    <fieldset class="half">
                        <asp:Label Text="Surname:" runat="server" />
                        <asp:TextBox required="required" runat="server" CssClass="" ID="txtLName" />
                    </fieldset>
                </div>
                <div class="input">
                    <fieldset class="half">
                        <asp:Label Text="Gender:" runat="server" />
                        <asp:DropDownList required="required" ID="pckGender" runat="server">
                            <asp:ListItem Text="Male" Value="1" />
                            <asp:ListItem Text="Female" Value="0" />
                        </asp:DropDownList>
                    </fieldset>
                    <fieldset class="half">
                        <asp:Label Text="Date of birth:" runat="server" />
                        <asp:TextBox runat="server" ID="txtDOB" TextMode="Date" />
                    </fieldset>
                </div>
                <fieldset>
                    <asp:Label Text="Phone number:" runat="server" />
                    <asp:TextBox runat="server" ID="txtPersonalPhoneNum" TextMode="Phone" />
                </fieldset>
                <fieldset>
                    <asp:Label Text="Address:" runat="server" />
                    <asp:TextBox runat="server" ID="txtPersonalAddress" TextMode="MultiLine" />
                </fieldset>
            </div>
            <div class="box">
                <div class="box-header">
                    <h2>Login details</h2>
                    <p>These login details will be required to manage and maintain the system...</p>
                </div>
                <div class="input">
                    <fieldset class="half">
                        <asp:Label Text="Email:" runat="server" />
                        <asp:TextBox runat="server" required="required" ID="txtEmail" TextMode="Email" />
                    </fieldset>
                    <fieldset class="half">
                        <asp:Label Text="Password:" runat="server" />
                        <asp:TextBox runat="server" required="required" CssClass="noleft" ID="txtPassword" TextMode="Password" />
                    </fieldset>
                </div>
            </div>
            <div class="box">
                <div class="box-header">
                    <h2>Practice details</h2>
                    <p>Please provide the details of this practice...</p>
                </div>
                <div class="input">
                    <fieldset class="half">
                        <asp:Label Text="Practice Name:" runat="server" />
                        <asp:TextBox runat="server" required="required" ID="txtPracticeName" />
                    </fieldset>
                    <fieldset class="half">
                        <asp:Label Text="Contact email:" runat="server" />
                        <asp:TextBox runat="server" ID="txtPracticeEmail" required="required" TextMode="Email" placeholder="example@domain.co.uk" />
                    </fieldset>
                </div>
                <fieldset>
                    <asp:Label Text="Contact number:" runat="server" />
                    <asp:TextBox runat="server" required="required" ID="txtContactNumber" TextMode="Phone" />
                </fieldset>
                <fieldset>
                    <asp:Label Text="Address:" runat="server" />
                    <asp:TextBox runat="server" required="required" ID="txtPracticeAddress" TextMode="MultiLine" />
                </fieldset>
            </div>
        </div>
        <div class="column size30">
            <div class="button-box">
                <div class="box">
                    <p>Please check and ensure all details entered are correct. If you believe all the details are correct and you are privileged to do so, continue.</p>
                    <hr />
                    <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:CheckBox AutoPostBack="true" Text="I confirm all details entered are correct." ID="chkConfirmDetails" runat="server" CssClass="checkbox" />
                            <asp:CheckBox AutoPostBack="true" Text="I confirm I am privileged to manage and run this system." ID="chkConfirmAccess" runat="server" CssClass="checkbox" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:Button Enabled="<%# checkConfirmed() %>" CssClass="button long" Text="Continue..." runat="server" ID="btnContinue" OnClick="btnContinue_Click" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="chkConfirmDetails" />
                        <asp:PostBackTrigger ControlID="chkConfirmAccess" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
</asp:Content>
