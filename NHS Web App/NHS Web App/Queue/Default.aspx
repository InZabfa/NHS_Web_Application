<%@ Page Title="Queue | NHS Management System" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NHS_Web_App.Queue.Default" %>

<%@ Register Src="~/Controls/TicketControl.ascx" TagPrefix="uc1" TagName="TicketControl" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- Meta -->
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <!-- Fonts -->
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:300,400,700' rel='stylesheet' type='text/css' />
    <script defer src="https://use.fontawesome.com/releases/v5.0.6/js/all.js"></script>

    <!-- Style sheets -->
    <link href="../Styles/reset.css" rel="stylesheet" />
    <link href="../Styles/style.css" rel="stylesheet" />
    <link href="../Styles/custom-style.css" rel="stylesheet" />
    <link href="../Styles/ticket-strip.css" rel="stylesheet" />
    <link href="Style/style.css" rel="stylesheet" />
    <!-- Modernizr -->
    <script src="js/modernizr.js"></script>
</head>
<body>
    <form id="mainForm" runat="server">
        <asp:ScriptManager runat="server" />
        <asp:Timer Interval="1000" Enabled="true" runat="server" ID="timer"></asp:Timer>
        <main class="nhs-main-content">
            <div class="time">
                <asp:UpdatePanel ID="timepanel" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <p><strong><%= DateTime.Now.ToLongDateString() %></strong></p>
                        <p><%= DateTime.Now.ToString("hh:mm:ss tt") %></p>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="timer" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="content-wrapper noside">
                <div class="nhs-center-header">
                    <img src="../img/nhs-logo.svg" height="60" style="position: center; margin-bottom: 10px; -moz-box-sizing: border-box; -webkit-box-sizing: border-box; box-sizing: border-box;"
                        alt="NHS Logo" />
                    <h1><span>Welcome to </span><strong><%= LoggedInUser.Practice_Info.Name %></strong></h1>
                </div>
                <div class="column-container">
                    <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <% foreach (var item in QueueHandler.GetTodayAppointments(DB.StaffGet(LoggedInUser)))
                                { %>
                            <div class="queue_item">
                                <p><%= item.Appointment.Patient.User.Forename %> <%= item.Appointment.Patient.User.Surname %></p>
                                <span>Room <%= item.Appointment.Room %></span>
                            </div>
                            <% } %>
                            <% foreach (var item in QueueHandler.GetCurrentPatients(DB.StaffGet(LoggedInUser)))
                                { %>
                            <div class="queue_item next">
                                <p><%= item.Appointment.Patient.User.Forename %> <%= item.Appointment.Patient.User.Surname %></p>
                                <span>Room <%= item.Appointment.Room %></span>
                            </div>
                            <% } %>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="timer" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </main>
    </form>
</body>
</html>
