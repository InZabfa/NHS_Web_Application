<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Practice.aspx.cs" Inherits="NHS_Web_App.Pages.Create.Practice" %>

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
        <h1>Add Practice</h1>
        <p style="text-align: center;">Here you can add a practice to the list...</p>
    </div>
    <div id="addingPracticeInfo" runat="server"></div>
    <div class="boxes">
        <div class="column size70">
            <div class="box">
                <div class="box-header">
                    <h2>Add details of the practice</h2>
                    <p>Enter all the information about the practice</p>
                </div>
                <fieldset>
                    <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:Label Text="Practice Name:*" runat="server" />
                            <asp:TextBox required="required" ID="practiceName" MaxLength="70" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </fieldset>
                <fieldset>
                    <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:Label Text="Practice Phone no.:*" runat="server" />
                            <asp:TextBox required="required" ID="practicePhNo" MaxLength="30" TextMode="Phone" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </fieldset>
                <fieldset>
                    <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:Label Text="Practice E-mail:*" runat="server" />
                            <asp:TextBox required="required" ID="practiceEmail" MaxLength="254" TextMode="Email" placeholder="example@domain.co.uk" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </fieldset>
                <fieldset>
                    <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:Label Text="Practice Address:*" runat="server" />
                            <asp:TextBox runat="server" required="required" ID="practiceAddress" MaxLength="300" Style="resize: vertical; min-height: 100px; height: 100px;" TextMode="MultiLine" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </fieldset>
            </div>
        </div>
        <div class="column size30">
            <div class="box">
                <div class="box-header">
                    <h2>Confirmation</h2>
                    <p>Before continuing, please ensure all details entered are correct.</p>
                </div>
                <div>
                    <asp:Button Text="Continue..." runat="server" CssClass="button long" ID ="btn_Continue" OnClick="btn_Continue_Clicked"/>
                </div>
                <div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>


