<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserProfiles.aspx.cs" Inherits="Stprm.Web.UserProfiles" MasterPageFile="~/Site.Master" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
<div style="text-align:left;">
	<asp:Button ID="_btn_new" runat="server" Text="Nuevo" /> 
	<asp:Button ID="_btn_save" runat="server" Text="Guardar" Enabled="false" />
	<asp:Panel ID="_pnl_userdata" DefaultButton="_btn_save" runat="server" Visible="false" >
		<table> 
			<tr>
			<td>Usuario</td>
			<td><asp:TextBox ID="_txt_username" runat="server" /></td>
			</tr>
	
			<tr>
				<td>Password</td>
				<td><asp:TextBox TextMode="Password" ID="_txt_password" runat="server" /></td>
			</tr>
			<tr>
				<td>Nombre</td>
				<td><asp:TextBox id="_txt_name" runat="server" /></td>
			</tr>
			<tr>
				<td>Email</td>
				<td><asp:TextBox ID="_txt_email" runat="server" /></td>
			</tr>
			<tr>
				<td><asp:Button runat="server" ID="_btn_cancel" Text="Cancelar" /></td>
			</tr>
		</table>
	</asp:Panel>
	<hr />
    <asp:GridView id="_grid_profiles" runat="server" AutoGenerateColumns="false" CssClass="gridview">
	<Columns>
		<asp:TemplateField>
			<ItemTemplate>
				<span ><%# Container.DataItemIndex + 1 %></span>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:BoundField DataField="Username" HeaderText="Nombre de Sesión" />
		<asp:BoundField DataField="Password" HeaderText="Password Encriptado" />
		<asp:BoundField DataField="Name" HeaderText="Nombre" />
		<asp:BoundField DataField="Email" HeaderText="Correo Electrónico" />
		<asp:CheckBoxField DataField="Active" HeaderText="Activo" />
		<asp:TemplateField HeaderText="">
		<ItemTemplate>
			<asp:Button runat="server" ID="_btn_edit" CommandName='<%# Bind("Username") %>' Text="Editar" CssClass="misc_button_delete" OnClick="btn_edit_Click" />	
		</ItemTemplate>
		</asp:TemplateField>
	</Columns>
    </asp:GridView>
    
    </div>


</asp:Content>