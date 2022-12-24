using DotnetHomework.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotnetHomework.Utility
{
    [TestFixture]
    public class StorageHDDTests
    {
        private Document document1;
        private Document document2;
        private string path;
        private string dictionary;
        private IStorage _storage;
        
        [SetUp]
        public void Setup()
        {
            path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UploadFiles");
            dictionary = Path.Combine(path, "dictionary");
            _storage = new StorageHDD();

            document1 = new Document()
            {
                Id = "TestId1",
                Data = new object[] { new { prop1 = "prop1", prop2 = 1 }, new { prop3 = "prop3", prop4 = 4 } },
                Tags = new List<string>() { "a", "b" }
            };
            document2 = new Document()
            {
                Id = "TestId1",
                Data = new object[] { new { prop1 = "prop5", prop2 = 6 }, new { prop1 = "prop7", prop2 = 8 } },
                Tags = new List<string>() { "c", "d" }
            };
        }

        [Test]
        public async Task SaveData_Document_CheckIfCreateFileAndWriteToDictionary()
        {
            int countFiles = Directory.GetFiles(path).Count();
            string[] lines;
            int linesLength = 0;
            if (new FileInfo(dictionary).Length != 0)
            {
                lines = File.ReadAllLines(dictionary);
                linesLength = lines.Length;
            } 

            await _storage.SaveData(document1);
            
            int countFilesAfterAdd = Directory.GetFiles(path).Count();            
            var linesAfterAdd = File.ReadAllLines(dictionary);
            
            Assert.That((countFiles + 1).Equals(countFilesAfterAdd));
            Assert.That((linesLength + 1).Equals(linesAfterAdd.Length));

            DeleteFilesAfterTest();
        }

        [Test]
        public async Task GetData_Document_CheckIfGetFile()
        {
            await _storage.SaveData(document1);

            var file = await _storage.GetData("TestId1");
            var jsonString = JsonSerializer.Serialize(document1);

            Assert.AreEqual(file, jsonString);
            DeleteFilesAfterTest();
        }

        [Test]
        public async Task SaveData_Document_AddSameIdCheckThrownException()
        {
            await _storage.SaveData(document1);

            var exception = Assert.ThrowsAsync<Exception>(
                async () => await _storage.SaveData(document2));
           
            Assert.AreEqual("You must use other ID", exception.Message);
            
            DeleteFilesAfterTest();
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
