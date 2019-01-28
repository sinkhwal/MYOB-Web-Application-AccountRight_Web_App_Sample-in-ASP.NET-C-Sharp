//  Copyright:   Copyright 2013 MYOB Technology Pty Ltd. All rights reserved.
//  Website:     http://www.myob.com
//  Author:      MYOB
//  E-mail:      info@myob.com
//
// Documentation, code and sample applications provided by MYOB Australia are for 
// information purposes only. MYOB Technology Pty Ltd and its suppliers make no 
// warranties, either express or implied, in this document. 
//
// Information in this document or code, including website references, is subject
// to change without notice. Unless otherwise noted, the example companies, 
// organisations, products, domain names, email addresses, people, places, and 
// events are fictitious. 
//
// The entire risk of the use of this documentation or code remains with the user. 
// Complying with all applicable copyright laws is the responsibility of the user. 
//
// Copyright 2013 MYOB Technology Pty Ltd. All rights reserved.


using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using MYOB.AccountRight.SDK;

namespace Web.Helpers
{
    internal static class OAuthLogin
    {
        private const string CsOAuthServer = "https://secure.myob.com/oauth2/account/authorize/";

        private const string CsOAuthScope = "CompanyFile";

        /// <summary>
        /// Function to return the OAuth code
        /// </summary>
        /// <param name="config">Contains the API configuration such as ClientId and Redirect URL</param>
        /// <returns>OAuth code</returns>
        /// <remarks></remarks>
        public static string GetAuthorizationCode(IApiConfiguration config)
        {
            //Format the URL so  User can login to OAuth server
            string url = string.Format("{0}?client_id={1}&redirect_uri={2}&scope={3}&response_type=code", CsOAuthServer,
                                       config.ClientId, HttpUtility.UrlEncode(config.RedirectUrl), CsOAuthScope);
            var t = new Thread(()=> {
                var frm = new Form();
                var webB = new WebBrowser();
                frm.Controls.Add(webB);
                webB.Dock = DockStyle.Fill;

                // Add a handler for the web browser to capture content change 
                webB.DocumentTitleChanged += WebBDocumentTitleChanged;

                // navigat to url and display form
                webB.Navigate(url);
                frm.Size = new Size(800, 600);
                frm.ShowDialog();

                //Retrieve the code from the returned HTML
                 ExtractSubstring(webB.DocumentText, "code=", "<");
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            // Create a new form with a web browser to display OAuth login page
            return "";
        }

        private static void MyThreadStartMethod()
        {
         
        }


        /// <summary>
        /// Handler that is called when HTML title is changed in browser (i.e. content is reloaded)
        /// Once user has signed in to OAth page and authorised this app the OAuth code is returned in the HTML content 
        /// </summary>
        /// <param name="sender">The web browser control</param>
        /// <param name="e">The event</param>
        /// <remarks>This assumes redirect URL is http://desktop</remarks>
        private static void WebBDocumentTitleChanged(object sender, EventArgs e)
        {
            var webB = (WebBrowser) sender;
            var frm = (Form) webB.Parent;

            //Check if OAuth code is returned
            if (webB.DocumentText.Contains("code="))
            {
                frm.Close();
            }
        }

        /// <summary>
        /// Function to retrieve content from a string based on begining and ending pattern
        /// </summary>
        /// <param name="input">input string</param>
        /// <param name="startsWith">start pattern</param>
        /// <param name="endsWith">end pattern</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static string ExtractSubstring(string input, string startsWith, string endsWith)
        {
            Match match = Regex.Match(input, startsWith + "(.*)" + endsWith);
            string code = match.Groups[1].Value;
            return code;
        }
    }
}