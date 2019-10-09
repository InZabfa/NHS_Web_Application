<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PatientListItem.ascx.cs" Inherits="NHS_Web_App.Controls.PatientListItem" %>
<div class="list_item">
    <a href="/Pages/View/Profile.aspx?id=<%= patientid %>">
        <div>
            <div>
                <img src="/img/default-picture.png" alt="Profile Picture" />
            </div>
            <div class="content">
                <p><%= String.Format("{0} {1}", forename, surname) %></p>
            </div>
            <div>
                <i class="fas fa-search"></i>
            </div>
        </div>
    </a>
</div>
