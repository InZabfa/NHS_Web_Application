<%@ Page Title="Add Condition | NHS Management System" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Condition.aspx.cs" Inherits="NHS_Web_App.Pages.Create.Condition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager runat="server" />
    <div class="nhs-center-header">
        <h1>Add Condition</h1>
        <p>Add a condition which can be used on patient records...</p>
    </div>
    <div id="messageContainer" runat="server"></div>
    <div class="boxes">
        <div class="column size70">
            <div class="box">
                <div class="box-header">
                    <h2>Condition details</h2>
                    <p>What are the details of this condition...</p>
                </div>
                <fieldset>
                    <asp:Label Text="Name:*" runat="server" />
                    <asp:TextBox required="required" placeholder="Medical name..." ID="txtMedicalCondition" MaxLength="200" runat="server" />
                </fieldset>
                <fieldset>
                    <asp:Label Text="Additional information:" runat="server" />
                    <asp:TextBox runat="server" Style="resize: vertical; min-height: 150px; height: 200px;" ID="txtAdditionalInfo" TextMode="MultiLine" />
                </fieldset>
            </div>
        </div>
        <div class="column size30">
            <div class="button-box">
                <div class="box">
                    <p>Before continuing, please ensure all details entered are correct.</p>
                </div>
                <asp:Button Text="Add condition" CssClass="button long" runat="server" ID="btnContinue" OnClick="btnContinue_Click" />
            </div>
        </div>
    </div>
</asp:Content>
