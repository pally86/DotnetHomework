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
            path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UploadFiles");
            dictionary = Path.Combine(path, "dictionary");

            document = new Document()
            {
                Id = "Id1",
                Data = new object[] { new { prop1 = "prop1", prop2 = 1 }, new { prop1 = "prop2", prop2 = 2 } },
                Tags = new List<string>() { "a", "b" }
            };
            _converter = new Mock<IConverter>();
            _storageFactory = new Mock<IStorageFactory>();
            _repository = new DocumentsRepository(_storageFactory.Object, _converter.Object);

        }

        [Test]
        public async Task GetDocument_Document_VerifyIfInvokedConvert()
        {
            _storageFactory.Setup(x => x.GetInstance("hdd"))
                .Returns(new StorageHDD());
            await _repository.Add(document, "hdd");
            //var ex = await _repository.GetDocument("Id1");
            _storageFactory.Verify(x => x.GetInstance("hdd"), Times.Once);
            DeleteFilesAfterTest();
        }

        [Test]
        public async Task GetDocument_Document_ThrowExceptionIfDocumentNotFound()
        {
            _storageFactory.Setup(x => x.GetInstance("hdd"))
                .Returns(new StorageHDD());
            await _repository.Add(document, "hdd");
            var exception = Assert.ThrowsAsync<Exception>(
                 async () => await _repository.GetDocument(""));
            DeleteFilesAfterTest();
            Assert.AreEqual("Document with that ID not found", exception.Message);
            

        }

        [Test]
        public async Task SaveDocument_Document_VerifyIfInvokedStorage()
        {
            await _repository.Add(document, "x");
            _storageFactory.Verify(x => x.GetInstance("x"), Times.Once);
        }

        private void DeleteFilesAfterTest()
        {
            var linesAfterAdd = File.ReadAllLines(dictionary);
            var fileName = linesAfterAdd[linesAfterAdd.Length - 1].Split(':')[1];
            File.Delete(Path.Combine(path, fileName));
            File.WriteAllLines(dictionary, linesAfterAdd.Take(linesAfterAdd.Length - 1).ToArray());
        }
    }
}
