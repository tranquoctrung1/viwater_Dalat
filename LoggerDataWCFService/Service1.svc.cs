using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;

namespace LoggerDataWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

        public string GetData(string value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public IEnumerable<SiteViewModel> GetSites()
        {
            List<SiteViewModel> lst = new List<SiteViewModel>();

            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string cmdText = "select [siteId],[location],[loggerId],[displayGroup],[latitude],[longitude],[labelAnchorX],[labelAnchorY] from [t_sites] order by [displayGroup],[location]";
                SqlCommand cmd = new SqlCommand(cmdText, connection);

                connection.Open();

                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    SiteViewModel s = new SiteViewModel();

                    s.SiteId = (string)rd["siteId"];
                    s.DisplayGroup = rd["displayGroup"].ToString();
                    s.Location = rd["location"].ToString();
                    s.LoggerId = rd["loggerId"].ToString();
                    if (rd["latitude"] != DBNull.Value)
                        s.Latitude = (double)rd["latitude"];
                    if (rd["longitude"] != DBNull.Value)
                        s.Longitude = (double)rd["longitude"];
                    if (rd["labelAnchorX"] != DBNull.Value)
                        s.LabelAnchorX = (int)rd["labelAnchorX"];
                    if (rd["labelAnchorY"] != DBNull.Value)
                        s.LabelAnchorY = (int)rd["labelAnchorY"];
                    lst.Add(s);
                }

                connection.Close();
            }
            return lst.Where(i => (i.LoggerId != null) && (i.LoggerId != "")).ToList();
        }

        public IEnumerable<SiteViewModel> GetSitesByUid(string uid)
        {
            string role = "";
            string consumer = "";
            string staff = "";

            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string cmdText = "select top 1 [consumerId],[staffId],[role] from [t_users] where [username]='" + uid + "'";
                SqlCommand cmd = new SqlCommand(cmdText, connection);

                connection.Open();

                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    role = (string)rd["role"];
                    consumer = (string)rd["consumerId"];
                    staff = (string)rd["staffId"];
                }

                connection.Close();
            }

            List<SiteViewModel> lst = new List<SiteViewModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string cmdText = "";
                switch (role)
                {
                    case "consumer":
                        cmdText = "select [siteId],[SiteAliasName],[location],[loggerId],[displayGroup],[latitude],[longitude],[labelAnchorX],[labelAnchorY] from [t_sites] where [consumerId]='" + consumer + "' order by [displayGroup],[location]";
                        break;
                    case "staff":
                        cmdText = "select [siteId],[SiteAliasName],[location],[loggerId],[displayGroup],[latitude],[longitude],[labelAnchorX],[labelAnchorY] from [t_sites] where [staffs] like '%" + staff + "%' order by [displayGroup],[location]";
                        break;
                    case "admin":
                        cmdText = "select [siteId],[SiteAliasName],[location],[loggerId],[displayGroup],[latitude],[longitude],[labelAnchorX],[labelAnchorY] from [t_sites] order by [displayGroup],[location]";
                        break;
                    default:
                        cmdText = "select [siteId],[SiteAliasName],[location],[loggerId],[displayGroup],[latitude],[longitude],[labelAnchorX],[labelAnchorY] from [t_sites] order by [displayGroup],[location]";
                        break;
                }

                SqlCommand cmd = new SqlCommand(cmdText, connection);

                connection.Open();

                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    SiteViewModel s = new SiteViewModel();

                    s.SiteId = (string)rd["siteId"];
                    s.SiteAliasName = rd["SiteAliasName"].ToString();
                    s.DisplayGroup = rd["displayGroup"].ToString();
                    s.Location = rd["location"].ToString();
                    s.LoggerId = rd["loggerId"].ToString();
                    if (rd["latitude"] != DBNull.Value)
                        s.Latitude = (double)rd["latitude"];
                    if (rd["longitude"] != DBNull.Value)
                        s.Longitude = (double)rd["longitude"];
                    if (rd["labelAnchorX"] != DBNull.Value)
                        s.LabelAnchorX = (int)rd["labelAnchorX"];
                    if (rd["labelAnchorY"] != DBNull.Value)
                        s.LabelAnchorY = (int)rd["labelAnchorY"];
                    lst.Add(s);
                }

                connection.Close();
            }

            return lst.Where(i => (i.LoggerId != null) || (i.LoggerId != "")).ToList();
        }

        public IEnumerable<SiteViewModel> GetSitesByUidDMA(string uid)
        {
            string role = "";
            string consumer = "";
            string staff = "";

            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string cmdText = "select top 1 [consumerId],[staffId],[role] from [t_users] where [username]='" + uid + "'";
                SqlCommand cmd = new SqlCommand(cmdText, connection);

                connection.Open();

                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    role = (string)rd["role"];
                    consumer = (string)rd["consumerId"];
                    staff = (string)rd["staffId"];
                }

                connection.Close();
            }

            List<SiteViewModel> lst = new List<SiteViewModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string cmdText = "";
                switch (role)
                {
                    case "consumer":
                        cmdText = "select [siteId],[baseLine],[location],[loggerId],[displayGroup],[latitude],[longitude],[labelAnchorX],[labelAnchorY] from [t_sites] where [consumerId]='" + consumer + "' and [displayGroup] ='DMA' order by [displayGroup],[location]";
                        break;
                    case "staff":
                        cmdText = "select [siteId],[baseLine],[location],[loggerId],[displayGroup],[latitude],[longitude],[labelAnchorX],[labelAnchorY] from [t_sites] where [staffs] like '%" + staff + "%' and [displayGroup] ='DMA' order by [displayGroup],[location]";
                        break;
                    case "admin":
                        cmdText = "select [siteId],[baseLine],[location],[loggerId],[displayGroup],[latitude],[longitude],[labelAnchorX],[labelAnchorY] from [t_sites] where [displayGroup] ='DMA' order by [displayGroup],[location]";
                        break;
                    default:
                        cmdText = "select [siteId],[baseLine],[location],[loggerId],[displayGroup],[latitude],[longitude],[labelAnchorX],[labelAnchorY] from [t_sites] where [displayGroup] ='DMA' order by [displayGroup],[location]";
                        break;
                }

                SqlCommand cmd = new SqlCommand(cmdText, connection);

                connection.Open();

                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    SiteViewModel s = new SiteViewModel();

                    s.SiteId = (string)rd["siteId"];
                    if (rd["baseLine"] != DBNull.Value)
                        s.BaseLine = (double)rd["baseLine"];
                    s.DisplayGroup = rd["displayGroup"].ToString();
                    s.Location = rd["location"].ToString();
                    s.LoggerId = rd["loggerId"].ToString();
                    if (rd["latitude"] != DBNull.Value)
                        s.Latitude = (double)rd["latitude"];
                    if (rd["longitude"] != DBNull.Value)
                        s.Longitude = (double)rd["longitude"];
                    if (rd["labelAnchorX"] != DBNull.Value)
                        s.LabelAnchorX = (int)rd["labelAnchorX"];
                    if (rd["labelAnchorY"] != DBNull.Value)
                        s.LabelAnchorY = (int)rd["labelAnchorY"];
                    lst.Add(s);
                }

                connection.Close();
            }

            return lst.Where(i => (i.LoggerId != null) || (i.LoggerId != "")).ToList();
        }

        public IEnumerable<SiteViewModel> GetSite(string id)
        {
            List<SiteViewModel> lst = new List<SiteViewModel>();

            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string cmdText = "select [siteId],[location],[loggerId],[displayGroup],[latitude],[longitude],[labelAnchorX],[labelAnchorY] from [t_sites] where [siteId]='" + id + "' order by [displayGroup],[location]";
                SqlCommand cmd = new SqlCommand(cmdText, connection);

                connection.Open();

                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    SiteViewModel s = new SiteViewModel();

                    s.SiteId = (string)rd["siteId"];
                    s.DisplayGroup = rd["displayGroup"].ToString();
                    s.Location = rd["location"].ToString();
                    s.LoggerId = rd["loggerId"].ToString();
                    if (rd["latitude"] != DBNull.Value)
                        s.Latitude = (double)rd["latitude"];
                    if (rd["longitude"] != DBNull.Value)
                        s.Longitude = (double)rd["longitude"];
                    if (rd["labelAnchorX"] != DBNull.Value)
                        s.LabelAnchorX = (int)rd["labelAnchorX"];
                    if (rd["labelAnchorY"] != DBNull.Value)
                        s.LabelAnchorY = (int)rd["labelAnchorY"];
                    lst.Add(s);
                }

                connection.Close();
            }

            return lst;
        }
        public IEnumerable<ComplexDataViewModelkq> GetDailyComplexData(string loggerID, string startDate, string endDate)
        {
            List<ComplexDataViewModel> listDailyComplexDataViewModel = new List<ComplexDataViewModel>();
            List<ComplexDataViewModelkq> listDailyComplexDataViewModelkq = new List<ComplexDataViewModelkq>();
            string dtFormat = "yyyy-MM-dd HH:mm";


            DateTime dStart = DateTime.FromOADate(double.Parse(startDate.Replace('_', '.')));
            DateTime dEnd = DateTime.FromOADate(double.Parse(endDate.Replace('_', '.')));

            int shour = dStart.Hour - 1;
            int sminute = dStart.Minute;
            dStart = dStart.AddHours(-shour).AddMinutes(-sminute);

            int ehour = dEnd.Hour;
            int eminute = dEnd.Minute;
            dEnd = dEnd.AddDays(1).AddHours(-ehour).AddMinutes(-eminute);


            int totalDay = Convert.ToInt32(dEnd.Subtract(dStart).TotalDays) + 1;

            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            //using (SqlConnection connection2 = new SqlConnection(connectionString))
            //{
            //    string cmdText2 = "select [startHour] from [t_Logger_Configurations] where [loggerId]='" + loggerID + "'";
            //    SqlCommand cmd2 = new SqlCommand(cmdText2, connection2);

            //    connection2.Open();

            //    SqlDataReader rd2 = cmd2.ExecuteReader();
            //    while (rd2.Read())
            //    {
            //        byte st = (byte)rd2["startHour"];
            //        dStart = dStart.AddHours(st + 1);
            //        dEnd = dEnd.AddDays(1).AddHours(st);
            //    }

            //    connection2.Close();
            //}
            using (SqlConnection connection2 = new SqlConnection(connectionString))
            {
                string cmdText2 = "select [Baseline] from [t_Sites] where [loggerId]='" + loggerID + "'";
                SqlCommand cmd2 = new SqlCommand(cmdText2, connection2);

                connection2.Open();

                SqlDataReader rd2 = cmd2.ExecuteReader();
                double? bl = 0;
                while (rd2.Read())
                {
                    if (rd2["Baseline"] != DBNull.Value)
                        bl = (double)rd2["Baseline"];

                }

                connection2.Close();
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string cmdText = "select [timeStamp],[forwardFlow],[reverseFlow],[minForwardFlow],[minreverseFlow],[maxforwardFlow],[maxreverseFlow],[minpressure1],[minpressure2] from [t_Data_Complexes] where [loggerId]='" + loggerID + "' and [timestamp] between '"
                    + dStart.ToString(dtFormat)
                    + "' and '"
                    + dEnd.ToString(dtFormat)
                    + "' order by [timestamp]";
                SqlCommand cmd = new SqlCommand(cmdText, connection);

                connection.Open();

                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ComplexDataViewModel c = new ComplexDataViewModel();
                    c.TimeStamp = (DateTime)rd["timeStamp"];
                    c.ForwardFlow = (double)rd["forwardFlow"];
                    c.ReverseFlow = (double)rd["reverseFlow"];
                    c.MinForwardFlow = (double)rd["minForwardFlow"];
                    c.MinreverseFlow = (double)rd["minreverseFlow"];
                    c.MaxforwardFlow = (double)rd["maxforwardFlow"];
                    c.MaxreverseFlow = (double)rd["maxreverseFlow"];
                    c.Minpressure1 = (double)rd["minpressure1"];
                    c.Minpressure2 = (double)rd["minpressure2"];
                    //if (rd["pressure1"] != DBNull.Value)
                    //    c.Press1 = (bool)rd["pressure1"];


                    listDailyComplexDataViewModel.Add(c);
                }

                connection.Close();
            }

            DateTime start;
            DateTime end;
            for (int i = 0; i < totalDay; i++)
            {
                start = dStart.AddDays(i);
                end = start.AddDays(1).AddHours(-1);


                DateTime startMNF = start.AddHours(1);//02:00
                DateTime endMNF = start.AddHours(3);//04:00
                var listMNF = listDailyComplexDataViewModel.Where(d => d.TimeStamp >= startMNF && d.TimeStamp <= endMNF).ToList();

                var list = listDailyComplexDataViewModel.Where(d => d.TimeStamp >= start && d.TimeStamp <= end).ToList();

                if (list != null && list.Count != 0)
                {
                    ComplexDataViewModelkq dailyComplexDataViewModel = new ComplexDataViewModelkq();
                    dailyComplexDataViewModel.TimeStamp = dStart.AddDays(i);
                    dailyComplexDataViewModel.Output = list.Sum(d => d.ForwardFlow ?? 0) - list.Sum(d => d.ReverseFlow ?? 0);
                    dailyComplexDataViewModel.MinFlowRate = list.Min(d => d.ForwardFlow ?? 0 - d.ReverseFlow ?? 0);
                    dailyComplexDataViewModel.MinPressure = list.Min(d => d.Minpressure2 ?? d.Minpressure1);
                    dailyComplexDataViewModel.MaxFlowRate = list.Max(d => d.ForwardFlow ?? 0 - d.ReverseFlow ?? 0);
                    dailyComplexDataViewModel.MaxPressure = list.Max(d => d.Minpressure2 ?? d.Minpressure1);
                    dailyComplexDataViewModel.MNF = listMNF.Min(d => d.ForwardFlow ?? 0 - d.ReverseFlow ?? 0);
                    listDailyComplexDataViewModelkq.Add(dailyComplexDataViewModel);
                }
            }

            return listDailyComplexDataViewModelkq;
        }
        public IEnumerable<ChannelViewModel> GetChannels(string loggerId)
        {
            List<ChannelViewModel> lst = new List<ChannelViewModel>();

            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string cmdText = "select [channelId],[channelName],[pressure1],[pressure2],[ForwardFlow],[timestamp],[lastValue],[unit],[indexTimestamp],[lastIndex], [DisplayOnLabel], [Offset] from [t_channel_configurations] where [loggerId]='" + loggerId + "' order by [channelName]";
                SqlCommand cmd = new SqlCommand(cmdText, connection);

                connection.Open();

                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ChannelViewModel c = new ChannelViewModel();
                    c.ChannelId = (string)rd["channelId"];
                    c.ChannelName = (string)rd["channelName"];
                    if (rd["pressure1"] != DBNull.Value)
                        c.Press1 = (bool)rd["pressure1"];
                    if (rd["pressure2"] != DBNull.Value)
                        c.Press2 = (bool)rd["pressure2"];
                    if (rd["ForwardFlow"] != DBNull.Value)
                        c.Flow = (bool)rd["ForwardFlow"];
                    if (rd["timestamp"] != DBNull.Value)
                        c.Timestamp = (DateTime)rd["timestamp"];
                    if (rd["lastValue"] != DBNull.Value)
                        c.Value = (double)rd["lastValue"];
                    if (rd["indexTimestamp"] != DBNull.Value)
                        c.IndexTimestamp = (DateTime)rd["indexTimestamp"];
                    if (rd["lastIndex"] != DBNull.Value)
                        c.LastIndex = (double)rd["lastIndex"];
                    c.Unit = rd["unit"].ToString();
                    if (rd["DisplayOnLabel"] != DBNull.Value)
                    {
                        c.DisplayOnLable = bool.Parse(rd["DisplayOnLabel"].ToString());
                    }
                    if(rd["Offset"] != DBNull.Value)
                    {
                        c.Offset = double.Parse(rd["Offset"].ToString());
                    }
                    lst.Add(c);
                }

                connection.Close();
            }

            int setDelayTime = 60;
            double setDiffValue = 0.3;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string cmdText = "select top 1 [setDelayTime],[setDiffValue] from [t_sites] where [loggerId]='" + loggerId + "'";
                SqlCommand cmd = new SqlCommand(cmdText, connection);

                connection.Open();

                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if (rd["setDelayTime"] != DBNull.Value)
                    {
                        setDelayTime = (int)rd["setDelayTime"];
                    }
                    if (rd["setDiffValue"] != DBNull.Value)
                    {
                        setDiffValue = (double)rd["setDiffValue"];
                    }
                }

                connection.Close();
            }

            foreach (var c in lst)
            {
                if (c.Press1 == true || c.Press2 == true)
                {
                    c.Status = 1;

                    if (c.Timestamp != null && (DateTime.Now - (DateTime)c.Timestamp).TotalMinutes >= setDelayTime)
                    {
                        c.Status = 2;
                        c.Status2 = true;
                    }

                    if (c.Timestamp != null && c.Value != null)
                    {
                        double? value = null;

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            string cmdText = "select [value] from [t_Data_Logger_" + c.ChannelId + "] where [Timestamp]='" + ((DateTime)c.Timestamp).AddDays(-1).ToString("yyyy-MM-dd HH:mm") + "'";
                            SqlCommand cmd = new SqlCommand(cmdText, connection);

                            connection.Open();
                            SqlDataReader rd = cmd.ExecuteReader();

                            while (rd.Read())
                            {
                                if (rd["Value"] != DBNull.Value)
                                {
                                    value = (double)rd["Value"];
                                }
                            }

                            connection.Close();
                        }

                        if (value != null && (((double)value <= ((double)c.Value) * (1 - setDiffValue)) || ((double)value >= ((double)c.Value) * (1 + setDiffValue))))
                        {
                            c.Status = 3;
                            c.Status3 = true;
                        }
                    }

                    if (c.Value <= 0 || c.Value == null)
                    {
                        c.Status = 4;
                        c.Status4 = true;
                    }

                    if (c.Status != 1)
                    {
                        c.Status1 = false;
                    }
                }
            }

            if (lst.Count == 0)
                lst.Add(new ChannelViewModel());
            return lst;
        }

        public IEnumerable<ChannelViewModel> GetChannelById(string id)
        {
            List<ChannelViewModel> lst = new List<ChannelViewModel>();

            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string cmdText = "select [channelId],[channelName],[pressure1],[pressure2],[timestamp],[lastValue],[unit],[indexTimestamp],[lastIndex] from [t_channel_configurations] where [channelId]='" + id + "' order by [channelName]";
                SqlCommand cmd = new SqlCommand(cmdText, connection);

                connection.Open();

                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ChannelViewModel c = new ChannelViewModel();
                    c.ChannelId = (string)rd["channelId"];
                    c.ChannelName = (string)rd["channelName"];
                    if (rd["pressure1"] != DBNull.Value)
                        c.Press1 = (bool)rd["pressure1"];
                    if (rd["pressure2"] != DBNull.Value)
                        c.Press2 = (bool)rd["pressure2"];
                    if (rd["timestamp"] != DBNull.Value)
                        c.Timestamp = (DateTime)rd["timestamp"];
                    if (rd["lastValue"] != DBNull.Value)
                        c.Value = (double)rd["lastValue"];
                    if (rd["indexTimestamp"] != DBNull.Value)
                        c.IndexTimestamp = (DateTime)rd["indexTimestamp"];
                    if (rd["lastIndex"] != DBNull.Value)
                        c.LastIndex = (double)rd["lastIndex"];
                    c.Unit = rd["unit"].ToString();

                    lst.Add(c);
                }

                connection.Close();
            }

            if (lst.Count == 0)
                lst.Add(new ChannelViewModel());

            return lst;
        }
        public IEnumerable<ChannelDataViewModel> GetChannelDataHourly(string siteId, string start, string end)
        {
            var ds = UnixToLocalDate(long.Parse(start));
            var de = UnixToLocalDate(long.Parse(end));
            List<ChannelDataViewModel> lst = new List<ChannelDataViewModel>();

            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("p_Calculate_One_Site_Hourly_Output", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                  new SqlParameter("@SiteId", siteId));
                cmd.Parameters.Add(
                   new SqlParameter("@Start", ds));
                cmd.Parameters.Add(
                  new SqlParameter("@End", de));

                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ChannelDataViewModel d = new ChannelDataViewModel();

                    d.Timestamp = (DateTime)rd["timestamp"];
                    if (rd["value"] != DBNull.Value)
                        d.Value = (double)rd["value"];

                    lst.Add(d);
                }

                connection.Close();
            }

            return lst;
        }
        public IEnumerable<ChannelDataViewModel> GetChannelDataDaily(string siteId, string start, string end)
        {
            var ds = UnixToLocalDate(long.Parse(start));
            var de = UnixToLocalDate(long.Parse(end));
            List<ChannelDataViewModel> lst = new List<ChannelDataViewModel>();

            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("p_Calculate_One_Site_Daily_Output", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                  new SqlParameter("@SiteId", siteId));
                cmd.Parameters.Add(
                   new SqlParameter("@Start", ds));
                cmd.Parameters.Add(
                  new SqlParameter("@End", de));

                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ChannelDataViewModel d = new ChannelDataViewModel();

                    d.Timestamp = (DateTime)rd["timestamp"];
                    if (rd["value"] != DBNull.Value)
                        d.Value = (double)rd["value"];

                    lst.Add(d);
                }

                connection.Close();
            }

            return lst;
        }
        public IEnumerable<ChannelDataViewModel> GetChannelDataMonthly(string siteId, string start, string end)
        {
            var ds = UnixToLocalDate(long.Parse(start));
            var de = UnixToLocalDate(long.Parse(end));
            List<ChannelDataViewModel> lst = new List<ChannelDataViewModel>();

            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("p_Calculate_One_Site_Monthly_Output", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                  new SqlParameter("@SiteId", siteId));
                cmd.Parameters.Add(
                   new SqlParameter("@Start", ds));
                cmd.Parameters.Add(
                  new SqlParameter("@End", de));

                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ChannelDataViewModel d = new ChannelDataViewModel();

                    d.Timestamp = (DateTime)rd["timestamp"];
                    if (rd["value"] != DBNull.Value)
                        d.Value = (double)rd["value"];

                    lst.Add(d);
                }

                connection.Close();
            }

            return lst;
        }

        public IEnumerable<ChannelDataViewModel> GetChannelDataYearly(string siteId, string start, string end)
        {
            var ds = UnixToLocalDate(long.Parse(start));
            var de = UnixToLocalDate(long.Parse(end));
            List<ChannelDataViewModel> lst = new List<ChannelDataViewModel>();

            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("p_Calculate_One_Site_Yearly_Output", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(
                  new SqlParameter("@SiteId", siteId));
                cmd.Parameters.Add(
                   new SqlParameter("@Start", ds));
                cmd.Parameters.Add(
                  new SqlParameter("@End", de));

                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ChannelDataViewModel d = new ChannelDataViewModel();

                    d.Timestamp = (DateTime)rd["timestamp"];
                    if (rd["value"] != DBNull.Value)
                        d.Value = (double)rd["value"];

                    lst.Add(d);
                }

                connection.Close();
            }

            return lst;
        }

        public IEnumerable<ChannelDataViewModel> GetChannelDataChart(string channelId, string start, string end)
        {
            string dtFormat = "yyyy-MM-dd";

            DateTime dStart = DateTime.FromOADate(double.Parse(start.Replace('_', '.')));
            DateTime dEnd = DateTime.FromOADate(double.Parse(end.Replace('_', '.')));
            //dStart = dStart.AddMinutes(1);
            //dEnd = dEnd.AddMinutes(1);

            List<ChannelDataViewModel> lst = new List<ChannelDataViewModel>();

            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string cmdText = "select [timestamp],[value] from [t_Data_Logger_" + channelId + "] where [timestamp] between '"
                    + dStart.ToString(dtFormat)
                    + "' and '"
                    + dEnd.ToString(dtFormat)
                    + "' order by [timestamp]";
                SqlCommand cmd = new SqlCommand(cmdText, connection);

                connection.Open();

                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ChannelDataViewModel d = new ChannelDataViewModel();

                    d.Timestamp = (DateTime)rd["timestamp"];
                    if (rd["value"] != DBNull.Value)
                        d.Value = (double)rd["value"];

                    lst.Add(d);
                }

                connection.Close();
            }

            return lst;
        }

        public IEnumerable<ChannelDataViewModel> GetChannelData(string channelId, string start, string end)
        {
            string dtFormat = "yyyy-MM-dd HH:mm";

            DateTime dStart = DateTime.FromOADate(double.Parse(start.Replace('_', '.')));
            DateTime dEnd = DateTime.FromOADate(double.Parse(end.Replace('_', '.')));
            dStart = dStart.AddMinutes(1);
            dEnd = dEnd.AddMinutes(1);

            List<ChannelDataViewModel> lst = new List<ChannelDataViewModel>();

            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string cmdText = "select [timestamp],[value] from [t_Data_Logger_" + channelId + "] where [timestamp] between '"
                    + dStart.ToString(dtFormat)
                    + "' and '"
                    + dEnd.ToString(dtFormat)
                    + "' order by [timestamp]";
                SqlCommand cmd = new SqlCommand(cmdText, connection);

                connection.Open();

                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ChannelDataViewModel d = new ChannelDataViewModel();

                    d.Timestamp = (DateTime)rd["timestamp"];
                    if (rd["value"] != DBNull.Value)
                        d.Value = (double)rd["value"];

                    lst.Add(d);
                }

                connection.Close();
            }

            return lst;
        }

        public IEnumerable<ChannelMultipleDataViewModel> GetMultipleChannelsData(string listChannelId, string start, string end)
        {
            string dtFormat = "yyyy-MM-dd HH:mm";

            DateTime dStart = DateTime.FromOADate(double.Parse(start.Replace('_', '.')));
            DateTime dEnd = DateTime.FromOADate(double.Parse(end.Replace('_', '.')));
            dStart = dStart.AddMinutes(1);
            dEnd = dEnd.AddMinutes(1);

            string[] channels = listChannelId.Split('|');

            List<ChannelMultipleDataViewModel> lst = new List<ChannelMultipleDataViewModel>();

            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string temp = "";
                string tempi = "";
                for (int i = 0; i < channels.Length; i++)
                {
                    if (i != channels.Length - 1)
                    {

                        temp += "SELECT*," + i.ToString() + " as col FROM [t_Data_Logger_" + channels[i] + "] where [Timestamp] between '"
                        + dStart.ToString(dtFormat)
                        + "' and '"
                        + dEnd.ToString(dtFormat) + "' UNION ";

                        tempi += "[" + i.ToString() + "],";

                    }
                    else
                    {

                        temp += "SELECT*," + i.ToString() + " as col FROM [t_Data_Logger_" + channels[i] + "] where [Timestamp] between '"
                        + dStart.ToString(dtFormat)
                        + "' and '"
                        + dEnd.ToString(dtFormat) + "'";

                        tempi += "[" + i.ToString() + "]";

                    }
                }
                string cmdText = "SELECT * FROM (";
                cmdText += temp;
                cmdText += ") AS myTBL ";
                cmdText += "PIVOT(sum(Value) ";
                cmdText += "FOR col IN (" + tempi + ")";
                cmdText += ") AS myTable order by [Timestamp]";

                SqlCommand cmd = new SqlCommand(cmdText, connection);

                connection.Open();

                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ChannelMultipleDataViewModel d = new ChannelMultipleDataViewModel();
                    List<double?> vals = new List<double?>();

                    d.Timestamp = (DateTime)rd["timestamp"];
                    for (int i = 0; i < channels.Length; i++)
                    {
                        if (rd[i.ToString()] != DBNull.Value)
                            vals.Add((double)rd[i.ToString()]);
                        else vals.Add(null);
                    }
                    d.Values = vals;
                    lst.Add(d);
                }
                connection.Close();
            }

            return lst;
        }

        public IEnumerable<AlarmViewModel> GetAlarms()
        {
            List<AlarmViewModel> lst = new List<AlarmViewModel>();

            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=C:\PMAC\PMACSITE.MDB";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                string cmdText = "select [AlarmTme],[Descript],[EntryTme],[Id],[Priority],[SiteName] from [ALARMLOG] order by [AlarmTme] desc";
                OleDbCommand cmd = new OleDbCommand(cmdText, connection);

                connection.Open();

                OleDbDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    AlarmViewModel a = new AlarmViewModel();
                    a.AlarmTme = rd["AlarmTme"].ToString();
                    a.Descript = rd["Descript"].ToString();
                    a.EntryTme = rd["EntryTme"].ToString();
                    a.Id = rd["Id"].ToString();
                    a.Priority = rd["Priority"].ToString();
                    a.SiteName = rd["SiteName"].ToString();
                    lst.Add(a);
                }

                connection.Close();
            }

            return lst;
        }

        public DateTime UnixToLocalDate(long UnixTimeSeconds)
        {
            return (new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).AddSeconds(UnixTimeSeconds).ToLocalTime();
        }

        public List<DisplayGroup> GetDisplayGroups()
        {
            List<DisplayGroup> list = new List<DisplayGroup>();
            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlQuery = "select [Group], [Description], [Name] from t_DisplayGroups";

                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlQuery, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            DisplayGroup el = new DisplayGroup();

                            try
                            {
                                el.Group = reader["Group"].ToString();
                            }
                            catch (Exception ex)
                            {
                                el.Group = "";
                            }
                            try
                            {
                                el.Description = reader["Description"].ToString();
                            }
                            catch (Exception ex)
                            {
                                el.Description = "";
                            }
                            try
                            {
                                el.Name = reader["Name"].ToString();
                            }
                            catch (Exception ex)
                            {
                                el.Name = "";
                            }

                            list.Add(el);
                        }
                    }

                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return list;
        }

        public List<SiteViewModel> GetSiteByDisplayGroup(string displayGroup)
        {
            List<SiteViewModel> list = new List<SiteViewModel>();

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlQuery = "select [siteId],[baseLine],[location],[loggerId],[displayGroup],[latitude],[longitude],[labelAnchorX],[labelAnchorY] from [t_sites] where [displayGroup] = '" + displayGroup + "' order by [displayGroup],[location]";
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(sqlQuery, connection);


                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        SiteViewModel s = new SiteViewModel();

                        s.SiteId = (string)rd["siteId"];
                        if (rd["baseLine"] != DBNull.Value)
                            s.BaseLine = (double)rd["baseLine"];
                        s.DisplayGroup = rd["displayGroup"].ToString();
                        s.Location = rd["location"].ToString();
                        s.LoggerId = rd["loggerId"].ToString();
                        if (rd["latitude"] != DBNull.Value)
                            s.Latitude = (double)rd["latitude"];
                        if (rd["longitude"] != DBNull.Value)
                            s.Longitude = (double)rd["longitude"];
                        if (rd["labelAnchorX"] != DBNull.Value)
                            s.LabelAnchorX = (int)rd["labelAnchorX"];
                        if (rd["labelAnchorY"] != DBNull.Value)
                            s.LabelAnchorY = (int)rd["labelAnchorY"];
                        list.Add(s);
                    }

                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return list.Where(i => (i.LoggerId != null) || (i.LoggerId != "")).ToList();

        }

        public List<ChannelDataViewer> GetChannelBySiteId(string siteid)
        {
            List<ChannelDataViewer> list = new List<ChannelDataViewer>();

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlQuery = "select c.ChannelId, c.ChannelName from t_Sites s join t_Channel_Configurations c on s.LoggerId = c.LoggerId where s.SiteId  = '" + siteid + "'";
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(sqlQuery, connection);


                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        ChannelDataViewer el = new ChannelDataViewer();
                        try
                        {
                            el.ChannelId = rd["ChannelId"].ToString();
                        }
                        catch (Exception ex)
                        {
                            el.ChannelId = "";
                        }
                        try
                        {
                            el.ChannelName = rd["ChannelName"].ToString();
                        }
                        catch (Exception ex)
                        {
                            el.ChannelName = "";
                        }

                        list.Add(el);
                    }

                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return list;
        }

        public List<DataViewer> GetCardSite(string siteid, string start, string end)
        {
            List<DataViewer> list = new List<DataViewer>();

            DateTime startDate = new DateTime(1970, 01, 01).AddHours(7).AddSeconds(int.Parse(start));
            DateTime endDate = new DateTime(1970, 01, 01).AddHours(7).AddSeconds(int.Parse(end));

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string store = "p_Data_Card_Site";
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(store, connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@siteid", siteid));
                    cmd.Parameters.Add(new SqlParameter("@start", startDate));
                    cmd.Parameters.Add(new SqlParameter("@end", endDate));

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DataViewer el = new DataViewer();
                        try
                        {
                            el.ChannelId = reader["ChannelId"].ToString();
                        }
                        catch (Exception ex)
                        {
                            el.ChannelId = "";
                        }
                        try
                        {
                            el.ChannelName = reader["ChannelName"].ToString();
                        }
                        catch (Exception ex)
                        {
                            el.ChannelName = "";
                        }
                        try
                        {
                            el.Unit = reader["Unit"].ToString();
                        }
                        catch (Exception ex)
                        {
                            el.Unit = "";
                        }
                        try
                        {
                            el.InstantTime = DateTime.Parse(reader["InstantTime"].ToString());
                        }
                        catch (Exception ex)
                        {
                            el.InstantTime = null;
                        }
                        try
                        {
                            el.InstantValue = double.Parse(reader["InstantValue"].ToString());
                        }
                        catch (Exception ex)
                        {
                            el.InstantValue = null;
                        }
                        try
                        {
                            el.MaxValue = double.Parse(reader["MaxValue"].ToString());
                        }
                        catch (Exception ex)
                        {
                            el.MaxValue = null;
                        }
                        try
                        {
                            el.MinValue = double.Parse(reader["MinValue"].ToString());
                        }
                        catch (Exception ex)
                        {
                            el.MinValue = null;
                        }

                        list.Add(el);
                    }

                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return list;
        }



        public List<List<DataMultipleChannel>> GetMultipleDataChannel(string siteid, string start, string end)
        {
            List<List<DataMultipleChannel>> matrix = new List<List<DataMultipleChannel>>();
            DateTime startDate = new DateTime(1970, 01, 01).AddHours(7).AddSeconds(int.Parse(start));
            DateTime endDate = new DateTime(1970, 01, 01).AddHours(7).AddSeconds(int.Parse(end));

            List<ChannelDataViewer> listChannel = GetChannelBySiteId(siteid);

            foreach (var channel in listChannel)
            {
                List<DataMultipleChannel> list = new List<DataMultipleChannel>();

                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string sqlQuery = "select TimeStamp, Value from t_Data_Logger_" + channel.ChannelId + " where TimeStamp between convert(nvarchar, '" + startDate + "', 120) and convert(nvarchar, '" + endDate + "', 120)";
                        connection.Open();

                        SqlCommand cmd = new SqlCommand(sqlQuery, connection);


                        SqlDataReader rd = cmd.ExecuteReader();
                        while (rd.Read())
                        {
                            DataMultipleChannel el = new DataMultipleChannel();

                            try
                            {
                                el.ChannelID = channel.ChannelId;
                            }
                            catch (Exception ex)
                            {
                                el.ChannelID = "";
                            }
                            try
                            {
                                el.TimeStamp = DateTime.Parse(rd["TimeStamp"].ToString());
                            }
                            catch (Exception ex)
                            {
                                el.TimeStamp = null;
                            }
                            try
                            {
                                el.Value = double.Parse(rd["Value"].ToString());
                            }
                            catch (Exception ex)
                            {
                                el.Value = null;
                            }

                            list.Add(el);
                        }

                        connection.Close();
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }

                matrix.Add(list);
            }

            return matrix;

        }

        public DateTime? GetCurrentTimeChannelId(string channelid)
        {
            DateTime? datetime = null;

            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "select top(1) TimeStamp from t_Data_Logger_" + channelid + " order by TimeStamp desc";

                connection.Open();

                SqlCommand cmd = new SqlCommand(sqlQuery, connection);


                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        try
                        {
                            datetime = DateTime.Parse(reader["TimeStamp"].ToString());
                        }
                        catch (Exception ex)
                        {
                            datetime = null;
                        }
                    }
                }
            }

            return datetime;
        }

        public List<LoggerAndSiteAliasNameViewModel> GetLoggerIdIsOtherDevice()
        {
            List<LoggerAndSiteAliasNameViewModel> list = new List<LoggerAndSiteAliasNameViewModel>();

            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "select  LoggerId, SiteAliasName from t_Sites where IsOtherDevice = 1";

                connection.Open();

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        LoggerAndSiteAliasNameViewModel el = new LoggerAndSiteAliasNameViewModel();

                        try
                        {
                            el.Loggerid = reader["LoggerId"].ToString();
                        }
                        catch (Exception ex)
                        {
                            el.Loggerid = "";
                        }
                        try
                        {
                            el.SiteAliasName = reader["SiteAliasName"].ToString();
                        }
                        catch(Exception ex)
                        {
                            el.SiteAliasName = "";
                        }
                        list.Add(el);
                    }
                }
            }

            return list;
        }

        public List<ListSiteForDetailTableViewModel> GetListSiteForDetailTable()
        {
            List<ListSiteForDetailTableViewModel> list = new List<ListSiteForDetailTableViewModel>();


            List<LoggerAndSiteAliasNameViewModel> listLoggers = GetLoggerIdIsOtherDevice();
            int count = 0;

            foreach(LoggerAndSiteAliasNameViewModel las in listLoggers)
            {
                ListSiteForDetailTableViewModel el = new ListSiteForDetailTableViewModel();
                el.NumberOrdered = (++count).ToString();
                el.Location = las.SiteAliasName;

                bool isUpdateTimeStamp = false;

                string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sqlQuery = "select  c.TimeStamp, c.ChannelId, c.LastValue, c.LastIndex from  t_Channel_Configurations c  where c.LoggerId = '"+ las.Loggerid+ "'";

                    connection.Open();

                    SqlCommand command = new SqlCommand(sqlQuery, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if(isUpdateTimeStamp == false)
                            {
                                try
                                {
                                    el.TimeStamp = DateTime.Parse(reader["TimeStamp"].ToString());
                                    isUpdateTimeStamp = true;

                                    DateTime now = DateTime.Now;

                                    if((now - el.TimeStamp.Value).TotalMinutes > 5)
                                    {
                                        el.Status = true;
                                    }
                                    else
                                    {
                                        el.Status = false;
                                    }
                                }
                                catch(Exception ex)
                                {
                                    el.TimeStamp = null;

                                }

                            }

                            try
                            {
                                string[] split = reader["ChannelId"].ToString().Split(new char[] { '_' }, StringSplitOptions.None);

                                if (split[1] == "Flow01")
                                {
                                    try
                                    {
                                        el.Flow01 = double.Parse(reader["LastValue"].ToString());
                                    }
                                    catch(Exception ex)
                                    {
                                        el.Flow01 = null;
                                    }

                                    try
                                    {
                                        el.Pulse01 = double.Parse(reader["LastIndex"].ToString());
                                    }
                                    catch(Exception ex)
                                    {
                                        el.Pulse01 = null;
                                    }
                                }
                                else if (split[1] == "Flow02")
                                {
                                    try
                                    {
                                        el.Flow02 = double.Parse(reader["LastValue"].ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        el.Flow02 = null;
                                    }

                                    try
                                    {
                                        el.Pulse02 = double.Parse(reader["LastIndex"].ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        el.Pulse02 = null;
                                    }
                                }
                                else if (split[1] == "P01")
                                {
                                    try
                                    {
                                        el.P01 = double.Parse(reader["LastValue"].ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        el.P01 = null;
                                    }
                                }
                                else if (split[1] == "P02")
                                {
                                    try
                                    {
                                        el.P02 = double.Parse(reader["LastValue"].ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        el.P02 = null;
                                    }
                                }
                                else if (split[1] == "P03")
                                {
                                    try
                                    {
                                        el.Bat = double.Parse(reader["LastValue"].ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        el.Bat = null;
                                    }

                                }
                                else if (split[1] == "P04")
                                {
                                    try
                                    {
                                        el.P04 = double.Parse(reader["LastValue"].ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        el.P04 = null;
                                    }
                                }
                            }
                            catch(Exception ex)
                            {

                            }

                        }
                    }
                }

                list.Add(el);
            }

            

            return list;
        }


        public int GetStartHourByLogger(string loggerid )
        {
            int result = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "select StartHour from [t_Logger_Configurations] where LoggerId = '" + loggerid + "'";

                connection.Open();

                SqlCommand cmd = new SqlCommand(sqlQuery, connection);

                SqlDataReader reader =  cmd.ExecuteReader();

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        try
                        {
                            result = int.Parse( reader["StartHour"].ToString());
                        }
                        catch(Exception ex)
                        {
                            result = 0;
                        }
                    }
                }
            }

            return result;
        }

        public DateTime? GetCurrentTimeStamp(string channelID)
        {
            DateTime? time = null;
            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "select top(1) TimeStamp from t_Index_Logger_" +channelID+ " order by TimeStamp desc";

                SqlCommand cmd = new SqlCommand(sqlQuery, connection);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        try
                        {
                            time = DateTime.Parse(reader["TimeStamp"].ToString());
                        }
                        catch (Exception ex)
                        {
                            time = null;
                        }
                    }
                }
            }

            return time;

        }

        public double? GetDataIndex(string channelid, DateTime start)
        {
            double? result = null;

            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "select top(1) Value from t_Index_Logger_" + channelid + " where TimeStamp >= convert(nvarchar, '"+start+"',120) and TimeStamp <= convert(nvarchar, '"+start.AddHours(1)+"', 120)";

                connection.Open();

                SqlCommand cmd = new SqlCommand(sqlQuery, connection);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        try
                        {
                            result = double.Parse(reader["Value"].ToString());
                        }
                        catch (Exception ex)
                        {
                            result = 0;
                        }
                    }
                }
            }

            return result;
        }

        public double? GetDataIndexCurrent(string channelid)
        {
            double? result = null;

            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "select top(1) Value from t_Index_Logger_" + channelid + " order by TimeStamp desc";

                connection.Open();

                SqlCommand cmd = new SqlCommand(sqlQuery, connection);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        try
                        {
                            result = double.Parse(reader["Value"].ToString());
                        }
                        catch (Exception ex)
                        {
                            result = 0;
                        }
                    }
                }
            }

            return result;
        }

        public string GetChannelForwardFlow(string loggerid)
        {
            string channelid = "";

            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                string sqlQuery = "select ChannelId from [t_Channel_Configurations] where LoggerId = '"+loggerid+ "' and ForwardFlow = 1";

                SqlCommand cmd = new SqlCommand(sqlQuery, connection);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        try
                        {
                            channelid = reader["ChannelId"].ToString();
                        }
                        catch (Exception ex)
                        {
                            channelid = "";
                        }
                    }
                }
            }

            return channelid;
        }

        public string GetChannelReverseFlow(string loggerid)
        {
            string channelid = "";

            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                string sqlQuery = "select ChannelId from [t_Channel_Configurations] where LoggerId = '" + loggerid + "' and ReverseFlow = 1";

                SqlCommand cmd = new SqlCommand(sqlQuery, connection);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        try
                        {
                            channelid = reader["ChannelId"].ToString();
                        }
                        catch (Exception ex)
                        {
                            channelid = "";
                        }
                    }
                }
            }

            return channelid;
        }


        public double GetValueIndexByLoggerId(string loggerid, string start)
        {
            double value = 0;

            DateTime startDate = new DateTime(1970,01,01).AddSeconds(int.Parse(start)).AddHours(7);

            int startHour = GetStartHourByLogger(loggerid);
            DateTime t = new DateTime(startDate.Year, startDate.Month, startDate.Day, startHour, 0, 0); ;

            if ((startDate.Hour < startHour) || (startDate.Hour == startHour && startDate.Minute == 0 && startDate.Second == 0))
            {
                t.AddDays(-1);
            }
          


            string channelForward = GetChannelForwardFlow(loggerid);
            string channelReverse = GetChannelReverseFlow(loggerid);

            double? indexForwardCurrent = 0;
            double? indexReverseCurrent = 0;

            double? indexForwardStartHour = 0;
            double? indexReverseStartHour = 0;

            if(channelForward != "")
            {
                indexForwardCurrent = GetDataIndexCurrent(channelForward);
                indexForwardStartHour = GetDataIndex(channelForward, t);
            }
            if(channelReverse != "")
            {
                indexReverseCurrent = GetDataIndexCurrent(channelReverse);
                indexReverseStartHour = GetDataIndex(channelReverse, t);
            }

            value = (indexForwardCurrent ?? 0 - indexReverseCurrent ?? 0) - (indexForwardStartHour ?? 0 - indexReverseStartHour ?? 0);

            return Math.Round(value,2);
        }

        public string Test()
        {
            return "Hello to Pi-solution";
        }
    }
}
