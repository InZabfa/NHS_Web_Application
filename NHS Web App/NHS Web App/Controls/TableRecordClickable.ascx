<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TableRecordClickable.ascx.cs" Inherits="NHS_Web_App.TableRecordClickable" %>
<div class="divTableRow record" id="tst" runat="server" onclick="">
    <%--<div class="divTableCell"><%= this.UserID %></div>--%>
    <div class="divTableCell"><%= this.Name %></div>
    <%--<div class="divTableCell"><%= this.Age %></div>--%>
    <div class="divTableCell"><%= this.AppointmentTime.ToShortTimeString() %></div>
</div>