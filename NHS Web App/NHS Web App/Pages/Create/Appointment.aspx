<%@ Page Title="Create Appointment | NHS Management System" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Appointment.aspx.cs" Inherits="NHS_Web_App.Pages.Create.Appointment" EnableViewState="true" %>

<%@ Import Namespace="DataLayer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>

    <link href="../../Styles/dropdown-theme.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <asp:ScriptManager runat="server" />
    <div class="nhs-center-header">
        <h1>Create appointment</h1>
        <p>Enter the details below...</p>
    </div>
    <div id="messageContainer" runat="server"></div>
    <div class="boxes">
        <div class="column size70">
            <div class="box">
                <div class="box-header">
                    <h2>Staff and Patient</h2>
                    <p>Pick the staff to create an appointment with a patient...</p>
                </div>
                <div>
                    <fieldset>
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <fieldset>
                                    <asp:Label Text="Doctor:" runat="server" />
                                    <asp:DropDownList ID="doctor_picker" OnSelectedIndexChanged="doctor_picker_SelectedIndexChanged" AutoPostBack="true" runat="server" class="search_picker md-select2"></asp:DropDownList>
                                </fieldset>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                    <fieldset>
                        <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <fieldset>
                                    <asp:Label Text="Patient:" runat="server" />
                                    <asp:DropDownList ID="select_patients" AutoPostBack="true" runat="server" class="search_picker md-select2"></asp:DropDownList>
                                </fieldset>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="doctor_picker" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </fieldset>
                </div>
            </div>
            <div class="box">
                <div class="box-header">
                    <h2>Appointment details</h2>
                    <p>Details of the appointment...</p>
                </div>
                <div>
                    <fieldset>
                        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <fieldset class="half">
                                    <asp:Label Text="Date:" runat="server" />
                                    <asp:TextBox runat="server" AutoPostBack="true" ID="txtDate" ClientIDMode="Static" TextMode="Date" />
                                </fieldset>
                                <fieldset class="half">
                                    <asp:Label Text="Duration (minutes):" runat="server" />
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox min="5" runat="server" AutoPostBack="true" Text="15" ID="txtDuration" TextMode="Number" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </fieldset>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                    <fieldset>
                        <asp:Label Text="Available times:" runat="server" Style="padding-bottom: 5px;" />
                        <asp:UpdatePanel runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <asp:TextBox Style="display: none;" runat="server" AutoPostBack="true" ClientIDMode="Static" ID="txtStartTime" TextMode="Time" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="available_times" UpdateMode="Conditional" RenderMode="Block" runat="server">
                            <ContentTemplate>
                                <div id="available_slots" style="float: none; clear: both;">
                                    <% foreach (var item in GetAvailableSlots().Where(i => i.Start >= DateTime.Now))
                                        { %>
                                    <button data-timebtn="1" class="button long" formnovalidate="formnovalidate" style="float: left; width: auto; min-width: 20%;" data-start="<%= item.Start.ToString("HH:mm") %>" data-end="<%= item.End.ToString("HH:mm") %>"><%= item.Start.ToString("HH:mm") %> - <%= item.End.ToString("HH:mm") %></button>
                                    <% } %>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtDate" />
                                <asp:AsyncPostBackTrigger ControlID="txtDuration" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </fieldset>
                    <fieldset>
                        <fieldset class="half">
                            <asp:Label Text="Reference number:" runat="server" />
                            <asp:Label Text="0000000000" Font-Bold="true" ID="refNumber" runat="server" />
                        </fieldset>
                        <fieldset class="half">
                            <asp:Label Text="Room:" runat="server" />
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:TextBox AutoPostBack="true" ID="txtRoom" required="required" MaxLength="10" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </fieldset>
                    </fieldset>
                </div>
            </div>
        </div>
        <div class="column size30">
            <div class="box">
                <div class="box-header">
                    <h2>Notifications</h2>
                    <p>Patient notification settings...</p>
                </div>
                <asp:CheckBox Text="Email this patient" ID="chckEmailNotifications" Checked="true" runat="server" />
            </div>
            <div class="button-box">
                <div class="box">
                    <p>Please check and ensure all details entered are correct. If you believe all the details are correct and you are privileged to do so, continue.</p>
                    <hr />
                    <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:CheckBox CssClass="checkbox" AutoPostBack="true" ID="chkRememberTime" Enabled="<%# Request.Browser.Cookies %>" Text="Remember appointment duration" runat="server" />
                            <asp:CheckBox AutoPostBack="true" CssClass="checkbox" Enabled="<%# Request.Browser.Cookies %>" ID="chkRememberRoom" Text="Remember appointment room" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <asp:Button CssClass="button long" Text="Continue..." runat="server" ID="btnContinue" OnClick="btnContinue_Click" />
            </div>
        </div>
    </div>
    <script>
        function pageLoad() {
            $(".search_picker").select2();

            updateSelected();

            $("button[data-timebtn]").click(function (e) {
                setTimePicker($(this).attr("data-start"));
            });

            $("#txtStartTime").change(function () {
                $(this).attr("value", $(this).val());
                updateSelected();
            });
        }

        function updateSelected() {
            var val = $("#txtStartTime").attr("value");
            $(".button[data-timebtn]").removeClass("selected");
            $("button[data-start='" + val + "']").addClass("selected");
        }

        function setTimePicker(val) {
            $("#txtStartTime").val(val);
            $("#txtStartTime").attr("value", val);
            updateSelected();
        }
    </script>
</asp:Content>
