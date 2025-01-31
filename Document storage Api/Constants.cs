using MessagePack;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Document_storage_Api
{
    public class Constants
    {
        public const string HeaderAccept = "Accept";

        public const string JSON = "json";
        public const string XML = "xml";
        public const string MessagePack = "messagepack";

        public const string ErrorWrongFormat = "Wrong format.";
        public const string ErrorInvalidData = "Invalid data.";
    }
}
