﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventList.ascx.cs" Inherits="Stprm.Web.EventList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register src="~/IdSearchControl.ascx" TagName="IdSearch" TagPrefix="med" %>

<ajax:ComboBox runat="server" ID="_cmb_places" Width="273px" DropDownStyle="DropDownList" AutoPostBack="true" />
<table width="100%">
	<tr>
		<td rowspan="3" width="310px" valign="top">
			<asp:ListBox runat="server" ID="_lst_events" OnSelectedIndexChanged="_lst_events_SelectedIndexChanged" Height="150px" width="300px" BorderColor="#999999" BorderWidth="1px" BorderStyle="Solid" AutoPostBack="true" />		
		</td>
		<td width="120px">
		<asp:Label runat="server" ID="_lbl_name" Text="Nombre del evento"/> 
		</td>
		<td>
			<asp:HiddenField runat="server" ID="_txt_id" Value="0" />
			<asp:Textbox runat="server" ID="_txt_name" style="width: 100%" /> 
		</td>
	</tr>
	<tr>
		<td><asp:label runat="server" ID="_lbl_place" Text="Lugar" /></td>
		<td><asp:TextBox runat="server" ID="_txt_address" style="width: 100%" /> </td>
	</tr>
	<tr>
		<td><asp:label runat="server" ID="_lbl_date" Text="Fecha" /></td>
		<td>
		<asp:Textbox runat="server" ID="_txt_date" ContentEditable="false" />
		<asp:Button runat="server" ID="button" Text=" " CssClass="skel_calendar_button" />
		<asp:Button runat="server" ID="_btn_save" Text="Guardar" Visible="false" 
				onclick="_btn_save_Click" />
		</td>
	</tr>
<ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="_txt_date" PopupButtonID="button" Format="dd-MM-yyyy">
</ajax:CalendarExtender>
</table>

<asp:Button runat="server" Text="Nuevo" id="_btn_new" OnClick="btn_new_Click" /> 
<asp:Button runat="server" Text="Modificar" id="_btn_edit" OnClick="btn_edit_Click" />
<asp:UpdatePanel runat="server">
<ContentTemplate>
<asp:Panel runat="server" ID="_pnl_edit" Visible="false" HorizontalAlign="Center">
	<hr />
	<table border="0">
    <tr>
    <td>
    <med:IdSearch runat="server" ID="_isc_search" />
    </td>
    <td>
	Mostrar: 
	<ajax:ComboBox runat="server" ID="_cbb_showtype" AutoPostBack="true" >
	<asp:ListItem Selected="True" Text="Todos" Value="all" />
	<asp:ListItem Selected="True" Text="Transitorios" Value="trans" />
	<asp:ListItem Selected="True" Text="De Planta" Value="plant" />
	</ajax:ComboBox>
	<asp:Button runat="server" ID="_btn_report" Text="Generar Impreso" />
    </td>
	</tr>
    </table>
    <hr />
	<asp:Panel ID="Panel1" runat="server" ScrollBars="Auto">
	<asp:GridView RowStyle-CssClass="gridview_rows" Width="100%" CssClass="gridview" HeaderStyle-CssClass="gridview_header" ShowHeader="true" runat="server" ID="_grid_members"  
	AutoGenerateColumns="false" AlternatingRowStyle-BackColor="#FFDFDF">
		<Columns>
            <asp:BoundField HeaderText="#" DataField="Num" />
			<asp:BoundField HeaderText="Ficha" DataField="Ficha" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField HeaderText="Regimen" DataField="SitContr" />
            <asp:BoundField HeaderText="Tipo de Apoyo" DataField="Apoyo" />
			
			<asp:TemplateField HeaderText="Suprimir">
				<ItemTemplate>
					<asp:Button runat="server" ID="_btn_delete" CommandName='<%# Bind("Ficha", "{0}") %>' Text=" " CssClass="misc_button_delete" OnClick="btn_delete_Click" />
					<ajax:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText='Estas seguro que deseas eliminar el trabajador del evento?' TargetControlID="_btn_delete" />
				</ItemTemplate>
				<ItemStyle HorizontalAlign="Center" />
			</asp:TemplateField>
		</Columns>
	</asp:GridView>
	</asp:Panel>
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>

