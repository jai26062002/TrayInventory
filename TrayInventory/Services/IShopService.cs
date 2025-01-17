using TrayInventoryApp.Models;

namespace TrayInventoryApp.Services
{
    public interface IShopService
    {
        void CreateShop(Shop shop);
        Shop GetShopById(int shopId);
        List<Shop> GetAllShops();
        void UpdateShop(Shop shop);
    }
}
