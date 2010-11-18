<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IdSearchControl.ascx.cs" Inherits="Stprm.Web.IdSearchControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>


	<table>
		<tr>
			<td>Ficha :</td>
			<td><asp:TextBox runat="server" ID="_txt_id" /></td>
			<td><asp:Button runat="server" ID="_btn_search" CssClass="misc_button_search" Text=" "/></td>
			<td><asp:Panel runat="server" ID="_loading" CssClass="icon_stock_loading" style="visibility: hidden;" /></td>
			<td><asp:Label ForeColor="Red" runat="server" ID="_lbl_msg" Text="" /></td>
            <td><input type="button" value="Nuevo" onclick="popup('EmployeeNew.aspx', 350, 220)" /></td>
		</tr>
	</table>

