using Microsoft.AspNetCore.Mvc;
using TrayInventoryApp.Models;
using TrayInventoryApp.Services;


namespace TrayInventoryApp.Controllers
{
    public class TrayInventoryController : Controller
    {
        private readonly ITrayInventoryService _trayInventoryService;

        public TrayInventoryController(ITrayInventoryService trayInventoryService)
        {
            _trayInventoryService = trayInventoryService;
        }

        public async Task<IActionResult> TrayInventory()
        {
            var trayInventory = _trayInventoryService.GetLatestTrayInventory();
            if (trayInventory == null) // Handle the case where no data exists
            {
                trayInventory = new TrayInventorys
                {
                   
                    TraysReceived = 0,
                    TraysIssued = 0,
                };
            }
            return View(trayInventory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetTrayInventory()
        {
            await _trayInventoryService.ResetInventoryAsync();
            return RedirectToAction("TrayInventory");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>AddTrayInventory(int traysReceived)
        {
            await _trayInventoryService.AddTrayInventoryAsync(traysReceived, 1); // Use AdminID from session
            return RedirectToAction("TrayInventory");
        }
    }
}
