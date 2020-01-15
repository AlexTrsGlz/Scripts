<asp:UpdatePanel ID="UpdGridSurchargeRate" runat="server" UpdateMode="Conditional"> 
		<Triggers>
        </Triggers>
		<ContentTemplate>
			<table style="width:100%; border-collapse: collapse;">
				<tr>
					<td align="center" style="width:100%;">
						<asp:GridView style="width:90%;margin:2%; border-collapse: collapse;" ID="GridSurchargeRate"
							SkinID="gridViewSkin" PageSize="10"  
							AutoGenerateColumns="false" AutoPostBack="true" AllowPaging="true" ShowFooter="false" runat="server"
							DataKeyNames="IdTasaRecargos" OnRowDeleting="GridSurchargeRate_RowDeleting" OnPageIndexChanging="GridSurchargeRate_PageIndexChanging"
							OnRowEditing="GridSurchargeRate_RowEditing" OnRowCancelingEdit="GridSurchargeRate_RowCancelingEditing" OnRowUpdating="GridSurchargeRate_RowUpdating">
							<Columns>
								<%----------------------------------------- COLUMNA AÑO ------------------------------------------------------%>
								<asp:TemplateField HeaderText="<%$ Resources:SupplyTrack, Year %>" ControlStyle-Width="20%" HeaderStyle-Width="20%" ItemStyle-Width="20%" >
									<itemtemplate>
										<table align="center" style="width:100%; border-collapse: collapse;" cellpadding="0" cellspacing="0">
											<tr>
												<td align="center" style="width:90%;margin:2%; border-collapse: collapse;">                                                
													<asp:Label ID="lblYear" Text= '<%# Eval("Year") %>' runat="server" ></asp:Label>
												</td>
											</tr>
										</table>						                							                   
									</itemtemplate>
								</asp:TemplateField>
								<%-------------------------------------------------------------------------------------------------------------%>

								<%------------------------------------------ COLUMNA MES ------------------------------------------------------%>
								<asp:TemplateField HeaderText="<%$ Resources:SupplyTrack, Mes %>" ControlStyle-Width="20%" HeaderStyle-Width="20%" ItemStyle-Width="20%" >
									<itemtemplate>
										<table align="center" style="width:100%; border-collapse: collapse;" cellpadding="0" cellspacing="0">
											<tr>
												<td align="center" style="width:90%;margin:2%; border-collapse: collapse;">                                                
													<asp:Label ID="lblMonth" Text= '<%# Eval("StrgMonth") %>' runat="server"></asp:Label>
												</td>
											</tr>
										</table>						                							                   
									</itemtemplate>
								</asp:TemplateField>
								<%--------------------------------------------------------------------------------------------------------------%>

								<%------------------------------------------ COLUMNA TASA ------------------------------------------------------%>
								<asp:TemplateField HeaderText="<%$ Resources:SupplyTrack, Rate %>" ControlStyle-Width="40%" HeaderStyle-Width="40%" ItemStyle-Width="40%" >
									<itemtemplate>
										<table align="center" style="width:100%; border-collapse: collapse;" cellpadding="0" cellspacing="0">
											<tr>
												<td align="center" style="width:90%;margin:2%; border-collapse: collapse;">                                                
													<asp:Label ID="lblRate" Text='<%# string.Format( "{0:0.########}", Eval("Tasa") ) %>' runat="server" ></asp:Label>
												</td>
											</tr>
										</table>						                							                   
									</itemtemplate>
									<EditItemTemplate>
										<table align="center" style="width:100%; border-collapse: collapse;" cellpadding="0" cellspacing="0">
											<tr>
												<td align="center" style="width:50%;margin:2%; border-collapse: collapse;">
													<asp:TextBox ID="txtRate"  Width="100%" placeholder="<%$ Resources:SupplyTrack, Rate %>" style="text-align: center" ValidationGroup="UpdateSurchargeDate" CausesValidation="false" cssClass="RequiredField" runat="server"></asp:TextBox>
												</td> 
												<td align="right" style="width:5%;margin:2%; border-collapse: collapse;">
													<asp:RequiredFieldValidator ID="rfvgrdRateValue" Width="100%"  ControlToValidate="txtRate"  Display="Dynamic"
														ErrorMessage="&lt;img src='../../Recursos/Imagenes/Campore2.gif' class='ImagenError'/&gt;" ValidationGroup="UpdateSurchargeDate" runat="server" />

													<asp:RegularExpressionValidator ID="revgrdRateValue"  ControlToValidate="txtRate" ValidationGroup="UpdateSurchargeDate"
														ValidationExpression="(\d{1,11})(\.\d{1,8})?" ToolTip="<%$Resources:SupplyTrack, MustBeNumeric %>" ErrorMessage="<button class='MensajeAdvertencia'/>"
														Display="Dynamic" Enabled ="true" runat="server"/>
												</td> 
											</tr>
										</table>
									</EditItemTemplate>
								</asp:TemplateField>
								<%--------------------------------------------------------------------------------------------------------------%>

								<%------------------------------------------ COLUMNA EDITAR ----------------------------------------------------%>
								<asp:TemplateField HeaderText="<%$ Resources:SupplyTrack, Action %>" ControlStyle-Width="10%" HeaderStyle-Width="10%" ItemStyle-Width="10%" >
									<itemtemplate>
										<table align="center" style="width:100%; border-collapse: collapse;"  cellpadding="0" cellspacing="0">
											<tr>
												<td align="center" style="width:90%;margin:2%; border-collapse: collapse;">                                                
													<asp:Button ID="btnEditSurchargeRate" CommandName="Edit"  ToolTip="<%$ Resources:SupplyTrack, Edit %>" CausesValidation="false" CssClass="LinkEditGrid" runat="server" />
												</td>
											</tr>
										</table>						                							                   
									</itemtemplate>
									<EditItemTemplate>
										<table align="center" style="width:100%; border-collapse: collapse;" cellpadding="0" cellspacing="0">
											<tr>
												<td align="right" style="width:50%;margin:2%; border-collapse: collapse;">                                   
													<asp:Button ID="btnUpdateSurchargeRate" CommandName="Update" ValidationGroup="UpdateSurchargeDate" CausesValidation="true" CssClass="LinkApplyAndCancelGrid" runat="server" />
												</td>
												<td align="left" style="width:50%;margin:2%; border-collapse: collapse;">
													<asp:Button ID="btnCancelSurchargeRate" CommandName="Cancel" CausesValidation="false" CssClass="LinkCancelAndApplyGrid" runat="server" />
												</td> 
											</tr>
										</table>
									</EditItemTemplate>
								</asp:TemplateField>
								<%----------------------------------------------------------------------------------------------------------------%>

								<%------------------------------------------ COLUMNA ELIMINAR ----------------------------------------------------%>
								<asp:TemplateField HeaderText="<%$ Resources:SupplyTrack, Delete %>" ControlStyle-Width="10%" HeaderStyle-Width="10%" ItemStyle-Width="10%" >
									<itemtemplate>
										<table align="center" style="width:100%; border-collapse: collapse;"  cellpadding="0" cellspacing="0">
											<tr>
												<td align="center" style="width:90%;margin:2%; border-collapse: collapse;">                                                
													<asp:Button ID="btnDeleteSurchargeRate" CommandName="Delete" ToolTip="<%$ Resources:SupplyTrack, Delete %>" CausesValidation="False" CssClass="LinkDeleteGrid" runat="server"/>
												</td>
											</tr>
										</table>						                							                   
									</itemtemplate>
									<EditItemTemplate>
										<table align="center" style="width:100%; border-collapse: collapse;" cellpadding="0" cellspacing="0">
											<tr>
												<td align="center" style="width:50%;margin:2%; border-collapse: collapse;"></td> 
											</tr>
										</table>
									</EditItemTemplate>
								</asp:TemplateField>
								<%----------------------------------------------------------------------------------------------------------------%>

								<%------------------------------------------ COLUMNA OCULTA NUMERO MES ----------------------------------------------------%>
								<asp:TemplateField  HeaderText="" Visible="false" >
									<itemtemplate>
										<asp:Label ID="lblMonthNumber" runat="server" Text = '<%# Eval("Month") %>'></asp:Label>
									</itemtemplate>
								</asp:TemplateField>
								<%----------------------------------------------------------------------------------------------------------------%>

							</Columns>
							<EmptyDataTemplate>
                                
							</EmptyDataTemplate>
						</asp:GridView>
					</td>
				</tr>
			</table>
			<asp:HiddenField ID="hdnIdSurchargeRateDelete" Value="-1" runat="server"/>
			<asp:HiddenField ID="hdnYear"				   Value="-1" runat="server"/>
			<asp:HiddenField ID="hdnMonth"				   Value="-1" runat="server"/>
		</ContentTemplate>
	</asp:UpdatePanel>