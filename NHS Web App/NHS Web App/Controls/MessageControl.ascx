<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageControl.ascx.cs" Inherits="NHS_Web_App.Controls.MessageControl" %>
<%@ Import Namespace="NHS_Web_App" %>

<div class="msg animated fadeIn" runat="server" id="msgcontrol" data-msg="<%# this.Name %>">
    <div class="content">
        <div class="icon">
            <% switch (this.msgType)
                {
                    case BasePage.MessageType.WARNING: %>
            <i class="fas fa-exclamation-circle"></i>
            <% break;
                case BasePage.MessageType.ERROR: %>
            <i class="fas fa-times-circle"></i>
            <% break;
                case BasePage.MessageType.INFORM: %>
            <i class="fas fa-info-circle"></i>
            <% break;
                case BasePage.MessageType.SUCCESS: %>
            <i class="fas fa-check-circle"></i>
            <% break;
                default: %>
            <i class="fas fa-info-circle"></i>
            <% break;
                } %>
        </div>
        <div class="message">
            <p><span><%= this.Title %></span><%= this.Description %></p>
        </div>
        <% if (this.ShowClose)
            { %>
        <div class="close">
            <button id="btnMessageClose" formnovalidate form="" onclick="closeMessage('<%= this.Name %>');">
                <i class="fas fa-times"></i>
            </button>
        </div>
        <% } %>
    </div>
</div>
