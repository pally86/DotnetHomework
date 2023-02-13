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
    public class ConverterTests
    {
        private Document _document1;
        private Converter _converter;
        private Result _result;
        [SetUp]
        public void Setup()
        {
            _document1 = new Document()
            {
                Id = "TestId1",
                Data = new object[] { new { prop1 = "prop1", prop2 = 1 }, new { prop3 = "prop3", prop4 = 4 } },
                Tags = new List<string>() { "a", "b" }
            };
            _converter = new Converter();
        }

        [TestCase("xml", ExpectedResult = "application/xml")]
        [TestCase("json", ExpectedResult = "application/json")]
        [TestCase("messagepack", ExpectedResult = "application/x-msgpack")]
        public async Task<string> Convert_Document_GetContentType(string typeFile)
        {
            var jsonString = JsonSerializer.Serialize(_document1);
            _result = await _converter.Convert(jsonString, typeFile);
            return _result.ContentType;
        }       
    }
}
