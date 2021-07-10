using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class grid : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_Click(object sender, EventArgs e)
    {
        GridView1.Visible = true;
        GridView2.Visible = false;
       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GridView1.Visible = false;
        GridView2.Visible = true;
    }
}