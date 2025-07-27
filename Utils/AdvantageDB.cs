using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AdvantageProvider;
using OpenQA.Selenium;

namespace NUnit_Selenium.Utils
{
    public class AdvantageDB
    {
        private AdsConnection _connection;

        public AdvantageDB(string location)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            string _dataSource = "Data Source=" + location;
            string _userID = "User ID=" + "blahblah";
            string _password = "Password=" + "redacted";
            string _trim = "TrimTrailingSpaces=" + "True";
            _connection = new AdsConnection($"{_dataSource}; {_userID}; {_password}; {_trim}");
        }

        public bool PeriodFinalled()
        {
            // CLAIMED will be F if finished, 0 if running. As we'll do this only after kicking off a final then the risk of picking up the 'F' from a previous
            // run is minimal.
            AdsCommand cmd = new AdsCommand($"SELECT * FROM PCTECPD WHERE LASTRUN IS NOT NULL ORDER BY PERIOD DESC", _connection);

            try
            {
                cmd.Connection.Open();
                AdsDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    if (reader.GetString(reader.GetOrdinal("CLAIMED")) == "F")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch
            {
            }
            finally
            {
                cmd.Connection.Close();
            }
            return false;
        }

        public bool QualPlan_CheckExistence(string id)
        {
            id = id.ToUpper();
            AdsCommand cmd = new AdsCommand($"SELECT * FROM PCPLAN WHERE PLAN = '{id}'", _connection);

            try
            {
                cmd.Connection.Open();
                AdsDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    return true;
                }
            }
            catch
            {
            }
            finally
            {
                cmd.Connection.Close();
            }


            return false;
        }

        public bool CheckReportFinished(string report)
        {
            string start = $"{DateTime.Now.ToString("yyyy-MM-dd")} 00:00:01";
            string end = $"{DateTime.Now.ToString("yyyy-MM-dd")} 23:59:59";
            AdsCommand cmd = new($"SELECT * FROM PCSTOREREP WHERE REPORT_DATA_TYPE = 'R' AND REPORT_TIME BETWEEN '{start}' AND '{end}' AND REPORT_TITLE = '{report}'", _connection);

            try
            {
                cmd.Connection.Open();
                AdsDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    return true;
                }
            }
            catch
            {
            }
            finally
            {
                cmd.Connection.Close();
            }

            return false;
        }

        public string? GetClassroomLearnerID()
        {
            AdsCommand cmd = new($"SELECT IDENT FROM TRAINEE WHERE SURNAME = 'Test' AND FIRSTNAME = 'ATLearnerCLS' AND STATUS = 'L'", _connection);

            try
            {
                cmd.Connection.Open();
                AdsDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    // Doesn't matter if there are multiple records, so take the first one.
                    reader.Read();
                    return reader.GetString(0);
                }
            }
            catch
            {
            }
            finally
            {
                cmd.Connection.Close();
            }

            return null;
        }

        public string? GetApprenticeLearnerID()
        {
            AdsCommand cmd = new($"SELECT IDENT FROM TRAINEE WHERE SURNAME = 'Test' AND FIRSTNAME = 'ATLearnerApp' AND STATUS = 'L'", _connection);

            try
            {
                cmd.Connection.Open();
                AdsDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    // Doesn't matter if there are multiple records, so take the first one.
                    reader.Read();
                    return reader.GetString(0);
                }
            }
            catch
            {
            }
            finally
            {
                cmd.Connection.Close();
            }

            return null;
        }

        public string GetLearnerNIN(string id)
        {
            AdsCommand cmd = new($"SELECT NI_NO FROM TRAINEE WHERE IDENT = '{id}'", _connection);

            try
            {
                cmd.Connection.Open();
                AdsDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    return reader.GetString(0);
                }
            }
            catch
            {
            }
            finally
            {
                cmd.Connection.Close();
            }

            return "";
        }

        public bool CheckDB(string id, string table, string field, string value)
        {
            AdsCommand cmd = new($"SELECT {field} FROM {table} WHERE IDENT = '{id}'", _connection);

            try
            {
                cmd.Connection.Open();
                AdsDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    if (reader.GetString(0) == value) { return true; }
                }
            }
            catch
            {
            }
            finally
            {
                cmd.Connection.Close();
            }

            return false;
        }

        public bool CheckLearnerExistence(string learnerID)
        {
            AdsCommand cmd = new($"SELECT STATUS FROM TRAINEE WHERE IDENT = '{learnerID}'", _connection);

            try
            {
                cmd.Connection.Open();
                AdsDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    return reader.GetString(0) switch
                    {
                        "L" => true,
                        _ => false,
                    };
                }
            }
            catch
            {
            }
            finally
            {
                cmd.Connection.Close();
            }

            return false;
        }

        public void DeleteAllReports()
        {
            AdsCommand cmd = new($"DELETE FROM PCSTOREREP", _connection);

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public void DeleteCourseType(string courseType)
        {
            AdsCommand cmd = new($"DELETE FROM PCCSTYPE WHERE [CODE] = '{courseType}'" , _connection);

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public string AddCourseType(string typeName)
        {
            // Get the next available TYPE_ID
            AdsCommand cmd = new()
            {
                Connection = _connection
            };

            string nextID = "";

            try
            {
                cmd.CommandText = $"SELECT LASTVALUE FROM PCUIDS WHERE [TABLE] = 'PCCSTYPE' AND FIELD = 'TYPE_ID'";
                cmd.Connection.Open();

                AdsDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    // TYPE_ID is always a number so inc by 1 and pad it to 5 characters
                    nextID = (Int32.Parse(reader.GetString(0)) + 1).ToString().PadLeft(5, '0');
                }
            }
            catch
            { 
            }
            finally
            {
                cmd.Connection?.Close();
            }

            // Create new row in pcCSType
            try
            {
                if (nextID != "")
                {
                    cmd.CommandText = $"INSERT INTO PCCSTYPE (TYPE_ID, CODE, NAME, STATUS) VALUES ('{nextID}', '{typeName}', 'Course Type Edit', 'L')";
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {    
            }
            finally
            {
                cmd.Connection?.Close();
            }

            // Update pcUIDs with the incremented TYPE_ID
            try
            {
                cmd.CommandText = $"UPDATE PCUIDS SET LASTVALUE = '{nextID}' WHERE [TABLE] = 'PCCSTYPE' AND FIELD = 'TYPE_ID'";
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
            }
            finally
            {
                cmd.Connection?.Close();
            }

            return nextID;
        }

        public bool CheckCourseTypeSite(string TypeID, string site)
        {
            // Only works if one site has been selected. Will generalise if/when needed
            AdsCommand cmd = new()
            {
                Connection = _connection
            };

            try
            {
                cmd.CommandText = $"SELECT SITE FROM PCCSTPST WHERE TYPE_ID = '{TypeID}'";
                cmd.Connection.Open();

                AdsDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    if (reader.GetString(0) == site)
                    {
                        return true;
                    }
                }
            }
            catch
            {
            }
            finally
            {
                cmd.Connection?.Close();
            }

            return false;
        }

        public bool CheckCourseTypeExists(string TypeID)
        {
            AdsCommand cmd = new()
            {
                Connection = _connection
            };

            try
            {
                cmd.CommandText = $"SELECT SITE FROM PCCSTPST WHERE TYPE_ID = '{TypeID}'";
                cmd.Connection.Open();

                AdsDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
            }
            finally
            {
                cmd.Connection?.Close();
            }

            return false;
        }
    }
}
