using Xunit;
using FluentAssertions;
using System.Text;
using Document_storage_Api.Classes;
using Document_storage_Api;

public class FormatHelperTests
{
    private readonly InMemoryDocumentStorage _storage = new InMemoryDocumentStorage();

    [Fact]
    public void GetObjectInRightFormatJson()
    {
        MyDocument oDocument = new MyDocument
        {
            id = "987",
            tags = new List<string> { "test" },
            data = new Dictionary<string, string> { { "key1", "value1" } }
        };
        _storage.Create(oDocument);
        MyDocument retrieved = _storage.GetById("987");

        retrieved.Should().NotBeNull();
        retrieved.id.Should().Be("987");

        byte[] jsonBytes = FormatHelper.GetObjectInRightFormat(retrieved, "application/json");
        string jsonString = Encoding.UTF8.GetString(jsonBytes);

        jsonString.Should().Contain("987");
        jsonString.Should().Contain("test");
        jsonString.Should().NotContain("555");
    }

    [Fact]
    public void GetObjectInRightFormatXml()
    {
        MyDocument oDocument = new MyDocument
        {
            id = "987",
            tags = new List<string> { "test" },
            data = new Dictionary<string, string> { { "key1", "value1" } }
        };
        _storage.Create(oDocument);
        MyDocument retrieved = _storage.GetById("987");

        byte[] xmlBytes = FormatHelper.GetObjectInRightFormat(retrieved, "application/xml");
        string xmlString = Encoding.UTF8.GetString(xmlBytes);

        xmlString.Should().Contain("<MyDocument");
        xmlString.Should().Contain("<id>987</id>");
    }

}
