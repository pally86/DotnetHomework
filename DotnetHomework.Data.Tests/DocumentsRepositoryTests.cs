using DotnetHomework.Models;
using DotnetHomework.Utility;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetHomework.Data
{
    [TestFixture]
    public class DocumentsRepositoryTests
    {
        private Document document;
        private Mock<IStorageFactory> _storageFactory;
        private Mock<IConverter> _converter;
        private DocumentsRepository _repository;

        private string path;
        private string dictionary;
                    
        [SetUp]
        public void Setup()
        {
            //path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UploadFiles");
            path = "UploadFiles";
            dictionary = Path.Combine(path, "dictionary");
            
            document = new Document()
            {
                Id = "Id1",
                Data = new object[] { new { prop1 = "prop1", prop2 = 1 }, new { prop3 = "prop3", prop4 = 4 } },
                Tags = new List<string>() { "a", "b" }
            };
            _converter = new Mock<IConverter>();
            _storageFactory = new Mock<IStorageFactory>();
            _repository = new DocumentsRepository(_storageFactory.Object, _converter.Object);
            _storageFactory.Setup(x => x.GetInstance("hdd"))
                .Returns(new StorageHDD());

            while (TestHelper.IsFileLocked(new FileInfo(dictionary), path, dictionary)) { }
        }

        [Test]
        public async Task GetDocument_InvokeMethod_VerifyIfInvokedConvert()
        {            
            await _repository.Add(document, "hdd");
            _storageFactory.Verify(x => x.GetInstance("hdd"), Times.Once);
            TestHelper.DeleteFilesAfterTest(path);
        }

        [Test]
        public async Task GetDocument_EmptyIdRequest_ThrowExceptionIfDocumentNotFound()
        {
            await _repository.Add(document, "hdd");
            var exception = Assert.ThrowsAsync<Exception>(
                 async () => await _repository.GetDocument(""));
            Assert.AreEqual("Document with that ID not found", exception.Message);
            TestHelper.DeleteFilesAfterTest(path);
        }

        [Test]
        public async Task SaveDocument_InvokeMethod_VerifyIfInvokedStorage()
        {
            await _repository.Add(document, "x");
            _storageFactory.Verify(x => x.GetInstance("x"), Times.Once);
            TestHelper.DeleteFilesAfterTest(path);
        }
    }
}
