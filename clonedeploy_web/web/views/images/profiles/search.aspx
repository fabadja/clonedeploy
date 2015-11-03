﻿<%@ Page Title="" Language="C#" MasterPageFile="~/views/images/profiles/profiles.master" AutoEventWireup="true" CodeFile="search.aspx.cs" Inherits="views_images_profiles_search" %>



<asp:Content ID="Content1" ContentPlaceHolderID="BreadcrumbSub2" Runat="Server">
    <li>Search</li>
</asp:Content>


<asp:Content runat="server" ContentPlaceHolderID="SubHelp">
    <a href="<%= ResolveUrl("~/views/help/index.html") %>" class="icon help" data-info="Help" target="_blank"></a>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ActionsRightSub">
      <a class="confirm" href="#">Delete Selected Profiles</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SubContent2" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function() {
            $('#search').addClass("nav-current");
        });
    </script>
   
   
    <br class="clear"/>

    <asp:GridView ID="gvProfiles" runat="server" AllowSorting="True" DataKeyNames="Id" OnSorting="gridView_Sorting" AutoGenerateColumns="False" CssClass="Gridview" AlternatingRowStyle-CssClass="alt">
        <Columns>
            <asp:TemplateField>
                <HeaderStyle CssClass="chkboxwidth"></HeaderStyle>
                <ItemStyle CssClass="chkboxwidth"></ItemStyle>
                <HeaderTemplate>
                    <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelectAll_CheckedChanged"/>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelector" runat="server"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="ID" Visible="False"/>
            <asp:BoundField DataField="ImageId" HeaderText="ImageID" SortExpression="ImageID" Visible="False"/>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" ItemStyle-CssClass="width_200"></asp:BoundField>
            <asp:HyperLinkField DataNavigateUrlFields="Id,ImageId" DataNavigateUrlFormatString="~/views/images/profiles/chooser.aspx?imageid={1}&profileid={0}&cat=profiles" Text="View"/>
        </Columns>
        <EmptyDataTemplate>
            No Profiles Found
        </EmptyDataTemplate>
    </asp:GridView>
  
    <div id="confirmbox" class="confirm-box-outer">
        <div class="confirm-box-inner">
            <h4>
                <asp:Label ID="lblTitle" runat="server" Text="Delete The Selected Hosts?"></asp:Label>
            </h4>
            <div class="confirm-box-btns">
                <asp:LinkButton ID="ConfirmButton" OnClick="ButtonConfirmDelete_Click" runat="server" Text="Yes" CssClass="confirm_yes"/>
                <asp:LinkButton ID="CancelButton" runat="server" Text="No" CssClass="confirm_no"/>
            </div>
        </div>
    </div>
</asp:Content>

