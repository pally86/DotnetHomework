using DotnetHomework.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotnetHomework.Utility
{
    [TestFixture]
    public class StorageHDDTests
    {
        private Document _document1;
        private Document _document2;
        private string _path;
        private string _dictionary;
        private IStorage _storage;
        private Stopwatch _stopwatch;
        
        [SetUp]
        public void Setup()
        {
            //path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UploadFiles");
            _path = "UploadFiles";
            _dictionary = Path.Combine(_path, "dictionary") ;
            _storage = new StorageHDD();
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            //program caka kym sa vytvori priecinok a subor.. inak vypisuje chybu ze su locknute
            // alebo ze ich vyuziva iny proces... ked sa nevytvori do 4s, tak vyhodi chybu
            while (TestHelper.IsFileLocked(new FileInfo(_dictionary), _path, _dictionary)) 
            {
                if (_stopwatch.ElapsedMilliseconds > 4000)
                    throw new Exception("Problem with creating directory or file!");
            }
            
            _document1 = new Document()
            {
                Id = "TestId1",
                Data = new object[] { new { prop1 = "prop1", prop2 = 1 }, new { prop3 = "prop3", prop4 = 4 } },
                Tags = new List<string>() { "a", "b" }
            };
            _document2 = new Document()
            {
                Id = "TestId1",
                Data = new object[] { new { prop1 = "prop5", prop2 = 6 }, new { prop1 = "prop7", prop2 = 8 } },
                Tags = new List<string>() { "c", "d" }
            };
        }
       
        [Test]
        public async Task SaveData_SaveDocument_CheckIfCreateFileAndWriteToDictionary()
        {
            int countFiles = Directory.GetFiles(_path).Count();
            string[] lines;
            int linesLength = 0;
            if (new FileInfo(_dictionary).Length != 0)
            {
                lines = File.ReadAllLines(_dictionary);
                linesLength = lines.Length;
            }

            await _storage.SaveData(_document1);

            int countFilesAfterAdd = Directory.GetFiles(_path).Count();
            var linesAfterAdd = File.ReadAllLines(_dictionary);

            Assert.That((countFiles + 1).Equals(countFilesAfterAdd));
            Assert.That((linesLength + 1).Equals(linesAfterAdd.Length));

            TestHelper.DeleteFilesAfterTest(_path);
        }

        [Test]
        public async Task GetData_GetFile_CheckIfGetCorrectFile()
        {
            await _storage.SaveData(_document1);

            var file = await _storage.GetData("TestId1");
            var jsonString = JsonSerializer.Serialize(_document1);

            Assert.AreEqual(file, jsonString);
            TestHelper.DeleteFilesAfterTest(_path);
        }

        [Test]
        public async Task SaveData_AddSameId_CheckThrownException()
        {            
            await _storage.SaveData(_document1);

            var exception = Assert.ThrowsAsync<Exception>(
                async () => await _storage.SaveData(_document2));

            Assert.AreEqual("You must use other ID", exception.Message);

            TestHelper.DeleteFilesAfterTest(_path);
        }
    }
}
