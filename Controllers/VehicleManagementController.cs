using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SUPREA_LOGISTICS.Context;
using SUPREA_LOGISTICS.Models;
using SUPREA_LOGISTICS.ViewModels;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SUPREA_LOGISTICS.Controllers
{
    public class VehicleManagementController : Controller
    {
        private readonly MyDBContext _context;

        public VehicleManagementController(MyDBContext context)
        {
            _context = context;
        }

        #region COMMON METHODS
        //View all vehicles
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //Database
            var vehicles = _context.Vehicles
                .Include(x => x.VehicleDocuments)
                .Include(x => x.DriverInCharge)
                .Include(x => x.VehicleLogs)
                .Include(x => x.VehicleStatuses)
                .Where(x => x.IsAvailable).ToList();

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
                .Include(x => x.DriverInCharge)
                .Include(x => x.VehicleDocuments)
                .Include(x => x.VehicleStatuses)
                .Include(x => x.VehicleLogs)
                .Include(x => x.MaintenanceLogs)
                .FirstOrDefault(x => x.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }


            return View(vehicle);
        }
        #endregion

        #region PICTURE METHODS
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
                IsAvailable = true
            };

            _context.VehiclePictures.Add(picture);
            await _context.SaveChangesAsync();

            return Redirect(Url.Action("Details", new { id = vehicleId }) + "#Pictures");
        }
        [HttpPost]
        public async Task<IActionResult> DeletePicture(int pictureId)
        {
            var picture = await _context.VehiclePictures.FindAsync(pictureId);
            if (picture == null)
                return NotFound();
            picture.IsAvailable = false;

            _context.VehiclePictures.Update(picture);
            await _context.SaveChangesAsync();

            return Redirect(Url.Action("Details", new { id = picture.VehicleId}) + "#Pictures");
        }
        #endregion

        #region VEHICLE MANIPULATION METHODS
        //Edit Vehicle Data
        [HttpGet]
        public IActionResult EditVehicle(int id)
        {
            var vehicle = _context.Vehicles
                .Include(v => v.DriverInCharge) // if you have a navigation property
                .FirstOrDefault(v => v.VehicleId == id);

            if (vehicle == null) return NotFound();

            ViewBag.Drivers = _context.Drivers
                .Select(d => new SelectListItem
                {
                    Value = d.DriverId.ToString(),
                    Text = $"{d.FullName} - {d.Position}"
                })
                .ToList();

            return View(vehicle);
        }

        //Edit Vehicle Data - Post
        [HttpPost]
        public async Task<IActionResult> EditVehicle(Vehicle vehicle)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Drivers = _context.Drivers
                .Select(d => new SelectListItem
                {
                    Value = d.DriverId.ToString(),
                    Text = $"{d.FullName} - {d.Position}"
                })
                .ToList();
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
            ViewBag.Drivers = _context.Drivers
                .Select(d => new SelectListItem
                {
                    Value = d.DriverId.ToString(),
                    Text = $"{d.FullName} - {d.Position}"
                })
                .ToList();
            return View();
        }
        //Create new vehicle data - Post
        [HttpPost]
        public async Task<IActionResult> CreateVehicle(VehicleFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Drivers = _context.Drivers
                .Select(d => new SelectListItem
                {
                    Value = d.DriverId.ToString(),
                    Text = $"{d.FullName} - {d.Position}"
                })
                .ToList();
                return View(vm);
            }

            // Save vehicle first
            _context.Vehicles.Add(vm.Vehicle);
            await _context.SaveChangesAsync();

            int vehicleId = vm.Vehicle.VehicleId;

            // Save pictures
            if (vm.VehiclePictures != null)
            {
                foreach (var file in vm.VehiclePictures)
                {
                    using var ms = new MemoryStream();
                    await file.CopyToAsync(ms);

                    _context.VehiclePictures.Add(new VehiclePicture
                    {
                        VehicleId = vehicleId,
                        FileName = file.FileName,
                        FileData = ms.ToArray(),
                        FileType = file.ContentType,
                        IsAvailable = true
                    });
                }
            }

            // Save documents
            if (vm.VehicleDocuments != null)
            {
                for (int i = 0; i < vm.VehicleDocuments.Count; i++)
                {
                    var file = vm.VehicleDocuments[i];
                    var type = vm.DocumentTypes != null && i < vm.DocumentTypes.Count ? vm.DocumentTypes[i] : "Other";
                    var expDate = vm.DocumentExpirationDates != null && i < vm.DocumentExpirationDates.Count ? vm.DocumentExpirationDates[i] : null;

                    using var ms = new MemoryStream();
                    await file.CopyToAsync(ms);

                    _context.VehicleDocuments.Add(new VehicleDocument
                    {
                        VehicleId = vehicleId,
                        FileName = file.FileName,
                        FileType = file.ContentType,
                        FileData = ms.ToArray(),
                        DocumentType = type,
                        ExpirationDate = expDate.HasValue
                        ? DateOnly.FromDateTime(expDate.Value)
                        : null,
                        IsAvailable = true
                    });
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = vehicleId });
        }

        #endregion  

        #region EXPORT METHOD
        public IActionResult Export()
        {
            var vehicles = _context.Vehicles.Include(x => x.VehicleStatuses).Where(x => x.IsAvailable).ToList(); // get data from DB
            var sb = new StringBuilder();

            // Header
            sb.AppendLine("VehicleID,Unit Type,Unit Model Series,Brandmake,Year Model, Engine No.,Chassis No., Plate No.,ORCR, Expiration Date, Insurance, Insurance Coverage, Insurance Provider, Date Aquired, Supplier, ProjectID, Site Location, Vehicle Status");

            // Rows
            foreach (var v in vehicles)
            {
                sb.AppendLine($"{v.VehicleId},{v.UnitType},{v.UnitModelSeries},{v.BrandMake},{v.YearModel},{v.EngineNo},{v.ChassisNo},{v.PlateNo},{v.Orcr},{v.ExpirationDate},{v.Insurance},{v.InsuranceCoverage},{v.InsuranceProvider},{v.DateAcquired},{v.Supplier},{v.ProjectId},{v.SiteLocation},{v.VehicleStatuses.Last().Status}");
            }

            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "vehicles.csv");
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> VehicleStatus(int id)
        {
            var vehicles = _context.Vehicles
                .Include(x => x.VehicleStatuses)
                .Where(x => x.IsAvailable).ToList();

            return View(vehicles);
        }

        [HttpPost]
        public async Task<IActionResult> UpsertStatus(int vehicleId, DateTime date, string status)
        {
            var existing = await _context.VehicleStatuses
                .FirstOrDefaultAsync(x => x.VehicleId == vehicleId && x.StatusDate.Date == date);

            if (existing == null)
            {
                _context.VehicleStatuses.Add(new VehicleStatus
                {
                    VehicleId = vehicleId,
                    StatusDate = date,
                    Status = status
                });
            }
            else
            {
                existing.Status = status;
                _context.VehicleStatuses.Update(existing);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
