<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlRC_ProgramDeclare.ascx.vb"
    Inherits="Recruitment.ctrlRC_ProgramDeclare" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward">
    </tlk:RadSplitBar>
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" MinHeight="26" Height="26px">
                <tlk:RadTabStrip runat="server" ID="rtabTab" Orientation="HorizontalTop" SelectedIndex="0">
                    <Tabs>
                        <tlk:RadTab Text="<%$ Translate: Thiết lập các môn thi %>">
                        </tlk:RadTab>
                        <tlk:RadTab Text="<%$ Translate: Lên lịch thi %>">
                        </tlk:RadTab>
                        <tlk:RadTab Text="<%$ Translate: Cập nhật kết quả %>">
                        </tlk:RadTab>
                    </Tabs>
                </tlk:RadTabStrip>
            </tlk:RadPane>
            <tlk:RadPane ID="RadPane2" runat="server" Scrolling="None">
                <asp:PlaceHolder ID="TabView" runat="server"></asp:PlaceHolder>
            </tlk:RadPane>
        </tlk:RadSplitter>
    </tlk:RadPane>
</tlk:RadSplitter>