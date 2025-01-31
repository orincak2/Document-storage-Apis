using Moq;
using Microsoft.AspNetCore.Mvc;
using Document_storage_Api;
using FluentAssertions;
using MyDocumentStorageApi.Controllers;
using Document_storage_Api.Classes;

public class DocumentsControllerTests
{
    private readonly Mock<IMyDocumentStorage> _mockRepo;
    private readonly DocumentsController _controller;

    public DocumentsControllerTests()
    {
        _mockRepo = new Mock<IMyDocumentStorage>();
        _controller = new DocumentsController(_mockRepo.Object);
    }

    [Fact]
    public void GetDocumentNonExisting()
    {
        _mockRepo.Setup(repo => repo.GetById(It.IsAny<string>())).Returns((MyDocument)null);
        IActionResult result = _controller.GetDocument("nonExisting", "application/json");

        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public void GetDocumentExist()
    {
        MyDocument oDocumnet = new MyDocument { id = "777", tags = new List<string> { "mock" }, data = new Dictionary<string, string> { { "key1", "value1" } } };
        _mockRepo.Setup(repo => repo.GetById("777")).Returns(oDocumnet);

        IActionResult result = _controller.GetDocument("test", "application/json");

        result.Should().NotBeNull();
    }

    [Fact]
    public void CreateDocumentNull()
    {
        IActionResult result = _controller.CreateDocument(null);
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public void UpdateDocumentExist()
    {
        MyDocument oDocument = new MyDocument
        {
            id = "123",
            tags = new List<string> { "test" },
            data = new Dictionary<string, string> { { "key1", "value1" } }
        };

        var updatedDocument = new MyDocument
        {
            id = "123",
            tags = new List<string> { "update" },
            data = new Dictionary<string, string> { { "key1", "new-value" } }
        };

        _mockRepo.Setup(repo => repo.GetById("123")).Returns(oDocument);

        IActionResult result = _controller.UpdateMyDocument("123", updatedDocument);

        result.Should().BeOfType<OkObjectResult>();
    }
}
