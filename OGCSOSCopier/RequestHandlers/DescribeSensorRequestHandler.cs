using Netcad.SWE.Interop.OpenGIS.Swes_20;
using RestSharp;
using System;
using System.IO;
using System.Xml.Serialization;

namespace OGCSOSCopier.RequestHandlers
{
    public class DescribeSensorRequestHandler
    {

        public static string Procedure;
        public static string Format;

        public DescribeSensorRequestHandler(string procedure, string format)
        {
            Procedure = procedure;
            Format = format;
        }

        public DescribeSensorRequestHandler(string procedure)
        {
            Procedure = procedure;
        }

        public DescribeSensorResponseType Run()
        {
            if (string.IsNullOrEmpty(Format))
                Format = "http://www.opengis.net/sensorml/2.0";

            var client = new RestClient(OGCSOSCopierConfig.SOURCE_SOS_URL);
            var request = new RestRequest(Method.GET);

            request.Parameters.Clear();
            request.AddParameter("service", "SOS");
            request.AddParameter("version", OGCSOSCopierConfig.SOURCE_SOS_VERSION);
            request.AddParameter("request", "DescribeSensor");
            request.AddParameter("procedure", Procedure);
            request.AddParameter("procedureDescriptionFormat", Format);
            request.AddHeader("Accept", "application/xml");

            IRestResponse response = client.Execute(request);
            var content = response.Content;

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var err = Util.Common.GetOGCExceptionText(content);
                throw new Exception("BadRequest " + err);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var err = Util.Common.GetOGCExceptionText(content);
                throw new Exception("Unauthorized " + err);
            }

            XmlSerializer serializer = new XmlSerializer(typeof(DescribeSensorResponseType));
            DescribeSensorResponseType describeSensorResponse = null;
            using (TextReader reader = new StringReader(content))
                describeSensorResponse = (DescribeSensorResponseType)serializer.Deserialize(reader);

            Util.Loggers.DescribeSensorRequestsLogger.Debug(Util.SerializerExtension.SerializeObjectToXmlString(describeSensorResponse));


            return describeSensorResponse;
        }



    }
}
