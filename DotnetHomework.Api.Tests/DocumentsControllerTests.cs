using DotnetHomework.Controllers;
using DotnetHomework.Data;
using DotnetHomework.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetHomework.Api
{
    
    [TestFixture]
    public class DocumentsControllerTests
    {
        private Mock<IDocumentsRepository> _documentRepository;
        private DocumentsController _documentsController;
        private Document _document;

        [SetUp]
        public void Setup()
        {
            _documentRepository = new Mock<IDocumentsRepository>();
            _documentsController = new DocumentsController(_documentRepository.Object);

            _document = new Document()
            {
                Id = "TestId1",
                Data = new object[] { new { prop1 = "prop1", prop2 = 1 }, new { prop3 = "prop3", prop4 = 4 } },
                Tags = new List<string>() { "a", "b" }
            };
        }

        [Test]
        public async Task Get_CallRequest_VerifyGetAllInvoked()
        {            
            await _documentsController.Get("0");
            _documentRepository.Verify(x => x.GetDocument("0"), Times.Once);
        }

        [Test]
        public async Task Put_CallRequest_VerifyRepoAddInvoked()
        {
            await _documentsController.Put(_document);
            _documentRepository.Verify(x => x.Add(_document, "hdd"), Times.Once);
        }

        [Test]
        public async Task Post_CallRequest_VerifyRepoAddInvoked()
        {
            await _documentsController.Post(_document, "");
            _documentRepository.Verify(x => x.Add(_document, ""), Times.Once);
        }

        [Test]
        public async Task Put_AddNullDocument_CheckIfExceptionThrown()
        {
            BadRequestObjectResult request = await _documentsController.Put(null) as BadRequestObjectResult;
            
            Assert.That(request.Value.Equals("Object reference not set to an instance of an object."));
            Assert.That(request.StatusCode.Equals(400));
        }

        [Test]
        public async Task Post_AddNullDocument_CheckIfExceptionThrown()
        {
            BadRequestObjectResult request = await _documentsController.Post(null, "") as BadRequestObjectResult;

            Assert.That(request.Value.Equals("Object reference not set to an instance of an object."));
            Assert.That(request.StatusCode.Equals(400));
        }

        [Test]
        public async Task Get_GetDocument_CheckIfResultIsOK200()
        {
            _documentRepository.Setup(x => x.GetDocument(It.IsAny<string>())).ReturnsAsync(new Document());
            var result = _documentsController.Get("");
            Assert.That(result.Result.GetType().Equals(typeof(OkObjectResult)));                       
        }
    }
}
