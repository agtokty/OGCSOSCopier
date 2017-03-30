using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGCSOSCopier.Util
{
    public class Loggers
    {
        private static log4net.ILog _InsertSensorRequestsLogger;
        private static log4net.ILog _DescribeSensorRequestsLogger;

        internal static log4net.ILog InsertSensorRequestsLogger
        {
            get
            {
                if (_InsertSensorRequestsLogger == null)
                    _InsertSensorRequestsLogger = log4net.LogManager.GetLogger("InsertSensorRequestsLogger");
                return _InsertSensorRequestsLogger;
            }
        }


        internal static log4net.ILog DescribeSensorRequestsLogger
        {
            get
            {
                if (_DescribeSensorRequestsLogger == null)
                    _DescribeSensorRequestsLogger = log4net.LogManager.GetLogger("DescribeSensorRequestsLogger");
                return _DescribeSensorRequestsLogger;
            }
        }

    }
}
