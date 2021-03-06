﻿<%@ Page Title="" Language="C#" MasterPageFile="~/views/help/content.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="subcontent" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function() {
            $('#groups').addClass("nav-current");
            $('#groups-bootmenu').addClass("nav-current-sub");
        });
    </script>
    <h1>Groups->Boot Menu</h1>
    <p>
        Sets a custom boot menu for all computers in a group. See <a href="<%= ResolveUrl("~/views/help/computers-bootmenu.aspx") %>">Computers->Boot Menu</a> for more info.
    </p>
</asp:Content>