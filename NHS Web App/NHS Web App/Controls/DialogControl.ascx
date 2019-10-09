<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DialogControl.ascx.cs" Inherits="NHS_Web_App.Controls.DialogControl" %>


<div class="dialog">
    <button formnovalidate="formnovalidate" class="dialog-close" form="" onclick="$('.dialog-behind').removeClass('visible'); return false;"><i class="fas fa-times"></i></button>
    <div class="inner">
        <header>
            <asp:PlaceHolder ID="header" ClientIDMode="AutoID" runat="server" />
        </header>
        <main>
            <asp:PlaceHolder ID="body" ClientIDMode="AutoID" runat="server" />
        </main>
    </div>
</div>
