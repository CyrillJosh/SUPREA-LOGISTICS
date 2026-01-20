using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SUPREA_LOGISTICS.Context;
using SUPREA_LOGISTICS.Models;

namespace SUPREA_LOGISTICS.Controllers
{
    public class MaintenanceLogController : Controller
    {
        private readonly MyDBContext _context;
        public MaintenanceLogController(MyDBContext context)
        {
            _context = context;
        }

        //Maintenace Logs
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var logs = _context.MaintenanceLogs
                .Include(x => x.Vehicle)
                .Where(x => x.Vehicle.IsAvailable)
                .ToList();
            return View(logs);
        }

        //Create Maintenance Log
        [HttpGet]
        public IActionResult CreateMaintenanceLog(int vehicleId = 0)
        {
            ViewBag.Vehicles = _context.Vehicles
                .Where(v => v.IsAvailable)
                .OrderBy(v => v.VehicleId)
                .ToList();

            return View(new MaintenanceLog
            {
                VehicleId = vehicleId,
                MaintenanceDate = DateOnly.FromDateTime(DateTime.Today)
            });
        }

        //Create Maintenance Log - Post
        [HttpPost]
        public async Task<IActionResult> CreateMaintenanceLog(MaintenanceLog log)
        {
            if (!ModelState.IsValid)
                return View(log);

            log.CreatedAt = DateTime.Now;

            _context.MaintenanceLogs.Add(log);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details","VehicleManagement", new { id = log.VehicleId });
        }


        //Edit Maintenance Log
        [HttpGet]
        public IActionResult EditMaintenanceLog(int id)
        {
            var log = _context.MaintenanceLogs.Include(x => x.Vehicle).FirstOrDefault(x => x.MaintenanceId == id);
            if (log == null)
                return NotFound();

            LoadVehicles();
            return View(log);
        }

        //Edit Maintenance Log - Post
        [HttpPost]
        public async Task<IActionResult> EditMaintenanceLog(MaintenanceLog model)
        {
            if (!ModelState.IsValid)
            {
                LoadVehicles();
                return View(model);
            }

            _context.MaintenanceLogs.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("MaintenaceLogs");
        }
        private void LoadVehicles()
        {
            ViewBag.Vehicles = _context.Vehicles
                .Where(v => v.IsAvailable)
                .Select(v => new
                {
                    v.VehicleId,
                    Display = $"{v.VehicleId:0000} - {v.UnitType} {v.UnitModelSeries} ({v.PlateNo})"
                })
                .ToList();
        }

    }
}
