using DotnetHomework.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetHomework.Utility
{
    [TestFixture]
    public class StorageHDDTests
    {
        private Document document;
        public StorageHDDTests()
        {
            document = new Document()
            {
                Id = "TestId1",
                Data = new object[] { new { prop1 = "prop1", prop2 = 1 }, new { prop1 = "prop2", prop2 = 2 } },
                Tags = new List<string>() { "a", "b" }
            };
        }

        [Test]
        public async Task SaveData_Document_CheckIfFileExist()
        {
            var storage = new StorageHDD();
            await storage.SaveData(document);

            //Assert.That(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UploadFiles");,)
        }
    }
}
