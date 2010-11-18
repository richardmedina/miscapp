<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RelationsControl.ascx.cs" Inherits="Stprm.Web.RelationsControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register src="~/EmployeeInfo.ascx" tagname="EmployeeInfo" tagprefix="richard" %>

<div id="anim_id_search" class="skel_anim_id_search">
</div>

Ficha: <asp:TextBox runat="server" ID="_txt_id" style="width: 70px;" />
<asp:Button runat="server" ID="_btn_search" Text=" " CssClass="misc_button_search" /> 
<asp:Label runat="server" ID="_lbl_search" Visible="false" Text="" ForeColor="Red" />

<hr />
<richard:EmployeeInfo ID="_ei_plant" runat="server" Visible="false" />
<hr />
Recomendado: <asp:TextBox runat="server" ID="_txt_id_recom" style="width: 70px;" />
<asp:Button runat="server" ID="_btn_search_recom" Text=" " CssClass="misc_button_search" /> 
<asp:Label runat="server" ID="_lbl_recom_result" ForeColor="Red" Visible="false" />
<p />
<richard:EmployeeInfo ID="_ei_trans" runat="server" Visible="false" /> </p>
<asp:Button runat="server" ID="_btn_assign" Text="Asignar" Enabled="false" />
<asp:Button runat="server" ID="_btn_assign_cancel" Text="Cancelar" Enabled="false" />

