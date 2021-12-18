using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanHoaPushNotification
{
    public class Site : IDisposable
    {
        private tanhoaEntities context = new tanhoaEntities();

        private bool disposedValue = false;

        public Site()
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

        public List<t_Sites> GetSites()
        {
            List<t_Sites> list = (
                from x in this.context.t_Sites
                orderby x.SiteId
                select x).ToList<t_Sites>();
            return list;
        }

        public List<t_Sites> GetSitesCLN()
        {
            List<t_Sites> list = (
                from i in this.context.t_Sites
                where i.DisplayGroup == "Quan_Trac"
                select i into x
                orderby x.SiteId
                select x).ToList<t_Sites>();
            return list;
        }
    }
}
