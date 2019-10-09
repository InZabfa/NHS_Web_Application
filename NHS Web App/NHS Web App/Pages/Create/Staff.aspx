<%@ Page Title="Staff | NHS Managment System" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Staff.aspx.cs" Inherits="NHS_Web_App.Pages.Create.Staff" %>

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
        <h1>Create Staff</h1>
        <p style="text-align: center;">Create new staff members...</p>
    </div>

    <div class="boxes">
        <div class="column size70">
            <div class="box">
                <div class="box-header">
                    <h2>Staff Information</h2>
                </div>
                <div>
                    <div class="input">
                        <fieldset class="half">
                            <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <asp:Label Text="Forename:*" runat="server" />
                                    <asp:TextBox runat="server" required="required" ID="staffName" MaxLength="100" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </fieldset>
                        <fieldset class="half">
                            <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <asp:Label Text="Surname:*" runat="server" />
                                    <asp:TextBox runat="server" required="required" ID="staffSurname" MaxLength="100" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </fieldset>
                    </div>
                    <div class="input">
                        <fieldset class="half">
                            <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <asp:Label Text="Gender:*" runat="server" CssClass="input" />
                                    <asp:DropDownList ID="selected_gender" runat="server" class="drop md-select2">
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
                                        <asp:TextBox runat="server" required="required" ID="staffEmail" MaxLength="254" placeholder="example@domain.co.uk" />
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
                                    <asp:TextBox runat="server" required="required" ID="staffDOB" TextMode="Date" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </fieldset>
                        <fieldset class="half">
                            <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <asp:Label Text="Phone No.:*" runat="server" />
                                    <asp:TextBox runat="server" required="required" ID="staffPhNo" MaxLength="30" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </fieldset>
                    </div>
                    <div>
                        <fieldset>
                            <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <asp:Label Text="Address:*" runat="server" />
                                    <asp:TextBox runat="server" required="required" ID="staffAddress" MaxLength="300" Style="resize: vertical; min-height: 100px; height: 100px;" TextMode="MultiLine" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </fieldset>
                    </div>
                </div>
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
                                <asp:TextBox runat="server" required="required" ID="emer_Forname" MaxLength="100" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                    <fieldset class="half">
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label Text="Surname:*" runat="server" />
                                <asp:TextBox runat="server" required="required" ID="emer_Surname" MaxLength="100" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </div>
                <div class="input">
                    <fieldset class="half">
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label Text="Relation:*" runat="server" />
                                <asp:TextBox runat="server" required="required" ID="relation" MaxLength="100" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>

                    <fieldset class="half">
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label Text="Phone Number:*" runat="server" />
                                <asp:TextBox runat="server" required="required" ID="emer_PhNo" MaxLength="100" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </div>
                <div>
                    <fieldset>
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label Text="Address:*" runat="server" />
                                <asp:TextBox runat="server" required="required" ID="emer_Address" MaxLength="300" Style="resize: vertical; min-height: 100px; height: 100px;" TextMode="MultiLine" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </div>
            </div>
            <div class="box">
                <div class="box-header">
                    <h2>Further Information</h2>
                </div>
                <div class="input">
                    <fieldset class="half">
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label Text="Contract Type:*" runat="server" />
                                <asp:DropDownList ID="contract_select" runat="server" class="drop md-select2">
                                    <asp:ListItem Value="PERMANENT" Text="Permanent" />
                                    <asp:ListItem Value="TEMPORARY" Text="Temporary" />
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>

                    <fieldset class="half">
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label Text="Full Time/Part Time:*" runat="server" />
                                <asp:DropDownList ID="ft_pt" runat="server" class="drop md-select2">
                                    <asp:ListItem Value="1" Text="Full Time" />
                                    <asp:ListItem Value="0" Text="Part Time" />
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </div>
                <div class="input">
                    <fieldset class="half">
                        <asp:Label Text="Role:*" runat="server" />
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList AutoPostBack="true" runat="server" ID="role" class="search_picker md-select2">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>

                    <fieldset class="half">
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label Text="Working Hours:*" runat="server" />
                                <asp:TextBox runat="server" required="required" ID="wrk_hours" TextMode="Number" Text="40" MaxLength="300" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>

                </div>
                <div class="input">
                    <fieldset>
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label Text="Working Days:*" runat="server" />
                                <asp:ListBox ID="working_days" ClientIDMode="Static" SelectionMode="Multiple" CssClass="working_days md-select2" runat="server">
                                    <asp:ListItem Text="Monday" Selected="True" Value="0" />
                                    <asp:ListItem Text="Tuesday" Selected="True" Value="1" />
                                    <asp:ListItem Text="Wednesday" Selected="True" Value="2" />
                                    <asp:ListItem Text="Thursday" Selected="True" Value="3" />
                                    <asp:ListItem Text="Friday" Selected="True" Value="4" />
                                    <asp:ListItem Text="Saturday" Value="5" />
                                    <asp:ListItem Text="Sunday" Value="6" />
                                </asp:ListBox>
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
                            <asp:CheckBox CssClass="checkbox" ID="staffOk" AutoPostBack="true" Text="Staff personal and Work information is correct" runat="server" Checked="true" />
                            <asp:CheckBox CssClass="checkbox" ID="emerOk" AutoPostBack="true" Text="Staff Emergency contact information is correct" runat="server" Checked="true" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <asp:Button CssClass="button long" Text="Continue..." Enabled="<%# staffOk.Checked && emerOk.Checked %>" runat="server" ID="saveInfo" OnClick="saveEntered" />
            </div>
        </div>

    </div>


    <script>
        function pageLoad() {
            try { $(".search_picker").select2(); } catch (e) { }
            try {
                $(".working_days").select2({
                    placeholder: "Select working days...",
                    allowClear: false
                });
            } catch (e) { }
            try {
                $(".drop").select2({
                    minimumResultsForSearch: -1
                });
            } catch (e) { }
        }
    </script>
</asp:Content>
