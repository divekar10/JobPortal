using Aspose.Words;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Api.Controllers.Helper
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelperController : BaseController
    {

        [HttpPost("ConvertToPdf")]
        public IActionResult ConvertToPdf(IFormFile file)
        {
            var bytes = GetBytesFromFormFile(file);
            MemoryStream ms = new MemoryStream(bytes);
            Document doc = new Document(ms);
            using (var pdfStream = new MemoryStream())
            {
                doc.Save(pdfStream, SaveFormat.Pdf);
                return File(pdfStream.ToArray(), "application/pdf", "converted.pdf");
            }
        }

        private byte[] GetBytesFromFormFile(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }

    }
}
