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
        private Mock<IStorage> _storage;

        public DocumentsRepositoryTests()
        {
            document = new Document()
            {
                Id = "Id1",
                Data = new object[] { new {prop1 = "prop1", prop2 = 1 }, new { prop1 = "prop2", prop2 = 2 } },
                Tags = new List<string>() { "a", "b" }
            };
        }
        [SetUp]
        public void Setup()
        {
            _storage = new Mock<IStorage>();
        }

        [Test]
        public void SaveDocument_Document_CheckIfFileExist()
        {
            Assert.Equals(1,2);
            //var repository = new DocumentsRepository(_storage.Object);
        }
    }
}
