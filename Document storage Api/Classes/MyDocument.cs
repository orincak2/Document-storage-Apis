using MessagePack;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Document_storage_Api.Classes
{
    [MessagePackObject]
    public class MyDocument
    {
        [Key(0)]
        public string id { get; set; }

        [Key(1)]
        public List<string> tags { get; set; }

        [XmlIgnore]
        [Key(2)]
        public Dictionary<string, string> data { get; set; }

        [IgnoreMember]
        [JsonIgnore]
        [XmlElement("data")]
        public List<Item> xmlData
        {
            get
            {
                if (data == null) return null;
                List<Item> xmlData = new List<Item>();
                foreach (var kv in data)
                {
                    xmlData.Add(new Item { key = kv.Key, value = kv.Value });
                }
                return xmlData;
            }
        }
    }

    public class Item
    {
        public string key { get; set; }
        public string value { get; set; }
    }
}
