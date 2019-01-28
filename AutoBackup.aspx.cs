using System;
using System.Collections.Generic;

using System.Linq;
using MYOB.AccountRight.SDK;
using Web.Helpers;
using MYOB.AccountRight.SDK.Contracts;
using Web.Model;
using System.Xml;
using System.Web.UI;

namespace Web
{
    public partial class AutoBackup : System.Web.UI.Page
    {
        private int PageSize = 1000;
        private int eventId = 1;
        private static IApiConfiguration _configurationCloud;
        private static IOAuthKeyService _oAuthKeyService;
        protected void Page_Load(object sender, EventArgs e)
        {
            _configurationCloud = new ApiConfiguration(SessionManager.DeveloperKey, SessionManager.DeveloperSecret,SessionManager.CallBackUrl);
            _oAuthKeyService = new OAuthKeyService();
            if (_oAuthKeyService.OAuthResponse == null)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Server.MapPath("~/info.xml"));

                XmlNode nodeObj = doc.SelectSingleNode("/user/code");
                //string id = nodeObj["Project"].InnerText; // For inner text  


                var oauthService = new MYOB.AccountRight.SDK.Services.OAuthService(_configurationCloud);
                _oAuthKeyService.OAuthResponse = oauthService.GetTokens(nodeObj.Attributes["value"].Value);
            }

            SessionManager.MyOAuthKeyServiceAutoBackup = _oAuthKeyService;
            SessionManager.MyConfigurationAutoBackup = _configurationCloud;

            var cfsCloud = new MYOB.AccountRight.SDK.Services.CompanyFileService(_configurationCloud, null, _oAuthKeyService);
            CompanyFile[] companyFiles = cfsCloud.GetRange();
            CompanyFile companyFile = companyFiles.FirstOrDefault(a => a.Id == Guid.Parse(SessionManager.CompanyId));
            SessionManager.SelectedCompanyFileAutoBackup = companyFile;
            ICompanyFileCredentials credentials = new CompanyFileCredentials(SessionManager.CompanyUserId, SessionManager.CompanyPassword);
            SessionManager.MyCredentialsAutoBackup = credentials;
            HandleSales();
            HandlePurchase();
        }
        protected void HandleSales()
        {

            string filter = string.Format("$filter=Date ge datetime'{0}' and Date le datetime'{1}'", SessionManager.ToDate.AddYears(-1).ToString("yyyy-MM-dd"), SessionManager.ToDate.ToString("yyyy-MM-dd"));
            string pageFilter = string.Empty;

            List<SalesData> listPL = new List<SalesData>();
            listPL.Clear();
            var service = new MYOB.AccountRight.SDK.Services.Sale.ItemOrderService(SessionManager.MyConfigurationAutoBackup, null, SessionManager.MyOAuthKeyServiceAutoBackup);

            int count = 1000;
            for (int currentPage = 1; count >= 1000; currentPage++)
            {
                pageFilter = string.Format("&$top={0}&$skip={1}&$orderby=Date desc", PageSize,
                                         PageSize * (currentPage - 1));

                var list = service.GetRange(SessionManager.SelectedCompanyFileAutoBackup, filter + pageFilter, SessionManager.MyCredentialsAutoBackup, null);
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

            var serviceItemInvoicService = new MYOB.AccountRight.SDK.Services.Sale.ItemInvoiceService(SessionManager.MyConfigurationAutoBackup, null, SessionManager.MyOAuthKeyServiceAutoBackup);
            count = 1000;
            for (int currentPage = 1; count >= 1000; currentPage++)
            {
                pageFilter = string.Format("&$top={0}&$skip={1}&$orderby=Date desc", PageSize,
                                         PageSize * (currentPage - 1));

                var list = serviceItemInvoicService.GetRange(SessionManager.SelectedCompanyFileAutoBackup, filter + pageFilter, SessionManager.MyCredentialsAutoBackup, null);
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

            CreateExcelFile.CreateExcelDocument(listPL, System.IO.Path.Combine(SessionManager.FilePath, "Sales-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx"));

        }

        protected void HandlePurchase()
        {
            string filter = string.Format("$filter=Date ge datetime'{0}' and Date le datetime'{1}'", SessionManager.ToDate.AddYears(-1).ToString("yyyy-MM-dd"), SessionManager.ToDate.ToString("yyyy-MM-dd"));
            string pageFilter = string.Empty;
            var service = new MYOB.AccountRight.SDK.Services.Purchase.ItemBillService(SessionManager.MyConfigurationAutoBackup, null, SessionManager.MyOAuthKeyServiceAutoBackup);
            //   var service = new ItemOrderService(SessionManager.MyConfiguration, null, SessionManager.MyOAuthKeyService);
            List<SalesData> listPL = new List<SalesData>();

            listPL.Clear();
            int count = 1000;
            for (int currentPage = 1; count >= 1000; currentPage++)
            {
                pageFilter = string.Format("&$top={0}&$skip={1}&$orderby=Date desc", PageSize,
                                         PageSize * (currentPage - 1));

                var list = service.GetRange(SessionManager.SelectedCompanyFileAutoBackup, filter + pageFilter, SessionManager.MyCredentialsAutoBackup, null);
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

            var serviceItemInvoicService = new MYOB.AccountRight.SDK.Services.Purchase.ItemPurchaseOrderService(SessionManager.MyConfigurationAutoBackup, null, SessionManager.MyOAuthKeyServiceAutoBackup);
            count = 1000;
            for (int currentPage = 1; count >= 1000; currentPage++)
            {
                pageFilter = string.Format("&$top={0}&$skip={1}&$orderby=Date desc", PageSize,
                                         PageSize * (currentPage - 1));

                var list = serviceItemInvoicService.GetRange(SessionManager.SelectedCompanyFileAutoBackup, filter + pageFilter, SessionManager.MyCredentialsAutoBackup, null);
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
            CreateExcelFile.CreateExcelDocument(listPL, System.IO.Path.Combine(SessionManager.FilePath, "Purchases-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx"));
        }
    }
}