using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGCSOSCopier
{
    public class OGCSOSCopierConfig
    {

        private static string _SOURCE_SOS_URL;
        private static string _SOURCE_SOS_VERSION;
        private static string _DEST_SOS_URL;
        private static string _DEST_SOS_VERSION;

        public static string SOURCE_SOS_URL
        {
            get
            {
                return _SOURCE_SOS_URL;
            }
            set
            {
                _SOURCE_SOS_URL = value;
            }
        }

        public static string DEST_SOS_URL
        {
            get
            {
                return _DEST_SOS_URL;
            }
            set
            {
                _DEST_SOS_URL = value;
            }
        }

        public static string DEST_SOS_VERSION
        {
            get
            {
                return _DEST_SOS_VERSION;
            }
            set
            {
                _DEST_SOS_VERSION = value;
            }
        }


        public static string SOURCE_SOS_VERSION
        {
            get
            {
                return _SOURCE_SOS_VERSION;
            }
            set
            {
                _SOURCE_SOS_VERSION = value;
            }
        }

    }
}
