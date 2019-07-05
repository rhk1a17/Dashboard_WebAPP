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
            var result = sqldata[0];
            return result.ToString();
        }

        public string serial()
        {
            var sqldata = ConnectSQL();
            var result = sqldata[1];
            return result.ToString();
        }

        public string mppts()
        {
            var sqldata = ConnectSQL();
            var result = sqldata[2];
            return result.ToString();
        }

        public string DC_c1()
        {
            var sqldata = ConnectSQL();
            var result = sqldata[3];
            return result.ToString();
        }

        public string DC_v1()
        {
            var sqldata = ConnectSQL();
            var result = sqldata[4];
            return result.ToString();
        }

        public string DC_p1()
        {
            var sqldata = ConnectSQL();
            var result = sqldata[5];
            return result.ToString();
        }

        public string DC_c2()
        {
            var sqldata = ConnectSQL();
            var result = sqldata[6];
            return result.ToString();
        }

        public string DC_v2()
        {
            var sqldata = ConnectSQL();
            var result = sqldata[7];
            return result.ToString();
        }

        public string DC_p2()
        {
            var sqldata = ConnectSQL();
            var result = sqldata[8];
            return result.ToString();
        }

        public string total_yield()
        {
            var sqldata = ConnectSQL();
            var result = sqldata[9];
            return result.ToString();
        }

        public string current_yield()
        {
            var sqldata = ConnectSQL();
            var result = sqldata[10];
            return result.ToString();
        }

        public string daily_yield()
        {
            var sqldata = ConnectSQL();
            var result = sqldata[11];
            return result.ToString();
        }

        public string condition()
        {
            var sqldata = ConnectSQL();
            var result = sqldata[12];
            return result.ToString();
        }

        public List<sqlData> ConnectSQL()
        {
            SqlConnectionStringBuilder sql = new SqlConnectionStringBuilder();

            // new list
            List<sqlData> dataList = new List<sqlData>();

            //retrieving latest row from sql table
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
                            dataList.Add(new sqlData
                            {
                                date = reader.GetValue(0),
                                serial = reader.GetValue(1),
                                mppts = reader.GetValue(2),
                                DC_c1 = reader.GetValue(3),
                                DC_v1 = reader.GetValue(4),
                                DC_p1 = reader.GetValue(5),
                                DC_c2 = reader.GetValue(6),
                                DC_v2 = reader.GetValue(7),
                                DC_p2 = reader.GetValue(8),
                                total_yield = reader.GetValue(9),
                                current_yield = reader.GetValue(10),
                                daily_yield = reader.GetValue(11),
                                condition = reader.GetValue(12),
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
            public object date
            {
                get;
                set;
            }

            public object serial
            {
                get;
                set;
            }

            public object mppts
            {
                get;
                set;
            }

            public object DC_c1
            {
                get;
                set;
            }

            public object DC_v1
            {
                get;
                set;
            }

            public object DC_p1
            {
                get;
                set;
            }

            public object DC_c2
            {
                get;
                set;
            }

            public object DC_v2
            {
                get;
                set;
            }

            public object DC_p2
            {
                get;
                set;
            }

            public object total_yield
            {
                get;
                set;
            }

            public object current_yield
            {
                get;
                set;
            }

            public object daily_yield
            {
                get;
                set;
            }

            public object condition
            {
                get;
                set;
            }
        }
    }
}

