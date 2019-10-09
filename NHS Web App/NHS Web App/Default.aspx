<%@ Page Title="Overview | NHS Management System" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NHS_Web_App.Default" %>

<%@ Register Src="~/Controls/TableRecordClickable.ascx" TagPrefix="uc1" TagName="TableRecordClickable" %>
<%@ Register Src="~/Controls/PatientListItem.ascx" TagPrefix="uc1" TagName="PatientListItem" %>
<%@ Register Src="~/Controls/ExpandableControl.ascx" TagPrefix="uc1" TagName="ExpandableControl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/DialogControl.ascx" TagPrefix="uc1" TagName="DialogControl" %>


<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/tables.css" rel="stylesheet" />
    <link href="Styles/ticket-strip.css" rel="stylesheet" />
    <link href="Styles/popup-dialog.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.1/Chart.bundle.js" integrity="sha256-vyehT44mCOPZg7SbqfOZ0HNYXjPKgBCaqxBkW3lh6bg=" crossorigin="anonymous"></script>
</asp:Content>
<asp:Content ID="content" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager runat="server" />
    <asp:Timer ID="ticketStripTimer" Interval="1000" OnTick="ticketStripTimer_Tick" Enabled="true" runat="server"></asp:Timer>

    <div class="nhs-center-header">
        <h1>Hey, let's take a look...</h1>
        <p style="text-align: center;">All the important details at a glance...</p>
    </div>

    <%--<div class="dialog-behind visible" id="dialogcontainer" runat="server">
        <uc1:DialogControl runat="server" ID="DialogControl">
            <Body>
                <fieldset>
                    <a class="button long" href="<%= string.Format("{0}?id={1}", DataLayer.Helper.PageAddress(DataLayer.Helper.Pages.VIEW_PROFILE), "1") %>">View profile...</a>
                    <button class="button long" data-dialogid="">Request patient from queue</button>
                </fieldset>
            </Body>
            <Header>
                <h2>Hey</h2>
            </Header>
        </uc1:DialogControl>
    </div>--%>
    <%--<fieldset>
        <a class="button long" href="<%= string.Format("{0}?id={1}", DataLayer.Helper.PageAddress(DataLayer.Helper.Pages.VIEW_PROFILE), "1") %>">View profile...</a>
        <button class="button long" data-dialogid="">Request patient from queue</button>
    </fieldset>--%>

    <div class="boxes">
        <div class="column size100">
            <asp:UpdatePanel ChildrenAsTriggers="false" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <% if (QueueHandler.HasWaitingPatients(DB.StaffGet(LoggedInUser)))
                        { %>
                    <div class="box">
                        <div class="box-header">
                            <h2>Arrived Patients | Live</h2>
                        </div>
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <div id="list_tickets" class="ticket-container" runat="server">
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ticketStripTimer" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <% } %>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="column size100">
            <div class="box">
                <div class="box-header">
                    <h2>Today's patients</h2>
                    <p>Your appointments for today...</p>
                </div>
                <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div id="appointments_list" runat="server"></div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <%--<div class="column size30">
            <div class="box">
                <div class="box-header">
                    <h2>At a glance...</h2>
                    <p>Your appointments summarised.</p>
                </div>
                <canvas id="appointmentsSummary" width="" height="" style="height: auto; margin-top: 1.5em; width: 100%;"></canvas>
                <script>
                    var ctx = document.getElementById("appointmentsSummary").getContext('2d');
                    var myChart = new Chart(ctx, {
                        type: 'pie',
                        data: {
                            labels: ["Missed", "Late", "Seen"],
                            datasets: [{
                                data: [12, 19, 3],
                                backgroundColor: [
                                    'rgba(190, 37, 37, 0.8)',
                                    'rgba(210, 142, 56, 0.8)',
                                    'rgba(124, 195, 120, 0.8)'
                                ]
                            }],
                        },
                        options: {
                            legend: {
                                position: 'bottom',
                                labels: {
                                    padding: 20,
                                    usePointStyle: 'true'
                                }
                            }
                        }
                    });
                </script>
            </div>
            <div class="box">
                <div class="box-header">
                    <h2>Next Patient...</h2>
                    <p>Summary of Paul Roberts</p>
                </div>
                <br />
                <p>Conditions:</p>
                <ul style="list-style: disc; list-style-position: inside; margin-top: 10px;">
                    <li>Fever</li>
                    <li>Headache</li>
                    <li>Insomnia</li>
                </ul>
            </div>
        </div>--%>
    </div>
    <script>
        $(document).ready(function () {
            handleDialogs();
        });
    </script>
</asp:Content>
