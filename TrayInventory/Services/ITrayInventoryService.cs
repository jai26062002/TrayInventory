using TrayInventoryApp.Models;

namespace TrayInventoryApp.Services
{
    public interface ITrayInventoryService
    {
        TrayInventorys GetLatestTrayInventory();
        Task AddTrayInventoryAsync( int traysReceived, int adminId);

        Task<TrayInventorys> UpdateInventoryAsync(TrayInventorys inventory);
        Task ResetInventoryAsync();

    }
}
