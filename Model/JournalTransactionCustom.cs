using MYOB.AccountRight.SDK.Contracts.Version2.GeneralLedger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Helpers;

namespace Web.Model
{
    public class JournalTransactionCustom
    {

        public string DisplayID { get; set; }
        public string JournalType { get; set; }
        public string DateOccurred { get; set; }
        public string DatePosted { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        // public IEnumerable<JournalTransactionLine> Lines { get; set; }
        // public AccountLink Account { get; set; }

        public string AccountName { get; set; }
        public string AccountDisplayID { get; set; }
        public decimal Amount { get; set; }
 
        public string JobNumber { get; set; }
        public string JobName { get; set; }
        public bool IsCredit { get; set; }
        public string LineDescription { get; set; }
        public string ReconciledDate { get; set; }
        // todo
        public string SourceTransaction { get; set; }

    }
}