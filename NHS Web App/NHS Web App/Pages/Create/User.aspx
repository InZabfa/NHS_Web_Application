<%@ Page Title="User | NHS Managment System" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="NHS_Web_App.Pages.Create.User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>

    <link href="../../Styles/dropdown-theme.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="topActionButtons" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="quickActions" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="content" runat="server">
    <div id="msg_contents" runat="server"></div>
    <asp:ScriptManager runat="server" />
    <div class="nhs-center-header">
        <h1>Add User</h1>
        <p style="text-align: center;">Create new Users...</p>
    </div>

    <div class="boxes">
        <div class="column size70">
            <div class="box">
                <div class="box-header">
                    <h2>User Information</h2>
                </div>

                <div class="input">
                    <fieldset class="half">
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label Text="Forename:*" runat="server" />
                                <asp:TextBox runat="server" required="required" ID="userName" MaxLength="100" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>

                    <fieldset class="half">
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label Text="Surname:*" runat="server" />
                                <asp:TextBox runat="server" required="required" ID="userSurname" MaxLength="100" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </div>

                <div class="input">
                    <fieldset class="half">
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label Text="Gender:*" runat="server" CssClass="input" />
                                <asp:DropDownList ID="userGender" runat="server" class="drop md-select2">
                                    <asp:ListItem Text="Male" Value="1" />
                                    <asp:ListItem Text="Female" Value="0" />
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>


                    <div>
                        <fieldset class="half">
                            <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <asp:Label Text="E-mail:*" runat="server" />
                                    <asp:TextBox runat="server" required="required" ID="userEmail" MaxLength="254" placeholder="example@domain.co.uk" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </fieldset>
                    </div>
                </div>

                <div class="input">
                    <fieldset class="half">
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label Text="DOB:*" runat="server" />
                                <asp:TextBox runat="server" required="required" ID="userDOB" TextMode="Date" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                    <fieldset class="half">
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label Text="Phone No.:*" runat="server" />
                                <asp:TextBox runat="server" required="required" ID="userPhNo" MaxLength="30" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </div>

                <fieldset>
                    <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:Label Text="Address:*" runat="server" />
                            <asp:TextBox runat="server" required="required" ID="userAddress" MaxLength="300" Style="resize: vertical; min-height: 100px; height: 100px;" TextMode="MultiLine" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </fieldset>
            </div>

            <div class="box">
                <div class="box-header">
                    <h2>Emergency Contact</h2>
                </div>
                <div class="input">
                    <fieldset class="half">
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label Text="Forename:*" runat="server" />
                                <asp:TextBox runat="server" required="required" ID="userEmer_Forname" MaxLength="100" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                    <fieldset class="half">
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label Text="Surname:*" runat="server" />
                                <asp:TextBox runat="server" required="required" ID="userEmer_Surname" MaxLength="100" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </div>
                <div class="input">
                    <fieldset class="half">
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label Text="Relation:*" runat="server" />
                                <asp:TextBox runat="server" required="required" ID="relationToUser" MaxLength="100" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>

                    <fieldset class="half">
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label Text="Phone Number:*" runat="server" />
                                <asp:TextBox runat="server" required="required" ID="userEmer_PhNo" MaxLength="100" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </div>
                <div>
                    <fieldset>
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label Text="Address:*" runat="server" />
                                <asp:TextBox runat="server" required="required" ID="userEmer_Address" MaxLength="300" Style="resize: vertical; min-height: 100px; height: 100px;" TextMode="MultiLine" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </div>
            </div>


        </div>
        <div class="column size30">
            <div class="button-box">
                <div class="box">
                    <p>Please check and ensure that details entered are correct.If you believe all the details are correct and you are privileged to do so, continue.</p>
                    <hr />
                    <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:CheckBox CssClass="checkbox" ID="userOk" AutoPostBack="true" Text="Staff personal and Work information is correct" runat="server" Checked="true" />
                            <asp:CheckBox CssClass="checkbox" ID="userEmerOk" AutoPostBack="true" Text="Staff Emergency contact information is correct" runat="server" Checked="true" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <asp:Button CssClass="button long" Text="Continue..." Enabled="<%# userOk.Checked && userEmerOk.Checked %>" runat="server" ID="saveInfo" OnClick="saveEntered" />
            </div>
        </div>

    </div>
            <script>
            function pageLoad() {
                try { $(".search_picker").select2(); } catch (e) { }

                try {
                $(".drop").select2({
                    minimumResultsForSearch: -1
                });
            } catch (e) { }

            }
        </script>
</asp:Content>
