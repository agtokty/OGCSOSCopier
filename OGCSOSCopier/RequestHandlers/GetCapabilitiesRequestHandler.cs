using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OGCSOSCopier.RequestHandlers
{
    public class GetCapabilitiesRequestHandler
    {

        private static string _sosUrl;
        private static string _version;

        public GetCapabilitiesRequestHandler(string sosUrl, string version)
        {
            _sosUrl = sosUrl;
            _version = version;
        }

        public void Run()
        {

            var client = new RestClient(_sosUrl);
            var request = new RestRequest(Method.GET);

            request.AddParameter("service", "SOS");
            request.AddParameter("version", _version);
            request.AddParameter("request", "GetCapabilities");

            IRestResponse response = client.Execute(request);
            var content = response.Content;
        }

 

    }
}
