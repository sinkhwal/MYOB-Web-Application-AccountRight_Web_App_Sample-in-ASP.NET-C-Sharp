using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Model
{

    [Serializable]
    public class SalesData
    {


        public string CustomerName { get; set; }

        public string TransactionNumber { get; set; }
        public string TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public string TransactionStatus { get; set; }

        public string Itemumber { get; set; }
        public string ItemName { get; set; }
        public string AccountNumber { get; set; }
        public string LineMemo { get; set; }
        // public CardLink Customer { get; set; }
        public string EmployeeName { get; set; }

        public decimal Qty { get; set; }

        public decimal Total { get; set; }
        //public decimal TaxAmount { get; set; }
        public string TaxCode { get; set; }
        public DateTime? PromisedDate { get; set; }

        public string Product { get; set; }





    }
}