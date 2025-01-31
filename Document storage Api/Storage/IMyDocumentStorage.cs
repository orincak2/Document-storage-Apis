using Document_storage_Api.Classes;
using MessagePack;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Document_storage_Api
{
    public interface IMyDocumentStorage
    {
        void Create(MyDocument document);
        MyDocument GetById(string id);
        void Update(MyDocument document);
    }
}
