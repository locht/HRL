<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlUpload.ascx.vb"
    Inherits="Common.ctrlUpload" %>
<style type="text/css">
    .RadUpload input.ruFakeInput {
        width: 160px;
    }
</style>
<tlk:RadWindow runat="server" ID="rwMessage" Behaviors="Close" VisibleStatusbar="false"
    Height="300" Width="400" Modal="true" Title="<%$ Translate: Tải dữ liệu %>">
    <ContentTemplate>
        <tlk:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
            <fieldset style="height: 190px">
                <legend>
                    <%# Translate("Danh sách file")%></legend>
                <tlk:RadAsyncUpload runat="server" ID="RadAsyncUpload1" OnClientValidationFailed="validationFailed" 
                    AllowedFileExtensions="xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx" MaxFileInputsCount="1" MaxFileSize="4096000" UploadedFilesRendering="BelowFileInput" 
                    Skin="Metro" Width="250px" Style="padding: 20px 0px 0px 10px;">
                    <Localization Select="<%$ Translate: Chọn %>" />
                    <Localization Remove="<%$ Translate: Xóa %>" />
                    <Localization Cancel="<%$ Translate: Hủy %>" />
                </tlk:RadAsyncUpload>
            </fieldset>
            <div style="margin: 0px 10px 10px 10px; text-align: right;">
                <tlk:RadButton ID="btnYES" runat="server" Width="60px" Text="<%$ Translate: Hoàn tất %>"
                    Font-Bold="true" CausesValidation="false">
                </tlk:RadButton>
                <tlk:RadButton ID="btnNO" runat="server" Width="60px" Text="<%$ Translate: Hủy %>"
                    Font-Bold="true" CausesValidation="false">
                </tlk:RadButton>
            </div>
        </tlk:RadAjaxPanel>
    </ContentTemplate>
</tlk:RadWindow>
<tlk:RadScriptBlock ID="rscriptblock" runat="server">
    <%--<script type="text/javascript">
        function validationFailed(sender, args) {
            sender.deleteFileInputAt(0);
        }
    </script>--%>
    <script type="text/javascript">
        //        function validationFailed(sender, args) {
        //            sender.deleteFileInputAt(0);
        //        }
        (function () {
            var $;
            var demo = window.demo = window.demo || {};

            demo.initialize = function () {
                $ = $telerik.$;
            };

            window.validationFailed = function (radAsyncUpload, args) {
                var $row = $(args.get_row());
                var erorMessage = getErrorMessage(radAsyncUpload, args);
                var span = createError(erorMessage);
                $row.addClass("ruError");
                $row.append(span);
            }

            function getErrorMessage(sender, args) {
                var fileExtention = args.get_fileName().substring(args.get_fileName().lastIndexOf('.') + 1, args.get_fileName().length);
                if (args.get_fileName().lastIndexOf('.') != -1) {//this checks if the extension is correct
                    if (sender.get_allowedFileExtensions().indexOf(fileExtention) == -1) {
                        return ("<br/>Loại tệp này không được hỗ trợ.");
                    }
                    else {
                        return ("<br/>Tập tin này vượt quá giới hạn cho phép, kích thước tối đa là 40 MB.");
                    }
                }
                else {
                    return ("<br/>Loại tệp này không được hỗ trợ.");
                }
            }

            function createError(erorMessage) {
                var input = '<span class="ruErrorMessage">' + erorMessage + ' </span>';
                return input;
            }



        })();
        Sys.Application.add_load(function () {
            demo.initialize();
        });

    </script>
</tlk:RadScriptBlock>
