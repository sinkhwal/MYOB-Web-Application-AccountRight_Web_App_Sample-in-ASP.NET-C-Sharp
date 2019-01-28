<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchPage.aspx.cs" Inherits="Web.SearchPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/js/bootstrap-datepicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('[id*=txtFromDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "yyyy-mm-dd",
                //language: "tr"
            });

            $('[id*=txtToDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "yyyy-mm-dd",
                //  language: "tr"
            });
        });
        window.onload = function HideSplash() {
            document.getElementById("slowScreenSplash").style.display = 'none';
        };

        function showSplash() {
            document.getElementById("slowScreenSplash").style.display = 'block';
        }

        function closeMyWindow() {
            showSplash();
        }

        window.onunload = closeMyWindow;

    </script>
    <style type="text/css">
        #slowScreenSplash {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 1000;
            /*background: url('Images/pageloader.gif') no-repeat center center;*/
        }
    </style>

    <br />
    <div id="slowScreenSplash" class="row" style="position: fixed; background-color: Transparent; top: 100px; left: 100px; width: 70%"
        align="center">
        <img src="Images/pageloader.gif" />
     
    </div>
    <div class="form-inline">
        <div class="form-group row">
            <div class="form-group mb-2">
                <label for="static" class="col-form-label">From Date</label>
                <asp:TextBox ID="txtFromDate" runat="server" class="form-control mr-2" autocomplete="off"></asp:TextBox>
            </div>
            <div class="form-group mb-2 mr-2"></div>
            <div class="form-group mb-2">
                <label for="static" class="col-form-label">To  Date</label>
                <asp:TextBox ID="txtToDate" runat="server" class="form-control" autocomplete="off"></asp:TextBox>
            </div>
        </div>
        <br /> <br />
   <div class="form-group row">
            <asp:Button ID="btnSalesSearch" runat="server" class="btn btn btn-info mb-2" Text="Sales Search" OnClientClick="showSplash();" OnClick="btnSearch_Click" />
            <asp:Button ID="btnPurchaseSearch" runat="server" class="btn btn btn-info  mb-2" Text="Purchases Search" OnClientClick="showSplash();" OnClick="btnPurchaseSearch_Click" />
            <asp:Button ID="btnProfitLossSearch" runat="server" class="btn btn btn-info mb-2" Text="Profit and Loss Search" OnClientClick="showSplash();" OnClick="btnProfitLossSearch_Click" />
            <asp:Button ID="btnBSSearch" runat="server" class="btn btn btn-info  mb-2" Text="Balance Sheet Search" OnClientClick="showSplash();" OnClick="btnBSSearch_Click"/>
            <asp:Button ID="btnExportToExcel" runat="server" class="btn btn btn-info  mb-2" Text="Export to Excel" OnClientClick="showSplash();" OnClick="btnExportToExcel_Click" />
        </div>
    </div>
    <br />
            <div class="form-group row">
    <div class="table-responsive">
        <asp:GridView ID="GridView1" runat="server" EnableViewState="true" UseAccessibleHeader="true" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="1000" class="table table-hover table-striped" Style="width: 70%" OnPreRender="GridView1_PreRender"></asp:GridView>
    </div></div>
</asp:Content>
