<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Consulta.aspx.cs" Inherits="Stprm.Web.Consulta" MasterPageFile="~/Site.Master" %>
<%@ Register src="~/BuscarFichaControl.ascx" TagName="Buscarficha" TagPrefix="med" %>
<%@ Register src="~/InfoTrabControl.ascx" TagName="InfoTrab" TagPrefix="med" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
<med:Buscarficha runat="server" ID="_bf_buscar" />
<hr />
<table width="100%">
    <tr>
        <td><med:InfoTrab runat="server" ID="_it_planta" /></td>
        <td><med:InfoTrab runat="server" ID="_it_transitorio" /></td>
    </tr>
</table>
<hr />
<asp:Button runat="server" ID="_btn_contratos" Text="Contratos" CssClass="button_unselected" /> | 
<asp:Button runat="server" ID="_btn_escalafon" Text="Escalafon" CssClass="button_unselected" /> |
<asp:Button runat="server" ID="_btn_militancia" Text="Militancias" CssClass="button_unselected" /> |
<asp:Button runat="server" ID="_btn_beneficios" Text="Beneficios" CssClass="button_unselected" /> |
<asp:Button runat="server" ID="_btn_derechohabiencia" Text="Derechohabiencia" CssClass="button_unselected" />
<hr />
<asp:Panel runat="server" ScrollBars="Auto" Width="100%">
<asp:GridView runat="server" ID="_gv_contratos" AutoGenerateColumns="false" AlternatingRowStyle-BackColor="#FFDFDF" RowStyle-CssClass="gridview_rows" Width="100%" CssClass="gridview" HeaderStyle-CssClass="gridview_header" ShowHeader="true">
<EmptyDataTemplate>
No hay datos
</EmptyDataTemplate>
</asp:GridView>
</asp:Panel>
</asp:Content>

