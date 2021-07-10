using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class calendar_examples_calendarView : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["myConnection"]);
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnup_Click(object sender, EventArgs e)
    {


        string venueid = Hidden1.Value;

   //   ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "test", "alert('kampion"+venueid+"' );" , true);
      Response.Redirect(string.Format("../../UpdateVenue.aspx?VenueID={0}",venueid));
    }
    protected void btndel_Click(object sender, EventArgs e)
    {

        string venueid = Hidden1.Value;
        Delete(Convert.ToInt32(venueid));
    }
    private int Delete(int VenuID)
    {

        SqlCommand del = new SqlCommand("Update IBTCVenue Set Visibile = @Visibile where venueID = @VenueID", con);


        del.Parameters.Add("@Visibile", "false");
        del.Parameters.Add("@VenueID", VenuID);

        con.Close();
        con.Open();


        int success = del.ExecuteNonQuery();
        if (success > 0)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "test", "alert('Venue Successfully Deleted');", true);
            //Response.Write("<script>alert('Venue Successfully Deleted')</script>");
            Response.Redirect("http://webtest.dwa.gov.za/ibtcnew/ibtc/calendar/examples/calendarview.aspx");
        }

        return success;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("http://webtest.dwa.gov.za/ibtcnew/ibtc/BookVenueT.aspx");
    }
}