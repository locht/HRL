<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlHU_JobPositionNewEdit.ascx.vb"
    Inherits="Profile.ctrlHU_JobPositionNewEdit" %>
<%@ Import Namespace="Common" %>
<asp:HiddenField ID="hidID" runat="server" />
<asp:HiddenField ID="hidOrgID" runat="server" />
<asp:HiddenField ID="hidTitleID" runat="server" />
<asp:HiddenField ID="hidCode" runat="server" />
 <asp:ValidationSummary ID="valSum" runat="server" DisplayMode="BulletList" CssClass="validationsummary" />
<tlk:RadSplitter ID="RadSplitter3" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Height="20px" Scrolling="None">
        <tlk:RadToolBar ID="tbarOrg" runat="server"  />
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Height="360px">
       <table class="table-form">
            <tr>
                <td colspan="8">
                    <b>
                        <%# Translate("Thông tin vị trí công việc")%>
                    </b>
                    <hr />
                </td>
            </tr>
           <tr>
               <td class="lb">
                   <%# Translate("Mã vị trí công việc")%>
               </td>
               <td>
                   <tlk:RadTextBox ID="txtCode" runat="server" ReadOnly="true">
                    </tlk:RadTextBox>
               </td>
               <td class="lb">
                   <%# Translate("Tên vị trí công việc")%><span class="lbReq">*</span>
               </td>
               <td>
                   <tlk:RadTextBox ID="txtJobName" runat="server">
                    </tlk:RadTextBox>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtJobName"
                        runat="server" ErrorMessage="<%$ Translate: Tên vị trí công việc không được để trống %>" ToolTip="<%$ Translate: Tên vị trí công việc không được để trống %>"> 
                    </asp:RequiredFieldValidator>
               </td>
               <td class="lb">
                    <%# Translate("Phòng ban")%><span class="lbReq">*</span>
                </td>
                <td>
                    <tlk:RadTextBox ID="txtOrgName" runat="server" ReadOnly="True">
                    </tlk:RadTextBox>
                    <tlk:RadButton EnableEmbeddedSkins="false" ID="btnFindOrg" runat="server" SkinID="ButtonView"
                        CausesValidation="false">
                    </tlk:RadButton>
                    <asp:RequiredFieldValidator ID="reqOrgCode" ControlToValidate="txtOrgName"
                        runat="server" ErrorMessage="<%$ Translate: Phòng ban không được để trống %>" ToolTip="<%$ Translate: Phòng ban không được để trống %>"> 
                    </asp:RequiredFieldValidator>
                </td>
                <td class="lb" style="width: 130px">
                    <%# Translate("Chức danh")%><span class="lbReq">*</span>
                </td>
                 <td>
                    <tlk:RadComboBox runat="server" ID="cboTitle" SkinID="LoadDemand" OnClientItemsRequesting="OnClientItemsRequesting" AutoPostBack ="true">
                    </tlk:RadComboBox>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="cboTitle"
                        runat="server" ErrorMessage="<%$ Translate: Chức danh không được để trống %>" ToolTip="<%$ Translate: Chức danh không được để trống %>"> 
                    </asp:RequiredFieldValidator>
                </td>
           </tr>
            <tr>
                 <td class="lb">
                   <%# Translate("Mã chi phí")%>
               </td>
               <td>
                   <tlk:RadTextBox ID="txtCostCode" runat="server">
                    </tlk:RadTextBox>
               </td>
                <td class="lb">
                        <%# Translate("Trưởng đơn vị")%>
                </td>
                <td id="tdSGcbIs_Incentive" runat="server" >
                    <div style="margin: -4px; margin-top: 1px">
                        <tlk:RadButton ID="cbIS_LEADER" ToggleType="CheckBox" ButtonType="ToggleButton"
                            Text="" runat="server" AutoPostBack="false" CausesValidation="false">
                        </tlk:RadButton>
                    </div>
                </td>
                <td class="lb" style="text-align: left;">
                    <%# Translate("Quản lý trực tiếp")%>
                </td>
                <td>
                    <tlk:RadComboBox ID="cboDM" runat="server"
                        EmptyMessage="<%$ Translate: Chọn quản lý trực tiếp %>" CheckBoxes="true"
                    EnableCheckAllItemsCheckBox="true" DropDownAutoWidth="Enabled">
                    </tlk:RadComboBox>
                </td>
                <td class="lb">
                    <%# Translate("Ngày hiệu lực")%><span class="lbReq">*</span>
                </td>
               <td>
                   <tlk:RadDatePicker runat="server" ID="rdEffectDate"></tlk:RadDatePicker>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdEffectDate"
                        runat="server" ErrorMessage="<%$ Translate: Ngày hiệu lực không được để trống %>" ToolTip="<%$ Translate:Ngày hiệu lực không được để trống %>"> 
                    </asp:RequiredFieldValidator>
               </td>
            </tr>
           <tr>
               <td class="lb">
                   <%# Translate("Chức năng/ yêu cầu công việc")%>
               </td>
               <td colspan="7">
                   <tlk:RadTextBox ID="txtJobNote" runat="server" Width="100%" SkinID="TextBox1023">
                    </tlk:RadTextBox>
               </td>
           </tr>
        </table>
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function OnClientItemsRequesting(sender, eventArgs) {
            var id = sender.get_id();
            var cbo;
            var value;

            if (!value) {
                value = null;
            }
            var context = eventArgs.get_context();
            context["valueCustom"] = value;
        }

    </script>
</tlk:RadCodeBlock>
