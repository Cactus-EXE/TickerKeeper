using System;
using System.Web.UI;

namespace FinalApplication
{
    public partial class Site1 : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Add any logic that needs to run on every page load if necessary
            if (!IsPostBack)
            {
                // Initialization code
            }
        }
    }
}
