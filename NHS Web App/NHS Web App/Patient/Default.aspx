<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NHS_Web_App.Patient.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="topActionButtons" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="quickActions" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager runat="server" />
    <div class="nhs-center-header">
        <h1>Welcome, <%= LoggedInUser.Forename %> <%= LoggedInUser.Surname %>!</h1>
        <p>Let's get started...</p>
    </div>
    <div class="boxes">
        <asp:UpdatePanel ID="apPanel" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <% if (FlaggableAppointments != null)
                    { %>
                <div class="column size100">
                    <div class="box">
                        <div class="box-header">
                            <h2>Appointment <%= FlaggableAppointments.Appointment_DateTime.ToShortTimeString() %> - <%= FlaggableAppointments.Appointment_DateTime.AddMinutes(FlaggableAppointments.Appointment_Duration_Minutes).ToShortTimeString() %></h2>
                            <p>Inform your doctor you're here</p>
                        </div>
                        <p><strong>Reference:</strong> <%= FlaggableAppointments.Ref_Number %></p>
                        <p><strong>Doctor:</strong> <%= FlaggableAppointments.Staff.User.Forename %> <%= FlaggableAppointments.Staff.User.Surname %></p>
                        <p><strong>Room:</strong> <%= FlaggableAppointments.Room %></p>
                        <asp:Button Text="Tell my doctor I'm here!" CssClass="button" OnClick="btnImhere_Click" ID="btnImhere" runat="server" />
                    </div>
                </div>
                <%} %>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="column size100">
            <div class="box">
                <div class="box-header">
                    <h2>Your Appointments</h2>
                    <p>Listed below are all your appointments...</p>
                </div>
                <div class="appointments" id="list_appointments" runat="server">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
