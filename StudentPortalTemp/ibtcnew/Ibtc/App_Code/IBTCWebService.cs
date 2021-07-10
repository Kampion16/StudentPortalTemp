using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
//using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for IBTCWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class IBTCWebService : System.Web.Services.WebService
{
    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["myConnection"]);

    // int VenueID;
    public IBTCWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public List<courseAttr> LoadCourseEdit(int coursID)
    {
        List<courseAttr> LstUd = new List<courseAttr>();
        DateTime today = DateTime.Today;
        DataTable dt;
        SqlCommand selectVenue = new SqlCommand("SELECT IBTCEvents.Event_name, IBTCCourse.CoursID, IBTCCourse.Sdate,IBTCCourse.edate,IBTCCourse.NumOfAttendees,IBTCCourse.ClientID,IBTCCourse.VenueID,IBTCCourse.EventID,IBTCCourse.BookmakerID, IBTCVenue.Type, IBTCLient.name as ClientName, IBTCLient.surname as Clientsurname,IBTCLient.phone_number as ClientCellNumber,IBTCLient.email as Clientmail,IBTCLient.fax_no as ClientFax,IBTCLient.Tel_no as ClientTel,IBTCBookMaker.name as BookMakerName,IBTCBookMaker.surname as BookMakersurname,IBTCBookMaker.email as BookMakermail,IBTCBookMaker.Fax_no as BookMakerFax,IBTCBookMaker.Tel_no as BookMakerTell from IBTCCourse INNER JOIN IBTClient ON IBTCCourse.ClientID = IBTClient.ClientID inner join IBTCBookMaker On IBTCCourse.BookmakerID = IBTCBookMaker.BookmakerID INNER JOIN IBTCVenue ON IBTCCourse.VenueID = IBTCVenue.VenueID INNER JOIN IBTCEvents ON IBTCCourse.EventID = IBTCEvents.EventID Where CoursID = @CoursID", con);

        //   courseAttr ud = new courseAttr();

        selectVenue.Parameters.AddWithValue("@Sdate", today);
        selectVenue.Parameters.AddWithValue("@CoursID", coursID);


        try
        {
            con.Close();
            con.Open();

            dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(selectVenue);
            sda.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                courseAttr ud = new courseAttr();
                ud.CourseID = dt.Rows[i]["CoursID"].ToString();
                ud.courseName = dt.Rows[i]["Event_Name"].ToString();
                ud._SdateTime = dt.Rows[i]["SDate"].ToString();
                ud._EdateTime = dt.Rows[i]["EDate"].ToString();
                ud._noAttendees = dt.Rows[i]["NumOfAttendees"].ToString();
                ud._Fax = dt.Rows[i]["ClientFax"].ToString();
                ud._ClientID = dt.Rows[i]["ClientID"].ToString();
                ud._VenueID = dt.Rows[i]["VenueID"].ToString();
                ud._surname = dt.Rows[i]["Clientsurname"].ToString();
                ud._email = dt.Rows[i]["Clientmail"].ToString();
                ud._name = dt.Rows[i]["ClientName"].ToString();
                ud._venue = dt.Rows[i]["type"].ToString();
                ud._Cell = dt.Rows[i]["ClientCellNumber"].ToString();
                ud._Tell = dt.Rows[i]["ClientTel"].ToString();
                ud.BookMakerName = dt.Rows[i]["BookMakerName"].ToString();
                ud.BookMakerSurName = dt.Rows[i]["BookMakersurname"].ToString();
                ud.BookMakerEmail = dt.Rows[i]["BookMakermail"].ToString();
                ud.BookMakerFax_no = dt.Rows[i]["BookMakerFax"].ToString();
                ud.BookMakerTel_no = dt.Rows[i]["BookMakerTell"].ToString();

                LstUd.Add(ud);
            }

            //if (success == 1)
            //{

            //}
        }
        catch (Exception)
        {

            throw;
        }
        return LstUd;
    }

    [WebMethod]
    public List<UserDetails> LoadVenuesEdit(int VenueID)
    {
        List<UserDetails> LstUd = new List<UserDetails>();
        DateTime today = DateTime.Today;
        DataTable dt;
        SqlCommand selectVenue = new SqlCommand("SELECT * From IBTClient INNER JOIN IBTCCourse on IBTCCourse.ClientID = IBTClient.ClientID INNER JOIN IBTCEvents on IBTCCourse.EventID = IBTCEvents.EventID INNER JOIN IBTCVenue on IBTCCourse.VenueID = IBTCVenue.VenueID where IBTCVenue.visibile = 'true' AND IBTCVenue.VenueID = @VenueID", con);

        //   courseAttr ud = new courseAttr();

        selectVenue.Parameters.AddWithValue("@Sdate", today);
        selectVenue.Parameters.AddWithValue("@VenueID", VenueID);


        try
        {
            con.Close();
            con.Open();

            dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(selectVenue);
            sda.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                UserDetails ud = new UserDetails();
                ud._VenueID = dt.Rows[i]["VenueID"].ToString();
                ud._venue = dt.Rows[i]["Type"].ToString();
                ud._surname = dt.Rows[i]["surname"].ToString();
                ud._name = dt.Rows[i]["name"].ToString();
                ud._ClientID = dt.Rows[i]["clientID"].ToString();
                ud._SdateTime = dt.Rows[i]["Sdate"].ToString();
                ud._EdateTime = dt.Rows[i]["Edate"].ToString();
                ud._NameSurnameID = ud._surname + " " + ud._name + " " + ud._ClientID + " " + ud._VenueID;
                ud._Cell = dt.Rows[i]["Phone_Number"].ToString();
                ud._email = dt.Rows[i]["Email"].ToString();
                ud._Tell = dt.Rows[i]["Tel_no"].ToString();
                ud._ClientID = dt.Rows[i]["ClientID"].ToString();
                ud.CourseID = dt.Rows[i]["CoursID"].ToString();
                ud._noAttendees = dt.Rows[i]["NumOfAttendees"].ToString();
                ud._eventDesc = dt.Rows[i]["Event_Description"].ToString();
                ud._eventName = dt.Rows[i]["Event_Name"].ToString();
                //ca.CourseSdate = dt.Rows[i]["EDate"].ToString();
                LstUd.Add(ud);
            }

            //if (success == 1)
            //{

            //}
        }
        catch (Exception)
        {

            throw;
        }
        return LstUd;
    }

    [WebMethod]
    public bool UserHasVenue(string name, string surname)
    {
        bool exists = false;
        //  int success;
        SqlCommand selectVenue = new SqlCommand("SELECT VenueID, name,surname from IBTCVenue INNER JOIN IBTClient ON IBTCVenue.ClientID = IBTClient.ClientID Where name = @name AND surname = @surname", con);
        DataTable dt;

        //  
        selectVenue.Parameters.AddWithValue("@name", name);
        selectVenue.Parameters.AddWithValue("@surname", surname);

        try
        {
            con.Close();
            con.Open();

            dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(selectVenue);
            sda.Fill(dt);


            // success = selectVenue.ExecuteNonQuery();
            if (dt.Rows.Count >= 1)
            {

                exists = true;
            }
            else
            {
                exists = false;
            }


        }
        catch (Exception)
        {

            exists = false;
        }
        return exists;
    }


    [WebMethod]
    public List<UserDetails> LoadCourses()
    {
        List<UserDetails> LstUd = new List<UserDetails>();
        DateTime today = DateTime.Today;
        DataTable dt;
        // SqlCommand selectVenue = new SqlCommand("SELECT * From IBTCVenue INNER JOIN IBTClient on IBTCVenue.ClientID = IBTClient.ClientID INNER JOIN IBTCCourse on IBTCCourse.VenueID = IBTCVenue.VenueID INNER JOIN IBTCEvents on IBTCCourse.EventID = IBTCEvents.EventID INNER JOIN IBTCCourse as coursClient on IBTCCourse.ClientID = IBTClient.ClientID where visibile = 'true'", con);
        //SqlCommand selectVenue = new SqlCommand("SELECT * From IBTClient INNER JOIN IBTCCourse on IBTCCourse.ClientID = IBTClient.ClientID INNER JOIN IBTCEvents on IBTCCourse.EventID = IBTCEvents.EventID INNER JOIN IBTCVenue on IBTCCourse.VenueID = IBTCVenue.VenueID where visibile = 'true'", con);
        SqlCommand selectVenue = new SqlCommand("SELECT  * From IBTClient INNER JOIN IBTCVenue on IBTCVenue.ClientID = IBTClient.ClientID INNER JOIN IBTCCourse on IBTCCourse.VenueID = IBTCVenue.VenueID INNER JOIN IBTCEvents on IBTCCourse.EventID = IBTCEvents.EventID where IBTCVenue.visibile = 'true'", con);
        //SqlCommand selectVenue = new SqlCommand("SELECT  * From IBTClient INNER JOIN IBTCCourse on IBTCCourse.ClientID = IBTClient.ClientID INNER JOIN IBTCEvents on IBTCCourse.EventID = IBTCEvents.EventID INNER JOIN IBTCVenue on IBTCCourse.VenueID = IBTCVenue.VenueID where IBTCVenue.visibile = 'true'", con);
        //IBTClient.ClientID, IBTClient.Name,IBTClient.Surname from IBTCVenue INNER JOIN IBTClient ON IBTCVenue.ClientID = IBTClient.ClientID  Where SDate >= @Sdate", con);

        //UserDetails ud = new UserDetails();
        selectVenue.Parameters.AddWithValue("@Sdate", today);
        selectVenue.Parameters.AddWithValue("@visibile", "true");

        try
        {
            con.Close();
            con.Open();

            dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(selectVenue);
            sda.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool found = false;
                UserDetails ud = new UserDetails();
                foreach (UserDetails item in LstUd)
                {
                    if (dt.Rows[i]["VenueID"].ToString() == item._VenueID.ToString())
                    {
                        found = true;
                    }                   
                }
                if (found)
                {

                }
                else
                {
                    ud._VenueID = dt.Rows[i]["VenueID"].ToString();
                    ud._venue = dt.Rows[i]["Type"].ToString();
                    ud._surname = dt.Rows[i]["surname"].ToString();
                    ud._name = dt.Rows[i]["name"].ToString();
                    ud._ClientID = dt.Rows[i]["clientID"].ToString();
                    ud._SdateTime = dt.Rows[i]["Sdate"].ToString();
                    ud._EdateTime = dt.Rows[i]["Edate"].ToString();
                    ud._NameSurnameID = ud._surname + " " + ud._name + " " + ud._ClientID + " " + ud._VenueID;
                    ud._Cell = dt.Rows[i]["Phone_Number"].ToString();
                    ud._email = dt.Rows[i]["Email"].ToString();
                    ud._Tell = dt.Rows[i]["Tel_no"].ToString();
                    ud._ClientID = dt.Rows[i]["ClientID"].ToString();
                    ud.CourseID = dt.Rows[i]["CoursID"].ToString();
                    ud._noAttendees = dt.Rows[i]["NumOfAttendees"].ToString();
                    ud._eventDesc = dt.Rows[i]["Event_Description"].ToString();
                    ud._eventName = dt.Rows[i]["Event_Name"].ToString();
                    LstUd.Add(ud);
                }
               
                
                //ca.CourseSdate = dt.Rows[i]["EDate"].ToString();
               
            }

            //if (success == 1)
            //{

            //}
        }
        catch (Exception)
        {

            throw;
        }

        return LstUd;
    }
    [WebMethod]
    public List<courseAttr> LoadVenues()
    {
        List<courseAttr> LstUd = new List<courseAttr>();
        DateTime today = DateTime.Today;
        DataTable dt;
        SqlCommand selectVenue = new SqlCommand("SELECT * From IBTCCourse INNER JOIN IBTClient on IBTCCourse.ClientID = IBTClient.ClientID INNER JOIN IBTCVenue on IBTCCourse.VenueID = IBTCVenue.VenueID INNER JOIN IBTCEvents on IBTCCourse.EventID = IBTCEvents.EventID where visible = @visible and IBTCVenue.Visibile = @visible", con);
        //IBTClient.ClientID, IBTClient.Name,IBTClient.Surname from IBTCVenue INNER JOIN IBTClient ON IBTCVenue.ClientID = IBTClient.ClientID  Where SDate >= @Sdate", con);

        //UserDetails ud = new UserDetails();
        selectVenue.Parameters.AddWithValue("@Sdate", today);
        selectVenue.Parameters.AddWithValue("@visible", "true");

        try
        {
            con.Close();
            con.Open();

            dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(selectVenue);
            sda.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                courseAttr ca = new courseAttr();
                ca.CourseID = dt.Rows[i]["CoursID"].ToString();
                ca.courseName = dt.Rows[i]["Event_Name"].ToString();
                ca._noAttendees = dt.Rows[i]["NumOfAttendees"].ToString();
                ca._SdateTime = dt.Rows[i]["Sdate"].ToString();
                ca._EdateTime = dt.Rows[i]["Edate"].ToString();
                ca._surname = dt.Rows[i]["surname"].ToString();
                ca._name = dt.Rows[i]["name"].ToString();
                ca._Fax = dt.Rows[i]["Fax_no"].ToString();
                //   ca._NameSurnameID = ud._surname + " " + ud._name + " " + ud._ClientID;
                ca._Cell = dt.Rows[i]["Phone_Number"].ToString();
                ca._email = dt.Rows[i]["Email"].ToString();
                ca._Tell = dt.Rows[i]["Tel_no"].ToString();
                ca._venue = dt.Rows[i]["Type"].ToString();
                LstUd.Add(ca);
            }

            //if (success == 1)
            //{

            //}
        }
        catch (Exception)
        {

            throw;
        }
        return LstUd;
    }

    [WebMethod]
    public List<EventsAttr> LoadEvents()
    {
        List<EventsAttr> LstUd = new List<EventsAttr>();
        // DateTime today = DateTime.Today;
        DataTable dt;
        SqlCommand selectVenue = new SqlCommand("SELECT * From IBTCEvents", con);
        //IBTClient.ClientID, IBTClient.Name,IBTClient.Surname from IBTCVenue INNER JOIN IBTClient ON IBTCVenue.ClientID = IBTClient.ClientID  Where SDate >= @Sdate", con);

        //UserDetails ud = new UserDetails();
        // selectVenue.Parameters.AddWithValue("@Sdate", today);

        try
        {
            con.Close();
            con.Open();

            dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(selectVenue);
            sda.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                EventsAttr ca = new EventsAttr();
                ca.EventID = dt.Rows[i]["eventID"].ToString();
                ca.EventName = dt.Rows[i]["Event_Name"].ToString();
                ca.EventIDName = dt.Rows[i]["eventID"].ToString() + "," + dt.Rows[i]["Event_Name"].ToString();
                LstUd.Add(ca);
            }

            //if (success == 1)
            //{

            //}
        }
        catch (Exception)
        {

            throw;
        }
        return LstUd;
    }

    [WebMethod]
    public List<VenueTypes> LoadVenueTypes()
    {
        List<VenueTypes> LstUd = new List<VenueTypes>();
        // DateTime today = DateTime.Today;
        DataTable dt;
        SqlCommand selectVenue = new SqlCommand("SELECT * From IBTCVenueTypes", con);
        //IBTClient.ClientID, IBTClient.Name,IBTClient.Surname from IBTCVenue INNER JOIN IBTClient ON IBTCVenue.ClientID = IBTClient.ClientID  Where SDate >= @Sdate", con);

        //UserDetails ud = new UserDetails();
        // selectVenue.Parameters.AddWithValue("@Sdate", today);

        try
        {
            con.Close();
            con.Open();

            dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(selectVenue);
            sda.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                VenueTypes ca = new VenueTypes();
                ca.VenueTypeID = dt.Rows[i]["VenueTypeID"].ToString();
                ca.VenueName = dt.Rows[i]["VenueName"].ToString();
                ca.VenueTypeIDName = dt.Rows[i]["VenueTypeID"].ToString() + "," + dt.Rows[i]["VenueName"].ToString();
                LstUd.Add(ca);
            }

            //if (success == 1)
            //{

            //}
        }
        catch (Exception)
        {

            throw;
        }
        return LstUd;
    }


    //[WebMethod]
    //public  void UpdateCourse(int CourseID, string EventID,string _noAttendees, DateTime _SdateTime,DateTime _EdateTime,string _name,string _surname, int _Cell, int _Tell, string _email)
    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["myConnection"]);
    //    List<courseAttr> LstUd = new List<courseAttr>();
    //    //  DateTime today = DateTime.Today;IBTCCourse inner join IBTClient on IBTCCourse.ClientID = IBTClient.ClientID
    // //   DataTable dt;
    //    SqlCommand selectVenue = new SqlCommand("Update IBTCCourse  SET EventID = @EventID,  Sdate = @Sdate, Edate = @Edate, NumOfAttendees = @NumOfAttendees, Time_stamp = @Time_stamp where coursID = @CoursID", con);

    //    int succes;
    //    //UserDetails ud = new UserDetails();name = @name, surname = @surname,
    //    selectVenue.Parameters.AddWithValue("@Title", courseName);
    //    selectVenue.Parameters.AddWithValue("@Sdate", _SdateTime);
    //    selectVenue.Parameters.AddWithValue("@Edate", _EdateTime);
    //    selectVenue.Parameters.AddWithValue("@NumOfAttendees", _noAttendees);
    //    selectVenue.Parameters.AddWithValue("@CoursID", CourseID);
    //    selectVenue.Parameters.AddWithValue("@Time_stamp", DateTime.Now);
    //    selectVenue.Parameters.AddWithValue("@name", _name);
    //    selectVenue.Parameters.AddWithValue("@surname", _surname);
    //    try
    //    {
    //        con.Close();
    //        con.Open();

    //        succes = selectVenue.ExecuteNonQuery();

    //    }
    //    catch (Exception)
    //    {

    //        throw;
    //    }

    //}
}
//ud.EventsNameID = dt.Rows[i]["EventsID"].ToString() + " " + dt.Rows[i]["Event_Name"]; 