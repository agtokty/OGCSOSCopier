using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace OGCSOSCopier.Util
{
    public class ObjectSerializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static XmlElement SerializeToXmlElement(object o)
        {
            XmlDocument doc = new XmlDocument();

            using (XmlWriter writer = doc.CreateNavigator().AppendChild())
            {
                new XmlSerializer(o.GetType()).Serialize(writer, o);
            }

            return doc.DocumentElement;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <param name="namespaces"></param>
        /// <returns></returns>
        public static XmlElement SerializeToXmlElement(object o, XmlSerializerNamespaces namespaces)
        {
            XmlDocument doc = new XmlDocument();

            using (XmlWriter writer = doc.CreateNavigator().AppendChild())
            {
                new XmlSerializer(o.GetType()).Serialize(writer, o, namespaces);
            }

            return doc.DocumentElement;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        public static T DeserializeFromXmlElement<T>(XmlElement element)
        {
            var serializer = new XmlSerializer(typeof(T));

            return (T)serializer.Deserialize(new XmlNodeReader(element));
        }


        /// <summary>
        /// 
        /// </summary>
        public static T ConvertRequest<T>(XmlElement xmlRequest) where T : class
        {
            string xml = xmlRequest.OuterXml;
            StringReader reader = new StringReader(xml);

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            T typeInstance;
            try
            {
                typeInstance = (T)serializer.Deserialize(reader);
            }
            catch (Exception exp)
            {
                string hata = "ValidateRequest HATA";
                throw new Exception(hata);
            }
            return typeInstance;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        public static T DeserializeFromXmlNode<T>(XmlNode element)
        {
            var serializer = new XmlSerializer(typeof(T));

            return (T)serializer.Deserialize(new XmlNodeReader(element));
        }


        /// <summary>
        /// http://stackoverflow.com/questions/1123718/format-xml-string-to-print-friendly-xml-string
        /// </summary>
        /// <param name="XML"></param>
        /// <returns></returns>
        public static string PrintXML(string XML)
        {
            string Result = "";

            MemoryStream mStream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(mStream, Encoding.Unicode);
            XmlDocument document = new XmlDocument();

            try
            {
                // Load the XmlDocument with the XML.
                document.LoadXml(XML);

                writer.Formatting = Formatting.Indented;

                // Write the XML into a formatting XmlTextWriter
                document.WriteContentTo(writer);
                writer.Flush();
                mStream.Flush();

                // Have to rewind the MemoryStream in order to read
                // its contents.
                mStream.Position = 0;

                // Read MemoryStream contents into a StreamReader.
                StreamReader sReader = new StreamReader(mStream);

                // Extract the text from the StreamReader.
                string FormattedXML = sReader.ReadToEnd();

                Result = FormattedXML;
            }
            catch (XmlException)
            {
            }

            mStream.Close();
            writer.Close();

            return Result;
        }

    }
}
