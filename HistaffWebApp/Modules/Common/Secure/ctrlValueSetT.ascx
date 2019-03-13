﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ctrlValueSetT.ascx.vb"
    Inherits="Common.ctrlValueSetT" %>
<%@ Register Src="../ctrlMessageBox.ascx" TagName="ctrlMessageBox" TagPrefix="Common" %>
<tlk:RadSplitter ID="RadSplitter3" runat="server" Height="100%" Width="100%" Orientation="Horizontal">
    <tlk:RadPane ID="RadPane1" runat="server" Scrolling="None" Height="230px">
        <tlk:RadToolBar ID="rtbMain" runat="server" OnClientButtonClicking="OnClientButtonClicking" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="BulletList"
            CssClass="validationsummary" />
        <div>
            <fieldset>
                <legend>Thông tin chi tiết</legend>
                <table class="table-form">
                    <tr>
                        <td class="lb" style="padding-right: 10px; width:100px">
                            <%# Translate("CM_CTRLVALUESETT_VALUESET_NAME")%>&nbsp;
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="rtbValueSetName" runat="server" Height="22" ToolTip="<%$ Translate: CM_CTRLVALUESETTVALUESET_NAME %>" 
                                Width="100%" ReadOnly="true"/>                          
                        </td>
                        <td>
                            <tlk:RadButton ID="cbbCheck" ToggleType="CheckBox" ButtonType="ToggleButton" runat="server"  CausesValidation="false" Width="40px"
                                Text="<%$ Translate: CM_CTRLVALUESETT_ONLY_TABLE %>">
                            </tlk:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="lb" style="padding-right: 10px;width:100px" >
                            <%# Translate("CM_CTRLVALUESETT_TABLE_VIEW_SELECT")%>&nbsp;
                        </td>
                        <td>
                            <tlk:RadComboBox ID="rcbbTableView" DataValueField="ID" DataTextField="NAME" runat="server" SkinID="Number" Width="100%" AutoPostBack="true"
                                CausesValidation="false"  ToolTip="<%$ Translate: CM_CTRLVALUESETT_TABLE_VIEW_SELECT %>">
                            </tlk:RadComboBox>                                             
                        </td>
                        <td class="lb" style="padding-right: 10px;width:150px">
                            <%# Translate("CM_CTRLVALUESETT_ID_COLUMN")%>&nbsp;<span class="lbReq">*</span>&nbsp;
                        </td>
                        <td>
                            <tlk:RadComboBox ID="rcbbIDColumn" DataValueField="ID" DataTextField="NAME" runat="server" SkinID="Number" Width="100%"
                                ToolTip="<%$ Translate: CM_CTRLVALUESETT_ID_COLUMN %>">
                            </tlk:RadComboBox>    
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rcbbIDColumn"
                                ErrorMessage="<%$ Translate: CM_CTRLVALUESETT_ID_COLUMN_ERROR %>" ToolTip="<%$ Translate: CM_CTRLVALUESETT_ID_COLUMN_ERROR %>"></asp:RequiredFieldValidator>                                                                     
                        </td> 
                        <td class="lb" style="padding-right: 10px;width:150px">
                            <%# Translate("CM_CTRLVALUESETT_VALUE_COLUMN_NAME")%>&nbsp;<span class="lbReq">*</span>&nbsp;
                        </td>
                        <td>
                            <tlk:RadComboBox ID="rcbbColumnName" DataValueField="ID" DataTextField="NAME" runat="server" SkinID="Number" Width="100%"
                                ToolTip="<%$ Translate: CM_CTRLVALUESETT_VALUE_COLUMN_NAME %>">
                            </tlk:RadComboBox>    
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rtbColumnName"
                                ErrorMessage="<%$ Translate: CM_CTRLVALUESETT_VALUE_COLUMN_NAME_ERROR %>" ToolTip="<%$ Translate: CM_CTRLVALUESETT_VALUE_COLUMN_NAME_ERROR %>"></asp:RequiredFieldValidator>                                                                     
                        </td>                                                                                                           
                    </tr>     
                    <tr>
                        <td class="lb" style="padding-right: 10px;width:100px">
                            <%# Translate("CM_CTRLVALUESETT_TABLE_VIEW_SELECT")%>&nbsp;
                        </td>
                        <td colspan="2">
                            <tlk:RadTextBox ID="rtbTableView" runat="server" Height="22" ToolTip="<%$ Translate: CM_CTRLVALUESETT_TABLE_VIEW_SELECT %>"
                                Width="100%" ReadOnly="true"/>                                    
                        </td>                           
                        <td class="lb" style="padding-right: 10px;width:150px">
                            <%# Translate("CM_CTRLVALUESETT_VALUE_COLUMN_NAME")%>&nbsp;<span class="lbReq">*</span>&nbsp;
                        </td>
                        <td colspan="2">
                            <tlk:RadTextBox ID="rtbColumnName" runat="server" Height="22" ToolTip="<%$ Translate: CM_CTRLVALUESETT_VALUE_COLUMN_NAME %>"
                                Width="100%" ReadOnly="true"/>                                    
                        </td> 
                   </tr>
                   <tr>     
                        <td class="lb" style="padding-right: 10px;width:150px">
                            <%# Translate("CM_CTRLVALUESETT_CONDITION_WHERE_CLAUSE")%>&nbsp;<span class="lbReq">*</span>&nbsp;
                        </td>
                        <td colspan="3">
                            <tlk:RadTextBox ID="rtbCondition" runat="server" Height="22" ToolTip="<%$ Translate: CM_CTRLVALUESETT_CONDITION_WHERE_CLAUSE %>"
                                Width="100%" ReadOnly="true"/>         
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rtbCondition"
                                ErrorMessage="<%$ Translate: CM_CTRLVALUESETT_CONDITION_WHERE_CLAUSE_ERROR %>" ToolTip="<%$ Translate: CM_CTRLVALUESETT_CONDITION_WHERE_CLAUSE_ERROR %>"></asp:RequiredFieldValidator>                            
                        </td>  
                        <td colspan="2">
                            <tlk:RadButton ID="rtbCheckSQL" runat="server" CausesValidation="false" Text="Check Sql">
                            </tlk:RadButton>
                        </td>                      
                    </tr>                    
                </table>
            </fieldset>
        </div>
    </tlk:RadPane>
    <tlk:RadPane ID="RadPane2" runat="server" Scrolling="Y">
        <tlk:RadGrid ID="rgValueSets" runat="server" AllowFilteringByColumn="True"
            AllowMultiRowSelection="True" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CellSpacing="0" GridLines="None" Height="100%" PageSize="50" 
            ShowStatusBar="True">
            <ClientSettings AllowColumnsReorder="True" EnablePostBackOnRowClick="true" EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="true" />
                <Scrolling UseStaticHeaders="true" AllowScroll="True" />
                <Resizing AllowColumnResize="true" />
            </ClientSettings>
            <PagerStyle AlwaysVisible="true" Mode="NextPrevAndNumeric" />
            <MasterTableView ClientDataKeyNames="ID, FLEX_VALUE_SET_NAME,TABLE_VIEW_SELECT,ID_COLUMN_NAME,VALUE_COLUMN_NAME,CONDITION_WHERE_CLAUSE,TABLE_COUNT"
                CommandItemDisplay="None" DataKeyNames="ID">
                <CommandItemSettings ExportToPdfText="Export to PDF" />
                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                    Visible="True">
                </RowIndicatorColumn>
                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                    Visible="True">
                </ExpandCollapseColumn>
                <Columns>
                    <tlk:GridBoundColumn DataField="ID" HeaderText="<%$ Translate: ID %>"
                        UniqueName="ID" Visible="false">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="FLEX_VALUE_SET_NAME" HeaderText="<%$ Translate: CM_CTRLVALUESETT_FLEX_VALUE_SET_NAME %>"
                        SortExpression="FLEX_VALUE_SET_NAME" UniqueName="FLEX_VALUE_SET_NAME"  FilterControlWidth="90%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="TABLE_VIEW_SELECT" HeaderText="<%$ Translate: CM_CTRLVALUESETT_TABLE_VIEW_SELECT %>"
                        SortExpression="TABLE_VIEW_SELECT" UniqueName="TABLE_VIEW_SELECT"  FilterControlWidth="90%">
                        <HeaderStyle HorizontalAlign="Center" />
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="ID_COLUMN_NAME" HeaderText="<%$ Translate: CM_CTRLVALUESETT_ID_COLUMN_NAME %>"
                        SortExpression="ID_COLUMN_NAME" UniqueName="ID_COLUMN_NAME"  FilterControlWidth="90%">
                        <HeaderStyle HorizontalAlign="Center" />   
                    </tlk:GridBoundColumn>
                    <tlk:GridBoundColumn DataField="VALUE_COLUMN_NAME" HeaderText="<%$ Translate: CM_CTRLVALUESETT_VALUE_COLUMN_NAME %>"
                        SortExpression="VALUE_COLUMN_NAME" UniqueName="VALUE_COLUMN_NAME"  FilterControlWidth="90%">
                        <HeaderStyle HorizontalAlign="Center" />   
                    </tlk:GridBoundColumn>    
                    <tlk:GridBoundColumn DataField="CONDITION_WHERE_CLAUSE" HeaderText="<%$ Translate: CM_CTRLVALUESETT_CONDITION_WHERE_CLAUSE %>" EmptyDataText=""
                        SortExpression="CONDITION_WHERE_CLAUSE" UniqueName="CONDITION_WHERE_CLAUSE"  FilterControlWidth="90%">
                        <HeaderStyle HorizontalAlign="Center" />   
                    </tlk:GridBoundColumn>  
                    <tlk:GridCheckBoxColumn HeaderText="<%$ Translate: CM_CTRLVALUESETT_TABLE_COUNT %>" DataField="TABLE_COUNT"
                        SortExpression="TABLE_COUNT" UniqueName="TABLE_COUNT"   FilterControlWidth="90%">
                        <HeaderStyle HorizontalAlign="Center" Width="50px"/>
                    </tlk:GridCheckBoxColumn>                                                 
                </Columns>
                <EditFormSettings>
                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                    </EditColumn>
                </EditFormSettings>
                <PagerStyle AlwaysVisible="True" />
            </MasterTableView>
            <FilterMenu EnableImageSprites="False">
            </FilterMenu>
        </tlk:RadGrid>
        <Common:ctrlMessageBox ID="ctrlMessageBox" runat="server" />
    </tlk:RadPane>
</tlk:RadSplitter>
<tlk:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        var enableAjax = true;
        function OnClientButtonClicking(sender, args) {
            var item = args.get_item();
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

        function onRequestStart(sender, eventArgs) {
            eventArgs.set_enableAjax(enableAjax);
            enableAjax = true;
        }        

    </script>
</tlk:RadCodeBlock>
