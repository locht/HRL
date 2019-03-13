﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlDBEmployeeStatistic.ascx.vb"
    Inherits="Profile.ctrlDBEmployeeStatistic" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Charting" TagPrefix="telerik" %>
<script src="../../../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
<script src="../../../Scripts/highcharts/highcharts.js" type="text/javascript"></script>
<script src="../../../Scripts/highcharts/highcharts-more.js" type="text/javascript"></script>
<script src="../../../Scripts/highcharts/modules/exporting.js" type="text/javascript"></script>
<div style="margin-top: 5px; text-align: center">
    <%# Translate("Thống kê theo: ")%>
    <tlk:RadComboBox ID="cboType" runat="server" AutoPostBack="true" OnClientSelectedIndexChanged="OnClientClicked">
    </tlk:RadComboBox>
    <tlk:RadButton ID="btnMax" Style="margin-top: -2px" runat="server" OnClientClicked="MaxClick"
        Text="Maximize" AutoPostBack="false" SkinID="Demo" Visible="false">
    </tlk:RadButton>
</div>
<div id="container" style="min-width: 250px; height: 250px; margin: 0 auto">
</div>
<%--<tlk:RadChart ID="charData1" runat="server" DefaultType="Pie" AutoLayout="True">
    <Series>
        <telerik:ChartSeries Name="Series 1" Type="Pie" DataYColumn="VALUE">
            <Appearance>
               <%-- <FillStyle MainColor="150, 150, 150" SecondColor="194, 194, 194" FillType="ComplexGradient">
                </FillStyle>--%>
<%--<Border Color="Transparent" />
                <TextAppearance TextProperties-Font="Tahoma, 12pt"></TextAppearance>
            </Appearance>
        </telerik:ChartSeries>
    </Series>
    <Appearance>
        <Border Visible="false" />
    </Appearance>
    <PlotArea>
        <XAxis>
            <Appearance Color="Silver" MajorTick-Color="Silver">
                <MajorGridLines Color="Silver" />
            </Appearance>
        </XAxis>
        <YAxis>
            <Appearance Color="Black" MajorTick-Color="Black" MinorTick-Color="Black">
                <MajorGridLines Color="Black" />
                <MinorGridLines Color="224, 224, 224" />
            </Appearance>
        </YAxis>
        <Appearance>
            <FillStyle MainColor="White" SecondColor="Black" FillType="Solid">
            </FillStyle>
            <Border Visible="false" />
        </Appearance>
        <EmptySeriesMessage>
            <TextBlock Text="<%$ Translate: Không tồn tại dữ liệu %>">
            </TextBlock>
        </EmptySeriesMessage>
    </PlotArea>
    <ChartTitle>
        <Appearance Position-AlignedPosition="Top">
            <Border Visible="false" />
        </Appearance>
        <TextBlock>
            <Appearance TextProperties-Color="Gray">
            </Appearance>
        </TextBlock>
    </ChartTitle>
</tlk:RadChart>--%>
<script type="text/javascript">
    var enableAjax = true;
    function MaxClick(sender, e) {
        var btn = $find('<%= btnMax.ClientId %>');
        var btnText = btn.get_text();
        oWnd = getRadWindow();
        switch (btnText) {
            case "Maximize":
                oWnd.maximize();
                var winW = $("#WindowMainRegion", parent.document).width() - 16;
                var winH = $("#WindowMainRegion", parent.document).height() + 91 - 10;
                oWnd.setUrl('Dialog.aspx?mid=Profile&fid=ctrlDBEmployeeStatistic&group=Dashboard&width=' + (winW) + '&height=' + (winH - 80) + "&noscroll=1&reload=1&resize=" + 1);
                break;
            case "Restore":
                oWnd.restore();
                var winW = ($("#WindowMainRegion", parent.document).width() - 16) / 3 * 2 - 100;
                var winH = ($("#WindowMainRegion", parent.document).height() + 91 - 10) / 2;
                oWnd.setUrl('Dialog.aspx?mid=Profile&fid=ctrlDBEmployeeStatistic&group=Dashboard&width=' + (winW) + '&height=' + (winH - 80) + "&noscroll=1&reload=1&resize=" + 1);
                break;
            default:
                break;

        }
    }
    function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }
    function OnClientAutoSizeEnd() {


    }

     function OnClientClicked(sender, args) {
            enableAjax = false;
     }
    //mandatory for the RadWindow dialogs functionality
    function getRadWindow() {
        if (window.radWindow) {
            return window.radWindow;
        }
        if (window.frameElement && window.frameElement.radWindow) {
            return window.frameElement.radWindow;
        }
        return null;
    }
    function InIEvent() {
        $(function () {
            // Create the chart
            $('#container').highcharts({
                credits: {
                    enabled: false               
                },
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'                    
                },
                title: {
                    text: ''
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.0f}%</b>'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: false
                        },
                        showInLegend: true
                    }
                },
                legend: {
                    enabled: true,
                    align: 'right',
                    verticalAlign: 'right',
                    y: 25,
                    x: -10,
                    padding: 0,
                    itemMarginTop: 0,
                    itemMarginBottom: 0,
                    itemStyle: {
                        fontSize: '12px',
                        fontWeight: 'normal',
                        color: '#767676'
                        },
                    layout: 'vertical'
                },
                exporting: {
                    sourceWidth: 900,
                    sourceHeight: 300,
                    // scale: 2 (default)
                    chartOptions: {
                        subtitle: null
                    }
                },
                series: [{
                    name: '<%=name %>',
                    colorByPoint: true,
                    data: [<%=data %>]
                }],
                title: {
                            text:'<span style="font-weight: bold;color: #767676;font-size:12px;"><%# Translate("CTRLDBEMPLOYEESTATISTIC")%></span>'
                        }
            });
        });
     }
        $(document).ready(InIEvent);
</script>
<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
</script>
