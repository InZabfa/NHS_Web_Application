<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TicketControl.ascx.cs" Inherits="NHS_Web_App.Controls.TicketControl" %>
<%@ Import Namespace="NHS_Web_App.Controls" %>

<a href="<%# Link %>" class="<%# GetClass() %>">
    <div class="content">
        <span><%# Title %></span>
        <p><%# Body %></p>
        <footer>
            <p><%# Footer %></p>
        </footer>
    </div>
</a>
