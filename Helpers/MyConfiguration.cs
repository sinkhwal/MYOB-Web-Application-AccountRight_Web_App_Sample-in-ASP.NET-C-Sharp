using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using MYOB.AccountRight.SDK;
using MYOB.AccountRight.SDK.Contracts;

namespace Web.Helpers
{
    public static class MySettings
    {

        public static CompanyFile MyCompanyFile;
        public static IApiConfiguration MyConfiguration;
        public static ICompanyFileCredentials MyCredentials;

        public static IOAuthKeyService MyOAuthKeyService;
        public static void Initialise(IApiConfiguration configuration, CompanyFile companyFile,
                              ICompanyFileCredentials credentials, IOAuthKeyService oAuthKeyService)
        {
            // Add any initialization after the InitializeComponent() call.
            MyConfiguration = configuration;
            MyCompanyFile = companyFile;
            MyCredentials = credentials;
            MyOAuthKeyService = oAuthKeyService;

          //  Text = string.Format("{0} - {1}", companyFile.Name, companyFile.Uri);
        }

    }
}