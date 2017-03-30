using Netcad.SWE.Interop.OpenGIS.Ows_11;
using Netcad.SWE.Interop.OpenGIS.Sos_20;
using Netcad.SWE.Interop.OpenGIS.Swes_20;
using OGCSOSCopier.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OGCSOSCopier.RequestHandlers
{
    public class GetCapabilitiesRequestHandler
    {

        //private static string _sosUrl;
        //private static string _version;
        private static CapabilitiesType _capabilitiesResponse = null;

        public GetCapabilitiesRequestHandler()
        {
            _capabilitiesResponse = null;
            Run();
        }

        public CapabilitiesType GetCapabilities(Settings settings)
        {
            if (_capabilitiesResponse == null)
                Run();
            return _capabilitiesResponse;
        }


        public AbstractContentsTypeOffering[] GetOfferings(List<string> includeProcedures = null, List<string> exculudeProcedures = null)
        {
            if (_capabilitiesResponse == null)
                Run();

            List<AbstractContentsTypeOffering> list = new List<AbstractContentsTypeOffering>();
            if (includeProcedures != null && includeProcedures.Count > 0)
            {
                foreach (var item in _capabilitiesResponse.contents.Contents.offering)
                {
                    if (includeProcedures.Contains(item.AbstractOffering.procedure))
                    {
                        list.Add(item);
                    }
                }
            }
            else
            {
                list = _capabilitiesResponse.contents.Contents.offering.ToList();
            }

            if (exculudeProcedures != null && exculudeProcedures.Count > 0)
            {
                foreach (var item in list)
                {
                    if (exculudeProcedures.Contains(item.AbstractOffering.procedure))
                        list.Remove(item);
                }
            }

            return list.ToArray();
        }

        private static void Run()
        {
            var client = new RestClient(OGCSOSCopierConfig.SOURCE_SOS_URL);
            var request = new RestRequest(Method.GET);

            request.AddParameter("service", "SOS");
            request.AddParameter("version", OGCSOSCopierConfig.SOURCE_SOS_VERSION);
            request.AddParameter("request", "GetCapabilities");


            IRestResponse response = client.Execute(request);
            var content = response.Content;

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                XmlSerializer serializer2 = new XmlSerializer(typeof(ExceptionReport));
                ExceptionReport exceptionReport = null;
                using (TextReader reader = new StringReader(content))
                {
                    exceptionReport = (ExceptionReport)serializer2.Deserialize(reader);
                }

                if (exceptionReport != null && exceptionReport.Exception != null && exceptionReport.Exception[0] != null && exceptionReport.Exception[0].ExceptionText[0] != null)
                    throw new Exception(exceptionReport.Exception[0].ExceptionText[0]);
                else
                    throw new Exception();
            }

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Source url returns " + response.StatusCode);

            XmlSerializer serializer = new XmlSerializer(typeof(CapabilitiesType));
            CapabilitiesType capabilitiesResponse = null;

            try
            {
                using (TextReader reader = new StringReader(content))
                {
                    capabilitiesResponse = (CapabilitiesType)serializer.Deserialize(reader);
                }
            }
            catch (Exception exp)
            {
            }

            if (capabilitiesResponse.contents == null ||
                capabilitiesResponse.contents.Contents == null ||
                capabilitiesResponse.contents.Contents.offering == null ||
                capabilitiesResponse.contents.Contents.offering.Length == 0)
                throw new Exception("Offering not found!");

            _capabilitiesResponse = capabilitiesResponse;


        }


    }
}
