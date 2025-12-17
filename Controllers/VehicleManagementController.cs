using Microsoft.AspNetCore.Mvc;
using SUPREA_LOGISTICS.Models;
using System.Text.Json;
using IOFile = System.IO.File;

namespace SUPREA_LOGISTICS.Controllers
{
    public class VehicleManagementController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "Vehicles.json"
            );

            // 1️⃣ Declare json
            string json = IOFile.ReadAllText(path);
            var vehicles = JsonSerializer.Deserialize<List<Vehicle>>(json);

            return View(vehicles);
        }

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
            return View(vehicle);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
    }
}
