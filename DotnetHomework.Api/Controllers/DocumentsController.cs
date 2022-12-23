using DotnetHomework.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using MessagePack;
using DotnetHomework.Data;
using DotnetHomework.Utility;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotnetHomework.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {

        private readonly IDocumentsRepository _repo;
        public DocumentsController(IDocumentsRepository repo)
        {
            _repo = repo;
        }
        

        // GET <DocumentsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string id)
        {
            Document data = await _repo.GetDocument(id);
            if (data == null)
                return BadRequest("Error");
            return Ok(data);
        }

        [HttpGet]
        [Route("{id}/{fileType}/{storageType}")]
        public async Task<IActionResult> Get(string id, string fileType, string storageType)
        {
            Result result = await _repo.GetDocument(id, storageType, fileType);

            return new ContentResult
            {
                ContentType = result.ContentType,
                Content = (string)result.Data,
                StatusCode = result.StatusCode
            };
        }


        [HttpGet]
        [Route("msgPack/{id}")]
        public async Task<IActionResult> GetMsgPack(string id)
        {
            string data;
            using (StreamReader reader = new StreamReader(id))
            {
                data = await reader.ReadToEndAsync();
            }
            var bytes = MessagePack.MessagePackSerializer.Serialize(data);
            var result = MessagePackSerializer.Deserialize<string>(bytes);
            return new ContentResult
            {
                ContentType = "application/x-msgpack",
                Content = result,
                StatusCode = 200
            };

        }

        // POST <DocumentsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Document document, string type)
        {
            try
            {
                await _repo.Add(document, type); 

                return Ok("Document successsfuly uploaded.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT <DocumentsController>/5
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Put([FromBody] Document document)
        {
            try
            {
                await _repo.Add(document);

                return Ok();
                //return CreatedAtRoute(("Document successsfuly uploaded.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        
    }
}
