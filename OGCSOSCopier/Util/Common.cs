using Netcad.SWE.Interop.OpenGIS.Ows_11;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace OGCSOSCopier.Util
{
    public class Common
    {

        public static SOSVerisonComboboxItem[] SOSVerisonComboboxItemGenerator()
        {
            List<SOSVerisonComboboxItem> list = new List<SOSVerisonComboboxItem>();
            foreach (var item in Constants.SOS_VERSIONS)
            {
                list.Add(new SOSVerisonComboboxItem(item, item));
            }

            return list.ToArray();
        }

        public static bool IsValidUrl(string url)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return result;
        }


        public static string GetOGCExceptionText(string content)
        {
            string RES = "";
            try
            {
                XmlSerializer serializer1 = new XmlSerializer(typeof(Netcad.SWE.Interop.OpenGIS.Ows_11.ExceptionReport));
                ExceptionReport _OgcException = null;
                using (TextReader reader = new StringReader(content))
                    _OgcException = (ExceptionReport)serializer1.Deserialize(reader);

                if (_OgcException.Exception.Length > 0 && _OgcException.Exception[0].ExceptionText != null && _OgcException.Exception[0].ExceptionText.Length > 0)
                {
                    RES = _OgcException.Exception[0].ExceptionText[0];
                }
            }
            catch
            {
                RES = "";
            }

            return RES;
        }

    }
}
