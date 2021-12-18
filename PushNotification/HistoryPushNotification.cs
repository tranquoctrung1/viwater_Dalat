using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanHoaPushNotification
{
    public class HistoryPushNotification : IDisposable
    {
        private tanhoaEntities context = new tanhoaEntities();

        private bool disposedValue = false;

        public HistoryPushNotification()
        {
        }

        public int Add(t_HistoryPushNotification h)
        {
            int num;
            try
            {
                this.context.t_HistoryPushNotification.Add(h);
                this.context.SaveChanges();
                num = 1;
            }
            catch (Exception exception)
            {
                num = -1;
            }
            return num;
        }

        public bool CheckExists(string siteid, string loggerid, string channelid)
        {
            bool flag;
            flag = (!(
                from x in this.context.t_HistoryPushNotification
                where x.siteid == siteid && x.loggerid == loggerid && x.channelid == channelid
                select x).Any<t_HistoryPushNotification>() ? false : true);
            return flag;
        }

        protected void Dispose(bool isDisposing)
        {
            if (!this.disposedValue)
            {
                if (isDisposing)
                {
                    this.context.Dispose();
                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public t_HistoryPushNotification getHistory(string siteid, string loggerid, string channelid)
        {
            t_HistoryPushNotification tHistoryPushNotification = (
                from x in this.context.t_HistoryPushNotification
                where x.siteid == siteid && x.loggerid == loggerid && x.channelid == channelid
                select x).FirstOrDefault<t_HistoryPushNotification>();
            return tHistoryPushNotification;
        }

        public int Update(t_HistoryPushNotification h)
        {
            int num;
            try
            {
                t_HistoryPushNotification entity = (
                    from x in this.context.t_HistoryPushNotification
                    where x.siteid == h.siteid && x.loggerid == h.loggerid && x.channelid == h.channelid
                    select x).FirstOrDefault<t_HistoryPushNotification>();
                if (entity != null)
                {
                    entity.siteid = h.siteid;
                    entity.@value = h.@value;
                    entity.channelid = h.channelid;
                    entity.loggerid = h.loggerid;
                    entity.type = h.type;
                    entity.timestamp = h.timestamp;
                }
                else
                {
                    this.context.t_HistoryPushNotification.Add(h);
                }
                this.context.SaveChanges();
                num = 1;
            }
            catch (Exception exception)
            {
                num = -1;
            }
            return num;
        }
    }
}
