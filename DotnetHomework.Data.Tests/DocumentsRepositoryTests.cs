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

        public DocumentsRepositoryTests()
        {

        }
        [SetUp]
        public void Setup()
        {
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
        public void SaveDocument_Document_CheckIfFileExist()
        { 
        }
    }
}
