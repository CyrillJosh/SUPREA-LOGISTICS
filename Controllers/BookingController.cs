using Microsoft.AspNetCore.Mvc;
using SUPREA_LOGISTICS.Context;
using SUPREA_LOGISTICS.Models;
using SUPREA_LOGISTICS.ViewModels;
using System;

namespace SUPREA_LOGISTICS.Controllers
{
    public class BookingController : Controller
    {
        private readonly MyDBContext _context;

        public BookingController(MyDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new BookingViewModel()
            {
                DateNeeded = DateTime.Today
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            byte[]? fileData = null;
            string? fileName = null;
            string? contentType = null;

            if (vm.Document != null && vm.Document.Length > 0)
            {
                using var ms = new MemoryStream();
                await vm.Document.CopyToAsync(ms);

                fileData = ms.ToArray();
                fileName = vm.Document.FileName;
                contentType = vm.Document.ContentType;
            }

            var booking = new BookingRequest()
            {
                Region = vm.Region == "other" ? (vm.RegionOther ?? "Other") : vm.Region,
                NatureOfRequest = vm.NatureOfRequest,
                DateNeeded = vm.DateNeeded,
                RequestedBy = vm.RequestedBy,

                DocumentFileData = fileData,
                DocumentFileName = fileName,
                DocumentContentType = contentType,

                CreatedAt = DateTime.Now
            };

            _context.BookingRequests.Add(booking);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Booking submitted successfully!";
            return RedirectToAction("Create");
        }

        // Optional: list bookings
        [HttpGet]
        public IActionResult Index()
        {
            var bookings = _context.BookingRequests
                .OrderByDescending(x => x.CreatedAt)
                .ToList();

            return View(bookings);
        }
    }
}
