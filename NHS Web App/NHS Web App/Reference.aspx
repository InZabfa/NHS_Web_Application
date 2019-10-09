<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterWithoutNavigation.Master" AutoEventWireup="true" CodeBehind="Reference.aspx.cs" Inherits="NHS_Web_App.Reference" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .boxes {
            margin-bottom: 0 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="nhs-center-header">
        <h1>Appointment details</h1>
        <p>Details about this appointment...</p>
    </div>
    <div class="boxes">
        <div class="column size100">
            <div class="button-box">
                <div class="" id="msg_box" runat="server"></div>
                <div class="box">
                    <div class="box-header">
                        <h2>Ref: <%= AppointmentFromRefNumber.Ref_Number %></h2>
                    </div>
                    <fieldset>
                        <p><strong>Date</strong></p>
                        <p><%= AppointmentFromRefNumber.Appointment_DateTime.ToLongDateString() %></p>
                    </fieldset>
                    <fieldset>
                        <p><strong>Time</strong></p>
                        <p><%= AppointmentFromRefNumber.Appointment_DateTime.ToString("HH:mm") %> - <%= AppointmentFromRefNumber.Appointment_DateTime.AddMinutes(AppointmentFromRefNumber.Appointment_Duration_Minutes).ToString("HH:mm") %></p>
                    </fieldset>
                    <fieldset>
                        <p><strong>Doctor</strong></p>
                        <p><%= AppointmentFromRefNumber.Staff.User.Forename %> <%= AppointmentFromRefNumber.Staff.User.Surname %></p>
                    </fieldset>
                    <fieldset>
                        <p><strong>Room</strong></p>
                        <p><%= AppointmentFromRefNumber.Room %></p>
                    </fieldset>
                    <p style="padding-top: 10px;"><strong>Because you are not logged in, you must inform <%= AppointmentFromRefNumber.Patient.User.Practice_Info.Name %> you are here by speaking to reception...</strong></p>
                </div>
                <a class="button long" href="Login.aspx">Login</a>
            </div>
        </div>
    </div>
</asp:Content>
