<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Audiencies.aspx.cs" Inherits="Stprm.Web.Audiencies" MasterPageFile="~/Site.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" Runat="Server">
<asp:UpdatePanel runat="server" ID="wizard">
<ContentTemplate>
<h1>Registro de Audiencias</h1>
<table width="100%">
<tr>
<td valign="top">
<h2>Fecha de audiencia</h2>
	<ul>
		<li><asp:RadioButton runat="server" ID="_radio_today" GroupName="grp_when" BorderWidth="0" Checked="true" AutoPostBack="true" /></li>
		<li>
			<asp:RadioButton runat="server" ID="_radio_later" GroupName="grp_when" Text="Audiencia para una Fecha posterior" BorderWidth="0px" AutoPostBack="true" /><br />
			<asp:TextBox ID="_txt_date" runat="server" Text="" ContentEditable="false" Visible="false" AutoPostBack="true" /> 
			<ajax:CalendarExtender id="_cal_extender" runat="server" Format="dd-MM-yyyy" TargetControlID="_txt_date" PopupButtonID="txt_date" />
		</li>
	</ul>
</td>
<td valign="top">
<h2>Tipo de audiencia</h2>
	<ul>
		<li><asp:CheckBox ID="_chk_plant" runat="server" Text="Trabajadores Planta" /></li>
		<li><asp:CheckBox ID="_chk_trans" runat="server" Text="Trabajadores Transitorios" /></li>
		<li><asp:CheckBox ID="_chk_extern" runat="server" Text="Externos" /></li>
	</ul>
</td>
</tr>	
</table>
<center>
<asp:Button ID="_btn_aud" runat="server" Text="Crear Audiencia" />
</center>
<asp:Panel ID="_pnl_employees" runat="server" Visible="false">
	<hr />
	Tipo: 
	<ajax:ComboBox runat="server" ID="_cmb_type" AutoCompleteMode="Suggest" AutoPostBack="true" DropDownStyle="DropDownList">
		<asp:ListItem Text="Trabajador" Value="worker" Selected="True" />
		<asp:ListItem Text="Externo" Value="extern" />
	</ajax:ComboBox>
	<asp:Label runat="server" ID="_lbl_type" Text="Ficha" />
	<asp:TextBox ID="_txt_id" runat="server" style="width: 70px" /> 
	Asunto: <asp:TextBox ID="_txt_matter" runat="server" style="width: 300px" />
	<asp:Button ID="_btn_add" Text="Agregar" runat="server" />
	<hr />
	<h2>Lista de trabajadores</h2>
	<asp:GridView runat="server" ID="_grid_employees" Width="100%" CssClass="gridview" AutoGenerateColumns="false" >
	<Columns>
	<asp:TemplateField HeaderText="Pos.">
		<ItemTemplate>
			<span >
				<%# Container.DataItemIndex + 1 %>
			</span>
		</ItemTemplate>
	</asp:TemplateField>
	<asp:BoundField DataField="Ficha" HeaderText="Ficha" />
	<asp:BoundField DataField="Nombre" HeaderText="Trabajador" />
	<asp:BoundField DataField="REG_CONTR" HeaderText="Regimen" />
	<asp:BoundField DataField="DEPTO" HeaderText="Departamento" />
	<asp:BoundField DataField="ASUNTO" HeaderText="Asunto" />
	<asp:TemplateField HeaderText="Acciones">
		<ItemTemplate>
			<asp:Button runat="server" ID="_btn_delete" CommandName='<%# Bind("Ficha", "{0}") %>' Text=" " CssClass="misc_button_delete" OnClick="btn_delete_Click" />
			<ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText='Estas seguro que deseas eliminar el trabajador del evento?' TargetControlID="_btn_delete" />
		</ItemTemplate>
		<ItemStyle HorizontalAlign="Center" />
	</asp:TemplateField>
	</Columns>
	</asp:GridView>
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>	
</asp:Content>

