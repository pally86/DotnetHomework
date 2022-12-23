using DotnetHomework.Models;
using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DotnetHomework.Utility
{
    public class Converter : IConverter
    {
        public async Task<Result> Convert(string data, string fileType)
        {
            Result convertRes = new Result();
            switch (fileType)
            {
                case "json":
                    convertRes.Data = data;
                    convertRes.ContentType = "application/json";
                    break;
                case "xml":
                    convertRes.Data = await GetDocumentXML(data);
                    convertRes.ContentType = "application/xml";
                    break;
                case "messagepack":
                    convertRes.Data = await GetDocumentMessagePack(data);
                    convertRes.ContentType = "application/x-msgpack";
                    break;

            }
            return convertRes;
        }
        private async Task<string> GetDocumentXML(string data)
        {
            XNode node = JsonConvert.DeserializeXNode(data, "Root");
            return node.ToString();
        }
        private async Task<string> GetDocumentMessagePack(string data)
        {
            var bytes = MessagePackSerializer.Serialize(data);
            var result = MessagePackSerializer.Deserialize<string>(bytes);
            return result;
        }
            
    }
}
