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
        [ProducesResponseType(StatusCodes.Status200OK)]
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] Document document, string type)
        {
            try
            {
                await _repo.Add(document, type);                
                return CreatedAtAction(nameof(Get), new { document.Id } ,document );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Put([FromBody] Document document)
        {
            try
            {
                await _repo.Add(document);
                return CreatedAtAction(nameof(Get), new { document.Id }, document);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }
    }
}
