<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Conditions.aspx.cs" Inherits="NHS_Web_App.Pages.View.Conditions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager runat="server" />
    <div class="boxes">
        <div class="column size70">
            <div class="box">
                <div class="box-header">
                    <h2>Conditions</h2>
                    <p>Manage conditions...</p>
                </div>
                <p>To search conditions, use the universal search bar at the top of the page...</p>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div id="conditions_list" runat="server">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="column size30">
            <div class="box">
            </div>
        </div>
    </div>
</asp:Content>
