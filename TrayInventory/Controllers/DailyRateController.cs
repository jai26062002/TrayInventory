using Microsoft.AspNetCore.Mvc;
using TrayInventoryApp.Services;
using TrayInventoryApp.Models;
namespace TrayInventoryApp.Controllers
{
    public class DailyRateController : Controller
    {
        private readonly IDailyRateService _dailyRateService;

        public DailyRateController(IDailyRateService dailyRateService)
        {
            _dailyRateService = dailyRateService;
        }

        public IActionResult Index()
        {
            var rates = _dailyRateService.GetAllRates();
            return View(rates);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DailyRate dailyRate)
        {
            if (ModelState.IsValid)
            {
                dailyRate.Date = DateTime.Now.Date; // Automatically set today's date
                _dailyRateService.AddOrUpdateDailyRate(dailyRate);
                return RedirectToAction(nameof(Index));
            }
            return View(dailyRate);
        }

        public IActionResult Edit(int id)
        {
            var rate = _dailyRateService.GetAllRates().FirstOrDefault(r => r.RateID == id);
            if (rate == null)
            {
                return NotFound();
            }
            return View(rate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DailyRate dailyRate)
        {
            if (ModelState.IsValid)
            {
                _dailyRateService.UpdateDailyRate(dailyRate);
                return RedirectToAction(nameof(Index));
            }
            return View(dailyRate);
        }


    }
}
