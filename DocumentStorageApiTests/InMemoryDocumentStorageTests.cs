using Xunit;
using FluentAssertions;
using Document_storage_Api;
using Document_storage_Api.Classes;

public class InMemoryDocumentStorageTests
{
    private readonly InMemoryDocumentStorage _storage = new InMemoryDocumentStorage();

    [Fact]
    public void CreateDocument()
    {
        MyDocument oDocument = new MyDocument
        {
            id = "123",
            tags = new List<string> { "test" },
            data = new Dictionary<string, string> { { "key1", "value1" } }
        };
        _storage.Create(oDocument);

        MyDocument retrieved = _storage.GetById("123");

        retrieved.Should().NotBeNull();
        retrieved.id.Should().Be("123");
        retrieved.data.Should().ContainKey("key1").WhoseValue.Should().Be("value1");
    }

    [Fact]
    public void GetByIdNonExisting()
    {
        MyDocument oDocument = _storage.GetById("wrongID");
        oDocument.Should().BeNull();
    }

    [Fact]
    public void UpdateDocument()
    {
        MyDocument oDocument = new MyDocument
        {
            id = "123",
            tags = new List<string> { "test" },
            data = new Dictionary<string, string> { { "key1", "value1" } }
        };
        _storage.Create(oDocument);

        MyDocument retrieved = _storage.GetById("123");
        retrieved.id.Should().Be("123");
        retrieved.tags.Should().Contain("test");
        retrieved.data.Should().ContainKey("key1").WhoseValue.Should().Be("value1");

        MyDocument updatedDocument = new MyDocument
        {
            id = "123",
            tags = new List<string> { "new" },
            data = new Dictionary<string, string> { { "key1", "newValue" } }
        };
        _storage.Update(updatedDocument);

        retrieved = _storage.GetById("123");
        retrieved.id.Should().Be("123");
        retrieved.tags.Should().NotContain("test");
        retrieved.tags.Should().Contain("new");
        retrieved.data.Should().ContainKey("key1").WhoseValue.Should().Be("newValue");
    }

    [Fact]
    public void UpdateNonExistingDocument()
    {
        MyDocument retrieved = _storage.GetById("333");
        retrieved.Should().BeNull();

        MyDocument updatedDocument = new MyDocument
        {
            id = "333",
            tags = new List<string> { "new" },
            data = new Dictionary<string, string> { { "key1", "newValue" } }
        };
        _storage.Update(updatedDocument);

        retrieved = _storage.GetById("333");
        retrieved.Should().BeNull();
    }
}
