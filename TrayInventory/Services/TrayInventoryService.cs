using TrayInventoryApp.Data;
using TrayInventoryApp.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace TrayInventoryApp.Services
{
    public class TrayInventoryService : ITrayInventoryService
    {
        private readonly AppDbContext _context;

        public TrayInventoryService(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public TrayInventorys GetLatestTrayInventory()
        {
            return _context.TrayInventory
                .FirstOrDefault(ti => ti.TrayInventoryID == 1);
        }
        public async Task ResetInventoryAsync()
        {
            // Find the existing inventory record (assuming only one record exists)
            var trayInventory = await _context.TrayInventory
                .FirstOrDefaultAsync(ti => ti.TrayInventoryID == 1); // Adjust ID as needed

            if (trayInventory != null)
            {
                // Reset the values
                trayInventory.TraysReceived = 0;
                trayInventory.TraysIssued = 0;

                // Save changes to the database
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddTrayInventoryAsync(int traysReceived, int adminId)
        {
            // Find the existing inventory record (only one entry should exist)
            var trayInventory = await _context.TrayInventory
                .FirstOrDefaultAsync(ti => ti.TrayInventoryID == 1);  // Assuming you want to update the first entry only (with Id = 1)

            if (trayInventory != null)
            {
                // Update the trays received for the existing record
                trayInventory.TraysReceived += traysReceived;
            }
            else
            {
                // If no record exists, create a new one
                trayInventory = new TrayInventorys
                {
                    TraysReceived = traysReceived,
                    TraysIssued = 0,  // Default value or calculated
                    AdminID = adminId
                };

                // Add the new inventory record
                _context.TrayInventory.Add(trayInventory);
            }

            // Save changes to the database
            await _context.SaveChangesAsync();
        }
        public async Task<TrayInventorys> UpdateInventoryAsync(TrayInventorys inventory)
        {
            // Assuming you're updating an existing tray inventory in the database
            var existingInventory = _context.TrayInventory
                .FirstOrDefault(ti => ti.TrayInventoryID == 1);

            if (existingInventory != null)
            {
                // Update values with the modified data
                existingInventory.TraysIssued = inventory.TraysIssued;
                existingInventory.TraysReceived = inventory.TraysReceived;
               // existingInventory.Date = inventory.Date; // Update date if necessary

                // Save changes to the database
                await _context.SaveChangesAsync();
            }

            return existingInventory;
        }

    }
}
