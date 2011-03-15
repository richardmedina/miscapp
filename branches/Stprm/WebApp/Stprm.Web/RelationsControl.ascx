<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RelationsControl.ascx.cs" Inherits="Stprm.Web.RelationsControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register src="~/EmployeeInfo.ascx" tagname="EmployeeInfo" tagprefix="richard" %>
<%@ Register Src="~/IdSearchControl.ascx" TagName="IdSearch" TagPrefix="richard" %>
<div id="anim_id_search" class="skel_anim_id_search">
</div>


Trabajador de planta <richard:IdSearch ID="_is_buscarplanta" runat="server" Visible="true" />
<hr />
<richard:EmployeeInfo ID="_ei_plant" runat="server" Visible="false" />
<hr />
Recomendado: <richard:IdSearch Id="_is_buscartransitorio" runat="server" Visible="false" />
<p />
<richard:EmployeeInfo ID="_ei_trans" runat="server" Visible="false" /> <br />
<asp:Panel runat="server" ID="_pnl_parentesco" Visible="false">
<center>
<table>
<tr>
<td>
Parentesco:
</td>
<td>
<asp:TextBox runat="server" ID="_txt_parentesco" ReadOnly="true" Visible="true" /> 
<asp:Button runat="server" ID="_btn_assign" Text="Asignar" Visible="false" />
</td>
</tr>
</table>
</center>
</asp:Panel>