using Netcad.SWE.Interop.OpenGIS.Ows_11;
using Netcad.SWE.Interop.OpenGIS.SensorML20;
using Netcad.SWE.Interop.OpenGIS.Sos_20;
using Netcad.SWE.Interop.OpenGIS.Swes_20;
using OGCSOSCopier.Util;
using RestSharp;
using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace OGCSOSCopier.RequestHandlers
{
    public class InsertSensorRequestHandler
    {

        private DescribeSensorResponseType DescribeSensorResponse;
        private string[] ObservedProperties;
        private string[] ObservationTypes;
        private string[] FeatureOfInterestType;

        public InsertSensorRequestHandler(DescribeSensorResponseType describeSensorResponse,
            string[] observedProperties, string[] observationTypes, string[] featureOfInterests)
        {
            DescribeSensorResponse = describeSensorResponse;
            ObservedProperties = observedProperties;
            ObservationTypes = observationTypes;
            FeatureOfInterestType = featureOfInterests;
        }

        public InsertSensorRequestHandler(DescribeSensorResponseType describeSensorResponse, ObservationOfferingType abstractOffering)
        {
            DescribeSensorResponse = describeSensorResponse;
            ObservedProperties = abstractOffering.observableProperty;
            ObservationTypes = abstractOffering.observationType;
            FeatureOfInterestType = abstractOffering.featureOfInterestType;
        }

        public Tuple<bool, string> Run()
        {
            Tuple<bool, string> result = new Tuple<bool, string>(false, "");
            string identifier = "";
            bool res = false;

            if (DescribeSensorResponse == null || DescribeSensorResponse.description == null ||
                DescribeSensorResponse.description.Length == 0 ||
                DescribeSensorResponse.description[0] == null ||
                DescribeSensorResponse.description[0].SensorDescription.data == null)
                return result;

            var descirption = DescribeSensorResponse.description[0].SensorDescription.data;

            try
            {
                InsertSensorType insertSensorRequest = new InsertSensorType();
                insertSensorRequest.observableProperty = ObservedProperties;
                insertSensorRequest.service = "SOS";
                insertSensorRequest.version = OGCSOSCopierConfig.DEST_SOS_VERSION;
                insertSensorRequest.procedureDescriptionFormat = DescribeSensorResponse.procedureDescriptionFormat;

                InsertSensorTypeMetadata meta = new InsertSensorTypeMetadata();
                meta.InsertionMetadata = new SosInsertionMetadataType()
                {
                    observationType = ObservationTypes,
                    featureOfInterestType = FeatureOfInterestType,
                };

                insertSensorRequest.metadata = new[] { meta };
                //insertSensorRequest.relatedFeature = 
                //insertSensorRequest.extension = 

                if (descirption.Name == "sml:PhysicalSystem" || descirption.Name == "PhysicalSystem")
                {
                    PhysicalSystemType PhysicalSystem = ObjectSerializer.DeserializeFromXmlElement<PhysicalSystemType>(descirption);
                    identifier = PhysicalSystem.identifier.Value;

                    insertSensorRequest.procedureDescription = ObjectSerializer.SerializeToXmlElement(PhysicalSystem, getNameSpacesForSensorML());
                }
                else if (descirption.Name == "sml:PhysicalComponent" || descirption.Name == "PhysicalComponent")
                {
                    PhysicalComponentType PhysicalComponent = ObjectSerializer.DeserializeFromXmlElement<PhysicalComponentType>(descirption);
                    identifier = PhysicalComponent.identifier.Value;

                    insertSensorRequest.procedureDescription = ObjectSerializer.SerializeToXmlElement(PhysicalComponent, getNameSpacesForSensorML());
                }

                var reqcontent = insertSensorRequest.SerializeObjectToXmlString(getNameSpacesForSos());


                var client = new RestClient(OGCSOSCopierConfig.DEST_SOS_URL);
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", "asdasdasdl");
                request.AddParameter("application/xml", reqcontent, ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);
                var content = response.Content;

                Util.Loggers.InsertSensorRequestsLogger.Debug(reqcontent + "\n RESPONSE : \n" + Util.ObjectSerializer.PrintXML(content));

                InsertSensorResponseType insertSensorResponse = new InsertSensorResponseType();

                XmlSerializer serializer = new XmlSerializer(typeof(InsertSensorResponseType));
                object resultXml = null;
                using (TextReader reader = new StringReader(content))
                {
                    resultXml = serializer.Deserialize(reader);
                }

                if (resultXml != null)
                {
                    if (resultXml is InsertSensorResponseType)
                    {
                        insertSensorResponse = (InsertSensorResponseType)resultXml;
                        res = true;
                    }
                    //else if (resultXml is ExceptionReport)
                    //{
                    //    ExceptionReport expReport = (ExceptionReport)resultXml;
                    //}
                }

                result = new Tuple<bool, string>(res, identifier);
            }
            catch (Exception exp)
            {
                Debug.WriteLine(exp);
                result = new Tuple<bool, string>(false, identifier);
            }

            return result;
        }


        public static XmlSerializerNamespaces getNameSpacesForSos()
        {
            XmlSerializerNamespaces _nameSpaces = new XmlSerializerNamespaces();
            _nameSpaces.Add("ogc", "http://www.opengis.net/ogc");
            _nameSpaces.Add("sos", "http://www.opengis.net/sos/2.0");
            _nameSpaces.Add("swes", "http://www.opengis.net/swes/2.0");
            _nameSpaces.Add("swe", "http://www.opengis.net/swe/2.0");
            _nameSpaces.Add("gmd", "http://www.isotc211.org/2005/gmd");
            _nameSpaces.Add("gco", "http://www.isotc211.org/2005/gco");
            _nameSpaces.Add("sml", "http://www.opengis.net/sensorml/2.0");
            _nameSpaces.Add("gml", "http://www.opengis.net/gml/3.2");
            _nameSpaces.Add("xlink", "http://www.w3.org/1999/xlink");
            _nameSpaces.Add("om", "http://www.opengis.net/om/2.0");
            _nameSpaces.Add("ows", "http://www.opengis.net/ows/1.1");
            _nameSpaces.Add("wsa", "http://www.w3.org/2005/08/addressing");
            _nameSpaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            _nameSpaces.Add("fes", "http://www.opengis.net/fes/2.0");
            _nameSpaces.Add("xs", "http://www.w3.org/2001/XMLSchema");
            //_nameSpaces.Add("xsi:schemaLocation", "http://www.opengis.net/fes/2.0 http://schemas.opengis.net/filter/2.0/filterAll.xsd http://www.opengis.net/sos/2.0 http://schemas.opengis.net/sos/2.0/sosGetCapabilities.xsd http://www.opengis.net/swes/2.0 http://schemas.opengis.net/swes/2.0/swes.xsd http://www.opengis.net/ows/1.1 http://schemas.opengis.net/ows/1.1.0/owsAll.xsd http://www.opengis.net/gml/3.2 http://schemas.opengis.net/gml/3.2.1/gml.xsd");

            return _nameSpaces;
        }


        private static XmlSerializerNamespaces getNameSpacesForSensorML()
        {
            XmlSerializerNamespaces _nameSpaces = new XmlSerializerNamespaces();
            _nameSpaces.Add("sml", "http://www.opengis.net/sensorml/2.0");
            _nameSpaces.Add("swe", "http://www.opengis.net/swe/2.0");
            _nameSpaces.Add("gml", "http://www.opengis.net/gml/3.2");
            _nameSpaces.Add("gmd", "http://www.isotc211.org/2005/gmd");
            _nameSpaces.Add("gco", "http://www.isotc211.org/2005/gco");
            _nameSpaces.Add("xlink", "http://www.w3.org/1999/xlink");
            _nameSpaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            return _nameSpaces;
        }
    }


}
