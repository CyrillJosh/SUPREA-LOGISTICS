using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SUPREA_LOGISTICS.Context;
using SUPREA_LOGISTICS.Models;

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
        public async Task<IActionResult> VehicleLog()
        {
            return View();
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
    }
}
