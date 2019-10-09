<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExpandableControl.ascx.cs" Inherits="NHS_Web_App.Controls.ExpandableControl" %>
<div class="expandable-outer collapsed" data-id="<%= UID %>">
    <div class="expandable-inner">
        <% if (IsExpandable)
            {  %>
        <div class="expandable-header" onclick="toggleExpandableControl('<%= UID %>', <%= CollapseAllUponExpanding.ToString().ToLower() %>);" onkeydown="if (event.which == 13 || event.keyCode == 13) toggleExpandableControl('<%= UID %>', <%= CollapseAllUponExpanding.ToString().ToLower() %>);" role="button" tabindex="0">
            <% }
                else
                { %>
            <a class="expandable-header" href="<%= URL %>">
                <%} %>


            <span><%= Title %></span>
            <div>
                <% if (IsExpandable)
                    { %>
                <span class="collapsed-only">
                    <i class="fas fa-arrow-down"></i>
                </span>
                <span class="expanded-only">
                    <i class="fas fa-arrow-up"></i>
                </span>
                <% }
                    else
                    { %>
                <i class="fas fa-ellipsis-h"></i>
                <% } %>
            </div>


            <% if (IsExpandable)
                { %>
        </div>
        <% }
            else
            { %>
            </a>
            <%} %>

        <% if (IsExpandable)
            { %>
        <div class="expandable-contents" data-id="<%= UID %>">
            <div id="contents">
                <asp:PlaceHolder ID="placeholder" ClientIDMode="AutoID" runat="server" />
            </div>
            <div class="expandable-buttons">
                <asp:PlaceHolder ID="buttonHolder" ClientIDMode="AutoID" runat="server" />
            </div>
        </div>
        <% } %>
    </div>
</div>
