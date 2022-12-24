using DotnetHomework.Models;
using DotnetHomework.Utility;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;
using System.Xml.Linq;

namespace DotnetHomework.Data
{
    public class DocumentsRepository : IDocumentsRepository
    {
        private readonly IStorageFactory _storageFactory;
        private readonly IConverter _converter;
        
        public DocumentsRepository(IStorageFactory storageFactory, IConverter converter)
        {
            _storageFactory = storageFactory;
            _converter = converter;
        }
      

        public async Task Add(Document document, string storageType)
        {
            await _storageFactory.GetInstance(storageType).SaveData(document);
        }


        public async Task<Document> GetDocument(string id)
        {
            string data = await _storageFactory.GetInstance("hdd").GetData(id);

            byte[] byteArray = Encoding.ASCII.GetBytes(data);
            using
                MemoryStream stream = new MemoryStream(byteArray);
            Document? document = await System.Text.Json.JsonSerializer.DeserializeAsync<Document>(stream);
            return document;
        }

        public async Task<Result> GetDocument(string id, string storageType, string fileType)
        {
            string data = "";
            Result result = new Result();

            try
            {
                data = await _storageFactory.GetInstance(storageType).GetData(id);
                result = await _converter.Convert(data, fileType);
                result.StatusCode = StatusCodes.Status201Created;
            }
            catch(Exception e)
            {
                result.Data = "";
                result.ContentType = "";
                result.StatusCode = StatusCodes.Status500InternalServerError;
                result.ErrorMessage = e.Message;
            }
            return result;         
        }
    }
}
