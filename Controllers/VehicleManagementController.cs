using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SUPREA_LOGISTICS.Context;
using SUPREA_LOGISTICS.Models;
using System.Text;

namespace SUPREA_LOGISTICS.Controllers
{
    public class VehicleManagementController : Controller
    {
        private readonly MyDBContext _context;

        public VehicleManagementController(MyDBContext context)
        {
            _context = context;
        }

        //View all vehicles
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //Database
            var vehicles = _context.Vehicles.Where(x => x.IsAvailable).ToList();

            return View(vehicles);
        }


        //View for Vehicle Details
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if(id <= 0)
            {
                return NotFound();
            }
            var vehicle = _context.Vehicles
                .Include(x => x.VehiclePictures)
                .Include(x => x.VehicleDocuments)
                .Include(x => x.MaintenanceLogs)
                .FirstOrDefault(x => x.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }


            return View(vehicle);
        }

        [HttpGet]
        public IActionResult ViewPicture(int id)
        {
            var pic = _context.VehiclePictures.FirstOrDefault(x => x.PictureId == id);
            if (pic == null) return NotFound();

            return File(pic.FileData, pic.FileType);
        }
        [HttpPost]
        public async Task<IActionResult> UploadPicture(int vehicleId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var picture = new VehiclePicture
            {
                VehicleId = vehicleId,
                FileName = file.FileName,
                FileType = file.ContentType,
                FileData = memoryStream.ToArray(),
                UploadedDate = DateTime.Now,
            };

            _context.VehiclePictures.Add(picture);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = vehicleId });
        }
        //[HttpPost]
        //public async Task<IActionResult> DeletePicture(int pictureId)
        //{
        //    var picture = await _context.VehiclePictures.FindAsync(pictureId);
        //    if (picture == null)
        //        return NotFound();

        //    _context.VehiclePictures.Remove(picture);
        //    await _context.SaveChangesAsync();

        //    // Redirect back to vehicle details
        //    return RedirectToAction("Details", new { id = picture.VehicleId });
        //}

        [HttpGet]
        public IActionResult ViewDocument(int id)
        {
            var doc = _context.VehicleDocuments.FirstOrDefault(d => d.DocumentId == id);
            if (doc == null)
                return NotFound();

            return File(doc.FileData, doc.FileType);
        }


        [HttpGet]
        public IActionResult DownloadDocument(int id)
        {
            var doc = _context.VehicleDocuments.FirstOrDefault(x=> x.DocumentId == id && x.IsAvailable);
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
            };

            _context.VehicleDocuments.Add(doc);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = vehicleId });
        }
        public IActionResult Export()
        {
            var vehicles = _context.Vehicles.Where(x => x.IsAvailable).ToList(); // get data from DB
            var sb = new StringBuilder();

            // Header
            sb.AppendLine("VehicleID,Unit Type,Unit Model Series,Brandmake,Year Model, Engine No.,Chassis No., Plate No.,ORCR, Expiration Date, Insurance, Insurance Coverage, Insurance Provider, Date Aquired, Supplier, ProjectID, Site Location, Vehicle Status");

            // Rows
            foreach (var v in vehicles)
            {
                sb.AppendLine($"{v.VehicleId},{v.UnitType},{v.UnitModelSeries},{v.BrandMake},{v.YearModel},{v.EngineNo},{v.ChassisNo},{v.PlateNo},{v.Orcr},{v.ExpirationDate},{v.Insurance},{v.InsuranceCoverage},{v.InsuranceProvider},{v.DateAcquired},{v.Supplier},{v.ProjectId},{v.SiteLocation},{v.VehicleStatus}");
            }

            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "vehicles.csv");
        }


        //Edit Vehicle Data
        [HttpGet]
        public IActionResult EditVehicle(string id)
        {
            var vehicle = _context.Vehicles.FirstOrDefault(x => x.VehicleId.ToString() == id && x.IsAvailable);
            return View(vehicle);
        }
        //Edit Vehicle Data - Post
        [HttpPost]
        public async Task<IActionResult> EditVehicle(Vehicle vehicle)
        {
            if(!ModelState.IsValid)
            {
                return View(vehicle);
            }
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(
                "Details",
                "VehicleManagement",
                new { id = vehicle.VehicleId }
            );

        }

        //Create new vehicle data
        [HttpGet]
        public IActionResult CreateVehicle()
        {
            return View();
        }
        //Create new vehicle data - Post
        [HttpPost]
        public async Task<IActionResult> CreateVehicle(Vehicle vehicle)
        {
            if(!ModelState.IsValid)
            {
                return View(vehicle);
            }
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            //Placeholder for future database create logic
            return RedirectToAction("Index");
        }

        //Vehicle Logs
        //Tobe added database
        [HttpGet]
        public async Task<IActionResult> VehicleLog()
        {
            return View();
        }

        //Create Vehicle Log
        [HttpGet]
        public IActionResult CreateVehicleLog()
        {
            return View();
        }
        //Create Vehicle Log - Post
        [HttpPost]
        public async Task<IActionResult> CreateVehicleLog(VehicleLog vehicleLog)
        {
            //Placeholder for future database create logic
            return RedirectToAction("VehicleLog");
        }

        //Maintenace Logs
        //Tobe added database
        [HttpGet]
        public async Task<IActionResult> MaintenaceLog()
        {
            return View();
        }

        //Create Maintenance Log
        [HttpGet]
        public IActionResult CreateMaintenanceLog()
        {
            return View();
        }
        //Create Maintenance Log - Post
        [HttpPost]
        public async Task<IActionResult> CreateMaintenanceLog(MaintenanceLog maintenanceLog)
        {
            //Placeholder for future database create logic
            return RedirectToAction("MaintenaceLog");
        }
    }
}
