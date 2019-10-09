<%@ Page Language="C#" Title="Login | NHS Management System" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="NHS_Web_App.Login" %>

<!DOCTYPE html>

<html class="fontawesome-i2svg-pending" xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head runat="server">
    <!-- Meta -->
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <title>Login - NHS Management System</title>
    <link href="Styles/reset.css" rel="stylesheet" />
    <link href="Styles/style.css" rel="stylesheet" />
    <link href="Styles/animate.css" rel="stylesheet" />
    <link href="Styles/custom-style.css" rel="stylesheet" />
    <link href="Styles/login.css" rel="stylesheet" />
    <!-- Fonts -->
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:300,400,700' rel='stylesheet' type='text/css' />
    <script src="https://use.fontawesome.com/releases/v5.0.6/js/all.js"></script>
</head>
<body>
    <form id="defaultForm" style="height: 100%;" class="" runat="server">
        <div id="error_message" class="error" runat="server"></div>
        <div class="logo-box">
            <img class="logo" src="img/nhs-logo.svg" alt="NHS Logo" />
        </div>

        <div class="body">
            <div>
                <div class="login-box">
                    <h1>Welcome, let's get started</h1>

                    <asp:Panel runat="server" DefaultButton="btnLogin">
                        <div class="field">
                            <label><i class="fas fa-at"></i>Email:</label>
                            <asp:TextBox ID="txtEmail" TextMode="Email" runat="server" />
                        </div>

                        <div class="field password-details">
                            <label><i class="fas fa-key"></i>Password:</label>
                            <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" />
                        </div>
                    </asp:Panel>

                    <hr />
                    <asp:Panel runat="server" DefaultButton="btnView">
                        <div class="alt-login">
                            <p><strong>Forgotten your login details?</strong></p>
                            <p>Use your appointment reference number:</p>
                            <br />
                            <asp:TextBox ID="txtReference" MaxLength="10" placeholder="Reference..." runat="server" />
                            <asp:Button Text="View" ID="btnView" OnClick="btnView_Click" runat="server" />
                        </div>
                    </asp:Panel>
                    <p class="space"></p>
                    <asp:Button ID="btnLogin" Text="Login" runat="server" OnClick="Login_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
