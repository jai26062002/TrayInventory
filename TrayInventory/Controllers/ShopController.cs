using Microsoft.AspNetCore.Mvc;
using TrayInventoryApp.Services;
using TrayInventoryApp.Models;
namespace TrayInventoryApp.Controllers
{
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;
       private readonly IDailyRateService _dailyRateService;
        private readonly ITransactionService _transactionService;
        private readonly ITrayInventoryService _trayInventoryService;

        public ShopController(IShopService shopService,IDailyRateService dailyRateService,
            ITransactionService transactionService, ITrayInventoryService trayInventoryService)
        {
            _shopService = shopService;
           _dailyRateService = dailyRateService;
            _transactionService = transactionService;
            _trayInventoryService = trayInventoryService;
        }

        // Display the shop list with pending amounts
        public IActionResult Index()
        {
            var shops = _shopService.GetAllShops();
            return View(shops);
        }
        public IActionResult Create()
        {
            return View(); // This will return the Create view for the new shop form
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] Shop shop)
        {
            if (ModelState.IsValid)
            {
                _shopService.CreateShop(shop);
                return RedirectToAction("Index", "Shop");
            }
            return View(shop);
        }

        [HttpGet]
        public IActionResult AddEntry(int shopId)
        {
            var shop = _shopService.GetShopById(shopId);
            if (shop == null)
            {
                return NotFound(); // Return error if shop is not found
            }
            return View(shop); // Pass the shop object to the view
        }
        // Display the shop details page
        public IActionResult Details(int id)
        {
            var shop = _shopService.GetShopById(id);
            var transactions = _transactionService.GetTransactionsByShopId(id);
            return View(new ShopDetailsViewModel
            {
                Shop = shop,
                Transaction = transactions
            });
        }

        // Add or update daily entry (Tray Issued, Amount Received, Pending Amount)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTodayEntry(int shopId, int trayCount, decimal amountReceived)
        {
            try
            {
                // Step 1: Get the price per egg for the day
                var pricePerEgg = _dailyRateService.GetPriceForToday();  // Fetch the price for today from the DailyPrice table
               // decimal pricePerEgg = 6;
                // Step 2: Calculate the total cost of the trays issued
                decimal totalCost = trayCount * 30 * pricePerEgg;

                // Step 3: Retrieve the current pending amount from the shop's previous pending balance
                var shop = _shopService.GetShopById(shopId); // Get the shop details

                // Calculate the new pending amount (old pending + current cost - amount received)
                decimal newPendingAmount = shop.PreviousPending + totalCost - amountReceived;

                // Step 4: Create a new daily transaction record
                Transaction transaction = new Transaction
                {
                    ShopID = shopId,
                    Date = DateTime.Now.Date,
                    TrayCount = trayCount,
                    Cost = totalCost,
                    AmountReceived = amountReceived,
                    PendingAmount = newPendingAmount
                };

                // Save the transaction record
                _transactionService.AddTransaction(transaction);

                // Step 5: Update the shop's pending amount and tray inventory
                shop.PreviousPending = newPendingAmount;  // Update the shop's pending amount
                _shopService.UpdateShop(shop);  // Save the updated shop

                var trayInventory = _trayInventoryService.GetLatestTrayInventory();
                trayInventory.TraysIssued += trayCount;
                trayInventory.TraysReceived-=trayCount;
                // Add the trays issued for the day
                 _trayInventoryService.UpdateInventoryAsync(trayInventory);  // Save the updated tray inventory

                // Redirect back to the shop's details page
                return RedirectToAction("Index", "Shop");
            }
            catch (Exception ex)
            {
                // Handle errors, display a message to the admin
                ModelState.AddModelError("", "Error processing the transaction: " + ex.Message);
                return RedirectToAction("Details", new { id = shopId });
            }
        }
    }

}
