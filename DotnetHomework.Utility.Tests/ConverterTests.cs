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
    public class ConverterTests
    {
        private Converter _converter;
        private Result result;
        [SetUp]
        public void Setup()
        {
            _converter = new Converter();
        }

        [TestCase("xml", ExpectedResult = "application/xml")]
        [TestCase("json", ExpectedResult = "application/json")]
        [TestCase("messagepack", ExpectedResult = "application/x-msgpack")]
        public async Task<string> Convert_Document_GetContentType(string typeFile)
        {
            result = await _converter.Convert("", typeFile);
            return result.ContentType;
        }
        

    }
}
