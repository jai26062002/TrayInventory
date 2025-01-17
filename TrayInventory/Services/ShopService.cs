using TrayInventoryApp.Data;
using TrayInventoryApp.Models;

namespace TrayInventoryApp.Services
{
    public class ShopService : IShopService
    {
        private readonly AppDbContext _context;

        public ShopService(AppDbContext context)
        {
            _context = context;
        }


        public Shop GetShopById(int shopId)
        {
            return _context.Shops.FirstOrDefault(s => s.ShopID == shopId);
        }

        public void CreateShop(Shop shop)
        {
             shop.PreviousPending = 0;

            _context.Shops.Add(shop);  // Add the new shop to the database
            _context.SaveChanges();    // Commit the changes
        }

        public List<Shop> GetAllShops()
        {
            return _context.Shops.ToList();
        }

        public void UpdateShop(Shop shop)
        {
            _context.Shops.Update(shop);
            _context.SaveChanges();
        }
    }
}
