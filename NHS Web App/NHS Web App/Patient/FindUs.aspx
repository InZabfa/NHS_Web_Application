<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="FindUs.aspx.cs" Inherits="NHS_Web_App.Patient.FindUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="topActionButtons" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="quickActions" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="content" runat="server">
    <div class="nhs-center-header">
        <h1>Find <%= LoggedInUser.Practice_Info.Name %></h1>
        <p>Where can you find us?</p>
    </div>

    <div class="boxes">
        <div class="column size100">
            <div class="box">
                <div class="box-header">
                    <h2>Contact Us!</h2>
                    <p>Our contact details are below...</p>
                </div>
                <fieldset>
                    <p><strong>Phone:</strong></p>
                    <p><a href="tel:<%= LoggedInUser.Practice_Info.Phone_Number %>"><%= LoggedInUser.Practice_Info.Phone_Number %></a></p>
                </fieldset>
                <fieldset>
                    <p><strong>Email:</strong></p>
                    <p><a href="mailto:<%= LoggedInUser.Practice_Info.Email %>"><%= LoggedInUser.Practice_Info.Email %></a></p>
                </fieldset>
                <fieldset>
                    <p><strong>Address:</strong></p>
                    <p><%= LoggedInUser.Practice_Info.Address %></p>
                </fieldset>
                <fieldset>
                    <iframe width="100%"
                        height="300"
                        frameborder="0" style="border: 0"
                        src="https://www.google.com/maps/embed/v1/place?key=AIzaSyDVnxFjY2lKV-F4iVjDKJXRgX7tDiBFMl8&q=<%= LoggedInUser.Practice_Info.Address %>"></iframe>
                </fieldset>
            </div>
        </div>
    </div>
</asp:Content>
