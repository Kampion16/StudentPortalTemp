using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Eventype : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn5_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "test", "alert('SuccessFully Saved');", true);
        ready.Text ="Submitted";
       // Response.Write("<script>alert('ERROR 101 : Please Contact Web Administator')</script>");
    }
}