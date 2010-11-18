<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="Stprm.Web.Events" MasterPageFile="~/Site.Master" %>
<%@ Register src="~/EventList.ascx" TagName="EventList" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
<asp:UpdatePanel runat="server" id="panel">
<ContentTemplate>
	<h1>Militancias</h1>
	<asp:Panel runat="server" id="_pnl_events">
		<asp:EventList ID="myeventlist" runat="server" style="width: 80%; border: solid 0px gray;"/>
	</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
