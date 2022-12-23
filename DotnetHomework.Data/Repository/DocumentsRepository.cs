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
        
        public DocumentsRepository(IStorageFactory storageFactory)
        {
            _storageFactory = storageFactory;
        }
        public async Task Add(Document document)
        {
            await _storageFactory.GetInstance("hdd").SaveData(document);
        }

        public async Task Add(Document document, string storageType)
        {
            await _storageFactory.GetInstance(storageType).SaveData(document);
            
            
            //switch(storageType)
            //{
            //    case "cloud": await SaveToCloud(document); break;
            //    case "hdd": await SaveToHDD(document); break;
            //    case "inmemory": await SaveToInMemory(document); break;
            //}
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
                

                switch (fileType)
                {
                    case "json":
                        result.Data = data;
                        result.ContentType = "application/json";
                        break;
                    case "xml":
                        result.Data = GetDocumentXML(data);
                        result.ContentType = "application/xml";
                        break;
                }
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

        

        //Nazov suboru v dictionary...  GUID
        
        //private async Task<Document> GetDocumentJSON(string data)
        //{
        //    byte[] byteArray = Encoding.ASCII.GetBytes(data);
        //    MemoryStream stream = new MemoryStream(byteArray);
        //    Document? result = await System.Text.Json.JsonSerializer.DeserializeAsync<Document>(stream);
        //    return result;
        //}

        private string GetDocumentXML(string data)
        {
            XNode node = JsonConvert.DeserializeXNode(data, "Root");
            return node.ToString();
        }


        

    }
}
