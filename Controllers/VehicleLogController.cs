using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SUPREA_LOGISTICS.Context;
using SUPREA_LOGISTICS.Models;
using System.Text;

namespace SUPREA_LOGISTICS.Controllers
{
    public class VehicleLogController : Controller
    {
        private readonly MyDBContext _context;
        public VehicleLogController(MyDBContext context)
        {
            _context = context;
        }
        //Vehicle Logs
        //Tobe added database
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var logs = _context.VehicleLogs.Include(x => x.Vehicle).ToList();
            return View(logs);
        }

        //Create Vehicle Log
        [HttpGet]
        public IActionResult CreateVehicleLog(int vehicleId = 0)
        {
            ViewBag.Vehicles = _context.Vehicles.Where(v => v.IsAvailable).ToList();

            return View(new VehicleLog
            {
                VehicleId = vehicleId,
            });
        }
        //Create Vehicle Log - Post
        [HttpPost]
        public async Task<IActionResult> CreateVehicleLog(VehicleLog vehicleLog)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Vehicles = _context.Vehicles.Where(v => v.IsAvailable).ToList();
                return View(vehicleLog);
            }

            vehicleLog.CreatedAt = DateTime.Now;
            _context.VehicleLogs.Add(vehicleLog);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "VehicleManagement", new { id = vehicleLog.VehicleId });
        }
        public IActionResult Export()
        {
            var logs = _context.VehicleLogs.Include(x => x.Vehicle).ToList(); // get data from DB
            var sb = new StringBuilder();

            // Header
            sb.AppendLine("Vehicle PLate No.,Date Created, Description, Remarks");

            // Rows
            foreach (var v in logs)
            {
                sb.AppendLine($"{v.Vehicle.PlateNo},{v.CreatedAt},{v.Description},{v.Remarks}");
            }

            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "VehicleLogs.csv");
        }
    }
}
