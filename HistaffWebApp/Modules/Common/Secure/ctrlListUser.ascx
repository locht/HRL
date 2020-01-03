<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlListUser.ascx.vb"
    Inherits="Common.ctrlListUser" %>
<%@ Import Namespace="Common" %>
<%@ Import Namespace="Framework.UI.Utilities" %><%--
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>--%>
<link href = "/Styles/StyleCustom.css" rel = "Stylesheet" type = "text/css" />
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="182px" Scrolling="None">
        <tlk:RadToolBar ID="rtbMain" runat="server" OnClientButtonClicking="OnClientButtonClicking"/>
        <asp:PlaceHolder ID="ViewPlaceHolder" runat="server"></asp:PlaceHolder>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
        <tlk:RadGrid PageSize=50 ID="rgGrid" runat="server" Height="100%" OnPreRender="rgGrid_PreRender" >
            <ClientSettings EnableRowHoverStyle="true" >
                        <Selecting AllowRowSelect="true" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <ClientEvents OnCommand="ValidateFilter" />
            </ClientSettings>
            <MasterTableView DataKeyNames="ID">
                <Columns>
                    <tlk:GridClientSelectColumn UniqueName="cbStatus" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="30px" />
                    <tlk:GridBoundColumn DataField="ID" Visible="false" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên tài khoản %>" DataField="USERNAME"
                        UniqueName="USERNAME" SortExpression="USERNAME" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Tên nhân viên %>" DataField="FULLNAME"
                        UniqueName="FULLNAME" SortExpression="FULLNAME" HeaderStyle-Width="100px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Email %>" DataField="EMAIL" UniqueName="EMAIL"
                        SortExpression="EMAIL" HeaderStyle-Width="150px" />
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Số điện thoại %>" DataField="TELEPHONE"
                        UniqueName="TELEPHONE" SortExpression="TELEPHONE" HeaderStyle-Width="80px" />
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: App User %>" AllowFiltering ="False" DataField="IS_APP"
                        UniqueName="IS_APP" SortExpression="IS_APP">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridCheckBoxColumn>
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: Portal User %>" AllowFiltering ="False" DataField="IS_PORTAL"
                        UniqueName="IS_PORTAL" SortExpression="IS_PORTAL">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridCheckBoxColumn>
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: AD User %>" AllowFiltering ="False" DataField="IS_AD"
                        UniqueName="IS_AD" SortExpression="IS_AD">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridCheckBoxColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Mã nhân viên %>" DataField="EMPLOYEE_CODE" 
                        UniqueName="EMPLOYEE_CODE" SortExpression="EMPLOYEE_CODE" HeaderStyle-Width="100px" />
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hiệu lực %>" DataField="EFFECT_DATE" AllowFiltering ="False"
                        UniqueName="EFFECT_DATE" SortExpression="EFFECT_DATE" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridDateTimeColumn HeaderText="<%$ Translate: Ngày hết hiệu lực %>" DataField="EXPIRE_DATE" AllowFiltering ="False"
                        UniqueName="EXPIRE_DATE" SortExpression="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </tlk:GridDateTimeColumn>
                    <tlk:GridBoundColumn HeaderText="<%$ Translate: Trạng thái %>" DataField="ACTFLG"
                        UniqueName="ACTFLG" SortExpression="ACTFLG" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                </Columns>
            </MasterTableView>
        </tlk:RadGrid>
    </tlk:RadPane>
</tlk:RadSplitter>
<Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
<tlk:RadWindow runat="server" ID="rwMessage" AutoSize="true" Behaviors="Close" VisibleStatusbar="false"
    Modal="true" EnableViewState="false" Title="<%$ Translate: Kết quả đồng bộ hóa %>">
    <ContentTemplate>
        <table class="table-form">
            <tr>
                <td>
                    <tlk:RadTextBox ID="txtResult" runat="server" TextMode="MultiLine" Width="400px"
                        Height="300px">
                    </tlk:RadTextBox></p>
                </td>
            </tr>
            <tr>
                <td>
                    <tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
                        <div style="margin: 0px 10px 10px 10px; text-align: center;">
                            <tlk:RadButton ID="btnClose" runat="server" Width="60px" Text="<%$ Translate: Đóng %>"
                                Font-Bold="true" CausesValidation="false" OnClientClicked="OnCloseClicked">
                            </tlk:RadButton>
                        </div>
                    </tlk:RadAjaxPanel>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</tlk:RadWindow>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">

        var splitterID = 'ctl00_MainContent_RadSplitter1';
        var pane1ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlListUser_RadPane1';
        var pane2ID = 'RAD_SPLITTER_PANE_CONTENT_ctl00_MainContent_ctrlListUser_RadPane2';
        var validateID = 'MainContent_ctrlListUser_ctrlListUserNewEdit_valSum';
        var oldSize = $('#' + pane1ID).height();
        var enableAjax = true;
        
        function ValidateFilter(sender, eventArgs) {
            var params = eventArgs.get_commandArgument() + '';
            if (params.indexOf("|") > 0) {
                var s = eventArgs.get_commandArgument().split("|");
                if (s.length > 1) {
                    var val = s[1];
                    if (validateHTMLText(val) || validateSQLText(val)) {
                        eventArgs.set_cancel(true);
                    }
                }
            }
        }

        function GridCreated(sender, eventArgs) {
            registerOnfocusOut(splitterID);
        }

        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
            if (item.get_commandName() == "SAVE") {
                // Nếu nhấn nút SAVE thì resize
                if (!Page_ClientValidate("")) {
                    ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgGrid');
                } else {
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                }
            }
            if (args.get_item().get_commandName() == 'EXPORT') {
                enableAjax = false;
            } else {
                if (item.get_commandName() == "DELETE") {
                    $('#ctl00_MainContent_ctrlListUser_ctrlListUserNewEdit_txtUSERNAME').val('')
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                } else {
                    // Nếu nhấn các nút khác thì resize default
                    ResizeSplitterDefault(splitterID, pane1ID, pane2ID, oldSize);
                }
            }
        }
        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
        function OnCloseClicked() {
            $find("<%=rwMessage.ClientID %>").close();
        }
    </script>
</tlk:RadCodeBlock>
<Common:ctrlMessageBox ID="ctrlMessageBox1" runat="server" />