<%@ Page Title="Add provider | NHS Management System" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ProviderInfo.aspx.cs" Inherits="NHS_Web_App.Pages.Create.ProviderInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager runat="server" />
    <div class="nhs-center-header">
        <% if (IsEditing)  %>
        <% { %>
        <h1>Edit Provider</h1>
        <p>Update the provider details...</p>
        <% } %>
        <% else %>
        <% { %>
        <h1>Add Provider</h1>
        <p>Add a provider to be used on medical records...</p>
        <% } %>
    </div>
    <div id="messageContainer" runat="server"></div>
    <div class="boxes">
        <div class="column size70">
            <div class="box">
                <div class="box-header">
                    <h2>Provider details</h2>
                    <p>Enter the contact information for this provider...</p>
                </div>
                <div>
                    <fieldset class="half">
                        <asp:Label Text="Name:*" runat="server" />
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:TextBox AutoPostBack="true" required="required" placeholder="Provider name..." ID="txtProviderName" MaxLength="70" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                    <fieldset class="half">
                        <asp:Label Text="Phone number:*" runat="server" />
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:TextBox AutoPostBack="true" runat="server" ID="txtPhoneNumber" MaxLength="30" TextMode="Phone" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </div>
                <fieldset>
                    <asp:Label Text="Email:*" runat="server" />
                    <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:TextBox AutoPostBack="true" runat="server" ID="txtEmail" MaxLength="254" TextMode="Email" placeholder="example@domain.co.uk" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </fieldset>
                <fieldset>
                    <asp:Label Text="Address:*" runat="server" />
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox AutoPostBack="true" runat="server" MaxLength="300" Style="resize: vertical; min-height: 100px; height: 100px;" ID="txtAddress" TextMode="MultiLine" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </fieldset>
            </div>
        </div>
        <div class="column size30">
            <div class="button-box">
                <div class="box">
                    <p>Before continuing, please ensure all details entered are correct.</p>
                </div>
                <asp:Button Text="Add provider" CssClass="button long" runat="server" ID="btnContinue" OnClick="btnContinue_Click" />
            </div>
        </div>
    </div>
</asp:Content>
