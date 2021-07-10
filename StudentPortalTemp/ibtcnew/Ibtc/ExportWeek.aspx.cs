using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ExportWeek : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["myConnection"]);
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn5_Click(object sender, EventArgs e)
    {
        EDateLbl.Text = "";
        DateTime Sdate1 = Convert.ToDateTime(Request.Form["SDate"]);
        DateTime Edate1 = Convert.ToDateTime(Request.Form["EDate"]);
        int compareDate = DateTime.Compare(Edate1, Sdate1);
        bool SDateEdate = false;
       if (compareDate < 0)
        {
            EDateLbl.Text = "Booking End Date must be greater than Booking Start Date";
            SDateEdate = true;
            string StartDte = SDate.Text;
            SDate.Text = StartDte;
            string EndtDte = EDate.Text;
            EDate.Text = EndtDte;


        }

        if (!SDateEdate)
        {
            ExportWeekly(retrieveData());  
        }
       
    }
    private void ExportWeekly(DataTable dt)
    {
     
        if (dt.Rows.Count > 0)
        {

            try
            {
                string filename = "IBTCWeeklySchedule.xls";
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.DataSource = dt;
                dgGrid.DataBind();

                //Get the HTML for the control.
                dgGrid.RenderControl(hw);
                //Write the HTML back to the browser.
                //Response.ContentType = application/vnd.ms-excel;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                this.EnableViewState = false;
                Response.Write(tw.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
             ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "test", "alert('Schedules has been succesfully Exported');", true);
            }
           
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "test", "alert('There are no Schedules for the Selected Dates!');", true);
        }
    }

    private DataTable retrieveData()
    {
        SqlCommand selectVenue = new SqlCommand("SELECT name,surname,Phone_number,email,Fax_no,Tel_no,IBTCVenue.Sdate,IBTCVenue.Edate,NumOfAttendees,Event_Name,Type as Venue From IBTClient INNER JOIN IBTCCourse on IBTCCourse.ClientID = IBTClient.ClientID INNER JOIN IBTCEvents on IBTCCourse.EventID = IBTCEvents.EventID INNER JOIN IBTCVenue on IBTCCourse.VenueID = IBTCVenue.VenueID where IBTCVenue.visibile = 'true' and IBTCVenue.SDate between @sdate and @edate", con);

        selectVenue.Parameters.Add("@sdate",SDate.Text);
        selectVenue.Parameters.Add("@edate", EDate.Text);
        DataTable dt = new DataTable();
        SqlDataAdapter sqla = new SqlDataAdapter(selectVenue);
        sqla.Fill(dt);
      
        return dt;
    }
}