using System;
using System.IO;
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

    }
}
