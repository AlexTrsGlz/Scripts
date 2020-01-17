<%@ Page Language="C#" MasterPageFile="~/WEB/Masters/MasterDefault.Master" AutoEventWireup="true" CodeBehind="Pag_SurchargeRate.aspx.cs" Inherits="SupplyTrack.WEB.Forms.Pag_SurchargeRate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit"			   	   TagPrefix="cc1" %>
<%@ Register Assembly="WebControlsEx"	   Namespace="WebControlsEx"					   TagPrefix="cc2" %>
<%@ Register Assembly="obout_Window_NET"   Namespace="OboutInc.Window"					   TagPrefix="owd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
		<scripts>
			<asp:ScriptReference Path="~/WEB/Resources/Javascript/WebKitHack.js" />
		</scripts>
	</asp:ScriptManager>
<!------------------------------------------------------ Modal Progress ------------------------------------------------------>
	<cc1:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="pnlPopup"
		 PopupControlID="pnlPopup" BackgroundCssClass="backgroundprogressbar" />
	<asp:Panel ID="pnlPopup" runat="server" CssClass="progressbar" Style="display: none"
		 Width="200px">
		 <%=LocRM.GetString("ProcesandoEspere") %>...
		 <div class="Loading" style="width: 100%">
			  &nbsp;</div>
	</asp:Panel>
<!---------------------------------------------------------------------------------------------------------------------------->

<!---------------------------------------------------- POP DELETE SECTION ----------------------------------------------------->
<asp:UpdatePanel ID="UpdDelete" runat="server" UpdateMode="Conditional">
	<Triggers>
	</Triggers>
	<ContentTemplate>
	</ContentTemplate>
</asp:UpdatePanel>
<!----------------------------------------------------------------------------------------------------------------------------->

<!------------------------------------------------------- SCRIPTS ------------------------------------------------------------>	
	<script type="text/javascript" language="javascript">
	</script>
<!---------------------------------------------------------------------------------------------------------------------------->
</asp:Content>