using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Xml;
using System.Xml.Serialization;
using MessagePack;
using System.Text.Json.Serialization;
using Document_storage_Api;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;
using System.Text;
using System.Xml.Linq;
using Document_storage_Api.Classes;

namespace MyDocumentStorageApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IMyDocumentStorage _oStorage;

        public DocumentsController(IMyDocumentStorage oStorage)
        {
            _oStorage = oStorage;
        }

        #region POST
        [HttpPost]
        public IActionResult CreateDocument([FromBody] MyDocument oDocument)
        {
            if (oDocument == null || string.IsNullOrEmpty(oDocument.id))          
                return BadRequest(Constants.ErrorInvalidData);

            _oStorage.Create(oDocument);

            return Ok(oDocument);
        }
        #endregion

        #region GET
        [HttpGet("{id}")]
        public IActionResult GetDocument(string id, [FromHeader(Name = Constants.HeaderAccept)] string sAcceptHeader)
        {
            MyDocument oDocument = _oStorage.GetById(id);

            if (oDocument == null)      
                return NotFound();        

            byte[] byFormatedDokument = FormatHelper.GetObjectInRightFormat(oDocument, sAcceptHeader);

            if (byFormatedDokument == null)
                return BadRequest(Constants.ErrorWrongFormat);

            return File(byFormatedDokument, sAcceptHeader);
        }
        #endregion

        #region PUT
        [HttpPut("{id}")]
        public IActionResult UpdateMyDocument(string id, [FromBody] MyDocument updatedMyDocument)
        {
            if (updatedMyDocument == null || string.IsNullOrEmpty(updatedMyDocument.id))
                return BadRequest(Constants.ErrorInvalidData);

            MyDocument existingMyDocument = _oStorage.GetById(id);
            if (existingMyDocument == null)
                return NotFound();

            _oStorage.Update(updatedMyDocument);

            return Ok(updatedMyDocument);
        }
        
        #endregion
    }
}