<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_Signer.ascx.vb"
    Inherits="Profile.ctrlHU_Signer" %>
    <asp:HiddenField ID="hidEmpID" runat="server" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="30px" Scrolling="None">
        <tlk:RadToolBar ID="rtbMain" runat="server" />
    </tlk:RadPane>
    <tlk:RadPane ID="MainPanel" runat="server"  Scrolling="None" Height="220px">
        <asp:Panel ID="pnlEdit" runat="server" Visible="true">
            <fieldset >
                <legend accesskey="I">
                    <asp:Label ID="lblViewTitle" runat="server"></asp:Label></legend>
                <table class="table-form">
                    <tr>
                      <td class="lb">
                            <%# Translate("Mã người ký")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtCode"  runat="server">
                            </tlk:RadTextBox>
                             <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindEmp" runat="server" SkinID="ButtonView"
                                CausesValidation="false">
                            </tlk:RadButton>
                            <asp:RequiredFieldValidator ID="reqNAME_EN" ControlToValidate="txtNAME_EN" runat="server"
                                ErrorMessage="<%$ Translate: Bạn phải nhập tên loại quyết định %>" ToolTip="<%$ Translate: Bạn phải nhập tên loại quyết định %>">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="lb">
                            <%# Translate("Tên người ký")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtTYPE_NAME" MaxLength="255" runat="server" Width="300px">
                            </tlk:RadTextBox>
                        </td>
                       
                    </tr>
                     <tr>
                      <td class="lb">
                            <%# Translate("Chức vị người ký")%><span class="lbReq">*</span>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtNAME_EN" MaxLength="255" runat="server" Width="300px">
                            </tlk:RadTextBox>
                            
                        </td>
                    </tr>
                   
                    <tr>
                        <td class="lb">
                            <%# Translate("Ghi chú")%>
                        </td>
                        <td>
                            <tlk:RadTextBox ID="txtRemark" MaxLength="255" runat="server" Width="100%">
                            </tlk:RadTextBox>
                        </td>
                       
                    </tr>
                </table>
               
            </fieldset>
        </asp:Panel>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid ID="rgData" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            Height="100%" AllowSorting="True" AllowMultiRowSelection="true">
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
            </ClientSettings>
            <MasterTableView CommandItemDisplay="None" DataKeyNames="ID" ClientDataKeyNames="ID,SIGNER_CODE,TITLE_NAME,REMARK,NAME">
                <Columns>
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<asp:PlaceHolder ID="phFindEmployee" runat="server"></asp:PlaceHolder>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        var oldSize = 0;

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "EXPORT") {
                enableAjax = false;
            } else if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                ResizeSplitter();
            } else {
                // Nếu nhấn các nút khác thì resize default
                ResizeSplitterDefault();
            }
        }

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }

        // Hàm Resize lại Splitter khi nhấn nút SAVE có validate
        function ResizeSplitter() {

        }

        // Hàm khôi phục lại Size ban đầu cho Splitter
        function ResizeSplitterDefault() {

        }
    </script>
</tlk:RadCodeBlock>
