<asp:GridView style="width:90%;margin:2%; border-collapse: collapse;"
        ID="GridGenerico"
        SkinID="gridViewSkin" PageSize="10"  
        AutoGenerateColumns="false" AutoPostBack="true" AllowPaging="true" ShowFooter="false" runat="server"
        DataKeyNames="IdKey" OnRowDeleting="GridGenerico_RowDeleting" OnPageIndexChanging="GridGenerico_PageIndexChanging"
        OnRowEditing="GridGenerico_RowEditing" OnRowCancelingEdit="GridGenerico_RowCancelingEditing" OnRowUpdating="GridGenerico_RowUpdating">
    <Columns>
        <%------------------------------------------ COLUMNA INFO ------------------------------------------------------%>
        <asp:TemplateField HeaderText="<%$ Resources:ClaseRecurso, Rate %>" ControlStyle-Width="40%" HeaderStyle-Width="40%" ItemStyle-Width="40%" >
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
                            <asp:TextBox ID="txtRate"  Width="100%" placeholder="<%$ Resources:ClaseRecurso, Rate %>" style="text-align: center" ValidationGroup="UpdateSurchargeDate" CausesValidation="false" cssClass="RequiredField" runat="server"></asp:TextBox>
                        </td> 
                    </tr>
                </table>
            </EditItemTemplate>
        </asp:TemplateField>
        <%--------------------------------------------------------------------------------------------------------------%>

        <%------------------------------------------ COLUMNA EDITAR ----------------------------------------------------%>
        <asp:TemplateField HeaderText="<%$ Resources:ClaseRecurso, Action %>" ControlStyle-Width="10%" HeaderStyle-Width="10%" ItemStyle-Width="10%" >
            <itemtemplate>
                <table align="center" style="width:100%; border-collapse: collapse;"  cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center" style="width:90%;margin:2%; border-collapse: collapse;">                                                
                            <asp:Button ID="btnEditSurchargeRate" CommandName="Edit"  ToolTip="<%$ Resources:ClaseRecurso, Edit %>" CausesValidation="false" CssClass="LinkEditGrid" runat="server" />
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
        <asp:TemplateField HeaderText="<%$ Resources:ClaseRecurso, Delete %>" ControlStyle-Width="10%" HeaderStyle-Width="10%" ItemStyle-Width="10%" >
            <itemtemplate>
                <table align="center" style="width:100%; border-collapse: collapse;"  cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center" style="width:90%;margin:2%; border-collapse: collapse;">                                                
                            <asp:Button ID="btnDeleteSurchargeRate" CommandName="Delete" ToolTip="<%$ Resources:ClaseRecurso, Delete %>" CausesValidation="False" CssClass="LinkDeleteGrid" runat="server"/>
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

        <%------------------------------------------ COLUMNA OCULTA ------------------------------------------------------%>
        <asp:TemplateField  HeaderText="" Visible="false" >
            <itemtemplate>
                <asp:Label ID="lblHidden" runat="server" Text = '<%# Eval("Month") %>'></asp:Label>
            </itemtemplate>
        </asp:TemplateField>
        <%----------------------------------------------------------------------------------------------------------------%>

    </Columns>

    <EmptyDataTemplate>
    </EmptyDataTemplate>
</asp:GridView>