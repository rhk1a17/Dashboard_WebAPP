using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Web.Helpers;
using Dashboard_WebAPP.Models;

namespace Dashboard_WebAPP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() // DASHBOARD
        {
            var m = new ViewModel();
            ViewData["selectedInverter"] = "No Inverter Selected.";
            viewbagData(Request["ddlInverter"]); // ADDING OUTPUT TO VIWBAG TO DISPLAY ON HTML
            return View(m);
        }


        public ActionResult Visual() // VISUALIZATION
        {
            var m = new ViewModel();
            ViewData["selectedInverter"] = "No Inverter Selected.";
            return View(m);
        }


        //===============================DROPDOWN MENU=======================================
        [HttpPost]
        public ActionResult Index(string dummy)
        {
            var m = new ViewModel();
            m.SelectedInverter = Request["ddlInverter"];
            ViewData["selectedInverter"] = "Serial Number of Selected Inverter: " + Request["ddlInverter"];
            viewbagData(Request["ddlInverter"]); // ADDING OUTPUT TO VIWBAG TO DISPLAY ON HTML
            weatherWidget();
            return View(m);
        }

        [HttpPost]
        public ActionResult Visual(string dummy)
        {
            var m = new ViewModel();
            m.SelectedInverter = Request["ddlInverter"];
            ViewData["selectedInverter"] = "Serial Number of Selected Inverter: " + Request["ddlInverter"];
            weatherWidget();
            return View(m);
        }

        public void weatherWidget()
        {
            // CONFIGURING WEATHER WIDGET URL AND LABEL
            if (Request["ddlInverter"] == "2130242191")
            {
                ViewBag.myURL = "https://forecast7.com/en/3d06101d72/sungai-besi/";
                ViewBag.myDataLabel = "SUNGAI BESI";
            }
            if (Request["ddlInverter"] == "123")
            {
                ViewBag.myURL = "https://forecast7.com/en/3d13101d67/bangsar/";
                ViewBag.myDataLabel = "Bangsar";
            }
        }

        //===============================START OF DASHBOARD==================================

        public void viewbagData(string serialNumber)
        {
            ViewBag.myDate = date(serialNumber);
            ViewBag.mySerial = serial(serialNumber);
            ViewBag.myMppts = mppts(serialNumber);
            ViewBag.myDCc1 = DC_c1(serialNumber);
            ViewBag.myDCv1 = DC_v1(serialNumber);
            ViewBag.myDCp1 = DC_p1(serialNumber);
            ViewBag.myDCc2 = DC_c2(serialNumber);
            ViewBag.myDCv2 = DC_v2(serialNumber);
            ViewBag.myDCp2 = DC_p2(serialNumber);
            ViewBag.myTotalYield = total_yield(serialNumber);
            ViewBag.myCurrentYield = current_yield(serialNumber);
            ViewBag.myDailyYield = daily_yield(serialNumber);
            ViewBag.myCondition = condition(serialNumber);
        }

        public string date(string serial_no)
        {
            var sqldata = ConnectSQL(serial_no);
            var result = string.Empty;
            foreach (sqlData element in sqldata)
            {
                result = element.date.ToString();
            }
            return result;
        }

        public string serial(string serial_no)
        {
            var sqldata = ConnectSQL(serial_no);
            var result = string.Empty;
            foreach (sqlData element in sqldata)
            {
                result = element.serial.ToString();
            }
            return result;
        }

        public string mppts(string serial_no)
        {
            var sqldata = ConnectSQL(serial_no);
            var result = string.Empty;
            foreach (sqlData element in sqldata)
            {
                result = element.mppts.ToString();
            }
            return result;
        }

        public string DC_c1(string serial_no)
        {
            var sqldata = ConnectSQL(serial_no);
            var result = string.Empty;
            foreach (sqlData element in sqldata)
            {
                result = element.DC_c1.ToString() + " A";
            }
            return result;
        }

        public string DC_v1(string serial_no)
        {
            var sqldata = ConnectSQL(serial_no);
            var result = string.Empty;
            foreach (sqlData element in sqldata)
            {
                result = element.DC_v1.ToString() + " V";
            }
            return result;
        }

        public string DC_p1(string serial_no)
        {
            var sqldata = ConnectSQL(serial_no);
            var result = string.Empty;
            foreach (sqlData element in sqldata)
            {
                result = element.DC_p1.ToString() + " W";
            }
            return result;
        }

        public string DC_c2(string serial_no)
        {
            var sqldata = ConnectSQL(serial_no);
            var result = string.Empty;
            foreach (sqlData element in sqldata)
            {
                result = element.DC_c2.ToString() + " A";
            }
            return result;
        }

        public string DC_v2(string serial_no)
        {
            var sqldata = ConnectSQL(serial_no);
            var result = string.Empty;
            foreach (sqlData element in sqldata)
            {
                result = element.DC_v2.ToString() + " V";
            }
            return result;
        }

        public string DC_p2(string serial_no)
        {
            var sqldata = ConnectSQL(serial_no);
            var result = string.Empty;
            foreach (sqlData element in sqldata)
            {
                result = element.DC_p2.ToString() + " W";
            }
            return result;
        }

        public string total_yield(string serial_no)
        {
            var sqldata = ConnectSQL(serial_no);
            var result = string.Empty;
            foreach (sqlData element in sqldata)
            {
                result = element.total_yield.ToString() + " kWh";
            }
            return result;
        }

        public string current_yield(string serial_no)
        {
            var sqldata = ConnectSQL(serial_no);
            var result = string.Empty;
            foreach (sqlData element in sqldata)
            {
                result = element.current_yield.ToString() + " W";
            }
            return result;
        }

        public string daily_yield(string serial_no)
        {
            var sqldata = ConnectSQL(serial_no);
            var result = string.Empty;
            foreach (sqlData element in sqldata)
            {
                result = element.daily_yield.ToString() + " kWh";
            }
            return result;
        }

        public string condition(string serial_no)
        {
            var sqldata = ConnectSQL(serial_no);
            var result = string.Empty;
            foreach (sqlData element in sqldata)
            {
                string cond = element.condition.ToString();
                if (cond == "307")
                {
                    result = "Good";
                }
                else
                {
                    result = "Error";
                }

            }
            return result;
        }

        public List<sqlData> ConnectSQL(string serial_pass)
        {
            SqlConnectionStringBuilder sql = new SqlConnectionStringBuilder();

            // new list
            List<sqlData> dataList = new List<sqlData>();

            //SQL query to retrieve latest row from sql table
            string retrieve = String.Format("SELECT TOP 1 * FROM INVERTER_DATA WHERE SERIAL = {0} ORDER BY _datetime DESC", serial_pass);
            
            // SQL login data
            sql.DataSource = "sqlsever-ers.database.windows.net";   // Server name from azure
            sql.UserID = "ers"; // ID to access DB
            sql.Password = "testing123#";   //password to access DB
            sql.InitialCatalog = "inverterDB";  //Database name

            using (SqlConnection sqlConn = new SqlConnection(sql.ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(retrieve, sqlConn);
                try
                {
                    sqlConn.Open();
                    sqlCommand.ExecuteNonQuery();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            DateTime myDate = reader.GetDateTime(0);
                            string myDate1 = myDate.ToString();

                            dataList.Add(new sqlData()
                            {
                                date = myDate1,
                                serial = reader.GetInt32(1),
                                mppts = reader.GetInt32(2),
                                DC_c1 = reader.GetDouble(3),
                                DC_v1 = reader.GetDouble(4),
                                DC_p1 = reader.GetDouble(5),
                                DC_c2 = reader.GetDouble(6),
                                DC_v2 = reader.GetDouble(7),
                                DC_p2 = reader.GetDouble(8),
                                total_yield = reader.GetDouble(9),
                                current_yield = reader.GetDouble(10),
                                daily_yield = reader.GetDouble(11),
                                condition = reader.GetInt32(12),
                            });
                        }
                        
                    }
                }

                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
                }
                sqlConn.Close();
            }
            return dataList;
        }

        public class sqlData
        {
            public string date
            {
                get;
                set;
            }

            public int serial
            {
                get;
                set;
            }

            public int mppts
            {
                get;
                set;
            }

            public double DC_c1
            {
                get;
                set;
            }

            public double DC_v1
            {
                get;
                set;
            }

            public double DC_p1
            {
                get;
                set;
            }

            public double DC_c2
            {
                get;
                set;
            }

            public double DC_v2
            {
                get;
                set;
            }

            public double DC_p2
            {
                get;
                set;
            }

            public double total_yield
            {
                get;
                set;
            }

            public double current_yield
            {
                get;
                set;
            }

            public double daily_yield
            {
                get;
                set;
            }

            public Int32 condition
            {
                get;
                set;
            }
        }

        //===============================END OF DASHBOARD===================================
        // ==============================START OF POWER CHART===============================

        public List<sqlData> sqlPowerChart(string serial_no)
        {
            SqlConnectionStringBuilder sql = new SqlConnectionStringBuilder();

            // QUERY TO RETRIEVE DATA FROM SQL TO PLOT POWER CHART
            string retrieve = string.Format("SELECT * FROM INVERTER_DATA WHERE CONVERT(date, _datetime) = FORMAT(GETDATE(), 'yyyy/MM/dd') AND SERIAL = {0} ORDER BY _datetime;",serial_no);
            
            // QUERY TO DISPLAY SPECIFIC DAY'S GRAPH
            //string retrieve = "SELECT * FROM INVERTER_DATA WHERE CONVERT(date, _datetime) = CONVERT(date, '2019-07-14') ORDER BY _datetime;";// yyyy-MM-dd

            List<sqlData> chartPowerData = new List<sqlData>(); // NEW LIST "chartData" TO PLOT POWER CHART
                
            sql.DataSource = "sqlsever-ers.database.windows.net";   // Server name from azure
            sql.UserID = "ers"; // ID to access DB
            sql.Password = "testing123#";   //password to access DB
            sql.InitialCatalog = "inverterDB";  //Database name

            //CONNECTING TO SQL TO RETREIVE DATA AND ADD IT TO "chartPowerData"
            using (SqlConnection sqlConn = new SqlConnection(sql.ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(retrieve, sqlConn);
                try
                {
                    sqlConn.Open();
                    sqlCommand.ExecuteNonQuery();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            chartPowerData.Add(new sqlData()
                            {
                                date = reader.GetDateTime(0).ToShortTimeString(),// CONVERTING DATETIME FORMAT FROM SQL TO ONLY TIME STRING
                                serial = reader.GetInt32(1),
                                mppts = reader.GetInt32(2),
                                DC_c1 = reader.GetDouble(3),
                                DC_v1 = reader.GetDouble(4),
                                DC_p1 = reader.GetDouble(5),
                                DC_c2 = reader.GetDouble(6),
                                DC_v2 = reader.GetDouble(7),
                                DC_p2 = reader.GetDouble(8),
                                total_yield = reader.GetDouble(9),
                                current_yield = reader.GetDouble(10),
                                daily_yield = reader.GetDouble(11),
                                condition = reader.GetInt32(12),
                            });
                        }
                    }

                }
                catch(SqlException ex)
                {
                    Debug.WriteLine(ex);
                }
                sqlConn.Close();
            }
            return chartPowerData;
        }

        public WebImage powerChart(string serial_no)
        {
            // GENERATING WEB IMAGE FROM SQL DATA TO DISPLAY ON HTML
            var chartData = sqlPowerChart(serial_no);
            var dataTimeList = chartData.Select(i => i.date).ToArray();
            var dataValueList = chartData.Select(i => i.current_yield).ToArray();
            var dataChart = CreatePowerChart(dataTimeList, dataValueList).ToWebImage();
            return dataChart;
        }

        public Chart CreatePowerChart(string[] dataTimeList, Double[] dataValueList)
        {
            // CREATE AND CONFIGURING CHART TO BE DISPLAYED ON HTML
            var chart = new Chart(width: 1000, height: 300)
                .AddLegend("Power Generated Today")
                .AddSeries(
                name: "ERS SMA Inverter",
                chartType: "line",
                xValue: dataTimeList,
                yValues: dataValueList)
                .SetYAxis(title: "Power (W)")
                .SetXAxis(title: "Time")
                .Write("png");
            return chart;
        }

        //===============================END OF POWER CHART===================================
        //===============================START OF MONTHLY CHART===============================

        public List<sqlData> sqlMonthlyChart(string serial_no)
        {
            SqlConnectionStringBuilder sql = new SqlConnectionStringBuilder();

            // QUERY TO RETRIEVE DATA FROM SQL TO PLOT POWER CHART
            string retrieve = string.Format(";WITH cte AS (SELECT *,ROW_NUMBER() OVER (PARTITION BY CONVERT(date, _datetime) ORDER BY daily_yield DESC) AS rn FROM INVERTER_DATA WHERE SERIAL = {0} )SELECT * FROM cte WHERE rn = 1 AND MONTH(_datetime) =  MONTH('2019/07/01');", serial_no); // FORMAT: yyyy/MM/dd

            List<sqlData> chartMonthlyData = new List<sqlData>(); // NEW LIST "chartData" TO PLOT POWER CHART

            sql.DataSource = "sqlsever-ers.database.windows.net";   // Server name from azure
            sql.UserID = "ers"; // ID to access DB
            sql.Password = "testing123#";   //password to access DB
            sql.InitialCatalog = "inverterDB";  //Database name

            //CONNECTING TO SQL TO RETREIVE DATA AND ADD IT TO "chartPowerData"
            using (SqlConnection sqlConn = new SqlConnection(sql.ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(retrieve, sqlConn);
                try
                {
                    sqlConn.Open();
                    sqlCommand.ExecuteNonQuery();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            chartMonthlyData.Add(new sqlData()
                            {
                                date = reader.GetDateTime(0).ToString("dd/MM/yyyy"),// CONVERTING DATETIME FORMAT FROM SQL TO ONLY TIME STRING
                                serial = reader.GetInt32(1),
                                mppts = reader.GetInt32(2),
                                DC_c1 = reader.GetDouble(3),
                                DC_v1 = reader.GetDouble(4),
                                DC_p1 = reader.GetDouble(5),
                                DC_c2 = reader.GetDouble(6),
                                DC_v2 = reader.GetDouble(7),
                                DC_p2 = reader.GetDouble(8),
                                total_yield = reader.GetDouble(9),
                                current_yield = reader.GetDouble(10),
                                daily_yield = reader.GetDouble(11),
                                condition = reader.GetInt32(12),
                            });
                        }
                    }

                }
                catch (SqlException ex)
                {
                    Debug.WriteLine(ex);
                }
                sqlConn.Close();
            }
            return chartMonthlyData;
        }

        public WebImage monthlyChart(string serial_no)
        {
            // GENERATING WEB IMAGE FROM SQL DATA TO DISPLAY ON HTML
            var chartData = sqlMonthlyChart(serial_no);
            var dataTimeList = chartData.Select(i => i.date).ToArray();
            var dataValueList = chartData.Select(i => i.daily_yield).ToArray();
            var dataChart = CreateMonthlyChart(dataTimeList, dataValueList).ToWebImage();
            return dataChart;
        }

        public Chart CreateMonthlyChart(string[] dataTimeList, Double[] dataValueList)
        {
            // CREATE AND CONFIGURING CHART TO BE DISPLAYED ON HTML
            var chart = new Chart(width: 1000, height: 300)
                .AddLegend("Daily Energy Generated")
                .AddSeries(
                name: "ERS SMA Inverter",
                chartType: "column",
                xValue: dataTimeList,
                yValues: dataValueList)
                .SetYAxis(title: "Energy (kWh)")
                .SetXAxis(title: "Date")
                .Write("png"); // CONVERTTO PNG TO BE DISPLAYED AS AN IMAGE ON HTML
            return chart;
        }
        //===============================END OF MONTHLY CHART===============================
    }
}

