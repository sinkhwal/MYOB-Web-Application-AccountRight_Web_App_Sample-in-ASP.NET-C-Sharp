using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using MYOB.AccountRight.SDK;
using MYOB.AccountRight.SDK.Contracts;

namespace Web.Helpers
{
    public static class SessionManager
    {

        private const string companyFile = "MyCompanyFile";
        private const string iApiConfiguration = "MyConfiguration";
        private const string iCompanyFileCredentials = "MyCredentials";
        private const string iOAuthKeyService = "MyOAuthKeyService";
        private const string companyFieles = "CompanyFiles";
        private const string selectedCompanyFile = "CompanyFile";

        private const string companyFileAutoBackup = "MyCompanyFileAutoBackup";
        private const string iApiConfigurationAutoBackup = "MyConfigurationAutoBackup";
        private const string iCompanyFileCredentialsAutoBackup = "MyCredentialsAutoBackup";
        private const string iOAuthKeyServiceAutoBackup = "MyOAuthKeyServiceAutoBackup";
       // private const string companyFieles = "CompanyFiles";
        private const string selectedCompanyFileAutoBackup = "CompanyFileAutoBackup";

        public static string CsOAuthServer = "https://secure.myob.com/oauth2/account/authorize/";
        public static string CsOAuthScope = "CompanyFile";
        public static string LocalApiUrl = "http://localhost:8080/accountright";
        public static string DeveloperKey = ConfigurationManager.AppSettings["DeveloperKey"];
        public static string DeveloperSecret = ConfigurationManager.AppSettings["DeveloperSecret"];
        public static string CallBackUrl = ConfigurationManager.AppSettings["CallBackUrl"];
        public static string Code { get; set; }

        public static string DeveloperKeyAutoBackup = ConfigurationManager.AppSettings["DeveloperKeyAutoBackup"];
        public static string DeveloperSecretAutoBackup = ConfigurationManager.AppSettings["DeveloperSecretAutoBackup"];
        public static string CodeWeb = ConfigurationManager.AppSettings["Code"];
        public static string CompanyId = ConfigurationManager.AppSettings["CompanyId"];
        public static string CompanyUserId = ConfigurationManager.AppSettings["CompanyUserId"];
        public static string CompanyPassword = ConfigurationManager.AppSettings["CompanyPassword"];
        public static DateTime ToDate = DateTime.Now;
        public static int TimerInHours = Convert.ToInt16(ConfigurationManager.AppSettings["TimerInHours"]);
        public static string FilePath = ConfigurationManager.AppSettings["FilePath"];
      
        public static CompanyFile CompanyFile
        {
            get
            {
                if (HttpContext.Current.Session[companyFile] != null)
                {
                    return (CompanyFile)HttpContext.Current.Session[companyFile];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session[companyFile] = value;
            }
        }
        public static IApiConfiguration MyConfiguration
        {
            get
            {
                if (HttpContext.Current.Session[iApiConfiguration] != null)
                {
                    return (IApiConfiguration)HttpContext.Current.Session[iApiConfiguration];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session[iApiConfiguration] = value;
            }
        }

        public static ICompanyFileCredentials MyCredentials
        {
            get
            {
                if (HttpContext.Current.Session[iCompanyFileCredentials] != null)
                {
                    return (ICompanyFileCredentials)HttpContext.Current.Session[iCompanyFileCredentials];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session[iCompanyFileCredentials] = value;
            }
        }
        public static IOAuthKeyService MyOAuthKeyService
        {
            get
            {
                if (HttpContext.Current.Session[iOAuthKeyService] != null)
                {
                    return (IOAuthKeyService)HttpContext.Current.Session[iOAuthKeyService];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session[iOAuthKeyService] = value;
            }
        }

        public static List<CompanyFile> CompanyFiles
        {
            get
            {
                if (HttpContext.Current.Session[companyFieles] == null)
                {
                    HttpContext.Current.Session[companyFieles] = new List<CompanyFile>();
                }
                return (List<CompanyFile>)HttpContext.Current.Session[companyFieles];
            }
            set { HttpContext.Current.Session[companyFieles] = value; }

        }

        public static CompanyFile SelectedCompanyFile
        {
            get
            {
                if (HttpContext.Current.Session[selectedCompanyFile] == null)
                {
                    HttpContext.Current.Session[selectedCompanyFile] = new CompanyFile();
                }
                return (CompanyFile)HttpContext.Current.Session[selectedCompanyFile];
            }
            set { HttpContext.Current.Session[selectedCompanyFile] = value; }

        }


        public static IApiConfiguration MyConfigurationAutoBackup
        {
            get
            {
                if (HttpContext.Current.Session[iApiConfigurationAutoBackup] != null)
                {
                    return (IApiConfiguration)HttpContext.Current.Session[iApiConfigurationAutoBackup];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session[iApiConfigurationAutoBackup] = value;
            }
        }

        public static ICompanyFileCredentials MyCredentialsAutoBackup
        {
            get
            {
                if (HttpContext.Current.Session[iCompanyFileCredentialsAutoBackup] != null)
                {
                    return (ICompanyFileCredentials)HttpContext.Current.Session[iCompanyFileCredentialsAutoBackup];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session[iCompanyFileCredentialsAutoBackup] = value;
            }
        }
        public static IOAuthKeyService MyOAuthKeyServiceAutoBackup
        {
            get
            {
                if (HttpContext.Current.Session[iOAuthKeyServiceAutoBackup] != null)
                {
                    return (IOAuthKeyService)HttpContext.Current.Session[iOAuthKeyServiceAutoBackup];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpContext.Current.Session[iOAuthKeyServiceAutoBackup] = value;
            }
        }

     

        public static CompanyFile SelectedCompanyFileAutoBackup
        {
            get
            {
                if (HttpContext.Current.Session[selectedCompanyFileAutoBackup] == null)
                {
                    HttpContext.Current.Session[selectedCompanyFileAutoBackup] = new CompanyFile();
                }
                return (CompanyFile)HttpContext.Current.Session[selectedCompanyFileAutoBackup];
            }
            set { HttpContext.Current.Session[selectedCompanyFileAutoBackup] = value; }

        }
    }
}
