﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Escalafones.aspx.cs" Inherits="Stprm.Web.Escalafones" MasterPageFile="~/Site.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content runat="server" ContentPlaceHolderID="body">
Escalafon <ajax:ComboBox runat="server" id="_cmb_escalafon" Width="300px" AutoPostBack="true" DropDownStyle="DropDownList" />
<hr />

<asp:GridView runat="server" ID="_gv_escalafones" AutoGenerateColumns="true" AlternatingRowStyle-BackColor="#FFDFDF" RowStyle-CssClass="gridview_rows" HeaderStyle-CssClass="gridview_header" Width="100%" CssClass="gridview">
<EmptyDataTemplate>
No hay datos
</EmptyDataTemplate>
</asp:GridView>

</asp:Content>
