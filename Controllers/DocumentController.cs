using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SUPREA_LOGISTICS.Context;
using SUPREA_LOGISTICS.Models;

namespace SUPREA_LOGISTICS.Controllers
{
    public class DocumentController : Controller
    {
        private readonly MyDBContext _context;
        public DocumentController(MyDBContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult ViewDocument(int id)
        {
            var doc = _context.VehicleDocuments.FirstOrDefault(d => d.DocumentId == id && d.IsAvailable);
            if (doc == null)
                return NotFound();

            return File(doc.FileData, doc.FileType);
        }


        [HttpGet]
        public IActionResult DownloadDocument(int id)
        {
            var doc = _context.VehicleDocuments.FirstOrDefault(x => x.DocumentId == id && x.IsAvailable);
            if (doc == null)
                return NotFound();

            return File(doc.FileData, doc.FileType, doc.FileName);
        }


        [HttpPost]
        public async Task<IActionResult> UploadDocument(
            int vehicleId,
            string documentType,
            IFormFile file,
            DateOnly? expirationDate)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            var doc = new VehicleDocument
            {
                VehicleId = vehicleId,
                DocumentType = documentType,
                FileName = file.FileName,
                FileType = file.ContentType,
                FileData = ms.ToArray(),
                ExpirationDate = expirationDate,
                UploadedDate = DateTime.Now,
                IsAvailable = true
            };

            _context.VehicleDocuments.Add(doc);
            await _context.SaveChangesAsync();

            return Redirect(Url.Action("Details", new { id = vehicleId }) + "#Documents");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var doc = await _context.VehicleDocuments.FindAsync(id);
            if (doc == null) return NotFound();

            doc.IsAvailable = false;
            _context.VehicleDocuments.Update(doc);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
