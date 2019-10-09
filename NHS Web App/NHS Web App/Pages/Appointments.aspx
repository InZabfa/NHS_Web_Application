<%@ Page Title="Appointments | NHS Management System" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" Async="true" CodeBehind="Appointments.aspx.cs" Inherits="NHS_Web_App.Appointments" %>

<%@ Import Namespace="DataLayer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="topBtns" ContentPlaceHolderID="topActionButtons" runat="server">
</asp:Content>

<asp:Content ID="btns" ContentPlaceHolderID="quickActions" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div id="msgContainer" runat="server"></div>
    <asp:ScriptManager runat="server" />
    <div class="nhs-center-header">
        <h1>Appointments</h1>
        <p style="text-align: center;">Here are <%= GetRole() == ROLES.RECEPTIONIST || GetRole() == ROLES.ADMIN ? "all" : "your" %> appointments...</p>
    </div>
    <div class="boxes">
        <div class="column size100">
            <div class="box">
                <div class="box-header">
                    <h2>Today</h2>
                </div>
                <asp:UpdatePanel ID="appointments_today_updatepanel" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <% if (GetTodaysAppointments().Count > 0)
                            { %>
                        <div id="appointments_today" runat="server"></div>
                        <% }
                            else
                            { %>
                        <p>You have no appointments for today...</p>
                        <% } %>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="box">
                <div class="box-header">
                    <h2>Tomorrow</h2>
                </div>
                <asp:UpdatePanel ID="appointments_tomorrow_updatepanel" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <% if (GetTomorrowsAppointments().Count > 0)
                            { %>
                        <div id="appointments_tomorrow" runat="server"></div>
                        <% }
                            else
                            { %>
                        <p>You have no appointments for tomorrow...</p>
                        <% } %>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="box">
                <div class="box-header">
                    <h2>Next week</h2>
                </div>
                <asp:UpdatePanel ID="appointments_nextweek_updatepanel" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <% if (GetNextWeekAppointments().Count > 0)
                            { %>
                        <div id="appointments_nextweek" runat="server"></div>
                        <% }
                            else
                            { %>
                        <p>You have no appointments next week...</p>
                        <% } %>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="box">
                <div class="box-header">
                    <h2>After next week</h2>
                </div>
                <asp:UpdatePanel ID="appointments_afternextweek_updatepanel" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <% if (GetAfterNextWeekAppointments().Count > 0)
                            { %>
                        <div id="appointments_afternextweek" runat="server"></div>
                        <% }
                            else
                            { %>
                        <p>You have no appointments after next week...</p>
                        <% } %>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
