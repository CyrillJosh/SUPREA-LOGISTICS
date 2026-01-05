using Microsoft.AspNetCore.Mvc;
using SUPREA_LOGISTICS.Models;
using SUPREA_LOGISTICS.ViewModels;
using System.Text.Json;
using IOFile = System.IO.File;

namespace SUPREA_LOGISTICS.Controllers
{
    public class VehicleManagementController : Controller
    {
        //Vehicle List using JSON file
        //Subject to change in the future
        //Change to Database in the future
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "Vehicles.json"
            );

            string json = IOFile.ReadAllText(path);
            var vehicles = JsonSerializer.Deserialize<List<Vehicle>>(json);

            //Database
            //var vehicles = _context.Vehicles.ToList();

            return View(vehicles);
        }


        //View for Vehicle Details
        //Subject to change in the future
        //Change to Database in the future
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            string path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "Vehicles.json"
            );
            string json = IOFile.ReadAllText(path);
            var vehicles = JsonSerializer.Deserialize<List<Vehicle>>(json);
            var vehicle = vehicles.FirstOrDefault(v => v.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            //Database
            //var vehicles = _context.Vehicles.FirstOrDefault(x => x.VehicleId = id);

            VehicleDetailsViewModel viewModel = new VehicleDetailsViewModel
            {
                Vehicle = vehicle,
                MaintenanceLogs = new List<MaintenanceLog>(), // Placeholder for future data
                VehicleLogs = new List<VehicleLog>(),         // Placeholder for future data
                Documents = new List<Document>(),              // Placeholder for future data
                Pictures = new List<Picture>()                 // Placeholder for future data
            };
            return View(viewModel);
        }

        //Edit Vehicle Data
        //Placeholder for future database implementation
        [HttpGet]
        public IActionResult EditVehicle(string id)
        {
            return View();
        }
        //Edit Vehicle Data - Post
        [HttpPost]
        public async Task<IActionResult> EditVehicle(Vehicle vehicle)
        {
            //Placeholder for future database update logic
            return RedirectToAction("Details");
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
