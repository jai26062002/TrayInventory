using Microsoft.AspNetCore.Mvc;
using TrayInventoryApp.Models;
using TrayInventoryApp.Services;

namespace TrayInventoryApp.Controllers
{
    public class CostCalculationController : Controller
    {
        private readonly ICostCalculation _costCalculationService;

        public CostCalculationController(ICostCalculation costCalculationService)
        {
            _costCalculationService = costCalculationService;
        }

        // Action to display cost calculation details
        [HttpGet]
        public IActionResult DaybyDayCost()
        {
            var model = new CostCalculationViewModel
            {
                TodayDate = DateTime.Today,
                TotalCostToday = _costCalculationService.TotalAmountReceivedToday(),
                DailyCost = _costCalculationService.TotalAmountReceivedByDay()
                    .Select(kvp => new DayCost
                    {
                        Date = kvp.Key,
                        TotalCost = kvp.Value
                    })
                    .ToList() // Ensure this is a List<DayCost>
            };

            return View(model);
        }
    }
}
