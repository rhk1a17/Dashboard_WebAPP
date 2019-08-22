using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using Dashboard_WebAPP.Models;

namespace Dashboard_WebAPP.Controllers
{
    public class PortalController : Controller
    {
        // GET: Portal
        public ActionResult PortalIndex()
        {
            var m = new PortalViewModel();
            ViewData["selectedInverter_sunny"] = "";
            viewbagData("TR Energy 0.99MWp");
            SqlStringRetrieve();
            ViewBag.myValue = 0;
            return View(m);
        }

        [HttpPost]
        public ActionResult PortalIndex(string dummy)
        {
            var m = new PortalViewModel();
            m.SelectedInverter = Request["ddlInverter_sunny"];
            ViewData["selectedInverter_sunny"] = "ID of Selected Inverter: " + Request["ddlInverter_sunny"];
            viewbagData(Request["ddlInverter_sunny"]);
            SqlStringRetrieve();
            ViewBag.myValue = 1;
            return View(m);
        }

        public void viewbagData(string Id)
        {
            ViewBag.myTimestamp = Timestamp(Id);
            ViewBag.myTitle = title(Id);
            ViewBag.myCurrentPower = current_power(Id);
            ViewBag.myEnergyToday = energy_today(Id);
            ViewBag.myTotalEnergy = total_energy(Id);
            ViewBag.myCO2today = co2_today(Id);
            ViewBag.myGraphURL = graph_image(Id);
        }

        public string Timestamp(string Id)
        {
            var sqldata = ConnectSQL(Id);
            string result = String.Empty;
            foreach (portal_data element in sqldata)
            {
                result = element.timestamp.ToString();
            }
            return result;
        }

        public string title(string Id)
        {
            var sqldata = ConnectSQL(Id);
            string result = String.Empty;
            foreach (portal_data element in sqldata)
            {
                result = element.title.ReadToEnd();
            }
            return result;
        }

        public string current_power(string Id)
        {
            var sqldata = ConnectSQL(Id);
            string result = String.Empty;
            foreach (portal_data element in sqldata)
            {
                result = element.current_power.ReadToEnd() + power_unit(Id).ToString();
            }
            return result;
        }

        public string power_unit(string Id)
        {
            var sqldata = ConnectSQL(Id);
            string result = String.Empty;
            foreach (portal_data element in sqldata)
            {
                result = element.power_unit.ReadToEnd();
            }
            return result;
        }

        public string energy_today(string Id)
        {
            var sqldata = ConnectSQL(Id);
            string result = String.Empty;
            foreach (portal_data element in sqldata)
            {
                result = element.energy_today.ReadToEnd() + energy_unit(Id).ToString();
            }
            return result;
        }

        public string energy_unit(string Id)
        {
            var sqldata = ConnectSQL(Id);
            string result = String.Empty;
            foreach (portal_data element in sqldata)
            {
                result = element.energy_unit.ReadToEnd();
            }
            return result;
        }

        public string total_energy(string Id)
        {
            var sqldata = ConnectSQL(Id);
            string result = String.Empty;
            foreach (portal_data element in sqldata)
            {
                result = element.total_energy.ReadToEnd() + total_energy_unit(Id).ToString();
            }
            return result;
        }

        public string total_energy_unit(string Id)
        {
            var sqldata = ConnectSQL(Id);
            string result = String.Empty;
            foreach (portal_data element in sqldata)
            {
                result = element.total_energy_unit.ReadToEnd();
            }
            return result;
        }
        public string co2_today(string Id)
        {
            var sqldata = ConnectSQL(Id);
            string result = String.Empty;
            foreach (portal_data element in sqldata)
            {
                result = element.co2_today.ReadToEnd() + co2_unit(Id).ToString();
            }
            return result;
        }

        public string co2_unit(string Id)
        {
            var sqldata = ConnectSQL(Id);
            string result = String.Empty;
            foreach (portal_data element in sqldata)
            {
                result = element.co2_unit.ReadToEnd();
            }
            return result;
        }

        public string graph_image(string Id)
        {
            var sqldata = ConnectSQL(Id);
            string result = String.Empty;
            foreach (portal_data element in sqldata)
            {
                result = element.real_datetime.ToString();
            }
            // OUTPUT TIMESTRING 16/8/2019 10:37:01 AM
            // REQUIRED TIMESTRING 2019-08-16T113430 //"yyyy-MM-ddTHHmmss"
            string string1 = String.Concat("https://ers123storage.blob.core.windows.net/graphcontainer/",result);
            string graph_url = String.Concat(string1, ".png");
            Debug.WriteLine(graph_url);
            return graph_url;
        }

        public List<portal_data> ConnectSQL(string Id)
        {
            SqlConnectionStringBuilder sql = new SqlConnectionStringBuilder();

            //SQL QUERY
            List<portal_data> dataList = new List<portal_data>();
            string retrieve = String.Format("SELECT TOP 1 * FROM SUNNY_PORTAL_STRING WHERE CONVERT(VARCHAR,title) ='{0}' ORDER BY real_datetime DESC;", Id);

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
                            string myDate = reader.GetDateTime(0).ToString();
                            dataList.Add(new portal_data()
                            {
                                timestamp = myDate,
                                title = reader.GetTextReader(1),
                                current_power = reader.GetTextReader(2),
                                power_unit = reader.GetTextReader(3),
                                energy_today = reader.GetTextReader(4),
                                energy_unit = reader.GetTextReader(5),
                                total_energy = reader.GetTextReader(6),
                                total_energy_unit = reader.GetTextReader(7),
                                co2_today = reader.GetTextReader(8),
                                co2_unit = reader.GetTextReader(9),
                                real_datetime = reader.GetDateTime(10).ToString("yyyy-MM-ddTHHmmss")

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

        public class portal_data
        {
            public string timestamp
            {
                get;
                set;
            }

            public TextReader title
            {
                get;
                set;
            }

            public TextReader current_power
            {
                get;
                set;
            }

            public TextReader power_unit
            {
                get;
                set;
            }

            public TextReader energy_today
            {
                get;
                set;
            }

            public TextReader energy_unit
            {
                get;
                set;
            }

            public TextReader total_energy
            {
                get;
                set;
            }

            public TextReader total_energy_unit
            {
                get;
                set;
            }

            public TextReader co2_today
            {
                get;
                set;
            }

            public TextReader co2_unit
            {
                get;
                set;
            }

            public string real_datetime
            {
                get;
                set;
            }
        }

        public void SqlStringRetrieve()
        {
            SqlConnectionStringBuilder sql = new SqlConnectionStringBuilder();

            //SQL QUERY
            float totalCurrentPowerVal = 0;
            float totalEnergyToday = 0;
            float totalEnergyAllTime = 0;

            string retrieve = "SELECT DISTINCT TOP 44 * FROM SUNNY_PORTAL_STRING WHERE CONVERT(date, _datetime) = FORMAT(GETDATE(), 'yyyy/MM/dd') ORDER BY real_datetime DESC;";

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
                            float tempPower;
                            float tempEnergy;
                            float tempTotal;

                            string power_unit = reader.GetTextReader(3).ReadToEnd().ToString();
                            string energy_unit = reader.GetTextReader(5).ReadToEnd().ToString();
                            string total_energy_unit = reader.GetTextReader(7).ReadToEnd().ToString();

                            if (float.TryParse(reader.GetTextReader(2).ReadToEnd().ToString(), out tempPower) && power_unit == "kW")
                            {
                                float current_power = tempPower;
                                totalCurrentPowerVal += (current_power * 1000);
                            }
                            else if (float.TryParse(reader.GetTextReader(2).ReadToEnd().ToString(), out tempPower) && power_unit == "W")
                            {
                                float current_power = tempPower;
                                totalCurrentPowerVal += current_power;
                            }

                            if (float.TryParse(reader.GetTextReader(4).ReadToEnd().ToString(), out tempEnergy) && energy_unit == "kWh")
                            {
                                float energy_today = tempEnergy;
                                totalEnergyToday += (energy_today * 1000);
                            }
                            else if (float.TryParse(reader.GetTextReader(4).ReadToEnd().ToString(), out tempEnergy) && energy_unit == "Wh")
                            {
                                float energy_today = tempEnergy;
                                totalEnergyToday += energy_today;
                            }

                            if (float.TryParse(reader.GetTextReader(6).ReadToEnd().ToString(), out tempTotal) && total_energy_unit == "MWh")
                            {
                                float total_energy = tempTotal;
                                totalEnergyAllTime += total_energy;
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
                }
                sqlConn.Close();
            }
            ViewBag.myTotalCurrentPowerVal = (Convert.ToInt32(totalCurrentPowerVal /1000)).ToString() + "kW" ;
            ViewBag.myTotalEnergyToday = (Convert.ToInt32(totalEnergyToday /1000)).ToString("N0") + "kWh";
            ViewBag.myTotalEnergyAllTime = totalEnergyAllTime.ToString() + "MWh";
        }
    }
}