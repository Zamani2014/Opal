using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using Zamani;

/// <summary>
/// Summary description for GenerateOrderID
/// </summary>
public class GenerateOrderID
{
	public GenerateOrderID()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static long GetNewOrderID()
    {
            PersianCalendar jc = new PersianCalendar();
            DateTime thisDate = System.DateTime.Now;

            string p_year = jc.GetYear(thisDate).ToString();
            string p_month = jc.GetMonth(thisDate).ToString();
            string p_day = jc.GetDayOfMonth(thisDate).ToString();

            string mytime = jc.GetHour(thisDate).ToString() + jc.GetMinute(thisDate).ToString() + jc.GetSecond(thisDate).ToString();
            string mydate = p_year + p_month + p_day;
            long OrderID = long.Parse(mytime + 1);
            int ID = GenerateIDColumn.GetNewID("OrderIDTbl");

            string ConnectionString = ConfigurationManager.ConnectionStrings["ArvidSMSConnectionString"].ConnectionString.ToString();
            SqlConnection CN = new SqlConnection(ConnectionString);
            CN.Open();
            string query = "INSERT INTO OrderIDTbl (ID, OrderID) values (@ID, @OrderID)";
            SqlCommand CMD = new SqlCommand(query, CN);
            CMD.Parameters.Add("@ID", SqlDbType.Int);
            CMD.Parameters.Add("@OrderID", SqlDbType.Float);

            CMD.Parameters["@ID"].Value = ID;
            CMD.Parameters["@OrderID"].Value = OrderID;
            CMD.ExecuteNonQuery();
            CN.Close();

            return OrderID;
    }
}