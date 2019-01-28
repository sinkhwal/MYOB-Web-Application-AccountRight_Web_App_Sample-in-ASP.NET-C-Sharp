using System;
using System.Web;
using System.Web.UI;
using System.Xml;
using MYOB.AccountRight.SDK;
using MYOB.AccountRight.SDK.Services;
using Web.Helpers;

namespace Web
{
    public partial class _Default : Page
    {
        private const bool UseCloud = true;
        private static IApiConfiguration _configurationCloud;
        private static IOAuthKeyService _oAuthKeyService;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            { 
                _configurationCloud = new ApiConfiguration(SessionManager.DeveloperKey, SessionManager.DeveloperSecret, SessionManager.CallBackUrl);
                _oAuthKeyService = new OAuthKeyService();

                SessionManager.MyConfiguration = _configurationCloud;

                if (_oAuthKeyService.OAuthResponse == null && string.IsNullOrEmpty(Request.QueryString["code"]))

                {
                    string url = string.Format("{0}?client_id={1}&redirect_uri={2}&scope={3}&response_type=code", SessionManager.CsOAuthServer,
                             _configurationCloud.ClientId, HttpUtility.UrlEncode(_configurationCloud.RedirectUrl), SessionManager.CsOAuthScope);
                    Response.Redirect(url);

                }
                else
                {
                    if (_oAuthKeyService.OAuthResponse == null)
                    {
                        var oauthService = new OAuthService(_configurationCloud);
                        _oAuthKeyService.OAuthResponse = oauthService.GetTokens(Request.QueryString["code"]);
                        SessionManager.Code = Request.QueryString["code"];

                        XmlDocument doc = new XmlDocument();
                        doc.Load(Server.MapPath("~/info.xml"));

                        XmlNode nodeObj = doc.SelectSingleNode("/user/code");
                        //string id = nodeObj["Project"].InnerText; // For inner text  
                        nodeObj.Attributes["value"].Value = Request.QueryString["code"];

                        doc.Save(Server.MapPath("~/info.xml"));
                    }
                    // SessionManager.Code =  Request.QueryString["code"];
                    SessionManager.MyOAuthKeyService = _oAuthKeyService;
                        Response.Redirect("CompanyListing.aspx");
                   
                }
            }

        }

       
    }
}