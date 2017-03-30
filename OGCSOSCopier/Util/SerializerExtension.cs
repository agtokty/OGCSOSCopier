using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace OGCSOSCopier.Util
{
    public static class SerializerExtension
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="toSerialize"></param>
        /// <param name="ns"></param>
        /// <returns></returns>
        public static string SerializeObjectToXmlString<T>(this T toSerialize, XmlSerializerNamespaces ns)
        {
            var stringwriter = new Utf8StringWriter();
            var serializer = new XmlSerializer(toSerialize.GetType());
            serializer.Serialize(stringwriter, toSerialize, ns);
            //serializer.Serialize(stringwriter, toSerialize );
            return stringwriter.ToString();
        }
    }

    public sealed class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding { get { return Encoding.UTF8; } }
    }
}
