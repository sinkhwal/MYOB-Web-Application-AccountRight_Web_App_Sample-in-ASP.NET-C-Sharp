using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using MYOB.AccountRight.SDK;
using MYOB.AccountRight.SDK.Contracts;
using MYOB.AccountRight.SDK.Services;
using Web.Helpers;

namespace Web
{
    public partial class Contact : Page
    {
        private static IApiConfiguration _configurationCloud;
        private static IOAuthKeyService _oAuthKeyService;
        private CompanyFile[] CompanyFiles;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Load all files from cloud and local simultaneously
            lblCode.Text = SessionManager.Code;
            var cfsCloud = new CompanyFileService(SessionManager.MyConfiguration, null, SessionManager.MyOAuthKeyService);
            CompanyFile[] a = cfsCloud.GetRange();
            CompanyFiles = a;
            GridView1.DataSource = a;
            GridView1.DataBind();
        }


        protected void GridViewData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ShowPopup")
            {
                LinkButton btndetails = (LinkButton)e.CommandSource;
                GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
                lblID.Text = GridView1.DataKeys[gvrow.RowIndex].Value.ToString();
                Popup(true);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

            CompanyFile companyFile = (CompanyFile)CompanyFiles.FirstOrDefault(a => a.Id == Guid.Parse(lblID.Text));//[e.RowIndex];

            ICompanyFileCredentials credentials = new CompanyFileCredentials(txtUserName.Text, txtPassword.Text);

            SessionManager.SelectedCompanyFile = companyFile;
            SessionManager.MyCredentials = credentials;
            Response.Redirect("SearchPage.aspx");

        }
        //To show message after performing operations
        void Popup(bool isDisplay)
        {
            StringBuilder builder = new StringBuilder();
            if (isDisplay)
            {
                builder.Append("<script language=JavaScript> ShowPopup(); </script>\n");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPopup", builder.ToString());
            }
            else
            {
                builder.Append("<script language=JavaScript> HidePopup(); </script>\n");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "HidePopup", builder.ToString());
            }
        }
    }
}