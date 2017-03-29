using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGCSOSCopier.Util
{
    public class Loggers
    {
        private static log4net.ILog _OGCSOSCopier;

        internal static log4net.ILog OGCSOSCopier
        {
            get
            {
                if (_OGCSOSCopier == null)
                    _OGCSOSCopier = log4net.LogManager.GetLogger("OGCSOSCopier");
                return _OGCSOSCopier;
            }
        }
    }
}
