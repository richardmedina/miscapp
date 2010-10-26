<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeSearch.aspx.cs" Inherits="Stprm.Web.EmployeeSearch" MasterPageFile="~/Site.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server" >
	<asp:UpdatePanel id="updatepanel1" runat="server">
	<ContentTemplate>
	<h1>Consulta de trabajadores</h1>
	
	Ficha: <asp:TextBox id="_txt_id" runat="server" style="width: 70px;" />
	<asp:RegularExpressionValidator id="_val_txt_id" runat="server"
		ControlToValidate="_txt_id"
		ValidationExpression="\d{5,6}"
		ErrorMessage="Por favor escriba una ficha valida"
		display="none">*
	</asp:RegularExpressionValidator>
	<asp:RequiredFieldValidator id="_val_txt_id_empty" runat="server"
		ControlToValidate="_txt_id" Text="Helo"
		ErrorMessage="Por favor escriba una ficha valida"
		display="none">*</asp:RequiredFieldValidator>
	<ajax:ValidatorCalloutExtender ID="val" TargetControlID="_val_txt_id" runat="server" />
	<ajax:ValidatorCalloutExtender ID="val2" TargetControlID="_val_txt_id_empty" runat="server" />
	<asp:Button id="_btn_ok" Text=" "  runat="server" OnClick="_btn_okClick" CssClass="misc_button_search" />
	<div id="anim_id_search" class="skel_anim_id_search">
	</div>
	</ContentTemplate>
	</asp:UpdatePanel>

	<asp:UpdatePanel ID="updatepanel2" runat="server">
	<ContentTemplate>
		<h2>Información básica</h2>
		Ficha. <asp:TextBox ID="_txt_data_id" runat="server" style="width:70px;" /></td>
		Nombre. <asp:TextBox ID="_txt_data_name" runat="server" style="width: 380px;" />
		Fecha de Nacimiento <asp:TextBox ID="_txt_data_borndate" runat="server" style="width: 90px;"/>
		<br />&nbsp;<br />
		Calle. <asp:TextBox ID="_txt_data_street" runat="server" style="width: 300px;" />
		Colonia. <asp:TextBox ID="_txt_data_colony" runat="server" style="width: 90px;" />
		Localidad. <asp:TextBox ID="_txt_data_locality" runat="server" style="width: 90px;" />
		Estado. <asp:TextBox ID="_txt_data_state" runat="server" style="width: 70px;" />
		<br />&nbsp;<br />
		C.P. <asp:TextBox ID="_txt_data_postal" runat="server" style="width: 70px;" />
		Edo.Civil <asp:TextBox ID="_txt_data_civil" runat="server" style="width: 70px;" />
		Régimen Contractual <asp:TextBox id="_txt_data_reg" runat="server" style="width: 70px;" />
		Teléfono <asp:TextBox ID="_txt_data_phone_personal" runat="server" style="width: 97px;" />
		Teléfono Emerg. <asp:TextBox ID="_txt_data_phone_emergency" runat="server" style="width: 97px;" />
		<br />&nbsp;<br />
		RFC. <asp:TextBox runat="server" ID="_txt_data_rfc" />
		CURP. <asp:TextBox runat="server" ID="_txt_data_curp" /> &nbsp;
		<br />&nbsp;<br />
		<center>
		<asp:Panel runat="server" ID="_pnl_reports">
		<a href="ViewReport.aspx?report_type=employee_resume&employee_id=<% =_txt_data_id.Text %>" target="_blank">
		Generar Resumen Simple
		</a> &nbsp;
		<!--
		<a href="ViewReport.aspx?Type=simple&id=<% =_txt_data_id.Text %>" target="_blank">
		Generar Resumen Detallado
		</a>-->
		</asp:Panel>
		</center>
		<h2>Información detallada</h2>
		
		<ajax:TabContainer ID="_tabulator" runat="server" ActiveTabIndex="0" 
			Visible="true">
		<ajax:TabPanel ID="_tab_current" runat="server" HeaderText="Puesto actual">
		<ContentTemplate>
			<h2>Ascenso actual</h2>
			Plaza. <asp:TextBox runat="server" ID="_txt_currentposition_posnum" style="width: 300px;"/>
			Nivel. <asp:TextBox runat="server" ID="_txt_currentposition_level" style="width: 20px;"/>
			Clasificación. <asp:TextBox runat="server" ID="_txt_currentposition_clasif" />
			Sección. <asp:TextBox runat="server" ID="_txt_currentposition_section" style="width: 25px" />
			<br />&nbsp;<br />
			Categoría. <asp:TextBox runat="server" ID="_txt_currentposition_category" style="width: 250px;" />
			Jor. <asp:TextBox runat="server" ID="_txt_currentposition_jor" style="width: 20px;" />
			Depto. <asp:TextBox runat="server" ID="_txt_currentposition_depto" style="width: 270px;" />
			<br />&nbsp;<br />
			Vigencia. <asp:TextBox runat="server" ID="_txt_currentposition_validity_start" style="width: 200px;" />
			al <asp:TextBox runat="server" ID="_txt_currentposition_validity_end" style="width: 200px;" />
		</ContentTemplate>
		</ajax:TabPanel>
		
		<ajax:TabPanel ID="_tab_base" runat="server" HeaderText="Puesto base">
		<ContentTemplate>
			<h2>Puesto base</h2>
			Plaza. <asp:TextBox runat="server" ID="_txt_baseposition_posnum" style="width: 300px;"/>
			Nivel. <asp:TextBox runat="server" ID="_txt_baseposition_level" style="width: 20px;"/>
			Clasificación. <asp:TextBox runat="server" ID="_txt_baseposition_clasif" />
			Sección. <asp:TextBox runat="server" ID="_txt_baseposition_section" style="width: 25px" />
			<br />&nbsp;<br />
			Categoría. <asp:TextBox runat="server" ID="_txt_baseposition_category" style="width: 250px;" />
			Jor. <asp:TextBox runat="server" ID="_txt_baseposition_jor" style="width: 20px;" />
			Depto. <asp:TextBox runat="server" ID="_txt_baseposition_depto" style="width: 270px;" />
			<br />&nbsp;<br />
			Vigencia. <asp:TextBox runat="server" ID="_txt_baseposition_validity_start" style="width: 200px;" />
			al <asp:TextBox runat="server" ID="_txt_baseposition_validity_end" style="width: 200px;" />
		</ContentTemplate>
		</ajax:TabPanel>
		
		<ajax:TabPanel ID="_tab_contracts" HeaderText="Contratos" runat="server">
		<HeaderTemplate>Contratos</HeaderTemplate>
			<ContentTemplate>
				<h2>Histórico de contratos</h2>
				<br />
				<asp:Panel ID="_pnl_contracts" ScrollBars="Auto" 
				runat="server" Height="200px" BorderColor="DarkGray" 
				BorderStyle="Solid" BorderWidth=1 Width="100%">
				<asp:GridView ID="_grid_contracts" runat="server" AutoGenerateColumns="false" Width="100%">
				<Columns>
					<asp:BoundField DataField="NUM_PLAZA" HeaderText="Plaza" />
					<asp:BoundField DataField="NUM_TARJ" HeaderText="Contrato" ItemStyle-HorizontalAlign="Center" />
					<asp:BoundField DataField="FECHA" HeaderText="Fecha" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yy}" />
					<asp:BoundField DataField="VIG_INI" HeaderText="Inicio" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yy}" />
					<asp:BoundField DataField="VIG_FIN" HeaderText="Fin" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yy}" />
					<asp:BoundField DataField="VIG_REAL" HeaderText="Corte" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yy}" />
				</Columns>
				</asp:GridView>
				</asp:Panel>
				
			</ContentTemplate>
		</ajax:TabPanel>
		
		
		<ajax:TabPanel ID="_tab_benefits" HeaderText="Beneficios" runat="server" >
		<ContentTemplate>
		<h2>Empresa.</h2>
		<br />&nbsp;<br />
		<center>
		<asp:GridView runat="server" ID="_grid_debits" CssClass="skel_grid" />
		</center>
		<center>--</center>
		<h2>Terceros.</h2>
		<center>
		<asp:GridView runat="server" ID="_grid_thirdparties" CssClass="skel_grid" />
		</center>
		<br />&nbsp;<br />
		<center>--</center>
		<h2>Prestaciones</h2>
		<asp:GridView runat="server" ID="_grid_benefits" CssClass="skel_grid" />
		<br />&nbsp;<br />
		<center>--</center>
		</ContentTemplate>
		</ajax:TabPanel>
		
		<ajax:TabPanel ID="_tab_parents" HeaderText="Derechohabiencia" runat="server" >
		<ContentTemplate>
			<h2>Derechohabientes</h2>
			<br />&nbsp;<br />
			<center>
			<asp:GridView runat="server" ID="_grid_parents_list" OnRowDataBound="OnRowDatabound" 
			 CssClass="skel_grid" />
			</center>
		</ContentTemplate>
		</ajax:TabPanel>
		
		<ajax:TabPanel ID="_tab_recommended" HeaderText="Recomendado" runat="server">
		<ContentTemplate>
			<h2>Datos del recomendado</h2>
			Ficha. <asp:TextBox ID="_txt_recom_id" runat="server" style="width:70px;" /></td>
			Nombre. <asp:TextBox ID="_txt_recom_name" runat="server" style="width: 315px;" />
			Fecha de Nacimiento <asp:TextBox ID="_txt_recom_borndate" runat="server" style="width: 90px;"/>
			<br />&nbsp;<br />
			Calle. <asp:TextBox ID="_txt_recom_street" runat="server" style="width: 232px;" />
			Colonia. <asp:TextBox ID="_txt_recom_colony" runat="server" style="width: 90px;" />
			Localidad. <asp:TextBox ID="_txt_recom_locality" runat="server" style="width: 90px;" />
			Estado. <asp:TextBox ID="_txt_recom_state" runat="server" style="width: 70px;" />
			<br />&nbsp;<br />
			C.P. <asp:TextBox ID="_txt_recom_postal" runat="server" style="width: 60px;" />
			Edo.Civil <asp:TextBox ID="_txt_recom_civil" runat="server" style="width: 70px;" />
			Régimen Contractual <asp:TextBox id="_txt_recom_reg" runat="server" style="width: 70px;" />
			Tel. <asp:TextBox ID="_txt_recom_phone_personal" runat="server" style="width: 77px;" />
			Tel. Emerg. <asp:TextBox ID="_txt_recom_phone_emergency" runat="server" style="width: 97px;" />
			<br />&nbsp;<br />
			RFC. <asp:TextBox runat="server" ID="_txt_recom_rfc" />
			CURP. <asp:TextBox runat="server" ID="_txt_recom_curp" />
			<br />&nbsp;<br />&nbsp;<br />
			<h2>Contratos del recomendado</h2>
				<br />
				<asp:Panel ID="Panel1" ScrollBars="Auto" 
				runat="server" Height="200px" BorderColor="DarkGray" 
				BorderStyle="Solid" BorderWidth=1 Width="100%">
				<asp:GridView ID="_grid_contracts_recom" runat="server" AutoGenerateColumns="false" Width="100%">
				<Columns>
					<asp:BoundField DataField="NUM_PLAZA" HeaderText="Plaza" />
					<asp:BoundField DataField="NUM_TARJ" HeaderText="Contrato" ItemStyle-HorizontalAlign="Center" />
					<asp:BoundField DataField="FECHA" HeaderText="Fecha" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yy}" />
					<asp:BoundField DataField="VIG_INI" HeaderText="Inicio" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yy}" />
					<asp:BoundField DataField="VIG_FIN" HeaderText="Fin" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yy}" />
					<asp:BoundField DataField="VIG_REAL" HeaderText="Corte" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yy}" />
				</Columns>
				</asp:GridView>
				</asp:Panel>
			<h2>Labor social del recomendado</h2>
			<center>
			<asp:GridView ID="_grid_socialwork_recom" runat="server" OnRowDataBound="OnRowDatabound" CssClass="skel_grid" />
			</center>
		</ContentTemplate>
		</ajax:TabPanel>
		
		<ajax:TabPanel ID="_tab_socialwork" HeaderText="Labor Social" runat="server">
		<ContentTemplate>
			<h2>Participaciones</h2>
			<br />&nbsp;<br />
			<center>
			<asp:GridView Id="_grid_militancy" runat="server" CssClass="skel_grid" OnRowDataBound="OnRowDatabound" />
			</center>
		</ContentTemplate>
		</ajax:TabPanel>
	</ajax:TabContainer>
	</ContentTemplate>
	</asp:UpdatePanel>
	<ajax:UpdatePanelAnimationExtender
            id="upae1" BehaviorID="animation"
            TargetControlID="updatepanel1"
            runat="server">
        <Animations>
            <OnUpdating>
		<Parallel duration="0">
			<ScriptAction Script="document.getElementById('anim_id_search').style.visibility='visible';" />
			<EnableAction AnimationTarget="_btn_ok" Enabled="false" />
			<EnableAction AnimationTarget="_txt_id" Enabled="false" />
		</Parallel>
            </OnUpdating>
            <OnUpdated>
		<Parallel duration="0">
		<ScriptAction Script="document.getElementById('anim_id_search').style.visibility='hidden';" />
		</Parallel>
            </OnUpdated>
        </Animations>
        </ajax:UpdatePanelAnimationExtender>
        <br /> &nbsp; <br />
</asp:Content>