<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="NHS_Web_App.Error" %>

<%@ Import Namespace="DataLayer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Styles/reset.css" rel="stylesheet" />
    <link href="Styles/style.css" rel="stylesheet" />
    <!-- Meta -->
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
</head>
<body style="margin: 0 auto;">
    <form id="mainForm" runat="server">
        <main class="nhs-main-content" style="margin: 0 auto;">
            <div class="content-wrapper noside">
                <div class="nhs-center-header">
                    <h1><%= ErrorTitle %></h1>
                    <p><%= Description %></p>
                    <a class="button" href="<%= Helper.PageAddress(Helper.Pages.OVERVIEW) %>">Go back home...</a>
                </div>
            </div>
        </main>
    </form>
</body>
</html>
