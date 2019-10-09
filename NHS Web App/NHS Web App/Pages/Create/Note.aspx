<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Note.aspx.cs" Inherits="NHS_Web_App.Pages.Create.Note" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="topActionButtons" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="quickActions" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="content" runat="server">
    <div class="nhs-center-header">
        <% if (GetPatientNotes == null)
            { %>
        <h1>Create note</h1>
        <p>Edit a note for this patient...</p>
        <% }
            else
            { %>
        <h1>Edit note</h1>
        <p>Edit this patients note...</p>
        <%} %>
    </div>
    <div class="boxes">
        <div class="column size70">
            <div class="box">
                <div class="box-header">
                    <h2>Patient Note</h2>
                    <p>Details about this note...</p>
                </div>
                <fieldset>
                    <asp:Label Text="Note:" runat="server" />
                    <asp:TextBox TextMode="MultiLine" Style="min-height: 300px;" ID="txtNote" runat="server" />
                </fieldset>
                <fieldset>
                    <asp:Label Text="Minimum access level required:" runat="server" />
                    <asp:TextBox ID="txtMinAccessLevel" min="1" max="6" Text="5" TextMode="Number" runat="server" />
                </fieldset>
            </div>
        </div>
        <div class="column size30">
            <div class="button-box">
                <div class="box">
                    <p>Please check and ensure all details entered are correct. If you believe all the details are correct and you are privileged to do so, continue.</p>
                </div>
                <asp:Button Text="Continue..." ID="btnContinue" OnClick="btnContinue_Click" CssClass="button long" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
