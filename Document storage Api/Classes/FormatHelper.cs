using MessagePack;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Document_storage_Api.Classes
{
    public static class FormatHelper
    {
        public static byte[] GetObjectInRightFormat(MyDocument oDokument, string sHeader)
        {
            string[] arrHeaderParts = sHeader.Split('/');
            if (arrHeaderParts.Length != 2)
                return null;

            string sFormat = arrHeaderParts[1];

            switch (sFormat.ToLowerInvariant())
            {
                case "json":
                    return GetJsonObject(oDokument);
                case "xml":
                    return GetXmlObject(oDokument);
                case "msgpack":
                    return GetMessagePackObject(oDokument);
                default:
                    return null;
            }
        }

        #region JSON
        private static byte[] GetJsonObject(MyDocument oDokument)
        {
            return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(oDokument));
        }
        #endregion

        #region XML
        private static byte[] GetXmlObject(MyDocument oDokument)
        {
            return Encoding.UTF8.GetBytes(SerializeToXml(oDokument));
        }

        private static string SerializeToXml<T>(T oDokuent)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StringWriter swTextWriter = new StringWriter())
            {
                xmlSerializer.Serialize(swTextWriter, oDokuent);
                return swTextWriter.ToString();
            }
        }
        #endregion

        #region MessagePack
        private static byte[] GetMessagePackObject(MyDocument oDokument)
        {
            return MessagePackSerializer.Serialize(oDokument);
        }
        #endregion

    }
}
