<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlAT_RegistersMng.ascx.vb"
    Inherits="Attendance.ctrlAT_RegistersMng" %>
<tlk:RadSplitter ID="RadSplitter1" runat="server" Width="100%" Height="100%">
    <tlk:RadPane ID="MainPane" runat="server" Scrolling="None">
        <tlk:RadSplitter ID="RadSplitter2" runat="server" Width="100%" Height="100%" Orientation="Horizontal">
            <tlk:RadPane ID="RadPane1" runat="server" MinHeight="26" Height="26px">
                <tlk:RadTabStrip runat="server" ID="rtabTab" Orientation="HorizontalTop" SelectedIndex="0">
                    <Tabs>
                        <tlk:RadTab Text="<%$ Translate: Đăng ký ca làm việc %>">
                        </tlk:RadTab>
                        <tlk:RadTab Text="<%$ Translate: Đăng ký nghỉ %>">
                        </tlk:RadTab>
                        <tlk:RadTab Text="<%$ Translate: Đăng ký làm thêm %>">
                        </tlk:RadTab>
                        <tlk:RadTab Text="<%$ Translate: Đăng ký đi trễ về sớm %>">
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