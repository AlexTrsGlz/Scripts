<asp:UpdatePanel ID="UpdDelete" runat="server" UpdateMode="Conditional" EnableViewState="false" ChildrenAsTriggers="false">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnTriggerUpdatePanel" EventName="Click" />
    </Triggers>
	<ContentTemplate>
		<owd:Dialog ID="dlg" runat="server" StyleFolder="~/web/Resources/WindowStyle/dogma"
			VisibleOnLoad="false" Visible="false" Width="220"  IsModal="true" IsDraggable="false" ShowCloseButton="false" Title="<%$ Resources:ClaseRecursos,Delete %>" >
			<div id="Div3" style="width: 100%; margin-top: 5px">
				<p style="width: 90%; text-align: left; margin-left: 10px">
					<%= Resources.ClaseRecursos.ConfirmationDeleteMessage%>
				</p>
			</div>
			<div id="Div4" style="width: 100%; text-align: center">
				<table style="width:100%; border-collapse: collapse; border:10%; border-bottom:10%; border-top:10%">
					<tr>
						<td style="width:45%; text-align:center">
							<asp:Button ID="BtnConfirmDelete" OnClick="BtnConfirmDelete_Click" Text="<%$ Resources:ClaseRecursos, Delete  %>"  
								CausesValidation="false"  Visible="true" class="Botones" runat="server" ></asp:Button>
						</td>
						<td style="width:45%; text-align:center">
							<asp:Button ID="BtnCancelDelete" OnClick="BtnCancelDelete_Click" Text="<%$ Resources:ClaseRecursos, Cancel  %>" 
								CausesValidation="false" class="Botones" Visible="true" runat="server" ></asp:Button>
						</td>
					</tr>
				</table>
			</div>
		</owd:Dialog>
	</ContentTemplate>
</asp:UpdatePanel>