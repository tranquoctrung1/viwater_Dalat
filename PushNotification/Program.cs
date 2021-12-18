using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TanHoaPushNotification.localhost;

namespace TanHoaPushNotification
{
    public class Program
    {
        private const int SW_HIDE = 0;

        private const int SW_SHOW = 5;

        public Program()
        {
        }

        [DllImport("kernel32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        private static extern IntPtr GetConsoleWindow();

        private static void Main(string[] args)
        {
            Program.ShowWindow(Program.GetConsoleWindow(), 0);
            Timer t = new Timer(new TimerCallback(Program.TimerCallback), null, 0, 30000);
            Console.ReadLine();
        }

        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private static void TimerCallback(object o)
        {
            DateTime? timeStamp;
            int? setDelayTime;
            double? lastValue;
            double? baseline;
            TimeSpan timeSpan;
            double? nullable;
            bool flag;
            double? nullable1;
            Site site = new Site();
            ChannelConfiguration channelConfiguration = new ChannelConfiguration();
            HistoryPushNotification historyPushNotification = new HistoryPushNotification();
            foreach (t_Sites s in site.GetSites())
            {
                foreach (t_Channel_Configurations channel in channelConfiguration.GetAlarmChannelByLoggerID(s.LoggerId))
                {
                    bool isAlarm = false;
                    if (!historyPushNotification.CheckExists(s.SiteId, s.LoggerId, channel.ChannelId))
                    {
                        t_HistoryPushNotification t = new t_HistoryPushNotification();
                        setDelayTime = s.SetDelayTime;
                        int? delayTime = (setDelayTime.HasValue ? s.SetDelayTime : new int?(60));
                        timeStamp = channel.TimeStamp;
                        if (timeStamp.HasValue)
                        {
                            t.timestamp = channel.TimeStamp;
                        }
                        baseline = channel.LastValue;
                        if (baseline.HasValue)
                        {
                            baseline = channel.LastValue;
                            t.@value = new double?(baseline.GetValueOrDefault());
                        }
                        baseline = channel.basemin;
                        if (baseline.HasValue)
                        {
                            baseline = channel.LastValue;
                            lastValue = channel.basemin;
                            if (baseline.GetValueOrDefault() < lastValue.GetValueOrDefault() & baseline.HasValue & lastValue.HasValue)
                            {
                                t.channelid = channel.ChannelId;
                                timeStamp = channel.TimeStamp;
                                if (timeStamp.HasValue)
                                {
                                    t.timestamp = channel.TimeStamp;
                                }
                                lastValue = channel.LastValue;
                                if (lastValue.HasValue)
                                {
                                    lastValue = channel.LastValue;
                                    t.@value = new double?(lastValue.GetValueOrDefault());
                                }
                                t.type = "Vượt ngưỡng dưới";
                                isAlarm = true;
                            }
                        }
                        lastValue = channel.basemax;
                        if (lastValue.HasValue)
                        {
                            lastValue = channel.LastValue;
                            baseline = channel.basemax;
                            if (lastValue.GetValueOrDefault() > baseline.GetValueOrDefault() & lastValue.HasValue & baseline.HasValue)
                            {
                                t.channelid = channel.ChannelId;
                                timeStamp = channel.TimeStamp;
                                if (timeStamp.HasValue)
                                {
                                    t.timestamp = channel.TimeStamp;
                                }
                                baseline = channel.LastValue;
                                if (baseline.HasValue)
                                {
                                    baseline = channel.LastValue;
                                    t.@value = new double?(baseline.GetValueOrDefault());
                                }
                                t.type = "Vượt ngưỡng trên";
                                isAlarm = true;
                            }
                        }
                        baseline = channel.Baseline;
                        if (baseline.HasValue)
                        {
                            baseline = channel.LastValue;
                            lastValue = channel.Baseline;
                            if (baseline.GetValueOrDefault() > lastValue.GetValueOrDefault() & baseline.HasValue & lastValue.HasValue)
                            {
                                t.channelid = channel.ChannelId;
                                timeStamp = channel.TimeStamp;
                                if (timeStamp.HasValue)
                                {
                                    t.timestamp = channel.TimeStamp;
                                }
                                lastValue = channel.LastValue;
                                if (lastValue.HasValue)
                                {
                                    lastValue = channel.LastValue;
                                    t.@value = new double?(lastValue.GetValueOrDefault());
                                }
                                t.type = "Baseline";
                                isAlarm = true;
                            }
                        }
                        timeStamp = channel.TimeStamp;
                        if (timeStamp.HasValue)
                        {
                            DateTime now = DateTime.Now;
                            timeStamp = channel.TimeStamp;
                            timeSpan = now - timeStamp.Value;
                            double totalMinutes = timeSpan.TotalMinutes;
                            setDelayTime = delayTime;
                            if (setDelayTime.HasValue)
                            {
                                nullable = new double?((double)setDelayTime.GetValueOrDefault());
                            }
                            else
                            {
                                baseline = null;
                                nullable = baseline;
                            }
                            lastValue = nullable;
                            if (totalMinutes >= lastValue.GetValueOrDefault() & lastValue.HasValue)
                            {
                                t.channelid = channel.ChannelId;
                                timeStamp = channel.TimeStamp;
                                if (timeStamp.HasValue)
                                {
                                    t.timestamp = channel.TimeStamp;
                                }
                                lastValue = channel.LastValue;
                                if (lastValue.HasValue)
                                {
                                    lastValue = channel.LastValue;
                                    t.@value = new double?(lastValue.GetValueOrDefault());
                                }
                                t.type = "Gửi trể dữ liệu";
                                isAlarm = true;
                            }
                        }
                        if (isAlarm)
                        {
                            t.siteid = s.SiteId;
                            t.loggerid = s.LoggerId;
                            if (historyPushNotification.Add(t) != 1)
                            {
                                Console.WriteLine("Insert Fail");
                            }
                            else
                            {
                                string logger = s.LoggerId;
                                lastValue = t.@value;
                                double value = lastValue.GetValueOrDefault();
                                Map push = new Map();
                                push.SubmitNotification(logger, string.Concat(new string[] { "Cảnh báo ", t.type, " từ trạm ", s.SiteAliasName, " " }), string.Concat(new string[] { "Kênh ", channel.ChannelName, " giá trị: ", value.ToString(), " đã bị ", t.type, ". Bạn vui lòng kiểm tra lại" }));
                                Console.WriteLine("Insert Success");
                            }
                        }
                    }
                    else
                    {
                        t_HistoryPushNotification t = historyPushNotification.getHistory(s.SiteId, s.LoggerId, channel.ChannelId);
                        DateTime? nullable2 = t.timestamp;
                        timeStamp = channel.TimeStamp;
                        if (nullable2.HasValue == timeStamp.HasValue)
                        {
                            flag = (nullable2.HasValue ? nullable2.GetValueOrDefault() != timeStamp.GetValueOrDefault() : false);
                        }
                        else
                        {
                            flag = true;
                        }
                        if (flag)
                        {
                            setDelayTime = s.SetDelayTime;
                            int? delayTime = (setDelayTime.HasValue ? s.SetDelayTime : new int?(60));
                            timeStamp = channel.TimeStamp;
                            if (timeStamp.HasValue)
                            {
                                t.timestamp = channel.TimeStamp;
                            }
                            lastValue = channel.LastValue;
                            if (lastValue.HasValue)
                            {
                                lastValue = channel.LastValue;
                                t.@value = new double?(lastValue.GetValueOrDefault());
                            }
                            lastValue = channel.basemin;
                            if (lastValue.HasValue)
                            {
                                lastValue = channel.LastValue;
                                baseline = channel.basemin;
                                if (lastValue.GetValueOrDefault() < baseline.GetValueOrDefault() & lastValue.HasValue & baseline.HasValue)
                                {
                                    t.channelid = channel.ChannelId;
                                    timeStamp = channel.TimeStamp;
                                    if (timeStamp.HasValue)
                                    {
                                        t.timestamp = channel.TimeStamp;
                                    }
                                    baseline = channel.LastValue;
                                    if (baseline.HasValue)
                                    {
                                        baseline = channel.LastValue;
                                        t.@value = new double?(baseline.GetValueOrDefault());
                                    }
                                    t.type = "Vượt ngưỡng dưới";
                                    isAlarm = true;
                                }
                            }
                            baseline = channel.basemax;
                            if (baseline.HasValue)
                            {
                                baseline = channel.LastValue;
                                lastValue = channel.basemax;
                                if (baseline.GetValueOrDefault() > lastValue.GetValueOrDefault() & baseline.HasValue & lastValue.HasValue)
                                {
                                    t.channelid = channel.ChannelId;
                                    timeStamp = channel.TimeStamp;
                                    if (timeStamp.HasValue)
                                    {
                                        t.timestamp = channel.TimeStamp;
                                    }
                                    lastValue = channel.LastValue;
                                    if (lastValue.HasValue)
                                    {
                                        lastValue = channel.LastValue;
                                        t.@value = new double?(lastValue.GetValueOrDefault());
                                    }
                                    t.type = "Vượt ngưỡng trên";
                                    isAlarm = true;
                                }
                            }
                            lastValue = channel.Baseline;
                            if (lastValue.HasValue)
                            {
                                lastValue = channel.LastValue;
                                baseline = channel.Baseline;
                                if (lastValue.GetValueOrDefault() > baseline.GetValueOrDefault() & lastValue.HasValue & baseline.HasValue)
                                {
                                    t.channelid = channel.ChannelId;
                                    timeStamp = channel.TimeStamp;
                                    if (timeStamp.HasValue)
                                    {
                                        t.timestamp = channel.TimeStamp;
                                    }
                                    baseline = channel.LastValue;
                                    if (baseline.HasValue)
                                    {
                                        baseline = channel.LastValue;
                                        t.@value = new double?(baseline.GetValueOrDefault());
                                    }
                                    t.type = "Baseline";
                                    isAlarm = true;
                                }
                            }
                            timeStamp = channel.TimeStamp;
                            if (timeStamp.HasValue)
                            {
                                DateTime dateTime = DateTime.Now;
                                timeStamp = channel.TimeStamp;
                                timeSpan = dateTime - timeStamp.Value;
                                double num = timeSpan.TotalMinutes;
                                setDelayTime = delayTime;
                                if (setDelayTime.HasValue)
                                {
                                    nullable1 = new double?((double)setDelayTime.GetValueOrDefault());
                                }
                                else
                                {
                                    lastValue = null;
                                    nullable1 = lastValue;
                                }
                                baseline = nullable1;
                                if (num >= baseline.GetValueOrDefault() & baseline.HasValue)
                                {
                                    t.channelid = channel.ChannelId;
                                    timeStamp = channel.TimeStamp;
                                    if (timeStamp.HasValue)
                                    {
                                        t.timestamp = channel.TimeStamp;
                                    }
                                    baseline = channel.LastValue;
                                    if (baseline.HasValue)
                                    {
                                        baseline = channel.LastValue;
                                        t.@value = new double?(baseline.GetValueOrDefault());
                                    }
                                    t.type = "Gửi trể dữ liệu";
                                    isAlarm = true;
                                }
                            }
                            if (isAlarm)
                            {
                                t.siteid = s.SiteId;
                                t.loggerid = s.LoggerId;
                                if (historyPushNotification.Update(t) != 1)
                                {
                                    Console.WriteLine("Update Fail");
                                }
                                else
                                {
                                    string logger = s.LoggerId;
                                    baseline = t.@value;
                                    double value = baseline.GetValueOrDefault();
                                    Map push = new Map();
                                    push.SubmitNotification(logger, string.Concat(new string[] { "Cảnh báo ", t.type, " từ trạm ", s.SiteAliasName, " " }), string.Concat(new string[] { "Kênh ", channel.ChannelName, " giá trị: ", value.ToString(), " đã bị ", t.type, ". Bạn vui lòng kiểm tra lại" }));
                                    Console.WriteLine("Update Success");
                                }
                            }
                        }
                    }
                }
            }
            GC.Collect();
        }
    }
}
