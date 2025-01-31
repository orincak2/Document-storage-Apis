using MessagePack;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using System.Collections.Concurrent;
using Document_storage_Api.Classes;

namespace Document_storage_Api
{
    public class InMemoryDocumentStorage : IMyDocumentStorage
    {
        private static Dictionary<string, MyDocument> _dicDocuments = new Dictionary<string, MyDocument>();

        public void Create(MyDocument oDocument)
        {
            _dicDocuments[oDocument.id] = oDocument;
        }

        public MyDocument GetById(string sId)
        {
            _dicDocuments.TryGetValue(sId, out var oDocument);
            return oDocument;
        }

        public void Update(MyDocument oDocument)
        {
            if(_dicDocuments.ContainsKey(oDocument.id))
                _dicDocuments[oDocument.id] = oDocument;
        }
    }
}
