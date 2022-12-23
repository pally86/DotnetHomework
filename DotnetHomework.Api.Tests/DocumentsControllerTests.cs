using DotnetHomework.Controllers;
using DotnetHomework.Data;
using DotnetHomework.Models;
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
        private Document document;
        [SetUp]
        public void Setup()
        {
            _documentRepository = new Mock<IDocumentsRepository>();
            _documentsController = new DocumentsController(_documentRepository.Object);

            document = new Document();
            //{
            //    Id = "TestId1",
            //    Data = new object[] { new { prop1 = "prop1", prop2 = 1 }, new { prop1 = "prop2", prop2 = 2 } },
            //    Tags = new List<string>() { "a", "b" }
            //};
        }

        [Test]
        public async Task Get_CallRequest_VerifyGetAllInvoked()
        {
            await _documentsController.Get("0");
            _documentRepository.Verify(x => x.GetDocument("0"), Times.Once);
        }

        [Test]
        public async Task Put_CallRequest_VerifyGetAllInvoked()
        {
            await _documentsController.Put(document);
            _documentRepository.Verify(x => x.Add(document, "hdd"), Times.Once);
        }
    }
}
