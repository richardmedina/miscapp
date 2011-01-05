<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NuevaAsamblea.aspx.cs" Inherits="Stprm.Web.NuevaAsamblea" MasterPageFile="~/Site.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register src="~/IdSearchControl.ascx" TagName="IdSearch" TagPrefix="med" %>
<%@ Register src="~/EmployeeInfo.ascx" TagName="EmployeeInfo" TagPrefix="med" %>
<%@ Register src="~/RegistrarFichaControl.ascx" TagName="RegFichaControl" TagPrefix="med" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <h1>Asamblea General Extraordinaria</h1>
    <h3><asp:TextBox runat="server" ID="_txt_fecha" Font-Size="Large" Text="25/12/2010" BorderWidth="0px" /></h3>
    <ajax:CalendarExtender runat="server" id="_ace_fecha" TargetControlID="_txt_fecha"  />
    <hr />
    <asp:Button runat="server" Text="Continuar>>" id="_btn_continuar" />
<asp:Panel runat="server" ID="_pnl_bancotrabajo" Visible = "false" >
<h4>Por favor teclee la información requerida para registrar la asistencia.</h4>
<hr />
<med:IdSearch id="_ids_ficha" runat="server" />
<hr />
<med:RegFichaControl runat="server" ID="_rfc_registrar" />
<hr />
<asp:GridView ID="_gv_miembros" runat="server" />
</asp:Panel>
</asp:Content>