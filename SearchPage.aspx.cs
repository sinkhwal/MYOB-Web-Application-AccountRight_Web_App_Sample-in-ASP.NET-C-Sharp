using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using MYOB.AccountRight.SDK.Services;
using MYOB.AccountRight.SDK.Contracts.Version2.Sale;
using MYOB.AccountRight.SDK.Services.Sale;
using MYOB.AccountRight.SDK.Services.Purchase;
using MYOB.AccountRight.SDK.Services.GeneralLedger;
using Web.Helpers;
using Web.Model;

namespace Web
{
    public partial class SearchPage : System.Web.UI.Page
    {
        private int PageSize = 1000;
        // private int _currentPage = 1;
        public List<SalesData> listPL = new List<SalesData>();
        public List<ProfitAndLoss> listProfitandLoss = new List<ProfitAndLoss>();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var salesDataService = new Sale();
            string filter = string.Format("$filter=Date ge datetime'{0}' and Date le datetime'{1}'", txtFromDate.Text, txtToDate.Text);
            string pageFilter = string.Empty;
            listPL.Clear();
            var service = new ItemOrderService(SessionManager.MyConfiguration, null, SessionManager.MyOAuthKeyService);

            int count = 1000;
            for (int currentPage = 1; count >= 1000; currentPage++)
            {
                pageFilter = string.Format("&$top={0}&$skip={1}&$orderby=Date desc", PageSize,
                                         PageSize * (currentPage - 1));

                var list = service.GetRange(SessionManager.SelectedCompanyFile, filter + pageFilter, SessionManager.MyCredentials, null);
                count = list.Items.Count();
                //var invoisvc = new ItemInvoiceService(SessionManager.MyConfiguration, null, SessionManager.MyOAuthKeyService);
                //var list = invoisvc.GetRange(SessionManager.SelectedCompanyFile, null, SessionManager.MyCredentials);
                //
                var invoices = list.Items;
                foreach (var inv in invoices)
                {
                    // var items = inv.Lines;
                    foreach (var item in inv.Lines)
                    {
                        listPL.Add(new SalesData
                        {
                            CustomerName = inv.Customer != null ? inv.Customer.Name : "",
                            TransactionNumber = inv.Number,
                            TransactionDate = inv.Date != null ? inv.Date.ToString("yyyy-MM-dd") : "",
                            TransactionType = "Sales Order",
                            TransactionStatus = inv.Status.ToString(),
                            Itemumber = item.Item != null ? item.Item.Number : "",
                            Product = item.Item != null ? item.Item.Name : "",
                            //  AccountNumber=inv.ac,
                            LineMemo = inv.JournalMemo,
                            EmployeeName = inv.Salesperson != null ? inv.Salesperson.Name : "",
                            Qty = item.ShipQuantity,
                            // TaxExAmount = item.Total,
                            Total = item.Total,
                            TaxCode = item.TaxCode != null ? item.TaxCode.Code.ToString() : "",
                            PromisedDate = inv.PromisedDate,
                            ItemName = item.Description


                        });
                    }
                }
            }

            var serviceItemInvoicService = new ItemInvoiceService(SessionManager.MyConfiguration, null, SessionManager.MyOAuthKeyService);
            count = 1000;
            for (int currentPage = 1; count >= 1000; currentPage++)
            {
                pageFilter = string.Format("&$top={0}&$skip={1}&$orderby=Date desc", PageSize,
                                         PageSize * (currentPage - 1));

                var list = serviceItemInvoicService.GetRange(SessionManager.SelectedCompanyFile, filter + pageFilter, SessionManager.MyCredentials, null);
                count = list.Items.Count();
                //var invoisvc = new ItemInvoiceService(SessionManager.MyConfiguration, null, SessionManager.MyOAuthKeyService);
                //var list = invoisvc.GetRange(SessionManager.SelectedCompanyFile, null, SessionManager.MyCredentials);
                //
                var invoices = list.Items;
                foreach (var inv in invoices)
                {
                    // var items = inv.Lines;
                    foreach (var item in inv.Lines)
                    {
                        listPL.Add(new SalesData
                        {
                            CustomerName = inv.Customer!=null?inv.Customer.Name:"",
                            TransactionNumber = inv.Number,
                            TransactionDate = inv.Date!=null?inv.Date.ToString("yyyy-MM-dd"):"",
                            TransactionType = "Sales Order",
                            TransactionStatus = inv.Status.ToString(),
                            Itemumber = item.Item != null ? item.Item.Number : "",
                            Product = item.Item != null ? item.Item.Name : "",
                            //  AccountNumber=inv.ac,
                            LineMemo = inv.JournalMemo,
                            EmployeeName = inv.Salesperson != null ? inv.Salesperson.Name : "",
                            Qty = item.ShipQuantity,
                            // TaxExAmount = item.Total,
                            Total = item.Total,
                            TaxCode = item.TaxCode != null ? item.TaxCode.Code.ToString() : "",
                            PromisedDate = inv.PromisedDate,
                            ItemName = item.Description

                        });
                    }
                }
            }

            ViewState["ListSalePurchase"] = listPL;
            GridView1.DataSource = listPL.ToList();
            GridView1.DataBind();
            GridView1.Caption = "Sales";
        }




        protected void btnPurchaseSearch_Click(object sender, EventArgs e)
        {
            var salesDataService = new Sale();
            string filter = string.Format("$filter=Date ge datetime'{0}' and Date le datetime'{1}'", txtFromDate.Text, txtToDate.Text);
            string pageFilter = string.Empty;
            var service = new ItemBillService(SessionManager.MyConfiguration, null, SessionManager.MyOAuthKeyService);
            //   var service = new ItemOrderService(SessionManager.MyConfiguration, null, SessionManager.MyOAuthKeyService);
            listPL.Clear();
            int count = 1000;
            for (int currentPage = 1; count >= 1000; currentPage++)
            {
                pageFilter = string.Format("&$top={0}&$skip={1}&$orderby=Date desc", PageSize,
                                         PageSize * (currentPage - 1));

                var list = service.GetRange(SessionManager.SelectedCompanyFile, filter + pageFilter, SessionManager.MyCredentials, null);
                count = list.Items.Count();
                //var invoisvc = new ItemInvoiceService(SessionManager.MyConfiguration, null, SessionManager.MyOAuthKeyService);
                //var list = invoisvc.GetRange(SessionManager.SelectedCompanyFile, null, SessionManager.MyCredentials);
                //
                var invoices = list.Items;
                foreach (var inv in invoices)
                {
                    var salesData = new SalesData();
                    // var items = inv.Lines;
                    salesData.Total = inv.TotalAmount;
                    salesData.TaxCode = inv.FreightTaxCode.Code.ToString();
                    salesData.PromisedDate = inv.PromisedDate;
                    salesData.CustomerName = inv.Supplier.Name;
                    salesData.TransactionNumber = inv.Number;
                    salesData.TransactionDate = inv.Date.ToString("yyyy-MM-dd");
                    // TransactionType=inv.InvoiceType,
                    salesData.TransactionStatus = inv.Status.ToString();
                    if (inv.Lines != null)
                        foreach (var item in inv.Lines)
                        {


                            salesData.Itemumber = item.Item != null ? item.Item.Number : "";
                            salesData.ItemName = item.Item != null ? item.Item.Name : "";
                            // item.//  AccountNumber=inv.ac,
                            salesData.LineMemo = inv.JournalMemo;
                            //  item./ EmployeeName = inv.Salesperson != null ? inv.Salesperson.Name : "",
                            salesData.Qty = item.BillQuantity;



                        }
                    listPL.Add(salesData);
                }
            }

            var serviceItemInvoicService = new ItemPurchaseOrderService(SessionManager.MyConfiguration, null, SessionManager.MyOAuthKeyService);
            count = 1000;
            for (int currentPage = 1; count >= 1000; currentPage++)
            {
                pageFilter = string.Format("&$top={0}&$skip={1}&$orderby=Date desc", PageSize,
                                         PageSize * (currentPage - 1));

                var list = serviceItemInvoicService.GetRange(SessionManager.SelectedCompanyFile, filter + pageFilter, SessionManager.MyCredentials, null);
                count = list.Items.Count();
                //var invoisvc = new ItemInvoiceService(SessionManager.MyConfiguration, null, SessionManager.MyOAuthKeyService);
                //var list = invoisvc.GetRange(SessionManager.SelectedCompanyFile, null, SessionManager.MyCredentials);
                //
                var invoices = list.Items;
                foreach (var inv in invoices)
                {
                    // var items = inv.Lines;
                    foreach (var item in inv.Lines)
                    {
                        var saleDataInvItem = new SalesData();
                        // var items = inv.Lines;
                        saleDataInvItem.Total = inv.TotalAmount;
                        saleDataInvItem.TaxCode = inv.FreightTaxCode.Code.ToString();
                        saleDataInvItem.PromisedDate = inv.PromisedDate;
                        saleDataInvItem.CustomerName = inv.Supplier.Name;
                        saleDataInvItem.TransactionNumber = inv.Number;
                        saleDataInvItem.TransactionDate = inv.Date.ToString("yyyy-MM-dd");
                        // TransactionType=inv.InvoiceType,
                        saleDataInvItem.TransactionStatus = inv.Status.ToString();
                        foreach (var itemInvoic in inv.Lines)
                        {


                            saleDataInvItem.Itemumber = itemInvoic.Item != null ? itemInvoic.Item.Number : "";
                            saleDataInvItem.ItemName = itemInvoic.Item != null ? itemInvoic.Item.Name : "";
                            // item.//  AccountNumber=inv.ac,
                            saleDataInvItem.LineMemo = inv.JournalMemo;
                            //  item./ EmployeeName = inv.Salesperson != null ? inv.Salesperson.Name : "",
                            saleDataInvItem.Qty = itemInvoic.BillQuantity;



                        }
                        listPL.Add(saleDataInvItem);
                    }
                }
            }
            ViewState["ListSalePurchase"] = listPL;
            GridView1.Caption = "Purchases";
            GridView1.DataSource = listPL.ToList();
            GridView1.DataBind();
        }

        protected void btnProfitLossSearch_Click(object sender, EventArgs e)
        {
            var salesDataService = new Sale();
            string filter = string.Format("StartDate={0}&EndDate={1}&ReportingBasis=Cash&YearEndAdjust=false", txtFromDate.Text, txtToDate.Text);
            var service = new ProfitAndLossSummaryService(SessionManager.MyConfiguration, null, SessionManager.MyOAuthKeyService);
            var list = service.Get(SessionManager.SelectedCompanyFile, filter, SessionManager.MyCredentials, null);

            //  var invoisvc = new InvoiceService(SessionManager.MyConfiguration, null, SessionManager.MyOAuthKeyService);
            //  var invoices = invoisvc.GetRange(SessionManager.SelectedCompanyFile, null, SessionManager.MyCredentials);
            List<ProfitAndLoss> listProLoss = new List<ProfitAndLoss>();

            var accbrek = list.AccountsBreakdown;
            foreach (var ac in accbrek)
            {
                listProLoss.Add(new ProfitAndLoss
                {
                    AccountTotal = ac.AccountTotal,
                    Name = ac.Account.Name,
                    DisplayID = ac.Account.DisplayID
                    ,
                    StartDate = list.StartDate.ToString("yyyy-MM-dd"),
                    EndDate = list.EndDate.ToString("yyyy-MM-dd"),
                    //,YearEndAdjust=list.YearEndAdjust
                });

            }

            GridView1.Caption = "ProfitAndLoss";
            ViewState["ProfitAndLossData"] = listProfitandLoss;
            GridView1.DataSource = listProLoss;//list.AccountsBreakdown((v)=> {}).ToList();
            GridView1.DataBind();
            // DataList1.DataSource = listProfitandLoss;
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (GridView1.Caption == "ProfitAndLoss")
                CreateExcelFile.CreateExcelDocument((List<ProfitAndLoss>)ViewState["ProfitAndLossData"], "D:\\ProfitAndLoss-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");
            else
                CreateExcelFile.CreateExcelDocument((List<SalesData>)ViewState["ListSalePurchase"], "D:\\" + GridView1.Caption + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");
        }



        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;

            if (GridView1.Caption == "ProfitAndLoss")
                GridView1.DataSource = (List<ProfitAndLoss>)ViewState["ProfitAndLossData"];
            else
                GridView1.DataSource = (List<SalesData>)ViewState["ListSalePurchase"];
            GridView1.DataBind();
        }

        protected void GridView1_PreRender(object sender, EventArgs e)
        {
            try
            {
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            catch (Exception)
            {


            }

        }

        protected void btnBSSearch_Click(object sender, EventArgs e)
        {
            var salesDataService = new Sale();
            string filter = string.Format("$filter=DateOccurred ge datetime'{0}' and DateOccurred le datetime'{1}'", txtFromDate.Text, txtToDate.Text);
            //var service = new JournalTransactionService(SessionManager.MyConfiguration, null, SessionManager.MyOAuthKeyService);
            //var list = service.GetRange(SessionManager.SelectedCompanyFile, filter, SessionManager.MyCredentials, null);
            ////  var invoisvc = new InvoiceService(SessionManager.MyConfiguration, null, SessionManager.MyOAuthKeyService);
            ////  var invoices = invoisvc.GetRange(SessionManager.SelectedCompanyFile, null, SessionManager.MyCredentials);
            //List<JournalTransactionCustom> listJournalTransactionCustom = new List<JournalTransactionCustom>();
            //var Journals = list.Items;
            //foreach (var Journal in Journals)
            //{
            //    var JournalTransactionC = new JournalTransactionCustom();
            //    // var items = inv.Lines;
            //    JournalTransactionC.DisplayID = Journal.DisplayID;
            //    JournalTransactionC.JournalType = Journal.JournalType.ToString();
            //    JournalTransactionC.SourceTransaction = Journal.SourceTransaction.TransactionType;
            //    JournalTransactionC.DateOccurred = Journal.DateOccurred;
            //    JournalTransactionC.DatePosted = Journal.DatePosted;
            //    JournalTransactionC.Description = Journal.Description;
            //    // var items = inv.Lines;
            //    foreach (var line in Journal.Lines)
            //    {
            //        JournalTransactionC.AccountName = line.Account.Name;
            //        JournalTransactionC.DisplayID = line.Account.DisplayID;
            //        JournalTransactionC.Amount = line.Amount;
            //        JournalTransactionC.IsCredit = line.IsCredit;
            //        JournalTransactionC.JobName = line.Job!=null?line.Job.Name:"";
            //        JournalTransactionC.JobNumber = line.Job != null ? line.Job.Number : "";
            //        JournalTransactionC.LineDescription = line.LineDescription;
            //    }
            //    listJournalTransactionCustom.Add(JournalTransactionC);

            //}

            var service = new GeneralJournalService(SessionManager.MyConfiguration, null, SessionManager.MyOAuthKeyService);
            var list = service.GetRange(SessionManager.SelectedCompanyFile, filter, SessionManager.MyCredentials, null);
            //  var invoisvc = new InvoiceService(SessionManager.MyConfiguration, null, SessionManager.MyOAuthKeyService);
            //  var invoices = invoisvc.GetRange(SessionManager.SelectedCompanyFile, null, SessionManager.MyCredentials);
            List<JournalTransactionCustom> listJournalTransactionCustom = new List<JournalTransactionCustom>();
            var Journals = list.Items;
            foreach (var Journal in Journals)
            {
                var JournalTransactionC = new JournalTransactionCustom();
                // var items = inv.Lines;
                JournalTransactionC.DisplayID = Journal.DisplayID;
                // JournalTransactionC.JournalType = Journal.JournalType.ToString();
                JournalTransactionC.SourceTransaction = Journal.Category!=null?Journal.Category.DisplayID:"";//Journal.SourceTransaction.TransactionType;
                JournalTransactionC.CategoryName = Journal.Category != null ? Journal.Category.Name:"";
                JournalTransactionC.DateOccurred = Journal.DateOccurred.ToString("yyyy-MM-dd");
                //JournalTransactionC.DatePosted = Journal.DatePosted;
                JournalTransactionC.Description = Journal.Memo;
                // var items = inv.Lines;
                foreach (var line in Journal.Lines)
                {
                    JournalTransactionC.AccountName = line.Account.Name;
                    JournalTransactionC.DisplayID = line.Account.DisplayID;
                    JournalTransactionC.Amount = line.Amount;
                    JournalTransactionC.IsCredit = line.IsCredit;
                    JournalTransactionC.JobName = line.Job != null ? line.Job.Name : "";
                    JournalTransactionC.JobNumber = line.Job != null ? line.Job.Number : "";
                   // JournalTransactionC.LineDescription = line.LineDescription;
                }
                listJournalTransactionCustom.Add(JournalTransactionC);

            }

            GridView1.Caption = "ournalTransaction";
            // ViewState["ProfitAndLossData"] = listProfitandLoss;
            GridView1.DataSource = listJournalTransactionCustom;//list.AccountsBreakdown((v)=> {}).ToList();
            GridView1.DataBind();

        }
    }
}