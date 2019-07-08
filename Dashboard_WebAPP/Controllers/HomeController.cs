using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace Dashboard_WebAPP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.myDate = date();
            ViewBag.mySerial = serial();
            ViewBag.myMppts = mppts();
            ViewBag.myDCc1 = DC_c1();
            ViewBag.myDCv1 = DC_v1();
            ViewBag.myDCp1 = DC_p1();
            ViewBag.myDCc2 = DC_c2();
            ViewBag.myDCv2 = DC_v2();
            ViewBag.myDCp2 = DC_p2();
            ViewBag.myTotalYield = total_yield();
            ViewBag.myCurrentYield = current_yield();
            ViewBag.myDailyYield = daily_yield();
            ViewBag.myCondition = condition();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string date()
        {
            var sqldata = ConnectSQL();
            var result = string.Empty;
            foreach (sqlData element in sqldata)
            {
                result = element.date.ToString();
            }
            return result;
        }

        public string serial()
        {
            var sqldata = ConnectSQL();
            var result = string.Empty;
            foreach (sqlData element in sqldata)
            {
                result = element.serial.ToString();
            }
            return result;
        }

        public string mppts()
        {
            var sqldata = ConnectSQL();
            var result = string.Empty;
            foreach (sqlData element in sqldata)
            {
                result = element.mppts.ToString();
            }
            return result;
        }

        public string DC_c1()
        {
            var sqldata = ConnectSQL();
            var result = string.Empty;
            foreach (sqlData element in sqldata)
            {
                result = element.DC_c1.ToString();
            }
            return result;
        }

        public string DC_v1()
        {
            var sqldata = ConnectSQL();
            var result = string.Empty;
            foreach (sqlData element in sqldata)
            {
                result = element.DC_v1.ToString();
            }
            return result;
        }

        public string DC_p1()
        {
            var sqldata = ConnectSQL();
            var result = string.Empty;
            foreach (sqlData element in sqldata)
            {
                result = element.DC_p1.ToString();
            }
            return result;
        }

        public string DC_c2()
        {
            var sqldata = ConnectSQL();
            var result = string.Empty;
            foreach (sqlData element in sqldata)
            {
                result = element.DC_c2.ToString();
            }
            return result;
        }

        public string DC_v2()
        {
            var sqldata = ConnectSQL();
            var result = string.Empty;
            foreach (sqlData element in sqldata)
            {
                result = element.DC_v2.ToString();
            }
            return result;
        }

        public string DC_p2()
        {
            var sqldata = ConnectSQL();
            var result = string.Empty;
            foreach (sqlData element in sqldata)
            {
                result = element.DC_p2.ToString();
            }
            return result;
        }

        public string total_yield()
        {
            var sqldata = ConnectSQL();
            var result = string.Empty;
            foreach (sqlData element in sqldata)
            {
                result = element.total_yield.ToString();
            }
            return result;
        }

        public string current_yield()
        {
            var sqldata = ConnectSQL();
            var result = string.Empty;
            foreach (sqlData element in sqldata)
            {
                result = element.current_yield.ToString();
            }
            return result;
        }

        public string daily_yield()
        {
            var sqldata = ConnectSQL();
            var result = string.Empty;
            foreach (sqlData element in sqldata)
            {
                result = element.daily_yield.ToString();
            }
            return result;
        }

        public string condition()
        {
            var sqldata = ConnectSQL();
            var result = string.Empty;
            foreach (sqlData element in sqldata)
            {
                result = element.condition.ToString();
            }
            return result;
        }

        public List<sqlData> ConnectSQL()
        {
            SqlConnectionStringBuilder sql = new SqlConnectionStringBuilder();

            // new list
            List<sqlData> dataList = new List<sqlData>();

            //SQL query to retrieve latest row from sql table
            string retrieve = "SELECT TOP 1 * FROM INVERTER_TABLE ORDER BY _datetime DESC";
            
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
                            dataList.Add(new sqlData()
                            {
                                date = reader.GetDateTime(0),
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
                                condition = reader.GetString(12),
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
            public DateTime date
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

            public string condition
            {
                get;
                set;
            }
        }
    }
}

