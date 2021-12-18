using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanHoaPushNotification
{
    public class ChannelConfiguration : IDisposable
    {
        private tanhoaEntities context = new tanhoaEntities();

        private bool disposedValue = false;

        public ChannelConfiguration()
        {
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

        public List<t_Channel_Configurations> GetAlarmChannelByLoggerID(string loggerID)
        {
            List<t_Channel_Configurations> list = (
                from c in this.context.t_Channel_Configurations
                orderby c.ChannelName
                where c.LoggerId == loggerID
                select c).ToList<t_Channel_Configurations>();
            return list;
        }
    }
}
