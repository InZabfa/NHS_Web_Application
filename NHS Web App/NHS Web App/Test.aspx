<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="NHS_Web_App.Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Login info</h2>
            <div>
                <asp:Label Text="Email" runat="server" />
                <asp:TextBox ID="email" runat="server" TextMode="Email" />
            </div>

            <div>
                <asp:Label Text="Password" runat="server" />
                <asp:TextBox ID="pwrd" runat="server" TextMode="Password" />
            </div>
        </div>

        <div>
            <h2>User details</h2>
            <div>
                <asp:Label Text="Name" runat="server" />
                <asp:TextBox ID="fname" runat="server" />
            </div>

            <div>
                <asp:Label Text="Surname" runat="server" />
                <asp:TextBox ID="sname" runat="server" />
            </div>

            <div>
                <asp:Label Text="Gender" runat="server" />
                <asp:CheckBox Text="Male" runat="server" Checked="true" ID="chkMale" />
            </div>

            <div>
                <asp:Label Text="DOB" runat="server" />
                <asp:TextBox ID="dob" runat="server" TextMode="Date" />
            </div>

            <div>
                <asp:Label Text="Address" runat="server" />
                <asp:TextBox ID="address" runat="server" TextMode="MultiLine" />
            </div>

            <div>
                <asp:Label Text="Phone number" runat="server" />
                <asp:TextBox ID="phonenum" runat="server" TextMode="Phone" />
            </div>

            <asp:Label Text="Same details will be used for an emergency contact" runat="server" />
        </div>

        <div>
            <h2>Practice Info</h2>
            <div>
                <asp:Label Text="Practice Name" runat="server" />
                <asp:TextBox ID="pName" runat="server" />
            </div>
            <asp:Label Text="Same details will be used for contact as account details" runat="server" />
        </div>

        <div>
            <h2>Role</h2>

            <div>
                <asp:Label Text="Role number" runat="server" />
                <asp:TextBox runat="server" TextMode="Number" Text="5" Min="0" Max="10" />
            </div>
        </div>

        <asp:Button Text="Create test data (Auto)" ID="btnAuto" runat="server" OnClick="btnAuto_Click" />
        <asp:Button Text="Create test data" ID="btnCreateTestData" runat="server" OnClick="btnCreateTestData_Click" />
        <asp:Button Text="Test emailing" ID="btnTestEmail" OnClick="btnTestEmail_Click" runat="server" />
        <asp:Button Text="Create access types" ID="btnCreateAccessTypes" OnClick="btnCreateAccessTypes_Click" runat="server" />
    </form>
</body>
</html>
