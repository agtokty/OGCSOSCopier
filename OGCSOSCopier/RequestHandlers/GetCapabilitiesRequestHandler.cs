using Netcad.SWE.Interop.OpenGIS.Sos_20;
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

        private static string _sosUrl;
        private static string _version;

        public GetCapabilitiesRequestHandler(string url, string version)
        {
            _sosUrl = url;
            _version = version;
        }

        public CapabilitiesType Run(Settings settings)
        {
            var client = new RestClient(_sosUrl);
            var request = new RestRequest(Method.GET);

            request.AddParameter("service", "SOS");
            request.AddParameter("version", _version);
            request.AddParameter("request", "GetCapabilities");

            try
            {
                IRestResponse response = client.Execute(request);
                var content = response.Content;

                XmlSerializer serializer = new XmlSerializer(typeof(CapabilitiesType));
                CapabilitiesType capabilitiesResponse = null;
                using (TextReader reader = new StringReader(content))
                {
                    capabilitiesResponse = (CapabilitiesType)serializer.Deserialize(reader);
                }

                return capabilitiesResponse;

            }
            catch (Exception exp)
            {
                Debug.WriteLine(exp);
            }

            return null;
        }



    }
}
