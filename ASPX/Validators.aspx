<asp:RegularExpressionValidator ID="RegexVal" 
    ErrorMessage="<div class='MensajeAdvertencia'></div>" 
    ControlToValidate="ControlAValidar"
    ValidationExpression="^[a-zA-Z0-9/-]*$" 
    Enabled="true" Display="Dynamic" runat="server"/>

<asp:RequiredFieldValidator ID="ReqFieldVal"  
    ControlToValidate="ControlAValidar"
    ErrorMessage="&lt;img src='../../Recursos/Imagenes/Campore2.gif' class='ImagenError'/&gt;"
    SetFocusOnError="True" runat="server"/>

<asp:CompareValidator ID="CompVal" runat="server" 
    ControlToValidate="ControlAValidar"
    Operator="NotEqual" ValueToCompare="-" 
    Text="<img src='../../Recursos/Imagenes/Campore2.gif' class='ImagenError'/>"
    Type="String" Display="Dynamic"
    ToolTip="<%$ Resources:SupplyTrack, RequiredField %>" />

<asp:CustomValidator ID="CustVal" runat="server" 
    ControlToValidate="ControlAValidar"
    ValidationGroup="RetentionsGrid" 
    Text="<img src='../../Recursos/Imagenes/Campore2.gif' class='ImagenError'/>"
    Display="Dynamic" 
    ToolTip="<%$ Resources:Errors,RetentionAlreadyRelated %>" 
    OnServerValidate="cuvRepeatedRetention_ServerValidate" />
                                                        