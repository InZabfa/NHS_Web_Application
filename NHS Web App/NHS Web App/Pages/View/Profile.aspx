<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="NHS_Web_App.Pages.View.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="actions" ContentPlaceHolderID="quickActions" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager runat="server" />
    <div class="nhs-center-header">
        <h1><%= GetUser.Forename %> <%= GetUser.Surname %></h1>
        <p>Date of birth: <%= GetUser.DOB.ToShortDateString() %></p>
    </div>


    <div class="boxes">

        <% if (QueueHandler.GetCurrentAppointment(GetUser.Patient) != null && QueueHandler.GetCurrentAppointment(GetUser.Patient).Status > 0)
            { %>
        <div class="column size100">
            <div class="box">
                <div class="box-header">
                    <h2>Queue</h2>
                </div>
                <% if (QueueHandler.GetCurrentAppointment(GetUser.Patient).Status == 1)
                    { %>
                <p>This patient is being informed to come to Room <%= QueueHandler.GetCurrentAppointment(GetUser.Patient).Appointment.Room %>.</p>
                <asp:Button Text="Patient has arrived" CssClass="button" ID="btnArrived" OnClick="btnArrived_Click" runat="server" />
                <% }
                    else if (QueueHandler.GetCurrentAppointment(GetUser.Patient).Status == 2)
                    { %>
                <p>If this appointment has ended, please click below...</p>
                <asp:Button Text="Appointment complete" CssClass="button" ID="btnAppointmentComplete" OnClick="btnAppointmentComplete_Click" runat="server" />
                <%} %>
            </div>
        </div>

        <% } %>
        <div class="column size30">
            <div class="button-box">
                <div class="box">
                    <div class="box-header">
                        <h2>Personal Details</h2>
                        <p>Details about this user...</p>
                    </div>
                    <fieldset>
                        <p><strong>Gender:</strong></p>
                        <p><%= GetUser.Gender ? "Male" : "Female" %></p>
                    </fieldset>
                    <fieldset>
                        <p><strong>Address:</strong></p>
                        <p><%= GetUser.Address %></p>
                    </fieldset>
                    <fieldset>
                        <p><strong>Email:</strong></p>
                        <p><a href="mailto:<%= GetUser.Email %>"><%= GetUser.Email %></a></p>
                    </fieldset>
                    <fieldset>
                        <p><strong>Phone:</strong></p>
                        <p><a href="tel:<%= GetUser.Phone_number %>"><%= GetUser.Phone_number %></a></p>
                    </fieldset>
                </div>
                <%--<asp:Button Text="Edit..." CssClass="button long" runat="server" />--%>
            </div>
            <div class="box">
                <div class="box-header">
                    <h2>Emergency Contacts</h2>
                </div>
                <% if (HasEmergencyContacts())
                    { %>
                <div id="emergency_contacts" runat="server"></div>
                <%}
                    else
                    { %>
                <p>No emergency contacts...</p>
                <% } %>
            </div>
            <div class="button-box">
                <div class="box">
                    <div class="box-header">
                        <h2>Patient Notes</h2>
                    </div>
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <% if (HasNotes())
                                { %>
                            <div id="patient_notes" runat="server"></div>
                            <% }
                                else
                                { %>
                            <p>This patient doesn't have any notes.</p>
                            <p>Add notes using the page actions on the left sidebar...</p>
                            <%} %>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <a href="<%= String.Format("{0}?patient={1}&return_url={2}", DataLayer.Helper.PageAddress(DataLayer.Helper.Pages.CREATE_NOTE),GetUser.Id, Request.Url.PathAndQuery) %>" class="button long">Add note...</a>
            </div>
        </div>
        <div class="column size70">
            <div class="box">
                <div class="box-header">
                    <h2>Appointments</h2>
                </div>
                <% if (HasAppointments())
                    { %>
                <div id="appointments_list" runat="server"></div>
                <% }
                    else
                    { %>
                <p>This patient has no appointments...</p>
                <%} %>
            </div>
            <div class="button-box">
                <div class="box">
                    <div class="box-header">
                        <h2>Patient Conditions</h2>
                        <p>Conditions that this patient has...</p>
                    </div>
                    <% if (HasConditions())
                        { %>
                    <div id="conditions_list" runat="server"></div>
                    <% }
                        else
                        { %>
                    <p>This patient has no conditions...</p>
                    <% } %>
                </div>
                <a class="button long" href="<%= String.Format("{0}?patient={1}&return_url={2}", DataLayer.Helper.PageAddress(DataLayer.Helper.Pages.CREATE_PAITENT_CONDITION),GetUser.Id, Request.Url.PathAndQuery) %>">Add condition</a>
            </div>
            <div class="button-box">
                <div class="box">
                    <div class="box-header">
                        <h2>Patient Medications</h2>
                        <p>Medications that this patient has...</p>
                    </div>
                    <% if (HasMedication())
                        { %>
                    <div id="medications_list" runat="server"></div>
                    <% }
                        else
                        { %>
                    <p>This patient has no medications...</p>
                    <% } %>
                </div>
                <a class="button long" href="<%= String.Format("{0}?patient={1}&return_url={2}", DataLayer.Helper.PageAddress(DataLayer.Helper.Pages.CREATE_PAITENT_CONDITION),GetUser.Id, Request.Url.PathAndQuery) %>">Add medication</a>
            </div>
        </div>
    </div>
</asp:Content>
